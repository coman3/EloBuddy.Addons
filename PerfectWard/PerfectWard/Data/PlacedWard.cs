using EloBuddy;
using SharpDX;
using Color = System.Drawing.Color;

namespace PerfectWard.Data
{
    public class PlacedWard
    {
        public Vector3 Position { get; private set; }
        public float AliveTo { get; private set; }
        public int NetworkId { get; private set; }
        public Color Color { get; set; }
        public PlacedWard(Vector3 position, float aliveTo, int networkId, Color drawColor)
        {
            Position = position;
            AliveTo = aliveTo;
            NetworkId = networkId;
            Color = drawColor;
        }

        public override int GetHashCode()
        {
            return NetworkId;
        }

        public override bool Equals(object obj)
        {
            var gameObject = obj as GameObject;
            if (gameObject == null) return false;

            return gameObject.NetworkId == NetworkId;
        }
    }
}