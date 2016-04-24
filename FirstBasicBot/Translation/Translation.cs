using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace FirstBasicBot.Translation
{
    public static class Translation
    {
        private const string LanguageToCode = "ja";
        private const string LanguageFromCode = "es";
           
        public static async Task<string> GetTranslation(string messageToTranslate)
        {
            var token = await GetAccessToken();

            var uri = string.Format("http://api.microsofttranslator.com/v2/Http.svc/Translate?text=" + System.Web.HttpUtility.UrlEncode(messageToTranslate) + "&from={0}" + "&to={1}", LanguageFromCode, LanguageToCode);

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await client.GetAsync(uri);
                var translatedStream = new StreamReader(await response.Content.ReadAsStreamAsync());

                System.Xml.XmlDocument xTranslation = new System.Xml.XmlDocument();

                xTranslation.LoadXml(translatedStream.ReadToEnd());

                return xTranslation.InnerText;
            }
        }
        private static async Task<string> GetAccessToken()
        {
            var clientID = "YOUR Client ID";
            var clientSecret = "YOUR ClIENT SECRET";

            var baseAddress = "https://datamarket.accesscontrol.windows.net/";

            var keyValues = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("grant_type", "client_credentials"),
                new KeyValuePair<string, string>("client_id", clientID),
                new KeyValuePair<string, string>("client_secret", clientSecret),
                new KeyValuePair<string, string>("scope", "http://api.microsofttranslator.com")
            };

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var request = new HttpRequestMessage(HttpMethod.Post, baseAddress + "v2/OAuth2-13")
                {
                    Content = new FormUrlEncodedContent(keyValues)
                };

                var response = await client.SendAsync(request);

                var content = await response.Content.ReadAsStringAsync();

                var result = JsonConvert.DeserializeObject<AdmAccesToken>(content);

                return result.AccessToken;
            }
        }
    }
}
