using System;
using UnityEngine;

namespace DataClasses
{
    public class ObservableValue<T>
    {
        public Action<T> onValueChanged;

        [field: SerializeField] public T value { get; protected set; }

        public void SetValue(T newValue)
        {
            value = newValue;
            onValueChanged?.Invoke(value);
        }
    }
}
