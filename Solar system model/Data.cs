using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Text.RegularExpressions;

namespace Solar_system_model
{
    public class Data
    {
        public Data()
        {
            data[0] = GetData(1);
        }

        private string[] data = new[] { "" };

        string GetData(int i)
        {
            string get = Get($"https://ssd.jpl.nasa.gov/api/horizons.api?format=text&COMMAND={i}&MAKE_EPHEM=YES&EPHEM_TYPE=VECTORS&CENTER=500@0");
            string match = Regex.Match(get, @"(\$\$SOE)(.|\n)*(:?\$\$EOE)");
            Console.Write(Regex.Match(match, @"X = \d*"));
            return match.Substring(6, match.Length - 12);
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
