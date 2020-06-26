using System;

namespace Database
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            DatabaseOfPlayers databaseOfPlayers = new DatabaseOfPlayers();

            bool isWorks = true;
            while (isWorks)
            {
                WriteMenu();
                
                Console.Write("Выбирите пункт меню: ");
                if (int.TryParse(Console.ReadLine(), out int result))
                {
                    Console.Clear();
                    switch (result)
                    {
                        case 1:
                            databaseOfPlayers.AddPlayer();
                            break;
                        case 2:
                            databaseOfPlayers.WriteList();;
                            break;
                        case 3:
                            databaseOfPlayers.BanPlayerBySerialNumber();
                            break;
                        case 4:
                            databaseOfPlayers.UnBanPlayerBySerialNumber();
                            break;
                        case 5:
                            databaseOfPlayers.DeletePlayer();
                            break;
                        case 6:
                            isWorks = false;
                            break;
                        default:
                            throw new InvalidOperationException($"Вы ввели не существующий пункт меню {result}");
                    }
                }
            }
        }

        private static void WriteMenu()
        {
            string[] menuItems =
            {
                "Добавить игрока",
                "Вывести список игроков",
                "Забанить игрока",
                "Разбанить игрока",
                "Удалить игрока",
                "Выход"
            };
            
            int index = 1;
            Console.WriteLine("Меню:");
            foreach (var menuItem in menuItems)
            {
                Console.WriteLine($"{index} - {menuItem}");
                index++;
            }
        }
    }
}