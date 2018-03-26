using ShakesAndFidgetLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using WebserviceProject;
using ShakesAndFidgetLibrary.Models.Characters;

namespace ShakesAndFidgetLibrary.Routes
{
    public abstract class CharacterRoutes
    {
        public const string BASE_URL = "http://localhost:3000";
        public const string CHARACTER_URL = "/characters";

        public static async Task<int> CountByUserId(int userId)
        {
            Webservice webservice = new Webservice(BASE_URL);
            String methodRoute = "/countByUserId/";
            JObject jObject;
            jObject = await webservice.HttpClientCaller(CHARACTER_URL + methodRoute + userId);
            JToken value = jObject.First;
            return value.ToObject<int>();
        }

        public static async Task<Boolean> CreateWarrior(Warrior warrior)
        {
            Webservice webservice = new Webservice(BASE_URL);
            String methodRoute = "/warrior/";
            Boolean result = false;
            result = await webservice.HttpClientSender<Warrior>(CHARACTER_URL + methodRoute, warrior, result);
            return result;
        }

        public static async Task<Boolean> CreateHunter(Hunter hunter)
        {
            Webservice webservice = new Webservice(BASE_URL);
            String methodRoute = "/createHunter/";
            Boolean result = false;
            result = await webservice.HttpClientSender<Hunter>(CHARACTER_URL + methodRoute, hunter, result);
            return result;
        }

        public static async Task<Boolean> CreateMagus(Magus magus)
        {
            Webservice webservice = new Webservice(BASE_URL);
            String methodRoute = "/createMagus/";
            Boolean result = false;
            result = await webservice.HttpClientSender<Magus>(CHARACTER_URL + methodRoute, magus, result);
            return result;
        }
    }
}
