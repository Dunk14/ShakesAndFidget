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
        public static async Task<int> CreateGear(Gear gear, int gearBaseId)
        {
            Webservice webservice = new Webservice(BASE_URL);
            String methodRoute = "/";
            JObject jObject = new JObject();
            jObject = await webservice.HttpClientSenderJObject(GEAR_URL + methodRoute + gearBaseId, gear);
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

        public static async Task<Gear> PutInInventory(int gearId, int characterId)
        {
            Webservice webservice = new Webservice(BASE_URL);
            String methodRoute1 = "/";
            String methodRoute2 = "/character/";
            Gear gear = new Gear();
            gear = await webservice.HttpClientModifier<Gear>(GEAR_URL + methodRoute1 + gearId + methodRoute2 + characterId, gear);
            return gear;
        }

        public static async Task<int> DeleteOne(int gearId)
        {
            Webservice webservice = new Webservice(BASE_URL);
            String methodRoute = "/";
            JObject jObject = new JObject();
            jObject = await webservice.HttpClientSuppressorJObject(GEAR_URL + methodRoute + gearId);
            JToken value = jObject.First;
            return value.ToObject<int>();
        }

        public static async Task<Gear> DeleteFromInventory(int gearId)
        {
            Webservice webservice = new Webservice(BASE_URL);
            String methodRoute = "/inventory/";
            Gear gear = new Gear();
            gear = await webservice.HttpClientSuppressor<Gear>(GEAR_URL + methodRoute + gearId, gear);
            return gear;
        }
    }
}
