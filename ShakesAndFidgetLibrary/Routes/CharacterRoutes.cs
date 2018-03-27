using ShakesAndFidgetLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using WebserviceProject;
using ShakesAndFidgetLibrary.Models.Characters;
using Newtonsoft.Json;
using System.Net.Http;

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

        // If success, return generated CharacterId
        public static async Task<int> CreateCharacter(Character character, int userId)
        {
            Webservice webservice = new Webservice(BASE_URL);
            String methodRoute = "/";
            JObject jObject = new JObject();

            if (character is Warrior)
                jObject = await webservice.HttpClientSenderJObject(CHARACTER_URL + methodRoute + userId, character as Warrior);
            else if (character is Hunter)
                jObject = await webservice.HttpClientSenderJObject(CHARACTER_URL + methodRoute + userId, character as Hunter);
            else if (character is Magus)
                jObject = await webservice.HttpClientSenderJObject(CHARACTER_URL + methodRoute + userId, character as Magus);
            JToken value = jObject.First;
            return value.ToObject<int>();
        }

        public static async Task<Character> GetCharacter(int userId)
        {
            Webservice webservice = new Webservice(BASE_URL);
            String methodRoute = "/byUserId/";
            JObject jObject = new JObject();
            jObject = await webservice.HttpClientCaller(CHARACTER_URL + methodRoute + userId);
            JToken value = jObject["Type"];
            string stringObject = JsonConvert.SerializeObject(jObject);

            if (value.ToObject<String>() == "W")
            {
                Warrior warrior = JsonConvert.DeserializeObject<Warrior>(stringObject);
                Console.WriteLine(warrior);
                return warrior;
            }
            else if (value.ToObject<String>() == "H")
            {
                Hunter hunter = JsonConvert.DeserializeObject<Hunter>(stringObject);
                return hunter;
            }
            else
            {
                Magus magus = JsonConvert.DeserializeObject<Magus>(stringObject);
                return magus;
            }
        }
    }
}
