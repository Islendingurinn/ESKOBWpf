﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ESKOBWpf.Viewmodels
{
    class API
    {
        private static readonly Uri uri = new Uri("https://localhost:44377/");

        public static async Task<HttpResponseMessage> GET(string url)
        {
            using (var client = new HttpClient())
            {

                client.BaseAddress = uri;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                return client.GetAsync(url).Result;
            }
        }

        public static async Task<string> POST(string url, object obj)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = uri;
                var json = JsonConvert.SerializeObject(obj);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var result = await client.PostAsync(url, content);
                string resultContent = await result.Content.ReadAsStringAsync();
            }

            return "error";
        }
    }
}
