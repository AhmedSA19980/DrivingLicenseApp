using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLSDATA
{
    public class clsDataAccessLayerSetting
    {
        static string connectionString = Environment.GetEnvironmentVariable("DBconnectionDrivingLicense");
         public static string Addresss = connectionString;
    }
}
