using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Text.RegularExpressions;
using System.Globalization;

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

        public double mass;
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
                string result = Regex.Match(ephem, @"(?<=\$\$SOE\n)(.|\n)*(?=\n\$\$EOE)").Value;

                GetMass(ephem);
                GetStates(result);

                Console.WriteLine(name + "\nMass: " + mass);
                return;
                for (int i = 0; i < states.Length; i++)
                {
                    
                    State state = states[i];
                    Console.WriteLine(state.time + " = A.D. " + state.date);
                }
            }
        }

        void GetMass(string ephem)
        {
            string line = Regex.Match(ephem, "(?<= Mass(, | x)).*").Value;
            string exponent = Regex.Match(line, @"(?<=\^)\d* ").Value;
            string coefficient = Regex.Match(line, @"(?<=((= ~)|(=\s*)))\d*((\w|$)|(\.\d*))").Value;

            mass = Double.Parse(coefficient + "E" + exponent, NumberStyles.Float, CultureInfo.InvariantCulture);
        }

        void GetStates(string result)
        {
            foreach (var v in Regex.Matches(result, ".*?TDB.*\n.*\n.*")) Console.WriteLine(v);
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