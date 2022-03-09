using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace PSM
{
    class JSONAdapter
    {
        public Taetigkeiten ReadData(string filename)
        {
            StreamReader reader = File.OpenText(filename);
            JsonSerializer serializer = new JsonSerializer();
            Taetigkeiten taetigkeiten = (Taetigkeiten)serializer.Deserialize(reader, typeof(Taetigkeiten));
            reader.Close();
            return taetigkeiten;

        }
    }
}
