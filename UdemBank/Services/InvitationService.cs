using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemBank.Controllers;

namespace UdemBank.Services
{
    internal class InvitationService
    {
        // Método para invitar a un amigo a un grupo de ahorro
        public static void InviteFriend(SavingGroup SavingGroup)
        {
            // Verificar si el usuario al que se quiere invitar existe
            var name = AnsiConsole.Ask<string>("Nombre de usuario al que desea invitar:");
            User? user = UserController.GetUserByName(name);

            // Verificar si el usuario existe
            if (user != null)
            {
                // Obtener los grupos de ahorro asociados al usuario
                List<SavingGroup> SavingGroups = SavingGroupController.GetSavingGroupsByUser(user);

                // Verificar si el usuario ya pertenece a 3 grupos de ahorro
                if (!(SavingGroups == null))
                {
                    if (SavingGroups.Count < 3)
                    {
                        // Agregar el usuario al grupo de ahorro
                        SavingController.AddSaving(user, SavingGroup, true);
                        Console.WriteLine("Se invitó al usuario correctamente... :)");
                    }
                    else
                    {
                        Console.WriteLine("El usuario ya se encuentra en 3 grupos de ahorro...");
                        Console.ReadLine();
                    }
                }
                else
                {
                    // Agregar el usuario al grupo de ahorro si no está en ningún grupo
                    SavingController.AddSaving(user, SavingGroup, true);
                    Console.WriteLine("Se invitó al usuario correctamente... :)");
                    Console.ReadLine();
                    AnsiConsole.Clear();
                }
            }
            else
            {
                // Mostrar mensaje si el usuario no se encontró
                Console.WriteLine("No se encontró el usuario...");
                Console.ReadLine();
            }
        }
    }
}
