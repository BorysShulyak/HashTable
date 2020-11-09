using System;
using System.Collections.Generic;
using System.Linq;

namespace HashTable
{
    public class CloseHashTable
    {
        private Dictionary<int, Item> _items = null;
        private byte _maxSize = 255;

        /// <summary>
        /// Коллекция хранимых данных в хеш-таблице в виде пар Хеш-Значения.
        /// </summary>
        public IReadOnlyCollection<KeyValuePair<int, Item>> Items => _items?.ToList()?.AsReadOnly();

        public CloseHashTable()
        {
            _items = new Dictionary<int, Item>(_maxSize);
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

            while (_items.ContainsKey(hash) && hash < _maxSize)
            {
                hash++;
            }
            _items[hash] = item;
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
                while (_items[hash].Key != key && hash < _maxSize)
                {
                    hash++;
                }
                if (hash != _maxSize)
                {
                    _items.Remove(hash);
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
                while (_items[hash].Key != key && hash < _maxSize)
                {
                    hash++;
                }
                if (hash != _maxSize)
                {
                    return _items[hash].Value;
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
