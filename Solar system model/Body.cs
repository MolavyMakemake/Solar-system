namespace Solar_system_model
{
    public struct Vector3
    {
        public Vector3 (double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
        public double x, y, z;
    }

    public class Body
    {
        public string name = String.Empty;

        public Vector3 position;
        public Vector3 velocity;
        public float mass;
    }
}
