using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemBank.Controllers;

namespace UdemBank
{
    public class SavingGroupService
    {
        // Método para verificar y agregar un grupo de ahorro
        public static SavingGroup? CheckAndAddSavingGroup(User user)
        {
            // Obtener los grupos de ahorro del usuario
            List<SavingGroup> savingGroups = SavingGroupController.GetSavingGroupsByUser(user);

            // Verificar si el usuario ya está en 3 grupos de ahorro
            if (savingGroups != null)
            {
                if (!(savingGroups.Count < 3))
                {
                    Console.WriteLine("El usuario ya pertenece a 3 grupos de ahorro y no se puede unir a más.");
                    Console.ReadLine();
                    Console.Clear();
                    return null;
                }
            }

            // Agregar un grupo de ahorro para el usuario
            SavingGroup SavingGroup = SavingGroupController.AddSavingGroup(user);
            return SavingGroup;
        }
    }
}
