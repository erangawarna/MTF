using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RestSharpWithCSharp
{
    public class MTF
    {
        private CancellationToken cancellationToken;

        [Test]
        public async Task GETTest()
        {
            var options = new RestClientOptions("https://core-uat.mtf.co.nz")
            {
                Authenticator = new HttpBasicAuthenticator("username", "password")
            };
            var client = new RestClient(options);
            var request = new RestRequest("calculator/afford?repayment=100&productType=secured-loan");
            // The cancellation token comes from the caller. You can still make a call without it.
            var response = await client.GetAsync(request, cancellationToken);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                Console.WriteLine(response.Content);
            }
            // Deserialize the JSON content into a dynamic object
            dynamic result = JsonConvert.DeserializeObject(response.Content);

            // Access the value of the "name" key
            string keyValue = result.rawCost;

            Console.WriteLine($"Key: {keyValue}");

        }

        [Test]
        public async Task POSTTest()
        {
            var options = new RestClientOptions("https://api-dev.feedmyfurbaby.co.nz")
            {
                //Authenticator = new HttpBasicAuthenticator("username", "password")
            };
            var client = new RestClient(options);
            var request = new RestRequest("/api/login/");
            request.AddJsonBody(new { email = "hh@hh.hh", password = "qwert1234" }, ContentType.Json);
            // The cancellation token comes from the caller. You can still make a call without it.
            var response = await client.PostAsync(request, cancellationToken);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                Console.WriteLine(response.Content);
            }
            // Deserialize the JSON content into a dynamic object
            dynamic result = JsonConvert.DeserializeObject(response.Content);

            // Access the value of the "name" key
            string keyValue = result.key;

            Console.WriteLine($"Key: {keyValue}");

        }

        [Test]
        public async Task POSTTest2()
        {
            var options = new RestClientOptions("https://api-dev.feedmyfurbaby.co.nz")
            {
                //Authenticator = new HttpBasicAuthenticator("username", "password")
            };
            var client = new RestClient(options);
            var request = new RestRequest("/api/my-profile/");
            request.AddHeader("Authorization", "Token 60a7b4c3b1619a0d5e448d9de11dc7f4158ad4bd");
            // The cancellation token comes from the caller. You can still make a call without it.
            var response = await client.GetAsync(request, cancellationToken);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                Console.WriteLine(response.Content);
            }
            // Deserialize the JSON content into a dynamic object
            dynamic result = JsonConvert.DeserializeObject(response.Content);

            // Access the value of the "name" key
            string keyValue = result.id;

            Console.WriteLine($"Key: {keyValue}");

        }
    }
}
