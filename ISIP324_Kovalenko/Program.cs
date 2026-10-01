using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
            string[] words = inputText.Split(new char[] { ' ', '\n', '\r', '\t' }, StringSplitOptions.RemoveEmptyEntries);

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
            List<string> historystatistics = new List<string>();
            

            string GenerateReport(string text, string[] wordsss, List<string> history)
            {
                int wordCount = words.Length;
                int sentenceCount = 0;
                int vowelsCount = 0;
                int consonantsCount = 0;
                string shortestWord = words[0];
                string longestWord = words[0];
                Dictionary<char, int> frequency = new Dictionary<char, int>();
                foreach (char c in text)
                {
                    if (c == '.' || c == '!' || c == '?')
                    {
                        sentenceCount++;
                    }
                    if ("аеёиоуыэюя".Contains(c))
                    {
                        vowelsCount++;
                    }
                    else if (char.IsLetter(c))
                    {
                        consonantsCount++;
                    }
                }
                foreach (string word in words)
                {
                    if (word.Length < shortestWord.Length)
                    {
                        shortestWord = word;
                    }
                    if (word.Length > longestWord.Length)
                    {
                        longestWord = word;
                    }
                }
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
                string report = $"Статистика по тексту:\n" +
                                $"Количество слов: {wordCount}\n" +
                                $"Количество предложений: {sentenceCount}\n" +
                                $"Количество гласных букв: {vowelsCount}\n" +
                                $"Количество согласных букв: {consonantsCount}\n" +
                                $"Самое короткое слово: {shortestWord}\n" +
                                $"Самое длинное слово: {longestWord}\n";
                                
                history.Add(report);
                return report;

            }
            void ShowStatistics()
            {
                if (historystatistics.Count == 0)
                {
                    Console.WriteLine("Статистика по прошлым текстам отсутствует.");
                }
                else
                {
                    Console.WriteLine("Статистика по прошлым текстам:");
                    foreach (string report in historystatistics)
                    {
                        Console.WriteLine(report);
                    }
                }
            }

            //Возможность вывести статистику по прошлым текстам
            while (true)
            {   Console.WriteLine("1. Подсчёт количества слов в тексте");
                Console.WriteLine("2. Поиск самого короткого слова");
                Console.WriteLine("3. Подсчёт количества предложений");
                Console.WriteLine("4. Подсчёт количества гласных и согласных букв");
                Console.WriteLine("5. Поиск самого длинного слова");
                Console.WriteLine("6. Создание статистики по частоте встречаемости каждой буквы");
                Console.WriteLine("7. Сохранить отчёт");
                Console.WriteLine("8. Ввывести статистику по прошлым текстам");
                Console.WriteLine("9. Ввести новый текст");
                Console.WriteLine("10. Выход из программы");
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
                        CountSentences(inputText);
                        break;
                    case "4":
                        CountVowelsAndConsonants(inputText);
                        break;
                    case "5":
                        FindLongestWord(words);
                        break;
                    case "6":
                        LetterFrequency(inputText);
                        break;
                    case "7":
                        GenerateReport(inputText,words, historystatistics);
                        break;
                    case "8":
                        ShowStatistics();
                        break;
                    case "9":
                        ContinueWithNewText();
                        return;
                    case "10":
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
