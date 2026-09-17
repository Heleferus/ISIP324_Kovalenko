using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISIP324_Kovalenko
{
    internal class Program
    {
        public class Product
        {
            public static int nextCode = 1;
             public int code = nextCode;
            public string Name;
            public decimal Price;
            public int Quantity;
            public bool InStock;
            public string Category;

            public Product(string name, decimal price, int quantity, string category)
            {
                if (name == null || name == "")
                {
                    Console.WriteLine("Название товара не может быть пустым");
                    return;
                }
                if (price < 0)
                {
                    Console.WriteLine("Цена товара не может быть отрицательной");
                    return;
                }
                if (quantity < 0)
                {
                    Console.WriteLine("Количество товара не может быть отрицательным");
                    return;
                }
                if (category == null || category == "")
                {
                    Console.WriteLine("Категория товара не может быть пустой");
                    return;
                }
                Name = name;
                Price = price;
                Quantity = quantity;
                InStock = quantity > 0;
                Category = category;
                nextCode++;
            }

        }


        static void Main(string[] args)
        {
            List<Product> products = new List<Product>();
            void AddProduct(List<Product> products)
            {
                Console.WriteLine("Введите название товара:");
                string name = Console.ReadLine();
                Console.WriteLine("Введите цену товара:");
                decimal price = Convert.ToDecimal(Console.ReadLine());
                Console.WriteLine("Введите количество товара:");
                int quantity = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Введите категорию товара (Электроника, Одежда, Продукты):");
                string category = Console.ReadLine();
                Product product = new Product(name, price, quantity, category);
                products.Add(product);
            }

            void DeleteProduct(List<Product> products)
            {
                Console.WriteLine("Введите код товара,который хотите удалить:");
                int code = Convert.ToInt32(Console.ReadLine());
                Product product = products.Find(p => p.code == code);
                if (product != null)
                {
                    products.Remove(product);
                    Console.WriteLine("Товар удален");
                }
                else
                {
                    Console.WriteLine("Товар с таким кодом не найден");
                }
            }

            void OrderProduct(List<Product> products)
            {
                Console.WriteLine("Введите код товара,который хотите заказать:");
                int code = Convert.ToInt32(Console.ReadLine());
                Product product = products.Find(p => p.code == code);
                if (product != null)
                {
                    Console.WriteLine("Введите количество товара,которое хотите заказать:");
                    int quantity = Convert.ToInt32(Console.ReadLine());
                    product.Quantity += quantity;
                    product.InStock = true;
                    Console.WriteLine("Товар заказан");
                }
                else
                {
                    Console.WriteLine("Товар с таким кодом не найден");
                }
            }

            void BuyProduct(List<Product> products)
            {
                Console.WriteLine("Введите код товара,который хотите купить:");
                int code = Convert.ToInt32(Console.ReadLine());
                Product product = products.Find(p => p.code == code);
                if (product != null)
                {
                    if (product.InStock)
                    {
                        Console.WriteLine("Введите количество товара,которое хотите купить:");
                        int quantity = Convert.ToInt32(Console.ReadLine());
                        if (quantity <= product.Quantity)
                        {
                            product.Quantity -= quantity;
                            if (product.Quantity == 0)
                            {
                                product.InStock = false;
                            }
                            Console.WriteLine("Товар куплен");
                        }
                        else
                        {
                            Console.WriteLine("Недостаточно товара на складе");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Товара нет в наличии");
                    }
                }
                else
                {
                    Console.WriteLine("Товар с таким кодом не найден");
                }
            }

            void SellProduct(List<Product> products)
            {
                Console.WriteLine("Введите код товара,который хотите продать:");
                int code = Convert.ToInt32(Console.ReadLine());
                Product product = products.Find(p => p.code == code);
                if (product != null)
                {
                    if (product.InStock)
                    {
                        Console.WriteLine("Введите количество товара,которое хотите продать:");
                        int quantity = Convert.ToInt32(Console.ReadLine());
                        if (quantity <= product.Quantity)
                        {
                            product.Quantity -= quantity;
                            if (product.Quantity == 0)
                            {
                                product.InStock = false;
                            }
                            Console.WriteLine("Товар продан");
                        }
                        else
                        {
                            Console.WriteLine("Недостаточно товара на складе");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Товара нет в наличии");
                    }
                }
                else
                {
                    Console.WriteLine("Товар с таким кодом не найден");
                }
            }
            void SearchProduct(List<Product> products)
            {
                Console.WriteLine("Как вы хотите искать товар? (1.по коду, 2.по названию или 3.по категории)");
                string searchType = Console.ReadLine();
                Product product = null;
                switch (searchType)
                {
                    case "1":
                        Console.WriteLine("Введите код товара,который хотите найти:");
                        int code = Convert.ToInt32(Console.ReadLine());
                        product = products.Find(p => p.code == code);
                        break;
                    case "2":
                        Console.WriteLine("Введите название товара,который хотите найти:");
                        string name = Console.ReadLine();
                        product = products.Find(p => p.Name == name);
                        break;
                    case "3":
                        Console.WriteLine("Введите категорию товара,который хотите найти:");
                        string category = Console.ReadLine();
                        product = products.Find(p => p.Category == category);
                        break;
                    default:
                        Console.WriteLine("Неизвестный тип поиска");
                        break;
                }
                if (product != null)
                {
                    Console.WriteLine("Код: " + product.code);
                    Console.WriteLine("Название: " + product.Name);
                    Console.WriteLine("Цена: " + product.Price);
                    Console.WriteLine("Количество: " + product.Quantity);
                    Console.WriteLine("В наличии: " + product.InStock);
                    Console.WriteLine("Категория: " + product.Category);
                }
                else
                {
                    Console.WriteLine("Товар с таким кодом не найден");
                }
            }

            while (true)
            {
                Console.WriteLine("Введите команду (Добавить, Удалить, Заказать, Продать, Найти, Выход):");
                string command = Console.ReadLine();
                switch (command)
                {
                    case "Добавить":
                        AddProduct(products);
                        break;
                    case "Удалить":
                        DeleteProduct(products);
                        break;
                    case "Заказать":
                        OrderProduct(products);
                        break;
                    case "Продать":
                        SellProduct(products);
                        break;
                    case "Найти":
                        SearchProduct(products);
                        break;
                    case "Выход":
                        return;
                    default:
                        Console.WriteLine("Неизвестная команда");
                        break;
                }
            }
            //Создайте консольное приложение C# для учёта товаров в магазине. 

            //У товара должны быть следующее параметры:

            //Уникальный код(начинается с "1", должен автоматически ставиться при пополнении списка товаров)

            //Название

            //Цена

            //Количество

            //Остался ли ещё товар на складе

            //Категория(выбирается из имеющихся, задаются в коде, сделайте как минимум 3)


            //Мы можем работать с товаром через команды:

            //            Добавить товар

            //Удалить товар

            //Заказать поставку товара

            //Продать товар

            //Поиск товаров(по коду, названию и категории).Необходимо выводить полную информацию о товаре.


            //Для выполнения задания используйте все возможности языка C#, изученные ранее (классы, списки, перечисления и так далее).Обязательно заполните список товаров пятью тестовыми данными.Обязательно сделайте проверку всевозможных вводимых значений (не должно быть возможности создать пустой товар, с отрицательной ценой, с отрицательным количеством).Программа не должна вылетать в процессе работы.Программа должна выводить информацию в чётком и ясном виде для пользователя.При продаже товара, обязательно сделайте проверку остатка на складе.Не забудьте отправлять код по частям, разными коммитами, и делать комментарии к коммитам осмысленные.
        }
    }
}
