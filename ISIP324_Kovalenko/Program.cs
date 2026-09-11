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
            Console.WriteLine("Введите количество операций ( от 2 до 40 ): ");
            int n = Convert.ToInt32(Console.ReadLine());
            while (n > 40 || n < 2)
            {
                Console.WriteLine("Не валидное количество операций");
                Console.WriteLine("Введите количество операций ( от 2 до 40 ): ");
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
                        SearchbyName(names, prices);
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
        static void printExpences(List<string> names, List<double> prices)
        {
            for (int i = 0; i < names.Count; i++)
            {
                Console.WriteLine($" {i + 1}. Предмет: {names[i]}, {prices[i]} руб");
            }

        }

        static void Statistics(List<double> prices)
        {
            double max = prices.Max();
            double min = prices.Min();
            double sum = prices.Sum();
            double average = prices.Average();

            Console.WriteLine($"Среднее: {average}");
            Console.WriteLine($"Максимальное: {max}");
            Console.WriteLine($"Минимальное: {min}");
            Console.WriteLine($"Сумма: {sum}");
        }

        static void Sort(List<string> names, List<double> prices)
        {
            for (int i = 0; i < names.Count - 1; i++)
            {
                for (int j = 0; j < names.Count - i - 1; j++)
                {

                    if (prices[j] > prices[j + 1])
                    {

                        double tempPrice = prices[j];
                        prices[j] = prices[j + 1];
                        prices[j + 1] = tempPrice;

                        string tempName = names[j];
                        names[j] = names[j + 1];
                        names[j + 1] = tempName;
                    }


                }
            }

            printExpences(names, prices);
        }

        static void ConvertValue(List<double> prices)
        {
            Console.WriteLine("1.USD (Курс 84 руб)");
            Console.WriteLine("2.EURO (Курс 97 руб)");
            Console.WriteLine("3.BYN (Курс 28 руб)");

            string choice = Console.ReadLine();
            int rate = 0;

            if (choice == "1") rate = 84;
            else if (choice == "2") rate = 97;
            else if (choice == "3") rate = 28;

            if (rate < 0)
            {
                Console.WriteLine("Неверный курс");
                return;
            }

            Console.WriteLine($"\nСуммы в пересчете (курс {rate}):");
            for (int i = 0; i < prices.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {prices[i] / rate}");
            }
        }

        static void SearchbyName(List<string> names, List<double> prices)
        {
            Console.WriteLine("Введите название предмета: ");
            string searchName = Console.ReadLine();
            bool found = false;
            for (int i = 0; i < names.Count; i++)
            {
                if (names[i].ToLower() == searchName.ToLower())
                {
                    Console.WriteLine($"Предмет: {names[i]}, {prices[i]} руб");
                    found = true;
                }
            }
            if (!found)
            {
                Console.WriteLine("Предмет не найден");
            }
        }
    }
}