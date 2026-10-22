using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISIP324_Kovalenko
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Users currentuser = null;
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
                    currentuser = user;
                    loggedin = true;

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
                var cartItems = Core.Context.CartItems.Where(c => c.UserID == currentuser.UserID).ToList();
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
                if (selectedPickupPoint == null)
                {
                    Console.WriteLine("ПВЗ с таким ID не найден.");
                    return;
                }

                Console.WriteLine("Вы хотите купить все товары из корзины?");
                string choice = Console.ReadLine();
                if (choice.ToLower() == "да")
                {
                    var cartItems = Core.Context.CartItems.Where(c => c.UserID == currentuser.UserID).ToList();
                    if (cartItems.Count == 0)
                    {
                        Console.WriteLine("Корзина пуста.");
                        return;
                    }

                    var order = new Orders
                    {
                        PointID = selectedPickupPoint.PointID,
                        OrderDate = DateTime.Now,
                        Status = "Создан"
                    };

                    foreach (var item in cartItems)
                    {
                        var product = Core.Context.Products.FirstOrDefault(p => p.ProductID == item.ProductID);
                        if (product != null)
                        {
                            order.OrderItems.Add(new OrderItems
                            {
                                Products = product,
                                Quantity = item.Quantity,
                                PriceAtPurchase = product.Price
                            });
                            order.TotalAmount += product.Price * item.Quantity;
                        }
                    }

                    Core.Context.Orders.Add(order);
                    Core.Context.CartItems.RemoveRange(cartItems);
                    Core.Context.SaveChanges();
                    Console.WriteLine("Все товары из корзины успешно куплены!");
                }
                else
                {
                    var cartItems = Core.Context.CartItems.Where(c => c.UserID == currentuser.UserID).ToList();
                    if (cartItems.Count == 0)
                    {
                        Console.WriteLine("Корзина пуста.");
                        return;
                    }

                    Console.WriteLine("Выберите товар из корзины для покупки:");
                    foreach (var item in cartItems)
                    {
                        var cartProduct = Core.Context.Products.FirstOrDefault(p => p.ProductID == item.ProductID);
                        if (cartProduct != null)
                        {
                            Console.WriteLine($"ID позиции: {item.CartItemID}, Товар: {cartProduct.Name}, Количество: {item.Quantity}, Цена за единицу: {cartProduct.Price}");
                        }
                    }

                    Console.Write("Введите ID позиции в корзине: ");
                    int cartItemId = int.Parse(Console.ReadLine());
                    var selectedCartItem = cartItems.FirstOrDefault(item => item.CartItemID == cartItemId);
                    if (selectedCartItem == null)
                    {
                        Console.WriteLine("Позиция с таким ID не найдена в корзине.");
                        return;
                    }

                    var product = Core.Context.Products.FirstOrDefault(p => p.ProductID == selectedCartItem.ProductID);
                    if (product == null)
                    {
                        Console.WriteLine("Товар из выбранной позиции не найден.");
                        return;
                    }

                    var order = new Orders
                    {
                        PointID = selectedPickupPoint.PointID,
                        OrderDate = DateTime.Now,
                        TotalAmount = product.Price * selectedCartItem.Quantity,
                        Status = "Создан"
                    };
                    order.OrderItems.Add(new OrderItems
                    {
                        Products = product,
                        Quantity = selectedCartItem.Quantity,
                        PriceAtPurchase = product.Price
                    });
                    Core.Context.Orders.Add(order);
                    Core.Context.CartItems.Remove(selectedCartItem);
                    Core.Context.SaveChanges();
                    Console.WriteLine($"Товар {product.Name} успешно куплен!");
                }

            }
            void ViewOrders()
            {
                var orders = Core.Context.Orders.OrderBy(o => o.OrderDate).ToList();
                Console.WriteLine("Список заказов:");
                foreach (var order in orders)
                {
                    Console.WriteLine($"ID заказа: {order.OrderID}, Дата: {order.OrderDate}, Сумма: {order.TotalAmount}, Статус: {order.Status}, ID точки выдачи: {order.PointID}");
                }
            }

            void menu()
            {
                Console.WriteLine("Добро пожаловать в магазин");
                while (true)
                {
                    Console.WriteLine("1 - registry, 2 - login, 3 - viewTovari");
                    if (loggedin) { Console.WriteLine("4 - viewCart, 5 - Buy, 6 - viewOrders, 7- Add"); } else { Console.WriteLine("С логином будет больше"); }
                ;
                    Console.Write("Что делаем? ");
                    int action = int.Parse(Console.ReadLine());
                    switch (action)
                    {
                        case 1: RegisterUser(); break;
                        case 2: LoginUser(); break;
                        case 3: ViewProducts(); break;
                        case 4: if (loggedin) { ViewCart(); break; } else { Console.WriteLine("Вы не залогинены для данной операции"); break; };
                        case 5: if (loggedin) { Buy(); break; } else { Console.WriteLine("Вы не залогинены для данной операции"); break; };
                        case 6: if (loggedin) { ViewOrders(); break; } else { Console.WriteLine("Вы не залогинены для данной операции"); break; };
                        case 7: if (loggedin) { AddToCart(); break; } else { Console.WriteLine("Вы не залогинены для данной операции"); break; };
                        default: return;
                    }
                }
            }
            menu();
        }
    }
}


