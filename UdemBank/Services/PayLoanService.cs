using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemBank.Controllers;

namespace UdemBank
{
    internal class PayLoanService
    {
        // Método para mostrar la información de la deuda y pagar el préstamo
        public static User? ShowInfoAndPayLoan(User user, Loan loan)
        {
            // Mostrar la deuda a pagar y la fecha de vencimiento
            Console.WriteLine("Deuda a pagar: ");
            Console.WriteLine(loan.Amount.ToString());
            Console.WriteLine("");

            Console.WriteLine("Fecha de vencimiento: ");
            Console.WriteLine(loan.DueDate.ToString("yyyy-MM-dd"));
            Console.WriteLine("");

            Console.WriteLine("Presione cualquier tecla para continuar...");
            Console.ReadLine();

            // Llamar a la función PayLoan para realizar el pago del préstamo
            return PayLoan(user, loan);
        }

        // Método para pagar el préstamo
        public static User? PayLoan(User user, Loan loan)
        {
            // Obtener el grupo de ahorro asociado al préstamo
            SavingGroup? savingGroup = SavingGroupController.GetSavingGroupById(loan.Saving.SavingGroupId);

            // Verificar si el usuario tiene suficiente dinero para pagar
            if (user.Account < loan.Amount)
            {
                Console.WriteLine("El usuario no tiene suficiente cash para pagar (se le embargará la casa) ");
                Console.ReadLine();
                Console.Clear();
                return null;
            }

            // Añadir la cantidad al grupo de ahorro
            SavingGroupController.AddAmountToSavingGroup(savingGroup, loan.Amount);

            // Deducción de la cantidad de la cuenta del usuario
            user = UserController.RemoveAmount(user, loan.Amount);

            // Obtener el Saving asociado al usuario y al grupo de ahorro
            Saving? saving = SavingController.GetSavingByUserAndSavingGroup(user, savingGroup);

            // Agregar la cantidad a la inversión en el Saving
            SavingController.AddInvestmentToSaving(saving.Id, loan.Amount);

            // Indicar que el préstamo ha sido pagado
            LoanController.UpdateLoanPaidStatus(loan, true);

            Console.WriteLine("Se pagó el préstamo correctamente :) ...");
            Console.ReadLine();
            AnsiConsole.Clear();
            return user;
        }

    }
}
