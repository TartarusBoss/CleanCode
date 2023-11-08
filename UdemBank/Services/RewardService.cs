using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemBank.Controllers;

namespace UdemBank.Services
{
    internal class RewardService
    {
        // Método para premiar al grupo de ahorro con el mayor ingreso
        public static void RewardSavingGroup()
        {
            // Obtener el grupo de ahorro con el mayor ingreso
            SavingGroup? savingGroup = SavingGroupController.GetSavingGroupWithBiggestIncome();

            // Inyectar el 10% del monto total al grupo de ahorro
            double percent = savingGroup.TotalAmount * 0.10;
            SavingGroupController.AddAmountToSavingGroup(savingGroup, percent);
            Console.WriteLine("Se premió al grupo de ahorro : " + savingGroup.Name + " con una transacción de valor : " + percent.ToString());
            Console.ReadLine();
            Console.Clear();
        }

        // Método para premiar a un usuario específico
        public static void RewardUser()
        {
            // Obtener el nombre de usuario
            var userName = AnsiConsole.Ask<string>("Ingrese el nombre del usuario : ");

            // Obtener el usuario por su nombre
            User user = UserController.GetUserByName(userName);

            // Verificar si el usuario existe
            if (user == null)
            {
                Console.WriteLine("No hay un usuario con ese nombre ... TwT");
                Console.ReadLine();
                AnsiConsole.Clear();
                return;
            }

            // Premiar al usuario con un 1% menos de comisión al hacer préstamos
            user = UserController.RewardUser(user);
            Console.WriteLine("Se premió al usuario de nombre : " + user.Name + " con un 1% menos de comisión a la hora de hacer préstamos.");
            Console.ReadLine();
            Console.Clear();
        }

    }
}
