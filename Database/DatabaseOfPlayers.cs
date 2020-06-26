using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Database
{
    public class DatabaseOfPlayers
    {
        private readonly List<Player> _players = new List<Player>();
        
        public void AddPlayer()
        {
            int serialNumber, level;
            string nickName;
            bool isBanned;
            
            bool isAddedPlayer = true;

            while (isAddedPlayer)
            {
                Console.Write("Введите порядковый номер: ");
                if (int.TryParse(Console.ReadLine(), out int resultOfInputSerialNumber) &&
                    resultOfInputSerialNumber > 0)
                {
                    serialNumber = resultOfInputSerialNumber;

                    if (_players.Any(player => player.SerialNumber == resultOfInputSerialNumber))
                    {
                        Console.WriteLine("Игрок с таким номером уже существует");
                        continue;
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Порядковый номер не может быть отрицательным или равным 0, или вы ввели не число");
                    continue;
                }

                Console.Write("Введите имя игрока: ");
                string nickNameInput = Console.ReadLine();
                
                if (_players.Any(player => player.NickName == nickNameInput))
                {
                    Console.WriteLine("Игрок с таким именем уже существует.");
                    continue;
                }

                nickName = nickNameInput;

                Console.Write("Введите уровень: ");
                if (int.TryParse(Console.ReadLine(), out int resultOfInputLevel) && resultOfInputLevel > 0)
                {
                    level = resultOfInputLevel;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Уровень не может быть отрицательным.");
                    continue;
                }

                isBanned = false;
                isAddedPlayer = false;

                Console.WriteLine("Игрок успешно добавлен");
                Thread.Sleep(1000);
                Console.Clear();
                
                _players?.Add(new Player(serialNumber, nickName, level, isBanned));
            }
        }
        
        public void BanPlayerBySerialNumber()
        {
            if (CheckIsListEmpty())
            {
                bool isPlayerNotBanned = true;

                while (isPlayerNotBanned)
                {
                    WriteList();
                    
                    if (ChangePlayerValueIsBanned(false, true))
                    {
                        Console.WriteLine("Игрок успешно забанен");
                        Console.WriteLine("Нажмите любую клавишу, чтобы продолжить.");
                        Console.ReadKey();
                        Console.Clear();
                        isPlayerNotBanned = false;
                    }
                    else
                    {
                        isPlayerNotBanned = false;
                    }
                }
            }
        }

        public void UnBanPlayerBySerialNumber()
        {
            if (CheckIsListEmpty())
            {
                bool isNotUnbanned = true;

                while (isNotUnbanned)
                {
                    WriteList();
                    
                    if (ChangePlayerValueIsBanned(true, false))
                    {
                        Console.WriteLine("Игрок успешно разбанен");
                        Console.WriteLine("Нажмите любую клавишу, чтобы продолжить.");
                        Console.ReadKey();
                        Console.Clear();
                        isNotUnbanned = false;
                    }
                    else
                    {
                        isNotUnbanned = false;
                    }
                }
            }
        }

        public void DeletePlayer()
        {
            if (CheckIsListEmpty())
            {
                WriteList();
                
                Console.Write("Введите порядковый номер игрока, которого хотите удалить: ");
                if (int.TryParse(Console.ReadLine(), out int result))
                {

                    if (_players.Any(player => player.SerialNumber == result))
                    {
                        _players.Remove(_players.Find(player => player.SerialNumber == result));
                        Console.WriteLine("Игрок успешно удалён.");
                        Console.WriteLine("Нажмите любую клавишу, чтобы продолжить.");
                        Console.ReadKey();
                        Console.Clear();
                    }
                    else
                    {
                        Console.WriteLine($"Игрока с порядковыми номером {result} не существует");
                        Console.WriteLine("Нажмите любую клавишу, чтобы продолжить.");
                        Console.ReadKey();
                        Console.Clear();
                    }

                }
                else
                {
                    Console.WriteLine("Вы вели не число.");
                    Console.WriteLine("Нажмите любую клавишу, чтобы продолжить.");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
        }

        public void WriteList()
        {
            if (CheckIsListEmpty())
            {
                foreach (var player in _players)
                {
                    Console.WriteLine($"{player.SerialNumber}. " +
                                      $"Имя игрока: {player.NickName} | " +
                                      $"Уровень игрока: {player.Level} | " +
                                      $"Забанен: {player.IsBanned}");
                }

                Console.WriteLine();
            }
        }
        
        private bool ChangePlayerValueIsBanned(bool oldValue, bool newValue)
        {
            Console.Write("Введите порядковый номер: ");
            if (int.TryParse(Console.ReadLine(), out int result))
            {
                var playerMeetingACondition = _players.Find(player => 
                                                                player.SerialNumber == result && player.IsBanned == oldValue);
                
                if (_players.Any(player => player.SerialNumber == result && player.IsBanned == oldValue))
                {
                    if (oldValue)
                    {
                        playerMeetingACondition.UnBan();
                    }
                    else
                    {
                        playerMeetingACondition.Ban();
                    }
                }
                else if (_players.Any(player => player.SerialNumber == result && player.IsBanned == newValue))
                {
                    Console.WriteLine($"Игрока с порядковым номером {result} забанен/разбанен");
                    Console.WriteLine("Нажмите любую клавишу, чтобы продолжить");
                    Console.ReadKey();
                    Console.Clear();
                    return false;
                }

                if (result > _players.Count)
                {
                    Console.WriteLine($"Игрока с порядковым номером {result} не существует");
                    Console.WriteLine("Нажмите любую клавишу, чтобы продолжить");
                    Console.ReadKey();
                    Console.Clear();
                    return false;
                }
            }
            else
            {
                Console.WriteLine("Вы ввели не число.");
                return false;
            }

            return true;
        }

        private bool CheckIsListEmpty()
        {
            if (_players.Count == 0)
            {
                Console.WriteLine("База данных пуста.");
                Console.WriteLine();
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}