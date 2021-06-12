using Bitfit.Models;
using Bitfit.Pages;
using Fitbit.Api.Portable;
using Fitbit.Api.Portable.OAuth2;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Bitfit
{
    public class FitbitFunctions
    {
        // Retrieves an accestoken
        public static async Task<OAuth2AccessToken> ExchangeAuthCodeForAccessTokenAsync(string code)
        {
            HttpClient httpClient = new HttpClient();

            string postUrl = OAuth2Helper.FitbitOauthPostUrl;

            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("grant_type", "authorization_code"),
                new KeyValuePair<string, string>("client_id", "23B29X"),
                new KeyValuePair<string, string>("code", code),
                new KeyValuePair<string, string>("redirect_uri", "https://localhost:44319/")
            });

            string clientIdConcatSecret = OAuth2Helper.Base64Encode("23B29X" + ":" + "4cc4197e96410a6d7007a1a63a4e3477");
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", clientIdConcatSecret);
            HttpResponseMessage response = await httpClient.PostAsync(postUrl, content);
            string responseString = await response.Content.ReadAsStringAsync();
            OAuth2AccessToken accessToken = ParseAccessTokenResponse(responseString);
            Debug.WriteLine(accessToken.Token + "\n" + accessToken.RefreshToken);
            return accessToken;
        }
        // Refreshes the accestoken
        public static async Task<OAuth2AccessToken> RefreshTokenAsync(FitbitClient client, List<FitbitCreds> AllFitbitCreds)
        {
            string postUrl = OAuth2Helper.FitbitOauthPostUrl;

            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("grant_type", "refresh_token"),
                new KeyValuePair<string, string>("refresh_token", AllFitbitCreds[0].RefreshToken),
            });

            HttpClient httpClient;
            if (client.HttpClient == null)
            {
                httpClient = new HttpClient();
            }
            else
            {
                httpClient = client.HttpClient;
            }

            var clientIdConcatSecret = OAuth2Helper.Base64Encode(client.AppCredentials.ClientId + ":" + client.AppCredentials.ClientSecret);
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", clientIdConcatSecret);

            HttpResponseMessage response = await httpClient.PostAsync(postUrl, content);
            string responseString = await response.Content.ReadAsStringAsync();
            Debug.WriteLine(responseString);
            return ParseAccessTokenResponse(responseString);
        }
        // Parses data from json into the OAuth2AccessToken format
        public static OAuth2AccessToken ParseAccessTokenResponse(string responseString)
        {
            // assumption is the errors json will return in usual format eg. errors array
            JObject responseObject = JObject.Parse(responseString);
            var error = responseObject["errors"];
            if (error != null)
            {
                var errors = new JsonDotNetSerializer().ParseErrors(responseString);
                throw new FitbitException($"Unable to parse token response in method -- {nameof(ParseAccessTokenResponse)}.", errors);
            }

            var deserializer = new JsonDotNetSerializer();
            return deserializer.Deserialize<OAuth2AccessToken>(responseString);
        }
        // Retrieves an authorization code -> Check the debugger for an url and use it to get the code inside of the new url
        // EX: The method print out https://www.google.com -> Go to this website -> When given acces the url changes to https://www.google.com/callback?code=7b64c4b088b9c841d15bcac15d4aa7433d35af3e#_=_
        // the code is in this case 7b64c4b088b9c841d15bcac15d4aa7433d35af3e
        public static void Authorize()
        {
            var appCredentials = new FitbitAppCredentials()
            {
                ClientId = "23B29X",
                ClientSecret = "4cc4197e96410a6d7007a1a63a4e3477"
            };
            var authenticator = new OAuth2Helper(appCredentials, "https://localhost:44319/");
            string[] scopes = new string[] { "activity", "heartrate", "location", "nutrition", "profile", "settings", "sleep", "social", "weight" };

            string authUrl = authenticator.GenerateAuthUrl(scopes, null);
            Debug.WriteLine($"\n------------------{authUrl}-----------------\n");
        }
    }
}
