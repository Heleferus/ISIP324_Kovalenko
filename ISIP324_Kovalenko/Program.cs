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
//            Необходимо написать программу, которая будет принимать текст от пользователя и делать над ним определённые действия.

            //Функциональные требования:

            //Программа принимает от пользователя минимум 100 символов
            Console.WriteLine("Введите текст (не менее 100 символов):");
            string inputText = Console.ReadLine();
            while (inputText.Length < 100)
            {
                Console.WriteLine("Текст слишком короткий. Пожалуйста, введите текст снова");
                inputText = Console.ReadLine();
            }
            string[] words = inputText.Split(new char[] { ' ', '.', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);

            //Подсчёт количества слов в тексте
            void CountWords(string[] wwords)
            {
                Console.WriteLine($"Количество слов в тексте: {wwords.Length}");
            }

            //Поиск самого короткого слова
            void FindShortestWord(string[] woords)
            {
                string shortestWord = woords[0];
                foreach (string word in woords)
                {
                    if (word.Length < shortestWord.Length)
                    {
                        shortestWord = word;
                    }
                }
                Console.WriteLine($"Самое короткое слово: {shortestWord}");
            }

            //Подсчёт количества предложений
            void CountSentences(string text)
            {
                int sentenceCount = 0;
                foreach (char c in text)
                {
                    if (c == '.' || c == '!' || c == '?')
                    {
                        sentenceCount++;
                    }
                }
                Console.WriteLine($"Количество предложений в тексте: {sentenceCount}");
            }

            //Подсчёт количества гласных и согласных букв
            void CountVowelsAndConsonants(string text)
            {
                int vowelsCount = 0;
                int consonantsCount = 0;
                foreach (char c in text.ToLower())
                {
                    if ("аеёиоуыэюя".Contains(c))
                    {
                        vowelsCount++;
                    }
                    else if (char.IsLetter(c))
                    {
                        consonantsCount++;
                    }
                }
                Console.WriteLine($"Количество гласных букв: {vowelsCount}");
                Console.WriteLine($"Количество согласных букв: {consonantsCount}");
            }

            //Поиск самого длинного слова
            void FindLongestWord(string[] wordds)
            {
                string longestWord = wordds[0];
                foreach (string word in wordds)
                {
                    if (word.Length > longestWord.Length)
                    {
                        longestWord = word;
                    }
                }
                Console.WriteLine($"Самое длинное слово: {longestWord}");
            }

            //Создание статистики по частоте встречаемости каждой буквы
            void LetterFrequency(string text)
            {
                Dictionary<char, int> frequency = new Dictionary<char, int>();
                foreach (char c in text.ToLower())
                {
                    if (char.IsLetter(c))
                    {
                        if (frequency.ContainsKey(c))
                        {
                            frequency[c]++;
                        }
                        else
                        {
                            frequency[c] = 1;
                        }
                    }
                }
                Console.WriteLine("Частота встречаемости каждой буквы:");
                foreach (var pair in frequency)
                {
                    Console.WriteLine($"{pair.Key}: {pair.Value}");
                }
            }

            //Возможность продолжить работу с новым текстом
            void ContinueWithNewText()
            {
                Console.WriteLine("Хотите ввести новый текст? (да/нет)");
                string answer = Console.ReadLine().ToLower();
                if (answer == "да")
                {
                    Main(null);
                }
                else
                {
                    Console.WriteLine("Программа завершена.");
                }
            }

            //Сохранение всей статистики в список
            List<string> statistics = new List<string>();

            //Возможность вывести статистику по прошлым текстам
            void ShowStatistics()
            {
                Console.WriteLine("Статистика по прошлым текстам:");
                foreach (string stat in statistics)
                {
                    Console.WriteLine(stat);
                }
            }
            while (true)
            {   Console.WriteLine("1. Подсчёт количества слов в тексте");
                Console.WriteLine("2. Поиск самого короткого слова");
                Console.WriteLine("3. Подсчёт количества предложений");
                Console.WriteLine("4. Подсчёт количества гласных и согласных букв");
                Console.WriteLine("5. Поиск самого длинного слова");
                Console.WriteLine("6. Создание статистики по частоте встречаемости каждой буквы");
                Console.WriteLine("7. Вывести статистику по прошлым текстам");
                Console.WriteLine("8. Ввести новый текст");
                Console.WriteLine("9. Выход из программы");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        CountWords(words);
                        break;
                    case "2":
                        FindShortestWord(words);
                        break;
                    case "3":
                        CountSentences(string.Join(" ", words));
                        break;
                    case "4":
                        CountVowelsAndConsonants(string.Join(" ", words));
                        break;
                    case "5":
                        FindLongestWord(words);
                        break;
                    case "6":
                        LetterFrequency(string.Join(" ", words));
                        break;
                    case "7":
                        ShowStatistics();
                        break;
                    case "8":
                        ContinueWithNewText();
                        return;
                    case "9":
                        Console.WriteLine("Программа завершена.");
                        return;
                    default:
                        Console.WriteLine("Неверный выбор. Попробуйте снова.");
                        break;
                }

            }
            //Обязательно выполняйте задание без использования LINQ.


        }

         
    }
}
