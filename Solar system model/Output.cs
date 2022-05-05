using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solar_system_model
{
    class Output
    {
        const int spacing = 30;
        static public void Print(Body[] bodies)
        {
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("\nposition: \nvelocity: ");
            for (int i = 0; i < bodies.Length; i++)
            {
                Body body = bodies[i];

                int x = i * spacing + 10;
                Console.SetCursorPosition(x, 0);
                Console.Write("| " + body.name);

                Console.SetCursorPosition(x, 1);
                Console.Write("| " + VectorToString(body.position));

                Console.SetCursorPosition(x, 2);
                Console.Write("| " + VectorToString(body.velocity));
            }
        }

        static public string VectorToString(Vector3 v) => string.Format("{0}, {1}, {2}", v.x, v.y, v.z);
    }
}
