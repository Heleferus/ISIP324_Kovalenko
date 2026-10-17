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
