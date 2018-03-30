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
        public static async Task<int> CreateGear(Gear gear, Stats stats)
        {
            Webservice webservice = new Webservice(BASE_URL);
            String methodRoute = "/";
            JObject jObject = new JObject();
            jObject  = await webservice.HttpClientSenderJObject(CHARACTER_URL + methodRoute, new List<Object>() { gear, stats });
            JToken value = jObject.First;
            return value.ToObject<int>();
        }
    }
}
