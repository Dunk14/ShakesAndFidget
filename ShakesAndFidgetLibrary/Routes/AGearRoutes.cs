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
    public abstract class AGearRoutes : AConfigRoutes
    {
        // If success, return generated GearId
        public static async Task<int> CreateGear(Gear gear)
        {
            Webservice webservice = new Webservice(BASE_URL);
            String methodRoute = "/";
            gear  = await webservice.HttpClientSender<Gear>(CHARACTER_URL + methodRoute, gear);
            return gear.Id;
        }
    }
}
