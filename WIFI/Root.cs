using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIFI
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class GeoData
    {
        public List<double> coordinates { get; set; }
        public string type { get; set; }
    }

    public class Cells
    {
        public string Name { get; set; }
        public string AdmArea { get; set; }
        public string District { get; set; }
        public string Location { get; set; }
        public int NumberOfAccessPoints { get; set; }
        public string WiFiName { get; set; }
        public int CoverageArea { get; set; }
        public string FunctionFlag { get; set; }
        public string AccessFlag { get; set; }
        public string Password { get; set; }
        public int global_id { get; set; }
        public GeoData geoData { get; set; }
    }

    public class Root
    {
        public int global_id { get; set; }
        public int Number { get; set; }
        public Cells Cells { get; set; }
    }



}
