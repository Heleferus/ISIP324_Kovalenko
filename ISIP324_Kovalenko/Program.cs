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
            List<Product> products = new List<Product>();
            void AddProduct()
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

            void DeleteProduct()
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

            void OrderProduct()
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

            void BuyProduct()
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
                            product.Quantity += quantity;
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

            void SellProduct()
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
            void SearchProduct()
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
                Console.WriteLine("Введите команду (1.Добавить, 2.Удалить, 3.Заказать, 4.Продать, 5.Найти, 6.Выход):");
                string command = Console.ReadLine();
                switch (command)
                {
                    case "1":
                        AddProduct();
                        break;
                    case "2":
                        DeleteProduct();
                        break;
                    case "3":
                        OrderProduct();
                        break;
                    case "4":
                        SellProduct();
                        break;
                    case "5":
                        SearchProduct();
                        break;
                    case "6":
                        return;
                    default:
                        Console.WriteLine("Неизвестная команда");
                        break;
                }
            }
        }   
    }
}
