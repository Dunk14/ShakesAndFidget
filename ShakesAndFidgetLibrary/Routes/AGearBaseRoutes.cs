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
    public abstract class AGearBaseRoutes : AConfigRoutes
    {
        public static async Task<List<GearBase>> GetAllGearBases()
        {
            Webservice webservice = new Webservice(BASE_URL);
            String methodRoute = "/gearBase/";
            List<GearBase> gearBases = new List<GearBase>();
            gearBases = await webservice.HttpClientCaller<List<GearBase>>(CHARACTER_URL + methodRoute, gearBases);
            return gearBases;
        }
    }
}
