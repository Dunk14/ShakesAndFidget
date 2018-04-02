using Newtonsoft.Json.Linq;
using ShakesAndFidgetLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebserviceProject;

namespace ShakesAndFidgetLibrary.Routes
{
    public abstract class AGearBaseRoutes : AConfig
    {
        public static async Task<List<GearBase>> GetAllGearBases()
        {
            Webservice webservice = new Webservice(BASE_URL);
            String methodRoute = "/";
            List<GearBase> gearBases = new List<GearBase>();
            gearBases = await webservice.HttpClientCaller<List<GearBase>>(GEAR_BASE_URL + methodRoute, gearBases);
            return gearBases;
        }
    }
}
