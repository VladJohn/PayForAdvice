using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using WebAPI.FacebookIntegration.Models;

namespace WebAPI.FacebookIntegration.Service
{
    public class FacebookService
    {
        public const string ApiVersion = "2.10";

        //Luiza
        //private const string AppId = "295726730896928";
        //private const string AppSecret = "ecbcc5a8fe2b5d6c713aa4e4c87f28bc";

        //Vlad
        private const string AppId = "128919027720116";
        private const string AppSecret = "cc0e61fc3c34b4ebbc2bf573290aeb9c";

        //Alexandra
        //private const string AppId = "1900019420271415";
        //private const string AppSecret = "96efa33355a2e946bcccca7ae1ab6aca";
        private const string BaseUrl = "https://graph.facebook.com/";
        private const string VideoBaseUrl = "https://graph-video.facebook.com.";


        private const string AccessToken =
                "EAACEdEose0cBAB912DzIFA5zperUyCIbF70jCe3cyQrZB7ZATI0qeYm6zZB7DySN00cN9oUnnOjjmDNsTbOBwEKnQFqZAMgZBawagRLoRKG6pEnDVWVcSIfaszq4sl3spCxuId3mhFOOqQb9HgT1XBZCUA57pORvhaHp5d4grO8MyDhAOt2eJcgVxlkaNDlPYZD"
            ;

        private static readonly HttpClient HttpClient = new HttpClient();


        public async Task<UserFacebookModel> GetFbUserDetails()
        {
            var queryParams = new NameValueCollection { { "fields", "id,name,email,picture.width(300).height(300)" } };

            var requestUri = BuildRequestUri("me", queryParams);
            return await ExecuteGetRequest<UserFacebookModel>(requestUri);
        }

        public async Task<UserId> ShareAdviceGiven(string categoryJson)
        {
            var msg = "Someone gave me advice on the topic of " + categoryJson;
            var queryParams = new NameValueCollection { { "message", msg }, {"link","www.google.com" } };

            var requestUri = BuildRequestUri("me/feed", queryParams);
            var queryParamsJson = JsonConvert.SerializeObject(queryParams);
            var content = new StringContent(queryParamsJson);
            return await ExecutePostRequest<UserId>(requestUri, content);
        }

        public async Task<UserId> ShareRatingGiven(int rating)
        {
            var msg = "I've been rated " + rating.ToString() + " stars on Advicy!";
            var queryParams = new NameValueCollection { { "message", msg }, { "link", "www.google.com" } };

            var requestUri = BuildRequestUri("me/feed", queryParams);
            var queryParamsJson = JsonConvert.SerializeObject(queryParams);
            var content = new StringContent(queryParamsJson);
            return await ExecutePostRequest<UserId>(requestUri, content);
        }

        private Uri BuildRequestUri(string method, NameValueCollection queryStringParameters)
        {
            var baseUri = new Uri(BaseUrl);
            var uri = new Uri(baseUri, $"/v{ApiVersion}/{method}");
            var uriBuilder = new UriBuilder(uri);

            if (queryStringParameters == null)
                queryStringParameters = new NameValueCollection();
            queryStringParameters.Add("access_token", AccessToken);
            var queryStringCollection = HttpUtility.ParseQueryString(uriBuilder.Query);
            queryStringCollection.Add(queryStringParameters);
            uriBuilder.Query = queryStringCollection.ToString();

            return uriBuilder.Uri;
        }

        private async Task<T> ExecuteGetRequest<T>(Uri uri)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            using (var httpResponseMessage = await HttpClient.SendAsync(requestMessage))
            {
                if (httpResponseMessage.StatusCode != HttpStatusCode.OK)
                {
                    throw new Exception(httpResponseMessage.ReasonPhrase);
                }

                var content = await httpResponseMessage.Content.ReadAsStringAsync();
                if (string.IsNullOrEmpty(content))
                {
                    throw new Exception("Empty content");
                }
                return JsonConvert.DeserializeObject<T>(content);
            }
        }

        private async Task<T> ExecutePostRequest<T>(Uri uri, HttpContent httpContent)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, uri);
            //var content = new StringContent();
            requestMessage.Content = httpContent;
            using (var httpResponseMessage = await HttpClient.SendAsync(requestMessage))
            {
                if (httpResponseMessage.StatusCode != HttpStatusCode.OK)
                {
                    throw new Exception(httpResponseMessage.ReasonPhrase);
                }

                var content = await httpResponseMessage.Content.ReadAsStringAsync();
                if (string.IsNullOrEmpty(content))
                {
                    throw new Exception("Empty content");
                }
                return JsonConvert.DeserializeObject<T>(content);
            }
        }
    }
}