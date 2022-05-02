namespace Solar_system_model
{
    struct Vector3
    {
        public Vector3 (double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
        public double x, y, z;
    }

    struct Body
    {
        public string name;

        public Vector3 position;
        public Vector3 velocity;
        public float mass;
    }
}
