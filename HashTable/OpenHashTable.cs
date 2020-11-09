using System;
using System.Collections.Generic;
using System.Linq;

namespace HashTable
{
    public class OpenHashTable
    {
        private Dictionary<int, List<Item>> _items = null;
        private byte _maxSize = 255;

        /// <summary>
        /// Коллекция хранимых данных в хеш-таблице в виде пар Хеш-Значения.
        /// </summary>
        public IReadOnlyCollection<KeyValuePair<int, List<Item>>> Items => _items?.ToList()?.AsReadOnly();

        public OpenHashTable()
        {
            _items = new Dictionary<int, List<Item>>(_maxSize);
        }

        public void Insert(string key, string value)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key));
            }
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException(nameof(value));
            }

            var item = new Item(key, value);
            var hash = GetHash(item.Key);
            List<Item> hashTableItem = null;

            if (_items.ContainsKey(hash))
            {
                hashTableItem = _items[hash];
                var oldElemWithKey = hashTableItem.SingleOrDefault(i => i.Key == item.Key);
                if (oldElemWithKey != null)
                {
                    throw new ArgumentException($"Хеш-таблица уже содержит элемент с ключом {key}");
                }
                _items[hash].Add(item);
            }
            else
            {
                hashTableItem = new List<Item>();
                hashTableItem.Add(item);
                _items.Add(hash, hashTableItem);
            }
        }

        public bool Delete(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            var hash = GetHash(key);

            if (_items.ContainsKey(hash))
            {
                List<Item> hashTableItem = _items[hash];

                var oldElemWithKey = hashTableItem.SingleOrDefault(i => i.Key == key);
                if (oldElemWithKey != null)
                {
                    hashTableItem.Remove(oldElemWithKey);
                    return true;
                }
            }
            return false;
        }

        public string Search(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            var hash = GetHash(key);

            if (_items.ContainsKey(hash))
            {
                List<Item> hashTableItem = _items[hash];
                var searchedElem = hashTableItem.SingleOrDefault(i => i.Key == key);
                if (searchedElem != null)
                {
                    return searchedElem.Value;
                }
            }

            return null;
        }

        private int GetHash(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key));
            }
            if (key.Length > _maxSize)
            {
                throw new ArgumentException($"Максимальная длинна ключа составляет {_maxSize} символов.", nameof(key));
            }
            var hash = key.Length;
            return hash;
        }
    }
}
