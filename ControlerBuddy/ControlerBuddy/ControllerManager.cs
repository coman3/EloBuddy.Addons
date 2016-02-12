using SharpDX;
using SharpDX.XInput;

namespace ControlerBuddy
{
    public static class ControllerManager
    {
        public static Controller Controller { get; private set; }
        public static State ControlerState { get { return Controller.GetState(); } }
        public static Gamepad Gamepad { get { return ControlerState.Gamepad; } }

        public static float Divider { get; set; }
        public static Vector2 LeftStick { get {  return LeftStickNative / Divider; } }
        public static Vector2 LeftStickNative { get { return new Vector2(Gamepad.LeftThumbX, Gamepad.LeftThumbY); } }
        public static byte LeftTrigger { get { return Gamepad.LeftTrigger; } }

        public static Vector2 RightStick { get { return RightStickNative / Divider; } }
        public static Vector2 RightStickNative { get { return new Vector2(Gamepad.RightThumbX, Gamepad.RightThumbY); } }
        public static byte RightTrigger { get { return Gamepad.RightTrigger; } }
        
        public static GamepadButtonFlags Buttons { get { return Gamepad.Buttons; } }

        static ControllerManager()
        {
            Divider = 100;
        }
        public static bool TryGetControler(out Controller controler)
        {
            controler = new Controller(UserIndex.One);
            return controler.IsConnected;
        }

        public static void SetControler(Controller controler)
        {
            Controller = controler;
        }
    }
}