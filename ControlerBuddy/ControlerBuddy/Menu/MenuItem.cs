using SharpDX;

namespace ControlerBuddy.Menu
{
    public abstract class MenuItem
    {
        protected MenuGroup Parent { get; set; }
        public MenuGroup Child { get; set; }
        public ControllerButton SelectButton { get; set; }

        public abstract void Draw(RectangleF rectangle);
        public abstract bool Select();
        public virtual void Reset() { }

        protected MenuItem()
        {
        }



        public void SetParent(BasicMenuGroup parent)
        {
            Parent = parent;
        }
    }
}