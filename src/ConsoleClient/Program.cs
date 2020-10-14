using IdentityModel.Client;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ConsoleClient
{
    class Program
    {
        private static async Task Main()
        {
            Console.WriteLine("Hello World!");

            Console.WriteLine("Press any key to get token & call read-access API");
            Console.ReadKey();

            var accessToken = await GetTokenAsync("order.read invoice.read");  
            
            await CallApiAsync(accessToken, "read-access");

            Console.WriteLine("Press any key to call delete API");
            Console.ReadKey();

            await CallApiAsync(accessToken, "delete-access");

            Console.WriteLine("Press any key to get token & call delete access API");
            Console.ReadKey();

            accessToken = await GetTokenAsync("order.delete invoice.read");

            await CallApiAsync(accessToken, "delete-access");

            Console.WriteLine("Press any key to get token & call user access API");
            Console.ReadKey();

            await CallApiAsync(accessToken, "me");

            Console.WriteLine("Press any key stop");
            Console.ReadKey();
        }

        public static async Task<string> GetTokenAsync(string scopes)
        {
            Console.WriteLine($"Get token, scopes: {scopes}");

            // discover endpoints from metadata
            var client = new HttpClient();
            var disco = await client.GetDiscoveryDocumentAsync("https://localhost:5001");
            if (disco.IsError)
            {
                Console.WriteLine(disco.Error);
                return null;
            }

            var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = disco.TokenEndpoint,

                ClientId = "consoleclient",
                ClientSecret = "secret1",
                Scope = scopes
            });

            if (tokenResponse.IsError)
            {
                Console.WriteLine($"Error: {tokenResponse.Error}");
                return null;
            }

            Console.WriteLine($"Token response: {tokenResponse.Json}");

            return tokenResponse.AccessToken;
        }

        public static async Task CallApiAsync(string accessToken, string endpoint)
        {
            Console.WriteLine($"Call API, endpoint: {endpoint}");

            // call api
            var apiClient = new HttpClient();
            apiClient.SetBearerToken(accessToken);

            var response = await apiClient.GetAsync("https://localhost:5011/identity/" + endpoint);
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Response code: {response.StatusCode}{Environment.NewLine}Error: {error}");
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(JArray.Parse(content));
            }
        }

        public static async Task GetTokenAndCallApiAsync()
        {
            // discover endpoints from metadata
            var client = new HttpClient();
            var disco = await client.GetDiscoveryDocumentAsync("https://localhost:5001");
            if (disco.IsError)
            {
                Console.WriteLine(disco.Error);
                return;
            }

            var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = disco.TokenEndpoint,

                ClientId = "consoleclient",
                ClientSecret = "secret1",
                Scope = "order.delete invoice.read"
            });

            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return;
            }

            Console.WriteLine(tokenResponse.Json);

            // call api
            var apiClient = new HttpClient();
            apiClient.SetBearerToken(tokenResponse.AccessToken);

            var response = await apiClient.GetAsync("https://localhost:5011/identity/delete");
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.StatusCode);
                var error = await response.Content.ReadAsStringAsync();
                Console.WriteLine(error);
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(JArray.Parse(content));
            }
        }

        public static async Task GetTokenAndCallNet4ApiAsync()
        {
            // discover endpoints from metadata
            var client = new HttpClient();
            var disco = await client.GetDiscoveryDocumentAsync("https://localhost:5001");
            if (disco.IsError)
            {
                Console.WriteLine(disco.Error);
                return;
            }

            var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = disco.TokenEndpoint,

                ClientId = "consoleclient",
                ClientSecret = "secret1",
                Scope = "order.read"
            });

            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return;
            }

            Console.WriteLine(tokenResponse.Json);

            // call api
            var apiClient = new HttpClient();
            apiClient.SetBearerToken(tokenResponse.AccessToken);

            var response = await apiClient.GetAsync("https://localhost:5014/identity");
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.StatusCode);
                Console.WriteLine(await response.Content.ReadAsStringAsync());

                Console.WriteLine("Calling Verify");
                response = await apiClient.GetAsync("https://localhost:5014/verify");
                Console.WriteLine(response.StatusCode);
                Console.WriteLine(await response.Content.ReadAsStringAsync());
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(JArray.Parse(content));
            }
        }
    }
}
