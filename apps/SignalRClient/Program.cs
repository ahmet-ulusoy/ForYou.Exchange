using Microsoft.AspNetCore.SignalR.Client;
using System.Net.Http.Json;

namespace SignalRClient
{
    public class Program
    {
        private static HubConnection? connection;

        private static List<string> messages = [];

        public static async Task Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            var tokenAndResult = await GetCookiesAndToken();

            connection = new HubConnectionBuilder()
                .WithUrl("https://localhost:44325/signalr-hubs/chat", options =>
                {
                    //options.AccessTokenProvider = () => Task.FromResult(tokenAndResult!.TokenResult!.access_token);

                    //options.DefaultTransferFormat = Microsoft.AspNetCore.Connections.TransferFormat.Text;

                    //options.SkipNegotiation = true;

                    //options.Transports = Microsoft.AspNetCore.Http.Connections.HttpTransportType.WebSockets;

                    options.UseDefaultCredentials = false;

                    options.Headers["Authorization"] = "Bearer " + tokenAndResult!.TokenResult!.access_token;

                    //foreach (var cookie in tokenAndResult!.Cookie)
                    //{
                    //    options.Cookies.Add(new Cookie(cookie.Key, cookie.Value, null, "localhost"));
                    //}

                    //options.HttpMessageHandlerFactory = (message) =>
                    //{
                    //    if (message is HttpClientHandler clientHandler)
                    //    {
                    //        clientHandler.ServerCertificateCustomValidationCallback += (sender, certificate, chain, sslPolicyErrors) => { return true; };
                    //    }

                    //    return message;
                    //};

                })
                .Build();

            await connection.StartAsync();

            connection.Closed += async (error) =>
            {
                Console.WriteLine("Connection closed.");
                Console.WriteLine("Trying to reconnect.");
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await connection.StartAsync();
                Console.WriteLine("Connection opened.");
            };
        }

        private static async Task<TokenAndCookies> GetCookiesAndToken()
        {
            //var cookies = new CookieContainer();

            //var handler = new HttpClientHandler
            //{
            //    CookieContainer = cookies
            //};

            //var client = new HttpClient(handler);

            var client = new HttpClient();

            var collection = new List<KeyValuePair<string, string>>
            {
                new("client_id", "Web"),
                new("client_secret", "1q2w3e*"),
                new("scope", "openid address email phone profile roles offline_access AccountService IdentityService AdministrationService"),
                new("grant_type", "client_credentials"),
                new("username", "ahmet.ulusoy"),
                new("password", "Aq1Sw2De3++__--")
            };
            var content = new FormUrlEncodedContent(collection);

            var response = await client.PostAsync("https://localhost:44322/connect/token", content);
            response.EnsureSuccessStatusCode();

            //var uri = new Uri("https://localhost:44322");

            //IEnumerable<Cookie> responseCookies = cookies.GetCookies(uri).Cast<Cookie>();

            var tokenResult = await response.Content.ReadFromJsonAsync<TokenResult>();

            var tokenAndCookies = new TokenAndCookies
            {
                TokenResult = tokenResult
            };

            //foreach (Cookie cookie in responseCookies)
            //{
            //    tokenAndCookies.Cookie.Add(cookie.Name, cookie.Value);
            //}

            return tokenAndCookies;
        }

        private static async Task Connect()
        {
            if (connection == null)
            {
                return;
            }

            connection.On<string, string>("ReceiveMessage", (user, message) =>
            {
                var newMessage = $"{user}: {message}";
                messages.Add(newMessage);
            });

            try
            {
                await connection.StartAsync();
                messages.Add("Connection started");
            }
            catch (Exception ex)
            {
                messages.Add(ex.Message);
            }
        }
    }

    public class TokenAndCookies
    {
        public TokenResult? TokenResult { get; set; }

        public Dictionary<string, string> Cookie { get; set; } = new Dictionary<string, string>();
    }

    public class TokenResult
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public int expires_in { get; set; }
        public string id_token { get; set; }
    }
}
