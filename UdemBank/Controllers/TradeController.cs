using Microsoft.EntityFrameworkCore;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static UdemBank.Enums;

namespace UdemBank.Controllers
{
    internal class TradeController
    {
        // Método para agregar un intercambio en la base de datos.
        public static void AddTrade(User user, SavingGroup savingGroup, TradeType type, int amount, DateTime date)
        {
            using var db = new UdemBankContext(); // Establece una conexión con la base de datos.

            var saving = db.Savings.FirstOrDefault(s => s.UserId == user.Id && s.SavingGroupId == savingGroup.Id);

            if (saving == null)
            {
                Console.WriteLine("No se encontró el grupo de ahorro para el usuario."); // Muestra un mensaje de error.
                return;
            }

            var trade = new Trade
            {
                SavingId = saving.Id,
                Amount = amount,
                Date = date,
                Type = type
            };

            db.Trades.Add(trade);
            db.SaveChanges(); // Guarda los cambios en la base de datos.
        }

        // Método para obtener un intercambio por su ID.
        public static Trade? GetTradeById(int Id)
        {
            using var db = new UdemBankContext(); // Establece una conexión con la base de datos.
            var trade = db.Trades.SingleOrDefault(b => b.Id == Id);
            return trade; // Devuelve el intercambio.
        }

        // Método para obtener los intercambios para los ahorros de un usuario.
        public static List<Trade> GetTradesForUserSavings(int knownUserId)
        {
            using (var context = new UdemBankContext()) // Establece una conexión con la base de datos.
            {
                var trades = context.Trades
                    .Where(trade => context.Savings
                        .Where(saving => saving.UserId == knownUserId)
                        .Select(saving => saving.Id)
                        .Contains(trade.SavingId))
                    .ToList();

                return trades; // Devuelve la lista de intercambios.
            }
        }

        // Método para obtener el historial de intercambios de un usuario.
        public static List<Trade> GetTradeHistoryForUser(User user)
        {
            using (var db = new UdemBankContext()) // Establece una conexión con la base de datos.
            {
                var tradeHistory = db.Trades
                    .Where(trade => db.Savings
                        .Where(saving => saving.UserId == user.Id)
                        .Select(saving => saving.Id)
                        .Contains(trade.SavingId))
                    .ToList();

                return tradeHistory; // Devuelve el historial de intercambios.
            }
        }
    }
}
