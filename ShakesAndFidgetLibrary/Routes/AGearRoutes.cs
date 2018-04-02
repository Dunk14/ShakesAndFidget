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
    public abstract class AGearRoutes : AConfig
    {
        // If success, return generated GearId
        public static async Task<int> CreateGear(Stats stats, int gearBaseId)
        {
            Webservice webservice = new Webservice(BASE_URL);
            String methodRoute = "/";
            JObject jObject = new JObject();
            jObject  = await webservice.HttpClientSenderJObject(GEAR_URL + methodRoute + gearBaseId, stats);
            JToken value = jObject.First;
            int result = value.ToObject<int>();
            return result;
        }

        public static async Task<Gear> GetOne(int gearId)
        {
            Webservice webservice = new Webservice(BASE_URL);
            String methodRoute = "/";
            Gear gear = new Gear();
            gear = await webservice.HttpClientCaller<Gear>(GEAR_URL + methodRoute + gearId, gear);
            return gear;
        }
    }
}
