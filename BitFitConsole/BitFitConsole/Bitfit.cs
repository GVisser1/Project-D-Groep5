using System;
using System.Collections.Generic;

namespace BitFitConsole
{
    class Bitfit
    {
        public static List<User> Users = new List<User>() { new User("Glenn Visser", "M", 21, 66) };
        public static User currentUser;

        public static void SignIn()
        {
            Console.WriteLine("Welcome to Bitfit!\n"
                + "Choose a user: ");
            var choices = new List<int>();
            for (int i = 0; i < Users.Count; i++)
            {
                Console.WriteLine($"({i + 1}) - {Users[i].Fullname}\n");
                choices.Add(i + 1);
            }
            
            Console.WriteLine("Please enter your choice: ");
            int choice = int.Parse(Console.ReadLine());

            while (!choices.Contains(choice)) {
                Console.WriteLine("Input was invalid. Enter a valid choice: ");
                choice = int.Parse(Console.ReadLine());
            }
            currentUser = Users[choice - 1];
            Console.WriteLine("------------------------------------\n");
            Menu();
        }

        public static void Menu()
        {
            Console.WriteLine($"Welcome {currentUser.Fullname}\n"
                              + "------------------------------------\n"
                              + "(1) - Add user\n"
                              + "(2) - View user\n"
                              + "(3) - Create workout schedule\n"
                              + "(4) - View workout schedule\n" 
                              + "(5) - Sign out\n"
                              + "------------------------------------\n");
            Console.WriteLine("Please enter your choice: ");
            int choice;
            while (!int.TryParse(Console.ReadLine(), out choice))
            {
                Console.WriteLine("Input was invalid. Enter a valid choice: ");
            }
            switch (choice)
            {
                case 1:
                    User.AddUser();
                    break;
                case 2:
                    User.ViewUser();
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    currentUser = null;
                    SignIn();
                    break;
            }
        }
        public static void ReturnToMenu()
        {
            Console.WriteLine("Press any key to return to the menu");
            Console.ReadLine();
            Menu();
        }
    }
}
