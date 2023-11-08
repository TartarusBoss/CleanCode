using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdemBank.Controllers
{
    internal class SavingController
    {
        // Método para agregar un ahorro para un usuario en un grupo de ahorro en la base de datos.
        public static void AddSaving(User user, SavingGroup SavingGroup, bool affiliation)
        {
            using var db = new UdemBankContext(); // Establece una conexión con la base de datos.

            // Verificar si el usuario y el grupo de ahorro existen en la base de datos.
            var existingUser = db.Users.FirstOrDefault(u => u.Id == user.Id);
            var existingSavingGroup = db.SavingGroups.FirstOrDefault(sg => sg.Id == SavingGroup.Id);

            if (existingUser != null && existingSavingGroup != null)
            {
                var newSaving = new Saving
                {
                    UserId = existingUser.Id,
                    SavingGroupId = existingSavingGroup.Id,
                    Affiliation = affiliation,
                    Investment = 0
                };

                db.Savings.Add(newSaving); // Agrega el nuevo ahorro a la base de datos.
                db.SaveChanges(); // Guarda los cambios en la base de datos.
                Console.WriteLine();
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Grave..."); // Muestra un mensaje de error.
                Console.ReadLine();
                return;
            }
        }

        // Método para obtener los ahorros por el nombre del grupo de ahorro y la afiliación.
        public static List<Saving> GetSavingsByUserSavingGroupName(string savingGroupName)
        {
            using var db = new UdemBankContext(); // Establece una conexión con la base de datos.

            // Buscar el SavingGroup por nombre.
            var savingGroup = db.SavingGroups.FirstOrDefault(g => g.Name == savingGroupName);

            if (savingGroup == null)
            {
                Console.WriteLine("Grave..."); // Muestra un mensaje de error.
                return new List<Saving>();
            }

            // Obtener los Savings con el SavingGroupId encontrado y Affiliation en true, incluyendo la entidad User.
            var savingsInGroup = db.Savings
                .Include(s => s.User) // Incluir la entidad User.
                .Where(s => s.SavingGroupId == savingGroup.Id && s.Affiliation)
                .ToList();

            return savingsInGroup; // Devuelve la lista de ahorros en el grupo.
        }

        // Método para obtener un ahorro por el usuario y el grupo de ahorro.
        public static Saving? GetSavingByUserAndSavingGroup(User user, SavingGroup savingGroup)
        {
            using var db = new UdemBankContext(); // Establece una conexión con la base de datos.

            var saving = db.Savings.FirstOrDefault(s =>
                s.UserId == user.Id && s.SavingGroupId == savingGroup.Id && s.Affiliation == true
            );
            db.SaveChanges();
            return saving; // Devuelve el ahorro.
        }

        // Método para agregar una inversión al ahorro en la base de datos.
        public static void AddInvestmentToSaving(int savingId, double amountToAdd)
        {
            using var db = new UdemBankContext(); // Establece una conexión con la base de datos.

            var saving = db.Savings.FirstOrDefault(s => s.Id == savingId);

            if (saving != null)
            {
                saving.Investment += amountToAdd; // Agrega la cantidad de inversión al ahorro.
                db.SaveChanges(); // Guarda los cambios en la base de datos.
            }
        }

        // Método para deducir una inversión del ahorro en la base de datos.
        public static void DeduceInvestmentToSaving(int savingId, int amountToAdd)
        {
            using var db = new UdemBankContext(); // Establece una conexión con la base de datos.

            var saving = db.Savings.FirstOrDefault(s => s.Id == savingId);

            if (saving != null)
            {
                saving.Investment -= amountToAdd; // Deduce la cantidad de inversión del ahorro.
                db.SaveChanges(); // Guarda los cambios en la base de datos.
            }
        }

        // Método para obtener los ahorros por el grupo de ahorro y la afiliación.
        public static List<Saving> GetSavingsBySavingGroupAndAffiliation(SavingGroup savingGroup)
        {
            using (var db = new UdemBankContext()) // Establece una conexión con la base de datos.
            {
                return db.Savings
                    .Where(s => s.SavingGroupId == savingGroup.Id && s.Affiliation)
                    .ToList();
            }
        }

        // Método para obtener los ahorros por el grupo de ahorro.
        public static List<Saving> GetSavingsBySavingGroup(SavingGroup savingGroup)
        {
            using (var db = new UdemBankContext()) // Establece una conexión con la base de datos.
            {
                return db.Savings
                    .Where(s => s.SavingGroupId == savingGroup.Id)
                    .Include(s => s.User)          // Incluye el usuario relacionado.
                    .Include(s => s.SavingGroup)  // Incluye el grupo de ahorro relacionado.
                    .ToList();
            }
        }

        // Método para obtener los usuarios por el grupo de ahorro.
        public static List<User> GetUsersBySavings(SavingGroup savingGroup)
        {
            List<Saving> savings = GetSavingsBySavingGroupAndAffiliation(savingGroup);

            using (var db = new UdemBankContext()) // Establece una conexión con la base de datos.
            {
                // Obtiene los IDs de los Savings en la lista.
                var savingIds = savings.Select(s => s.UserId).ToList();

                // Obtiene los Users cuyo ID está en la lista de Savings.
                var users = db.Users
                    .Where(u => savingIds.Contains(u.Id))
                    .ToList();

                return users; // Devuelve la lista de usuarios.
            }
        }
    }
}
