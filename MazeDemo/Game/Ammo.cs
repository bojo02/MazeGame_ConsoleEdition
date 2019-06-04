namespace MazeDemo
{
    public class Ammo
    {
        public Location Location { get; set; }

        public int Side { get; set; }

        public bool HaveToShoot = true;

        public Ammo(Location loc, int side)
        {
            this.Location = loc;

            this.Side = side;
        }
    }
}
