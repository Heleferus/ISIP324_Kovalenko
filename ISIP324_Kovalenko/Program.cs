using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISIP324_Kovalenko
{
    internal class Program
    {
        class Book
        {
            private static int nextId = 1;
            public int Id { get; private set; }
            public string Title { get; set; }
            public string Author { get; set; }
            public Genre Genre { get; set; }
            public int Year { get; set; }
            public decimal Price { get; set; }
            public Book(string title, string author, Genre genre, int year, decimal price)
            {
                Id = nextId++;
                Title = title;
                Author = author;
                Genre = genre;
                Year = year;
                Price = price;
            }
        }


        enum Genre
        {
            Fiction,
            NonFiction,
            ScienceFiction,
            Fantasy,
            Mystery,
        }


        static void Main(string[] args)
        {
            List<Book> books = new List<Book>
            {
                new Book("Великий Гэтсби", "Джон Фицджеральд", Genre.Fiction, 1925, 10.99m),
                new Book("1984", "Джордж Оруэлл", Genre.ScienceFiction, 1949, 8.99m),
                new Book("Убить пересмешника", "Харпер Ли", Genre.Fiction, 1960, 12.99m),
                new Book("Хоббит", "Джон Роллинг", Genre.Fantasy, 1937, 15.99m),
                new Book("Код Да Винчи", "Дэн Браун", Genre.Mystery, 2003, 9.99m),
                new Book("Код ", "Дэн Браун", Genre.Mystery, 2003, 9.99m)
            };

            void Add()
            {
                Console.WriteLine("Введите название книги:");
                string title = Console.ReadLine();
                if (title == null || title.Trim() == "")
                {
                    Console.WriteLine("Название книги не может быть пустым.");
                    return;
                }
                Console.WriteLine();
                Console.WriteLine("Введите автора книги:");
                string author = Console.ReadLine();
                if (author == null || author.Trim() == "")
                {
                    Console.WriteLine("Автор книги не может быть пустым.");
                    return;
                }
                Console.WriteLine();
                Console.WriteLine("Выберите жанр книги (0 - Fiction, 1 - NonFiction, 2 - ScienceFiction, 3 - Fantasy, 4 - Mystery):");
                if (!Enum.TryParse(Console.ReadLine(), out Genre genre) || !Enum.IsDefined(typeof(Genre), genre))
                {
                    Console.WriteLine("Неверный жанр.");
                    return;
                }
                Console.WriteLine("Введите год издания книги:");
                if (!int.TryParse(Console.ReadLine(), out int year) || year <= 0)
                {
                    Console.WriteLine("Год издания должен быть положительным числом.");
                    return;
                }
                Console.WriteLine();
                Console.WriteLine("Введите цену книги:");
                if (!decimal.TryParse(Console.ReadLine(), out decimal price) || price < 0)
                {
                    Console.WriteLine("Цена книги не может быть отрицательной.");
                    return;
                }
                Console.WriteLine();
                books.Add(new Book(title, author, genre, year, price));
                Console.WriteLine("Книга  добавлена");
                Console.WriteLine();
            }

            void Delete()
            {
                Console.WriteLine("Введите идентификатор книги для удаления:");
                if (!int.TryParse(Console.ReadLine(), out int id))
                {
                    Console.WriteLine("Неверный идентификатор.");
                    return;
                }
                Console.WriteLine();
                var bookToRemove = books.FirstOrDefault(b => b.Id == id);
                if (bookToRemove != null)
                {
                    books.Remove(bookToRemove);
                    Console.WriteLine("Книга удалена.");
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine("Книга с таким идентификатором не найдена.");
                    Console.WriteLine();
                }
            }

            void Search()
            {
                Console.WriteLine("Введите название, автора или жанр книги для поиска:");
                string searchTerm = Console.ReadLine();
                var results = books.Where(b => b.Title.Contains(searchTerm) || b.Author.Contains(searchTerm) || b.Genre.ToString().Contains(searchTerm) || b.Year.ToString().Contains(searchTerm) || b.Price.ToString().Contains(searchTerm)).ToList();
                if (results.Any())
                {
                    foreach (var book in results)
                    {
                        Console.WriteLine($"ID: {book.Id}, Название: {book.Title}, Автор: {book.Author}, Жанр: {book.Genre}, Год: {book.Year}, Цена: {book.Price}");
                        Console.WriteLine();
                    }
                }
                else
                {
                    Console.WriteLine("Книги не найдены.");
                    Console.WriteLine();
                }
                Console.WriteLine();
            }

            void Sort()
            {
                Console.WriteLine("Выберите параметр для сортировки (0 - Название, 1 - Год):");
                if (!int.TryParse(Console.ReadLine(), out int sortOption))
                {
                    Console.WriteLine("Неверный параметр.");
                    return;
                }
                Console.WriteLine();
                Console.WriteLine();
                List<Book> sortedBooks;
                if (sortOption == 0)
                {
                    sortedBooks = books.OrderBy(b => b.Title).ToList();
                }
                else if (sortOption == 1)
                {
                    sortedBooks = books.OrderBy(b => b.Year).ToList();
                }
                else
                {
                    Console.WriteLine("Неверный параметр.");
                    return;
                }
                foreach (var book in sortedBooks)
                {
                    Console.WriteLine($"ID: {book.Id}, Название: {book.Title}, Автор: {book.Author}, Жанр: {book.Genre}, Год: {book.Year}, Цена: {book.Price}");
                }
                Console.WriteLine();

            }

            void Pricesminormax()
            {
                var mostExpensiveBook = books.OrderByDescending(b => b.Price).FirstOrDefault();
                var leastExpensiveBook = books.OrderBy(b => b.Price).FirstOrDefault();
                if (mostExpensiveBook != null)
                {
                    Console.WriteLine($"Самая дорогая книга: ID: {mostExpensiveBook.Id}, Название: {mostExpensiveBook.Title}, Автор: {mostExpensiveBook.Author}, Жанр: {mostExpensiveBook.Genre}, Год: {mostExpensiveBook.Year}, Цена: {mostExpensiveBook.Price}");
                    Console.WriteLine();
                }
                if (leastExpensiveBook != null)
                {
                    Console.WriteLine($"Самая дешевая книга: ID: {leastExpensiveBook.Id}, Название: {leastExpensiveBook.Title}, Автор: {leastExpensiveBook.Author}, Жанр: {leastExpensiveBook.Genre}, Год: {leastExpensiveBook.Year}, Цена: {leastExpensiveBook.Price}");
                    Console.WriteLine();
                }
                Console.WriteLine();
            }

            void GroupByAuthor()
            {
                var groupedBooks = books.GroupBy(b => b.Author).Select(n => new { Author = n.Key, Count = n.Count() });
                foreach (var group in groupedBooks)
                {
                    Console.WriteLine($"Автор: {group.Author}, Количество книг: {group.Count}");
                    Console.WriteLine();
                }
                Console.WriteLine();
            }
            void AddMultipleBooks()
            {
                Console.WriteLine("Введите книги в формате: Название; Автор; Жанр; Год; Цена. Введите пустую строку для завершения.");
                while (true)
                {
                    string input = Console.ReadLine();
                    if (input == "")
                    {
                        break;
                    }
                    var parts = input.Split(';');
                    if (parts.Length != 5)
                    {
                        Console.WriteLine("Неверный формат. Попробуйте снова.");
                        continue;
                    }
                    string title = parts[0].Trim();
                    string author = parts[1].Trim();
                    if (!Enum.TryParse(parts[2].Trim(), out Genre genre) || !Enum.IsDefined(typeof(Genre), genre))
                    {
                        Console.WriteLine("Неверный жанр. Попробуйте снова.");
                        continue;
                    }
                    if (!int.TryParse(parts[3].Trim(), out int year) || year <= 0)
                    {
                        Console.WriteLine("Год издания должен быть положительным числом. Попробуйте снова.");
                        continue;
                    }
                    if (!decimal.TryParse(parts[4].Trim(), out decimal price) || price < 0)
                    {
                        Console.WriteLine("Цена книги не может быть отрицательной. Попробуйте снова.");
                        continue;
                    }
                    books.Add(new Book(title, author, genre, year, price));
                }
                Console.WriteLine("Книги добавлены.");
            }
            List<Book> cart = new List<Book>();
            void AddToCart()
            {
               Console.WriteLine("Введите идентификатор книги для добавления в корзину:");
                string input = Console.ReadLine();
                if (!int.TryParse(input, out int id))
                {
                    Console.WriteLine("Неверный идентификатор.");
                    return;
                }
                var bookToAdd = books.FirstOrDefault(b => b.Id == id);
                if (bookToAdd != null)
                {
                    cart.Add(bookToAdd);
                    Console.WriteLine("Книга добавлена в корзину.");
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine("Книга с таким идентификатором не найдена.");
                    Console.WriteLine();
                }
            }
            void ViewCart()
            {

                if (cart.Count == 0)
                {
                    Console.WriteLine("Корзина пуста.");
                    Console.WriteLine();
                    return;
                }
                decimal totalPrice = 0;
                foreach (var book in cart)
                {
                    Console.WriteLine($"ID: {book.Id}, Название: {book.Title}, Автор: {book.Author}, Жанр: {book.Genre}, Год: {book.Year}, Цена: {book.Price}");
                    totalPrice += book.Price;
                }
                Console.WriteLine($"Итоговая стоимость корзины: {totalPrice}");
                Console.WriteLine();
            }
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Выберите команду: 1 - Добавить книгу, 2 - Удалить книгу, 3 - Найти книги, 4 - Сортировать книги, 5 - Вывести самую дорогую и самую дешевую книгу, 6 - Сгруппировать книги по авторам, 7 - Добавить несколько книг, 8 - Добавить в корзину, 9 - Просмотреть корзину, 0 - Выход");
                string command = Console.ReadLine();
                switch (command)
                {
                    case "1":
                        Add();
                        break;
                    case "2":
                        Delete();
                        break;
                    case "3":
                        Search();
                        break;
                    case "4":
                        Sort();
                        break;
                    case "5":
                        Pricesminormax();
                        break;
                    case "6":
                        GroupByAuthor();
                        break;
                    case "7":
                        AddMultipleBooks();
                        break;
                    case "8":
                        AddToCart();
                        break;
                    case "9":
                        ViewCart();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Неверная команда.");
                        Console.WriteLine();
                        break;
                }
            }

//            1) Пакетный импорт.Добавьте в меню пункт "Вставить блок книг".В нём пользователь должен вставлять несколько строк, в каждой по одной книге, в следующем формате: Название; Автор; Жанр; Год; Цена.Введённые книги добавляются в общий список книг.

//2) Корзина.Добавьте возможность добавления книг в корзину для покупки. Должна быть возможность вывода итоговой стоимости корзины.
        }
    }
}
