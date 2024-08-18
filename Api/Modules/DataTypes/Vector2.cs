namespace Synth.Api
{
    public class Vector2(int? x, int? y)
    {

        // properties
        public int X { get; set; } = x ?? 0;
        public int Y { get; set; } = y ?? 0;

        // methods
        public static Vector2 New(int? x, int? y)
        {
            return new Vector2(x, y);
        }
    }
}
