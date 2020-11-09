using System;
using System.Collections.Generic;

namespace HashTable
{
    class Program
    {
        static void Main(string[] args)
        {
            // Создаем новую хеш таблицу.
            var hashTable = new OpenHashTable();

            // Добавляем данные в хеш таблицу в виде пар Ключ-Значение.
            hashTable.Insert("Little Prince", "I never wished you any sort of harm; but you wanted me to tame you...");
            hashTable.Insert("Fox", "And now here is my secret, a very simple secret: It is only with the heart that one can see rightly; what is essential is invisible to the eye.");
            hashTable.Insert("Rose", "Well, I must endure the presence of two or three caterpillars if I wish to become acquainted with the butterflies.");
            hashTable.Insert("King", "He did not know how the world is simplified for kings. To them, all men are subjects.");

            // Выводим хранимые значения на экран.
            ShowCloseHashTable(hashTable, "Created hashtable.");
            Console.ReadLine();

            // Удаляем элемент из хеш таблицы по ключу
            // и выводим измененную таблицу на экран.
            hashTable.Delete("King");
            ShowCloseHashTable(hashTable, "Delete item from hashtable.");
            Console.ReadLine();

            // Получаем хранимое значение из таблицы по ключу.
            Console.WriteLine("Little Prince say:");
            var text = hashTable.Search("Little Prince");
            Console.WriteLine(text);
            Console.ReadLine();
        }

        /// <summary>
        /// Выводит на екран закрытую хеш таблицу
        /// </summary>
        /// <param name="hashTable"></param>
        /// <param name="tittle"></param>
        static void ShowOpenHashTable(CloseHashTable hashTable, string tittle)
        {
            Console.WriteLine(tittle);
            foreach (KeyValuePair<int, Item> keyValue in hashTable.Items)
            {
                Console.WriteLine($"Hash: {keyValue.Key}");
                Console.WriteLine($"Key: {keyValue.Value.Key} -- Value: {keyValue.Value.Value}");
            }
        }
        
        /// <summary>
        /// Выводит на екран открытую хеш таблицу
        /// </summary>
        /// <param name="hashTable"></param>
        /// <param name="tittle"></param>
        static void ShowCloseHashTable(OpenHashTable hashTable, string tittle)
        {
            Console.WriteLine(tittle);
            foreach(KeyValuePair<int, List<Item>> keyValue in hashTable.Items)
            {
                Console.WriteLine($"Hash: {keyValue.Key}");
                foreach(var item in keyValue.Value)
                {
                    Console.WriteLine($"Key: {item.Key} -- Value: {item.Value}");
                }
            }
        }
    }
}
