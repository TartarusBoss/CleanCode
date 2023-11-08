using Org.BouncyCastle.Crypto.Tls;
using Piranha;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using UdemBank;

namespace UdemBank.Controllers
{
    internal class UserController
    {
        // Método para verificar si existe un usuario con el nombre de usuario proporcionado.
        public static User? VerifyExistingUser(string username)
        {
            using var db = new UdemBankContext(); // Establece una conexión con la base de datos.

            var existingUser = db.Users.SingleOrDefault(u => u.Name == username);

            if (existingUser != null)
            {
                return existingUser; // Devuelve el usuario existente.
            }
            return null;
        }

        // Método para verificar si existe un usuario con el ID de usuario proporcionado.
        public static User? VerifyExistingUserById(int userId)
        {
            using var db = new UdemBankContext(); // Establece una conexión con la base de datos.

            var existingUser = db.Users.SingleOrDefault(u => u.Id == userId);

            if (existingUser != null)
            {
                return existingUser; // Devuelve el usuario existente.
            }
            Console.WriteLine("HOLY FUCK EL USUARIO ES NULO QUE");
            Console.ReadLine();
            return null;
        }

        // Método para agregar un nuevo usuario a la base de datos.
        public static void AddUser()
        {
            List<string> userInfo = UserService.GetUserLogInInput(); // Obtiene la información del usuario.

            string name = userInfo[0]; // Obtiene el nombre de usuario.
            string password = userInfo[1]; // Obtiene la contraseña del usuario.

            var account = AnsiConsole.Ask<int>("Cantidad inicial en la cuenta:"); // Solicita al usuario la cantidad inicial en la cuenta.

            using var db = new UdemBankContext(); // Establece una conexión con la base de datos.

            if (!(VerifyExistingUser(name) == null))
            {
                Console.WriteLine("El usuario ya se encuentra registrado..."); // Muestra un mensaje de error si el usuario ya está registrado.
                Console.ReadLine();
                Console.Clear();
                return;
            }

            // Añade un nuevo usuario a la base de datos.
            db.Add(new User { Name = name, Password = password, Account = account });

            // Guarda los cambios en la base de datos.
            db.SaveChanges();
        }

        // Método para autenticar a un usuario en el sistema.
        public static User? AuthenticateUser()
        {
            List<string> userInfo = UserService.GetUserLogInInput(); // Obtiene la información del usuario.

            string name = userInfo[0]; // Obtiene el nombre de usuario.
            string password = userInfo[1]; // Obtiene la contraseña del usuario.

            using var db = new UdemBankContext(); // Establece una conexión con la base de datos.

            // Busca un usuario por nombre y contraseña.
            var user = db.Users.SingleOrDefault(u => u.Name == name && u.Password == password);

            if (user == null)
            {
                return null; // Devuelve nulo si el usuario no se encuentra.
            }
            return user; // Devuelve el usuario autenticado.
        }

        // Método para agregar una cantidad específica a la cuenta de un usuario.
        public static User? AddAmount(User user, double amount)
        {
            using var db = new UdemBankContext(); // Establece una conexión con la base de datos.
            if (!(user == null))
            {
                var existingUser = db.Users.SingleOrDefault(u => u.Id == user.Id);

                if (existingUser == null)
                {
                    Console.WriteLine("El existingUser es null"); // Muestra un mensaje de error si el usuario no se encuentra.
                    Console.ReadLine();
                    AnsiConsole.Clear();
                    return null;
                }
                existingUser.Account += amount; // Añade la cantidad especificada a la cuenta del usuario.
                db.SaveChanges(); // Guarda los cambios en la base de datos.
                AnsiConsole.Clear();
                return existingUser; // Devuelve el usuario con la cantidad añadida.
            }
            Console.WriteLine("Al addAmount del UserController se le mandó un user == null");
            Console.ReadLine();
            AnsiConsole.Clear();
            return null;
        }

        // Método para deducir una cantidad específica de la cuenta de un usuario.
        public static User? RemoveAmount(User user, double amount)
        {
            using var db = new UdemBankContext(); // Establece una conexión con la base de datos.

            var existingUser = db.Users.SingleOrDefault(u => u.Id == user.Id);

            if (existingUser == null)
            {
                Console.WriteLine("El existingUser es null"); // Muestra un mensaje de error si el usuario no se encuentra.
                Console.ReadLine();
                AnsiConsole.Clear();
                return null;
            }

            if (existingUser.Account >= amount)
            {
                existingUser.Account -= amount; // Deduce la cantidad especificada.
                db.SaveChanges(); // Guarda los cambios en la base de datos.
                AnsiConsole.Clear();
                return existingUser; // Devuelve el usuario con la cantidad deducida.
            }
            else
            {
                Console.WriteLine("No se puede deducir la cantidad ..."); // Muestra un mensaje de error si no se puede deducir la cantidad.
                Console.ReadLine();
                AnsiConsole.Clear();
            }
            return null;
        }

        // Método para recompensar a un usuario.
        public static User? RewardUser(User user)
        {
            using (var db = new UdemBankContext()) // Establece una conexión con la base de datos.
            {
                var existingUser = db.Users.FirstOrDefault(u => u.Id == user.Id);

                if (existingUser != null)
                {
                    existingUser.Rewarded = true; // Establece la propiedad "Rewarded" del usuario en verdadero.
                    db.SaveChanges(); // Guarda los cambios en la base de datos.
                    return existingUser; // Devuelve el usuario recompensado.
                }
                else
                {
                    Console.WriteLine("El RewardUser dice que el user es null (mero loko)"); // Muestra un mensaje de error si el usuario no se encuentra.
                    Console.ReadLine();
                    AnsiConsole.Clear();
                    return user;
                }
            }
        }

        public static void DeleteUser()
        {
            throw new NotImplementedException(); // Lanza una excepción de método no implementado.
        }

        // Método para obtener un usuario por su ID.
        public static User? GetUserById(int Id)
        {
            using var db = new UdemBankContext(); // Establece una conexión con la base de datos.
            var user = db.Users.SingleOrDefault(b => b.Id == Id);
            return user; // Devuelve el usuario encontrado.
        }

        // Método para obtener un usuario por su nombre de usuario.
        public static User? GetUserByName(string name)
        {
            using var db = new UdemBankContext(); // Establece una conexión con la base de datos.
            var user = db.Users.SingleOrDefault(b => b.Name == name);
            return user; // Devuelve el usuario encontrado.
        }
    }
}
