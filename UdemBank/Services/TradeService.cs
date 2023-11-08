using Org.BouncyCastle.Crypto.Tls;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemBank.Controllers;
using static UdemBank.Enums;

namespace UdemBank.Services
{
    internal class TradeService
    {
        // Agregar cantidad al grupo
        public static User? AddAmountToGroup(User user, SavingGroup savingGroup)
        {
            // Solicitar al usuario que ingrese la cantidad
            var amount = AnsiConsole.Ask<int>("Ingrese la cantidad: ");
            DateTime currentDateTime = DateTime.Now;

            // Verificar si el usuario tiene suficiente capital
            if (user.Account >= amount)
            {
                // Se agrega la cantidad al grupo de ahorro y se crea un registro de trade
                SavingGroupController.AddAmountToSavingGroup(savingGroup, amount);
                TradeController.AddTrade(user, savingGroup, TradeType.TransferenciaGrupoAhorro, amount, currentDateTime);

                // Agregar la cantidad al Saving asociado al usuario y al grupo de ahorro
                Saving? saving = SavingController.GetSavingByUserAndSavingGroup(user, savingGroup);
                SavingController.AddInvestmentToSaving(saving.Id, amount);

                // Deducir la cantidad del account del usuario
                user = UserController.RemoveAmount(user, amount);

                Console.WriteLine("Se ingresó el capital correctamente :) ...");
                Console.ReadLine();
                AnsiConsole.Clear();
                return user;
            }
            else
            {
                Console.WriteLine("El usuario no posee suficiente capital...");
                Console.ReadLine();
                AnsiConsole.Clear();
                return user;
            }
        }
    }
}
