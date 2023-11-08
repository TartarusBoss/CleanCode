using AutoMapper.Internal;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemBank.Controllers;

namespace UdemBank.Services
{
    internal class UserInterface
    {
        // Mostrar historial de transacciones
        public static void ShowTradesHistory(List<Trade> trades)
        {
            var table = new Table();
            table.AddColumn("Cantidad");
            table.AddColumn("Fecha");
            table.AddColumn("Tipo de transacción");

            // Agregar filas al historial de transacciones
            foreach (var trade in trades)
            {
                table.AddRow(trade.Amount.ToString(), trade.Date.ToString(), trade.Type.ToString());
            }
            AnsiConsole.Write(table);
            Console.ReadLine();
            Console.Clear();
        }

        // Mostrar información de usuario de un grupo de ahorro
        public static void ShowUserInfoOfSavingGroup(SavingGroup savingGroup)
        {
            string SavingGroupName = savingGroup.Name;
            List<Saving> savings = SavingController.GetSavingsByUserSavingGroupName(SavingGroupName);

            var table = new Table();
            table.AddColumn("Nombre del usuario: ");

            // Agregar filas con el nombre de usuario al grupo de ahorro
            foreach (var saving in savings)
            {
                table.AddRow(saving.User.Name.ToString());
            }
            AnsiConsole.Write(table);
            Console.ReadLine();
            AnsiConsole.Clear();
        }

        // Mostrar historial de transacciones del usuario
        public static void ShowUserTransactionHistory(User user)
        {
            // Obtener historial de intercambios y préstamos del usuario
            var tradeHistory = TradeController.GetTradeHistoryForUser(user);
            var loanHistory = LoanController.GetLoanHistoryForUser(user);

            // Combinar ambos historiales y ordenar por fecha
            var transactionHistory = new List<ITransaction>();
            transactionHistory.AddRange(tradeHistory);
            transactionHistory.AddRange(loanHistory);

            transactionHistory = transactionHistory
                .OrderBy(transaction => transaction.Date)
                .ToList();

            // Crear una tabla para mostrar el historial
            var table = new Table();
            table.Border = TableBorder.Rounded;
            table.AddColumn("Fecha");
            table.AddColumn("Hora");
            table.AddColumn("Tipo");
            table.AddColumn("Monto");
            table.AddColumn("Saldo en cuenta");

            // Agregar filas a la tabla de historial de transacciones
            foreach (var transaction in transactionHistory)
            {
                string date = "";
                string time = "";

                date = transaction.Date.Date.ToString();
                time = transaction.Date.TimeOfDay.ToString();

                table.AddRow(date, time, transaction.GetType().Name, transaction.Amount.ToString(), transaction.CurrentBalance.ToString());
            }
            AnsiConsole.Render(table);
            Console.ReadLine();
            AnsiConsole.Clear();
        }
    }
}
