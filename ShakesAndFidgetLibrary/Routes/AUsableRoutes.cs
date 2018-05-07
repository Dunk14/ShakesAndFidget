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
    public abstract class AUsableRoutes : AConfig
    {
        // If success, return generated UsableId
        public static async Task<int> CreateUsable(Stats stats, int usableBaseId)
        {
            Webservice webservice = new Webservice(BASE_URL);
            String methodRoute = "/";
            JObject jObject = new JObject();
            jObject = await webservice.HttpClientSenderJObject(USABLE_URL + methodRoute + usableBaseId, stats);
            JToken value = jObject.First;
            int result = value.ToObject<int>();
            return result;
        }

        public static async Task<Usable> GetOne(int usableId)
        {
            Webservice webservice = new Webservice(BASE_URL);
            String methodRoute = "/";
            Usable usable = new Usable();
            usable = await webservice.HttpClientCaller<Usable>(USABLE_URL + methodRoute + usableId, usable);
            return usable;
        }
    }
}
