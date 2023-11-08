using Org.BouncyCastle.Utilities;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemBank.Controllers;

namespace UdemBank.Services
{
    internal class CreateLoanService
    {
        // Método para verificar si el usuario está afiliado a un grupo de ahorro
        public static bool UserIsAffiliated(User user, SavingGroup savingGroup)
        {
            // se debe obtener el Saving asociado al usuario y al savingGroups
            Saving? saving = SavingController.GetSavingByUserAndSavingGroup(user, savingGroup);

            return (saving.Affiliation);
        }

        // Método para crear un préstamo para un usuario en un grupo de ahorro específico
        public static void CreateLoan(User user, SavingGroup savingGroup, double interestRate)
        {
            // Solicitar la cantidad y la fecha de vencimiento del préstamo
            var amount = AnsiConsole.Ask<int>("Ingrese la cantidad que desea prestar : ");
            var date = AnsiConsole.Ask<DateTime>("Ingrese el plazo máximo de pago deseado (YYYY-MM-DD): ");

            DateOnly dateOnly = DateOnly.FromDateTime(date);

            // Este sería igual a date pero es de tipo DateTime y se establece la hora como 00:00:00 ...
            DateTime dateTime = new DateTime(dateOnly.Year, dateOnly.Month, dateOnly.Day, 0, 0, 0);

            // se debe obtener el Saving asociado al usuario y al savingGroups
            Saving? saving = SavingController.GetSavingByUserAndSavingGroup(user, savingGroup);

            // Verificar que el plazo de pago sea de al menos dos meses
            double months = CalculateMonths(dateOnly);

            if (months < 2)
            {
                Console.WriteLine("El plazo de pago debe ser de al menos dos meses.");
                Console.ReadLine();
                return;
            }

            // Tomar en cuenta la fidelización y ajustar la tasa de interés en consecuencia
            if (user.Rewarded)
            {
                interestRate = interestRate - 0.01;
            }

            // Calcular el interés basado en la cantidad y la tasa de interés
            double interest = CalculateInterest(amount, dateOnly, interestRate);

            // Verificar si el usuario tiene suficiente capital para el préstamo
            if (saving.Investment >= amount)
            {
                // Descontar la cantidad del grupo de ahorro
                SavingGroupController.DeduceAmountToSavingGroup(savingGroup, amount);

                // Agregar el préstamo a la base de datos
                LoanController.AddLoan(saving, amount + interest, dateOnly);

                // Descontar la inversión del ahorro del usuario
                SavingController.DeduceInvestmentToSaving(savingGroup.Id, amount);

                // Añadir la cantidad prestada a la cuenta del usuario
                UserController.AddAmount(user, amount);

                Console.WriteLine("Se hizo el prestamo correctamente :) ...");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("El usuario no posee suficiente capital...");
                Console.ReadLine();
            }
        }

        // Método para crear un préstamo para un usuario que no está en un grupo de ahorro
        public static void CreateLoanNotInSavingGroup(User user, SavingGroup savingGroup)
        {
            return;
        }

        // Método para calcular el interés del préstamo
        public static double CalculateInterest(int amount, DateOnly date, double interestRate)
        {
            double months = CalculateMonths(date);
            double interest = amount * interestRate * months;

            return interest;
        }

        // Método para calcular la duración del préstamo en meses
        public static double CalculateMonths(DateOnly date)
        {
            double months = (date.Year - DateTime.Now.Year) * 12 + date.Month - DateTime.Now.Month;
            return months;
        }

        // Método para mostrar los grupos de ahorro disponibles para un usuario
        public static SavingGroup? ShowAvailableSavingGroups(User user)
        {
            var eligibleSavingGroups = SavingGroupController.GetEligibleSavingGroupsForUser(user);

            // Crear una lista de cadenas con la combinación de nombre de grupo de ahorro y cantidad del préstamo
            var groupsOptions = eligibleSavingGroups.Select(group => $"{group.Name}").ToList();

            var option = AnsiConsole.Prompt(new SelectionPrompt<string>()
                .Title("Selecciona un grupo de ahorro: ")
                .AddChoices(groupsOptions));

            // Extraer el Id del préstamo seleccionado a partir del texto seleccionado
            var selectedGroup = eligibleSavingGroups.FirstOrDefault(group => $"{group.Name}" == option);

            if (selectedGroup != null)
            {
                return selectedGroup;
            }
            else
            {
                Console.WriteLine("Selección no válida.");
                Console.ReadLine();
                return null;
            }
        }
    }
}
