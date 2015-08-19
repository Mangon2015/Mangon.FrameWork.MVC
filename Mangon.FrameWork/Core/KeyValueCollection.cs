using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mangon.FrameWork.Core
{
    /// <summary>
    /// 字键集合
    /// </summary>
    /// <typeparam name="TKey">字类型</typeparam>
    /// <typeparam name="TValue">健类型</typeparam>
    public class KeyValueCollection<TKey,TValue>:ICollection<KeyValuePair<TKey,TValue>>
    {
        /// <summary>
        /// 存放集合用的字典
        /// </summary>
        internal List<KeyValuePair<TKey, TValue>> items;
        private TKey[] _keys = null;
        private TValue[] _values = null;

        public KeyValueCollection()
        {
            items = new List<KeyValuePair<TKey, TValue>>(16);
        }

        public KeyValueCollection(int capacity)
        {
            items = new List<KeyValuePair<TKey, TValue>>(capacity);
        }
        public KeyValueCollection(KeyValuePair<TKey, TValue>[] kvp)
        {
            items = new List<KeyValuePair<TKey, TValue>>(kvp);
        }
        public KeyValueCollection(KeyValueCollection<TKey, TValue> kvc)
        {
            items = new List<KeyValuePair<TKey, TValue>>();
            KeyValuePair<TKey, TValue>[] item = new KeyValuePair<TKey, TValue>[kvc.Count];
            kvc.CopyTo(item, 0);

        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            CleanCache();
            items.Add(item);
        }

        public void Clear()
        {
            CleanCache();
            items.Clear();
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return items.Contains(item);
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            items.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return items.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            bool rm = items.Remove(item);
            CleanCache();
            return rm;
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return items.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return items.GetEnumerator();
        }

        private void CleanCache()
        {
            if (_keys!=null)
            {
                _keys = null;
                _values = null;
            }
        }

        public TKey[] GetKeys()
        {
            TKey[] tk = new TKey[items.Count];
            for (int i = 0; i < items.Count; i++)
            {
                tk[i] = items[i].Key;
            }
            _keys = tk;
            return Keys;
        }

        public TValue[] GetValues()
        {
            TValue[] tv = new TValue[items.Count];
            for (int i = 0; i < items.Count; i++)
            {
                tv[i] = items[i].Value;
            }
            _values = tv;
            return Values;

        }


        public TKey[] Keys {
            get {
                if (_keys==null)
                {
                    GetKeys();
                }
                return _keys;
            }
        }
        public TValue[] Values
        {
            get {
                if (_values==null)
                {
                    GetValues();
                }
                return _values;
            }
        }

        /// <summary>
        /// 根据key返回key值所在的位置 -1 表示没有找到
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public int Find(TKey key)
        {
            int index = -1;
            for (int i = 0; i < Keys.Length; i++)
            {
                if (Keys[i].Equals(key))
                {
                    index = i;
                    break;
                }
            }
            return index;
        }

        public TValue GetValue(TKey key)
        {
            int index = Find(key);
            if (index<0)
            {
                throw new ArgumentOutOfRangeException();
            }
            return items[index].Value;
        }

        public KeyValuePair<TKey, TValue> this[TKey key]
        {
            get {
                int index = Find(key);
                if (index<0)
                {
                    throw new ArgumentOutOfRangeException(); 
                }
                return items[index];
            }
            set {
                int index = Find(key);
                if (index<0)
                {
                    throw new ArgumentOutOfRangeException();
                }
                items[index] = value;
            }
        }

        public KeyValuePair<TKey, TValue> this[int index]
        {
            get {
                if (index < 0 || index > items.Count)
                {
                    throw new ArgumentOutOfRangeException();
                }
                return items[index];
            }
            set {
                if (index<0||index>items.Count)
                {
                    throw new ArgumentOutOfRangeException();
                }
                items[index] = value;
            }
        }

        public KeyValuePair<TKey,TValue> FindByKey(TKey key)
        {
            int index=Find(key);
            if (index<0)
            {
                throw new ArgumentOutOfRangeException();
            }
            return items[index];
        }

    }
}
