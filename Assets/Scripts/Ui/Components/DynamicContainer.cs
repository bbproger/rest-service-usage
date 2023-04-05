using System.Collections.Generic;
using UnityEngine;

namespace Ui.Components
{
    public interface IDynamicItem<in TData>
    {
        void SetData(TData data);
    }

    public class DynamicContainer<T, TData> where T : Object, IDynamicItem<TData>
    {
        private readonly T _prefab;
        private readonly Transform _container;

        private readonly List<T> _items = new();

        public DynamicContainer(T prefab, Transform container)
        {
            _prefab = prefab;
            _container = container;
        }

        public void Propagate(TData[] data)
        {
            if (data == null)
            {
                return;
            }

            ClearItems();
            foreach (TData datum in data)
            {
                T item = CreateItem(datum);
                _items.Add(item);
            }
        }

        public void ClearItems()
        {
            foreach (T item in _items)
            {
                Object.Destroy(item);
            }

            _items.Clear();
        }

        private T CreateItem(TData data)
        {
            T item = Object.Instantiate(_prefab, _container);
            item.SetData(data);
            return item;
        }
    }
}