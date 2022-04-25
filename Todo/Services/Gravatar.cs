using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Todo.Models.TodoItems;
using Todo.Models.Users;

namespace Todo.Services
{
    public static class Gravatar
    {
        public const string API_BASE_ADDRESS = "https://www.gravatar.com";

        public static string GetHash(string emailAddress)
        {
            using (var md5 = MD5.Create())
            {
                var inputBytes = Encoding.Default.GetBytes(emailAddress.Trim().ToLowerInvariant());
                var hashBytes = md5.ComputeHash(inputBytes);

                var builder = new StringBuilder();
                foreach (var b in hashBytes)
                {
                    builder.Append(b.ToString("X2"));
                }
                return builder.ToString().ToLowerInvariant();
            }
        }

        public static async Task<string> GetUserName(string emailAddress)
        {
            try
            {
                var hash = GetHash(emailAddress);
                if (string.IsNullOrWhiteSpace(hash))
                {
                    return null;
                }

                using var client = new HttpClient();
                client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/100.0.4896.127 Safari/537.36");
                var result = await client.GetAsync($"{API_BASE_ADDRESS}/{hash}.json");
                if (!result.IsSuccessStatusCode)
                {
                    return null;
                }

                var json = await result.Content.ReadAsStringAsync();
                var entry  = JsonConvert.DeserializeObject<GravatarEntry>(json);
                return entry?.Profiles?.FirstOrDefault()?.DisplayName;
            }
            catch
            {
                return null;
            }
        }
    }
}