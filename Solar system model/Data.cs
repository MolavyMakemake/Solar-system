using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Text.RegularExpressions;

namespace Solar_system_model
{
    public class State
    {
        public double time;
        public string date;

        public Vector3 position;
        public Vector3 velocity;
    }

    public class Data
    {
        static string majorBodies = "";

        public string mass;
        public string name;
        public State[] states;

        public Data(string name)
        {
            if (majorBodies == "")
            {
                majorBodies = GetEPHEM("mb");
            }

            this.name = name;
            string index = Regex.Match(majorBodies, @$"\d*(?=  {name}  )").Value;
            if (index != null)
            {
                string ephem = GetEPHEM(index);

                GetMass(ephem);

                string vectors = Regex.Match(ephem, @"(?<=\$\$SOE\n)(.|\n)*(?=\n\$\$EOE)").Value;
            }
        }

        void GetMass(string ephem)
        {
            string line = Regex.Match(ephem, "(?<= Mass(, | x)).*").Value;
            string exponent = Regex.Match(line, @"(?<=\^)\d* ").Value;
            string coefficient = Regex.Match(line, @"(?<=((= ~)|(=\s*)))\d*((\w|$)|(\.\d*))").Value;
            
            Console.WriteLine(name + "\nMass: " + coefficient + "E" + exponent + "\n");
        }

        string GetEPHEM(string i)
        {
            return Get($"https://ssd.jpl.nasa.gov/api/horizons.api?format=text&COMMAND={i}&MAKE_EPHEM=YES&EPHEM_TYPE=VECTORS&CENTER=500@0");
        }
        string Get(string uri)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using Stream stream = response.GetResponseStream();
            using StreamReader reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }
    }
}
