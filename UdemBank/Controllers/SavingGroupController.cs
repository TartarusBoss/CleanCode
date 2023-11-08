﻿using Microsoft.EntityFrameworkCore;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemBank.Controllers;

namespace UdemBank
{
    internal class SavingGroupController
    {
        // Método para agregar un grupo de ahorro en la base de datos.
        public static SavingGroup? AddSavingGroup(User user)
        {
            using (var db = new UdemBankContext()) // Establece una conexión con la base de datos.
            {
                var name = AnsiConsole.Ask<string>("Ingrese el nombre que tendrá el grupo: "); // Solicita al usuario ingresar el nombre del grupo.

                // Verifica si el usuario ya está rastreado en el contexto.
                var existingUser = db.Users.FirstOrDefault(u => u.Id == user.Id);

                if (existingUser == null)
                {
                    Console.WriteLine("how the fuck"); // Muestra un mensaje de error.
                    return null;
                }
                // Crea un nuevo grupo de ahorro con TotalAmount inicial en 0.
                var newSavingGroup = new SavingGroup
                {
                    UserId = existingUser.Id,
                    Name = name,
                    TotalAmount = 0
                };

                // Agrega el nuevo grupo de ahorro a la base de datos.
                db.SavingGroups.Add(newSavingGroup);
                db.SaveChanges();

                Console.WriteLine("El grupo de ahorro se ha creado con éxito."); // Muestra un mensaje de éxito.
                Console.ReadLine();
                Console.Clear();

                return newSavingGroup; // Devuelve el nuevo grupo de ahorro.
            }
        }

        // Método para agregar una cantidad al grupo de ahorro en la base de datos.
        public static void AddAmountToSavingGroup(SavingGroup? savingGroup, double Amount)
        {
            using var db = new UdemBankContext(); // Establece una conexión con la base de datos.

            if (savingGroup == null)
            {
                Console.WriteLine("Grave..."); // Muestra un mensaje de error.
            }
            if (!(savingGroup == null))
            {
                savingGroup.TotalAmount += Amount; // Agrega la cantidad al TotalAmount del grupo.
                db.SavingGroups.Update(savingGroup);
            }
            db.SaveChanges(); // Guarda los cambios en la base de datos.
        }

        // Método para deducir una cantidad del grupo de ahorro en la base de datos.
        public static void DeduceAmountToSavingGroup(SavingGroup savingGroup, double Amount)
        {
            using var db = new UdemBankContext(); // Establece una conexión con la base de datos.

            if (savingGroup == null)
            {
                Console.WriteLine("Grave..."); // Muestra un mensaje de error.
            }

            savingGroup.TotalAmount -= Amount; // Deduce la cantidad del TotalAmount del grupo.

            db.SavingGroups.Update(savingGroup);
            db.SaveChanges(); // Guarda los cambios en la base de datos.
        }

        // Método para obtener el grupo de ahorro con el TotalAmount más alto.
        public static SavingGroup? GetSavingGroupWithBiggestIncome()
        {
            using (var db = new UdemBankContext()) // Establece una conexión con la base de datos.
            {
                // Obtiene el SavingGroup con el TotalAmount más alto.
                var savingGroupWithBiggestIncome = db.SavingGroups
                    .OrderByDescending(sg => sg.TotalAmount)
                    .FirstOrDefault();

                return savingGroupWithBiggestIncome; // Devuelve el grupo de ahorro con el TotalAmount más alto.
            }
        }

        // Método para obtener los grupos de ahorro por el usuario.
        public static List<SavingGroup>? GetSavingGroupsByUser(User user)
        {
            using var db = new UdemBankContext(); // Establece una conexión con la base de datos.

            // Verifica si el usuario ya está rastreado en el contexto.
            User? existingUser = db.Users.FirstOrDefault(u => u.Id == user.Id);

            if (existingUser == null)
            {
                Console.WriteLine("El usuario no existe en el contexto."); // Muestra un mensaje de error.
                return null;
            }

            var userId = existingUser.Id; // Copia el valor en una variable local

            Console.WriteLine(userId); // Muestra el userId.
            Console.ReadLine();

            var savings = db.Savings
                .Include(s => s.SavingGroup) // Carga las entidades SavingGroup relacionadas.
                .Where(s => s.UserId == userId)
                .ToList();

            if (savings == null)
            {
                Console.WriteLine("El conjunto de ahorros del usuario es nulo."); // Muestra un mensaje de error.
                Console.ReadLine();
                return null;
            }

            if (!savings.Any())
            {
                Console.WriteLine("El conjunto de ahorros del usuario está vacío."); // Muestra un mensaje de error.
                Console.ReadLine();
                return null;
            }

            foreach (var saving in savings)
            {
                Console.WriteLine($"El saving tiene userId :{saving.UserId}"); // Muestra el userId del saving.
            }

            var savingGroups = savings.Select(s => s.SavingGroup).ToList();

            foreach (var savinggroup in savingGroups)
            {
                Console.WriteLine($"El savingGroup tiene userId :{savinggroup.UserId}"); // Muestra el userId del savingGroup.
            }

            return savingGroups; // Devuelve la lista de grupos de ahorro.
        }

        // Método que devuelve todos los usuarios que pertenecen a grupos en los que está un usuario.
        public static List<User> GetUsersInEligibleSavingGroups(User user)
        {
            using (var db = new UdemBankContext()) // Establece una conexión con la base de datos.
            {
                // Paso 1: Obtener todos los Savings asociados al usuario con inclusión de objetos relacionados.
                var userSavings = db.Savings
                    .Where(s => s.UserId == user.Id)
                    .Include(s => s.SavingGroup) // Incluir el objeto SavingGroup relacionado.
                    .ToList();

                // Paso 2: Obtener todos los grupos de ahorro a los que pertenece el usuario.
                var userSavingGroupIds = userSavings.Select(s => s.SavingGroup.Id).ToList();
                var userSavingGroups = db.SavingGroups
                    .Where(g => userSavingGroupIds.Contains(g.Id))
                    .ToList();

                // Paso 3: Obtener todos los Savings asociados a los grupos de ahorro en los que se encuentra el usuario.
                var savingsInUserGroups = db.Savings
                    .Where(s => userSavingGroupIds.Contains(s.SavingGroupId))
                    .Include(s => s.User) // Incluir el objeto User relacionado.
                    .ToList();

                // Paso 4: Obtener todos los usuarios que se encuentran en grupos de ahorro a los que pertenece el usuario.
                var userIdsInUserGroups = savingsInUserGroups.Select(s => s.UserId).ToList();
                var usersInUserGroups = db.Users
                    .Where(u => userIdsInUserGroups.Contains(u.Id))
                    .ToList();

                return usersInUserGroups; // Devuelve la lista de usuarios.
            }
        }
    }
}
