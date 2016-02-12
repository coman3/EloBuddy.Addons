using System.Collections.Generic;

namespace ControlerBuddy.Menu
{
    public class ButtonCollection<T> : Dictionary<ControllerButton, T>
    {
        public ButtonCollection() : base(4)
        {
            
        }

        public void AddAll(T itemA, T itemB, T itemX, T itemY)
        {
            Add(ControllerButton.A, itemA);
            Add(ControllerButton.B, itemB);
            Add(ControllerButton.X, itemX);
            Add(ControllerButton.Y, itemY);
        }
        public bool TryAdd(ControllerButton button, T item)
        {
            if (ContainsKey(button)) return false;
            Add(button, item);
            return true;
        }
        public bool TryGet(ControllerButton button, out T item)
        {
            item = default(T);
            if (ContainsKey(button)) item = this[button];
            return ContainsKey(button);
        }

        public T Get(ControllerButton button)
        {
            return this[button];
        }
    }
}