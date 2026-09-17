using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISIP324_Kovalenko
{
    internal class Program
    {
        class Product
        {
            static int nextCode = 1;
            int code = nextCode;
            string Name;
            decimal Price;
            int Quantity;
            bool InStock;
            Categories Category;

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
            
            public void DeleteProduct()
            {
                Name = null;
                Price = 0;
                Quantity = 0;
                InStock = false;
                Category = null;
            }

            public void AddProduct(int quantity)
            {
                if (quantity < 0)
                {
                    Console.WriteLine("Количество товара не может быть отрицательным");
                    return;
                }
                Quantity += quantity;
                InStock = Quantity > 0;
            }


        }

        enum Categories
        {
            "Электроника",
            "Одежда",
            "Продукты"
        }
        static void Main(string[] args)
        {
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
