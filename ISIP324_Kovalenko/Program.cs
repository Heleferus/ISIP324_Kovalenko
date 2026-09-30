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
         Console.WriteLine("Введите текст (минимум 100 символов):");
         string[] words = Console.ReadLine().Split(new char[] { ' ', '\t', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            if (words.Length < 100)
         {
                Console.WriteLine("Текст должен содержать минимум 100 символов.");
                return;
         }


            //Программа принимает от пользователя минимум 100 символов

            //Подсчёт количества слов в тексте
            void CountWords(string[] worrds)
            {
                int wordCount = 0;
                foreach (string word in words)
                {
                    wordCount++;
                }
                Console.WriteLine($"Количество слов в тексте: {wordCount}");
            }

            //Поиск самого короткого слова
            void FindShortestWord(string[] worrrds)
            {
                string shortestWord = words[0];
                foreach (string word in words)
                {
                    if (word.Length < shortestWord.Length)
                    {
                        shortestWord = word;
                    }
                }
                Console.WriteLine($"Самое короткое слово: {shortestWord}");
            }

            //Подсчёт количества предложений
            void CountSentences(string ttext)
            {
                int sentenceCount = 0;
                foreach (char c in ttext)
                {
                    if (c == '.' || c == '!' || c == '?')
                    {
                        sentenceCount++;
                    }
                }
                Console.WriteLine($"Количество предложений в тексте: {sentenceCount}");
            }

            //Подсчёт количества гласных и согласных букв
            void CountVowelsAndConsonants(string texxxt)
            {
                int vowelCount = 0;
                int consonantCount = 0;
                foreach (char c in texxxt.ToLower())
                {
                    if ("аеиоуыэюя".Contains(c))
                    {
                        vowelCount++;
                    }
                    else if (char.IsLetter(c))
                    {
                        consonantCount++;
                    }
                }
                Console.WriteLine($"Количество гласных букв: {vowelCount}");
                Console.WriteLine($"Количество согласных букв: {consonantCount}");
            }

            //Поиск самого длинного слова
            void FindLongestWord(string[] wwords)
            {
                string longestWord = words[0];
                foreach (string word in words)
                {
                    if (word.Length > longestWord.Length)
                    {
                        longestWord = word;
                    }
                }
                Console.WriteLine($"Самое длинное слово: {longestWord}");
            }

            //Создание статистики по частоте встречаемости каждой буквы
            void LetterFrequency(string texxt)
            {
                Dictionary<char, int> letterCount = new Dictionary<char, int>();
                foreach (char c in texxt.ToLower())
                {
                    if (char.IsLetter(c))
                    {
                        if (letterCount.ContainsKey(c))
                        {
                            letterCount[c]++;
                        }
                        else
                        {
                            letterCount[c] = 1;
                        }
                    }
                }
                Console.WriteLine("Статистика по частоте встречаемости каждой буквы:");
                foreach (var pair in letterCount)
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

            //Возможность вывести статистику по прошлым текстам
            void ShowStatistics(List<string> statistics)
            {
                Console.WriteLine("Статистика по прошлым текстам:");
                foreach (string stat in statistics)
                {
                    Console.WriteLine(stat);
                }
            }
           while (true)
            {
                CountWords(words);
                FindShortestWord(words);
                CountSentences(string.Join(" ", words));
                CountVowelsAndConsonants(string.Join(" ", words));
                FindLongestWord(words);
                LetterFrequency(string.Join(" ", words));
                ContinueWithNewText();
            }
            //Обязательно выполняйте задание без использования LINQ.
        }   
    }
}
