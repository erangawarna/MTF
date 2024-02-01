using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading;
//using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Nodes;
using RestSharp.Authenticators;

namespace RestSharpWithCSharp
{
    public class Tests
    {
        private static dynamic resp;

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void VerifyLogin()
        {
            var client = new RestClient("https://api-dev.feedmyfurbaby.co.nz");
            var request = new RestRequest("/api/login", Method.Post);

            request.AddHeader("Content-Type", "application/json");
            //request.AddParameter("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            request.AddJsonBody(new { email = "hh@hh.hh", password = "qwert1234" });

            RestResponse response = client.Execute(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string content = response.Content;
                Console.WriteLine("Response Content:");
                Console.WriteLine(content);

                // Deserialize the JSON content into a dynamic object
                dynamic result = JsonConvert.DeserializeObject(content);

                // Access the value of the "name" key
                string keyValue = result.key;

                Console.WriteLine($"Key: {keyValue}");

            }
            else
            {
                // Handle error cases
                Console.WriteLine($"Error: {response.StatusCode}");
                string content = response.Content;
                Console.WriteLine("Response Content:");
                Console.WriteLine(content);
            }
        }

        [Test]
        public void ProfileAsync()
        {


            string url = "https://jsonplaceholder.typicode.com";

            using var client = new RestClient(url);
            var req = new RestRequest("todos/3");
            //var resp = await client.GetAsync(req);

            var data = System.Text.Json.JsonSerializer.Deserialize<JsonNode>(resp.Content!)!;

            Console.WriteLine(data["id"]);
            Console.WriteLine(data["title"]);
            Console.WriteLine(data["completed"]);
            Console.WriteLine(data);

        }


        [Test]
        public void ProfileAsyncMy()
        {


            string url = "https://api-dev.feedmyfurbaby.co.nz";

            using var client = new RestClient(url);
            client.AddDefaultHeader("Authorization", "Token 60a7b4c3b1619a0d5e448d9de11dc7f4158ad4bd");
            var req = new RestRequest("/api/my-profile");
            //req.AddHeader("Authorization", "Token 60a7b4c3b1619a0d5e448d9de11dc7f4158ad4bd");
            //var resp = await client.GetAsync(req);

            var data = System.Text.Json.JsonSerializer.Deserialize<JsonNode>(resp.Content!)!;

            Console.WriteLine(data["email"]);
            Console.WriteLine(data["first_name"]);
            Console.WriteLine(data);

        }

        [Test]
        public async void Test()
        {
            //var options = new RestClientOptions("https://api-dev.feedmyfurbaby.co.nz")
            {
                ////Authenticator = new HttpBasicAuthenticator("username", "password")
            };
            ////var client = new RestClient(options);
            var request = new RestRequest("/api/my-profile");
            // The cancellation token comes from the caller. You can still make a call without it.
            //var response = await client.GetAsync(request);
            //var data = System.Text.Json.JsonSerializer.Deserialize<JsonNode>(resp.Content!)!;

            ////Console.WriteLine(data["email"]);
            ////Console.WriteLine(data["first_name"]);
            ////Console.WriteLine(data);
        }

        [Test]
        public async System.Threading.Tasks.Task Test2Async()
        {
            var options = new RestClientOptions("https://api-dev.feedmyfurbaby.co.nz")
            {
                MaxTimeout = -1,
            };
            var client = new RestClient(options);
            var request = new RestRequest("/api/my-profile", Method.Get);
            request.AddHeader("Authorization", "Token 60a7b4c3b1619a0d5e448d9de11dc7f4158ad4bd");
            request.AddHeader("Cookie", "csrftoken=tuqrApVlWT40LM2hpXllIC6JAvaDwYEZkSsczHaYMmtTBWgcKDZQfdJ8r0DuJakc; sessionid=apd91ur3nqkb8ya3ji1ezf1n1dmpnkdy");
            RestResponse response = await client.ExecuteAsync(request);
            Console.WriteLine(response.Content);
        }

        [Test]
        public void VerifyRetrieveProfile()
        {
            var client = new RestClient("https://api-dev.feedmyfurbaby.co.nz");
            var request = new RestRequest("/api/my-profile", Method.Get);

            request.AddHeader("Authorization", "Token 60a7b4c3b1619a0d5e448d9de11dc7f4158ad4bd");
            //request.AddHeader("Cache-Control", "no-cache");
            //request.AddOrUpdateParameter("Authorization", "Token 60a7b4c3b1619a0d5e448d9de11dc7f4158ad4bd", ParameterType.HttpHeader);

            RestResponse response = client.Execute(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string content = response.Content;
                Console.WriteLine("Response Content:");
                Console.WriteLine(content);

                // Deserialize the JSON content into a dynamic object
                dynamic result = JsonConvert.DeserializeObject(content);

                // Access the value of the "name" key
                string emailValue = result.email;

                Console.WriteLine($"Email: {emailValue}");

            }
            else
            {
                // Handle error cases
                Console.WriteLine($"Error: {response.StatusCode}");
                string content = response.Content;
                Console.WriteLine("Response Content:");
                Console.WriteLine(content);
            }
        }

        [Test]
        public void VerifyRerieveCategories()
        {
            
            var client = new RestClient("https://api-dev.feedmyfurbaby.co.nz");
            var request = new RestRequest("/api/categories", Method.Get);
            //request.AddHeader("Authorization", "Bearer YOUR_ACCESS_TOKEN");
            //request.AddParameter("param_name", "param_value");

            RestResponse response = client.Execute(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string content = response.Content;
                Console.WriteLine("Response Content:");
                Console.WriteLine(content);

                // Deserialize the JSON content into a list of dynamic objects
                List<dynamic> resultList = JsonConvert.DeserializeObject<List<dynamic>>(content);

                foreach (var item in resultList)
                {
                    // Access the values of the "id" and "name" keys
                    int idValue = item.id;
                    string nameValue = item.name;

                    Console.WriteLine($"ID: {idValue}, Name: {nameValue}");
                }
            }
            else
            {
                // Handle error cases
                Console.WriteLine($"Error: {response.StatusCode}");
            }
        }
    }
}