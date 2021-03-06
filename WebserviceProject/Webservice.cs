﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace WebserviceProject
{
    public class Webservice
    {
        static void Main() {}

        private String baseSite = "";

        public Webservice(String baseSite)
        {
            this.baseSite = baseSite;
        }

        public JsonSerializerSettings IgnoreNullValue()
        {
            return new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            };
        }

        public async Task<TItem> HttpClientCaller<TItem>(String url, TItem item)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.baseSite);
                client.DefaultRequestHeaders
                .Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    String result = await response.Content.ReadAsStringAsync();
                    item = JsonConvert.DeserializeObject<TItem>(result);
                }
            }
            return item;
        }

        public async Task<JObject> HttpClientCaller(String url)
        {
            JObject item = new JObject();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.baseSite);
                client.DefaultRequestHeaders
                .Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    String result = await response.Content.ReadAsStringAsync();
                    item = JsonConvert.DeserializeObject<JObject>(result);
                }
            }
            return item;
        }

        public async Task<TItem> HttpClientSender<TItem>(String url, TItem item)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.baseSite);
                client.DefaultRequestHeaders
                .Accept
                .Add(new MediaTypeWithQualityHeaderValue(
                "application/json"));
                HttpResponseMessage response = await client.PostAsync(url,
                new StringContent(JsonConvert.SerializeObject(item),
                Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                {
                    String result = await response.Content.ReadAsStringAsync();
                    item = JsonConvert.DeserializeObject<TItem>(result);
                }
            }
            return item;
        }

        public async Task<JObject> HttpClientSenderJObject(String url, Object item)
        {
            JObject jObject = new JObject();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.baseSite);
                client.DefaultRequestHeaders
                .Accept
                .Add(new MediaTypeWithQualityHeaderValue(
                "application/json"));
                HttpResponseMessage response = await client.PostAsync(url,
                new StringContent(JsonConvert.SerializeObject(item, Formatting.None, IgnoreNullValue()),
                Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                {
                    String result = await response.Content.ReadAsStringAsync();
                    jObject = JsonConvert.DeserializeObject<JObject>(result);
                }
            }
            return jObject;
        }

        public async Task<TItem> HttpClientModifier<TItem>(String url, TItem item)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.baseSite);
                client.DefaultRequestHeaders
                .Accept
                .Add(new MediaTypeWithQualityHeaderValue(
                "application/json"));
                HttpResponseMessage response = await client.PutAsync(url,
                new StringContent(JsonConvert.SerializeObject(item),
                Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                {
                    String result = await response.Content.ReadAsStringAsync();
                    item = JsonConvert.DeserializeObject<TItem>(result);
                }
            }
            return item;
        }

        public async Task<JObject> HttpClientModifierJObject<TItem>(String url, TItem item)
        {
            JObject jObject = new JObject();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.baseSite);
                client.DefaultRequestHeaders
                .Accept
                .Add(new MediaTypeWithQualityHeaderValue(
                "application/json"));
                HttpResponseMessage response = await client.PutAsync(url,
                new StringContent(JsonConvert.SerializeObject(item),
                Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                {
                    String result = await response.Content.ReadAsStringAsync();
                    jObject = JsonConvert.DeserializeObject<JObject>(result);
                }
            }
            return jObject;
        }

        public async Task<TItem> HttpClientSuppressor<TItem>(String url, TItem item)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.baseSite);
                client.DefaultRequestHeaders
                .Accept
                .Add(new MediaTypeWithQualityHeaderValue(
                "application/json"));
                HttpResponseMessage response = await client.DeleteAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    String result = await response.Content.ReadAsStringAsync();
                    item = JsonConvert.DeserializeObject<TItem>(result);
                }
            }
            return item;
        }

        public async Task<JObject> HttpClientSuppressorJObject(String url)
        {
            JObject jObject = new JObject();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.baseSite);
                client.DefaultRequestHeaders
                .Accept
                .Add(new MediaTypeWithQualityHeaderValue(
                "application/json"));
                HttpResponseMessage response = await client.DeleteAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    String result = await response.Content.ReadAsStringAsync();
                    jObject = JsonConvert.DeserializeObject<JObject>(result);
                }
            }
            return jObject;
        }
    }
}
