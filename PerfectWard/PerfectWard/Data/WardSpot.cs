using PerfectWard.Draw;
using SharpDX;

namespace PerfectWard.Data
{
    public class WardSpot
    {
        public Vector3 MagneticPosition { get; private set; }
        public RenderCircle MagneticCircle { get; set; }
        public Vector3 ClickPosition { get; private set; }

        public Vector3 WardPosition { get; private set; }
        public RenderCircle WardCircle { get; set; }
        public Vector3 MovePosition { get; private set; }

        public RenderLine ArrowLine { get; set; }

        public WardSpot(Vector3 magneticPosition)
        {
            MagneticPosition = magneticPosition;
        }

        public WardSpot(Vector3 magneticPosition, Vector3 clickPosition,
                            Vector3 wardPosition, Vector3 movePosition)
        {
            MagneticPosition = magneticPosition;
            ClickPosition = clickPosition;
            WardPosition = wardPosition;
            MovePosition = movePosition;
        }
    }
}