using System;
using System.Numerics;

// https://ssd.jpl.nasa.gov/api/horizons.api?format=text&COMMAND=3&MAKE_EPHEM=YES&EPHEM_TYPE=VECTORS&CENTER=500@0
// https://ssd.jpl.nasa.gov/api/horizons.api?format=text&COMMAND=MB

namespace Solar_system_model
{
    class Program
    {
        static Data[] data;
        static Body[] bodies;

        static void Main()
        {
            Initialize();
        }

        static void Initialize()
        {
            data = new[]
            {
                new Data("Sun"),
                new Data("Mercury"),
                new Data("Venus"),
                new Data("Earth"),
                new Data("Mars"),
                new Data("Jupiter"),
                new Data("Saturn")
            };
        }
    }
}