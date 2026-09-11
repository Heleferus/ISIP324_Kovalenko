using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISIP324_Kovalenko
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int n = Convert.ToInt32(Console.ReadLine());
            while (n > 40 || n < 2)
            {
                Console.WriteLine("Не валидное количество операций");
                Console.WriteLine("Введите количество операций: ");
                n = Convert.ToInt32(Console.ReadLine());
            }

            List<string> names = new List<string>();
            List<double> prices = new List<double>();
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine("(Название услуги или товара; Количество денег) ");
                string newItem = Console.ReadLine();
                string[] newItems = newItem.Trim().Replace("(", "").Replace(")", "").Split(';');
                string name = newItems[0].Trim();
                double price = Convert.ToDouble(newItems[1].Trim());
                names.Add(name);
                prices.Add(price);
                Console.WriteLine($"Введен предмет: {name}, {price} руб");
            }

            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("Меню");
                Console.WriteLine("1.Вывод данных");
                Console.WriteLine("2.Статистика (среднее, максимальное, минимальное, сумма)");
                Console.WriteLine("3.Сортировка по цене (пузырьковая сортировка)");
                Console.WriteLine("4.Конвертация валюты (пользователь вводит курс или выбирает из списка)");
                Console.WriteLine("5.Поиск по названию ");
                Console.WriteLine("0.Выход");

                string action = Console.ReadLine();
                Console.WriteLine();
                switch (action)
                {
                    case "1":
                        printExpences(names, prices);
                        break;
                    case "2":
                        Statistics(prices);
                        break;
                    case "3":
                        Sort(names, prices);
                        break;
                    case "4":
                        ConvertValue(prices);
                        break;
                    case "5":
                        Search(names, prices);
                        break;
                    case "0":
                        exit = true;
                        Console.WriteLine("Завершение работы");
                        break;
                    default:
                        Console.WriteLine("Неверное действие");
                        break;
                }
            }
        }
            void printExpences(List<string> names, List<double> prices)
            {
                for (int i = 0; i < names.Count; i++)
                {
                    Console.WriteLine($"Предмет: {names}, {prices[i]}");
                }

            }
            
    }
}