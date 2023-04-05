using UnityEngine;

namespace Ui.Components
{
    public abstract class AbstractItem<TData> : MonoBehaviour,IDynamicItem<TData>
    {
        protected TData Data { get; private set; }

        public void SetData(TData data)
        {
            Data = data;
            UpdateUI();
        }

        protected abstract void UpdateUI();
    }
}