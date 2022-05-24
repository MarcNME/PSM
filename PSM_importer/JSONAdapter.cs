using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using PSM_Libary.model;

namespace PSM
{
    public class JSONAdapter
    {
        PSM_Libary.DbAdapter dbAdapter = new PSM_Libary.DbAdapter("psm", "127.0.01", "psm", "psm");

        public void ReadData(string filename)
        {
            string jsonValue;
            StreamReader reader = new StreamReader(filename);
            jsonValue = reader.ReadToEnd();
            Activity activity = JsonConvert.DeserializeObject<Activity>(jsonValue);
            dbAdapter.AddActivity(activity);
            reader.Close();
        }
    }
}
