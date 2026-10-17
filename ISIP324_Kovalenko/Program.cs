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

            bool loggedin = false;
            void RegisterUser()
            {
        
                Console.Write("Введите логин: ");
                string login = Console.ReadLine();
                Console.Write("Введите пароль: ");
                string password = Console.ReadLine();
                Console.Write("Подтвердите пароль: ");
                string confirmPassword = Console.ReadLine();
                if (password != confirmPassword)
                {
                    Console.WriteLine("Пароли не совпадают. Попробуйте снова.");
                    return;
                }
                Core.Context.Users.Add(new Users { Username = login, PasswordHash = password });
                Core.Context.SaveChanges();
                Console.WriteLine($"Пользователь {login} успешно зарегистрирован!");
            }

            void ViewProducts()
            {
                var products = Core.Context.Products.ToList();
                Console.WriteLine("Список товаров:");
                foreach (var product in products)
                {
                    Console.WriteLine($"ID: {product.ProductID}, Название: {product.Name}, Цена: {product.Price}");
                }
            }

            void LoginUser()
            {
                Console.Write("Введите логин: ");
                string login = Console.ReadLine();
                Console.Write("Введите пароль: ");
                string password = Console.ReadLine();
                var user = Core.Context.Users.FirstOrDefault(u => u.Username == login && u.PasswordHash == password);
                if (user != null)
                {
                    Console.WriteLine($"Добро пожаловать, {login}!");
                    
                }
                else
                {
                    Console.WriteLine("Неверный логин или пароль.");
                }
            }

            void AddToCart()
            {
                Console.Write("Введите ID товара для добавления в корзину: ");
                int productId = int.Parse(Console.ReadLine());
                var product = Core.Context.Products.FirstOrDefault(p => p.ProductID == productId);
                if (product != null)
                {
                    Console.WriteLine("Введите количество:");
                    int quantity = int.Parse(Console.ReadLine());
                    Console.WriteLine($"Товар {product.Name} добавлен в корзину.");
                    Core.Context.CartItems.Add(new CartItems { ProductID = productId, Quantity = quantity });
                    Core.Context.SaveChanges();
                }
                else
                {
                    Console.WriteLine("Товар с таким ID не найден.");
                }
            }

            void ViewCart()
            {
                var cartItems = Core.Context.CartItems.ToList();
                Console.WriteLine("Содержимое корзины:");
                foreach (var item in cartItems)
                {
                    var product = Core.Context.Products.FirstOrDefault(p => p.ProductID == item.ProductID);
                    if (product != null)
                    {
                        Console.WriteLine($"Товар: {product.Name}, Количество: {item.Quantity}, Цена за единицу: {product.Price}");
                    }
                }
            }

            void Buy()
            {
                Console.WriteLine("Выберите ПВЗ:");
                var pickupPoints = Core.Context.PickUpPoints.ToList();
                foreach (var point in pickupPoints)
                {
                    Console.WriteLine($"ID: {point.PointID}, Адрес: {point.Address}");
                }
                Console.Write("Введите ID выбранного ПВЗ: ");
                int pickupPointId = int.Parse(Console.ReadLine());
                var selectedPickupPoint = Core.Context.PickUpPoints.FirstOrDefault(p => p.PointID == pickupPointId);
                Console.WriteLine("Вы хотите купить все товары из корзины?");
                string choice = Console.ReadLine();
                if (choice.ToLower() == "да")
                {
                    var cartItems = Core.Context.CartItems.ToList();
                    foreach (var item in cartItems)
                    {
                        var order = new Orders
                        {
                            ProductID = item.ProductID,
                            Quantity = item.Quantity,
                            PickupPointID = pickupPointId,
                            OrderDate = DateTime.Now
                        };
                        Core.Context.Orders.Add(order);
                    }
                    Core.Context.CartItems.RemoveRange(cartItems);
                    Core.Context.SaveChanges();
                    Console.WriteLine("Все товары из корзины успешно куплены!");
                }
                else
                {
                    Console.Write("Введите ID товара для покупки: ");
                    int productId = int.Parse(Console.ReadLine());
                    var cartItem = Core.Context.CartItems.FirstOrDefault(c => c.ProductID == productId);
                    if (cartItem != null)
                    {
                        var order = new Orders
                        {
                            ProductID = cartItem.ProductID,
                            Quantity = cartItem.Quantity,
                            PickupPointID = pickupPointId,
                            OrderDate = DateTime.Now
                        };
                        Core.Context.Orders.Add(order);
                        Core.Context.CartItems.Remove(cartItem);
                        Core.Context.SaveChanges();
                        Console.WriteLine($"Товар с ID {productId} успешно куплен!");
                    }
                    else
                    {
                        Console.WriteLine("Товар с таким ID не найден в корзине.");
                    })
            }



        }
    }
}

//Разработать консольное приложение - онлайн маркетплейс GMWOG||GG.MOW||WONGG.

//У маркетплейса должна быть своя база данных, нормализованная под 3НФ. 
//Создайте консольное приложение, в котором должна быть регистрация пользователя с подтверждением пароля, просмотр товаров (даже для незарегистрированного пользователя).
//Должна быть корзина товаров и покупка как одного предмета, так и всех предметов из всей корзины сразу. 
//При покупке, пользователь должен выбирать ПВЗ (пункт выдачи заказов) из доступных. 
//Должен быть функционал для просмотра заказов с сортировкой по дате, когда был сделан заказ. 
//Также, реализуйте возможность зайти в аккаунт уже зарегистрированного пользователя.
