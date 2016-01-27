using System;
using EloBuddy;
using EloBuddy.SDK;
using SharpDX;

namespace CameraBuddy.Camera
{
    public class CameraState : IDisposable
    {
        private CameraState _oldState;

        public float Zoom { get; set; }
        public Vector2 Position { get; set; }

        public Vector3 YawPitch { get; set; }
        public float YawX { get { return YawPitch.X; } set { YawPitch = new Vector3(value, YawY, Pitch); } }
        public float YawY { get { return YawPitch.Y; } set { YawPitch = new Vector3(YawX, value, Pitch); } }
        public float Pitch { get { return YawPitch.Z; } set { YawPitch = new Vector3(YawX, YawY, value); } }
        
        /// <summary>
        /// Creates an instance of <see cref="CameraState"/> with the with the specified values.
        /// </summary>
        public CameraState(Vector3 yawPitch, Vector2 screenPos, float zoomDistance) 
        {
            YawPitch = yawPitch;
            Position = screenPos;
            Zoom = zoomDistance;
        }

        /// <summary>
        /// Creates an instance of <see cref="CameraState"/> with the specified values; The values are not set until Set() is called.
        /// </summary>
        /// <param name="yaw"></param>
        /// <param name="pitch"></param>
        /// <param name="screenPos"></param>
        /// <param name="zoomDistance"></param>
        public CameraState(Vector2 yaw, float pitch, Vector2 screenPos, float zoomDistance) : this(new Vector3(yaw, pitch), screenPos, zoomDistance)
        {

        }
        /// <summary>
        /// Creates an instance of <see cref="CameraState"/> with the current <see cref="Camera"/>'s settings.
        /// </summary>
        public CameraState() : this(EloBuddy.Camera.Yaw, EloBuddy.Camera.Pitch, EloBuddy.Camera.ScreenPosition, EloBuddy.Camera.ZoomDistance)
        {

        }

        public void Set(float speed = -1)
        {
            _oldState = new CameraState();
            EloBuddy.Camera.Pitch = Pitch;
            EloBuddy.Camera.Yaw = YawPitch.To2D();
            EloBuddy.Camera.SetZoomDistance(Zoom);
            if(speed > 0)
                CameraMovement.MoveToSmooth(Priority.LowMedium, Position, speed);
            else
                EloBuddy.Camera.ScreenPosition = Position;
        }

        public void Clear()
        {
            _oldState.Set();
            _oldState.Dispose();
            _oldState = null;
        }

        public void Dispose()
        {
            if(_oldState != null) _oldState.Set();
        }
    }
}
