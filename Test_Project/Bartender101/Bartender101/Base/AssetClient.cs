using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Reflection;
using Newtonsoft.Json;
using PortableRest;

namespace rest_tutorialClient.Base
{
    /// <summary>
    /// Self defined client derived from RestClient to use, instead of RestClient
    /// Everything in this class should be as generic as possible
    /// </summary>
    public class AssetClient : RestClient
    {
        #region Properties

        //UserID and UserPW if needed
        public string UserID { get; private set; }
        public string UserPW { get; private set; }

        #endregion

        /// <summary>
        /// Create a new instance of the AssetClient
        /// </summary>
        public AssetClient()
        {
            //Add the UserID and UserPW parameter if needed
            BaseUrl = "http://127.0.0.1:8000/api/";
            //*TODO: Create a setting file to read the url from
        }

        //*TODO: Play around with JsonSerializerSettings

        /// <summary>
        /// The generic GET function. This function is used for GET
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url">The full url of the item, or just the part after the baseUrl</param>
        /// <returns></returns>
        public async Task<T> Get<T>(string url, Dictionary<string,object> query_list = null) where T : class, new()
        {
            var request = MakeRequest(url);
            if (query_list != null)
            {
                foreach (KeyValuePair<string, object> kvp in query_list)
                {
                    request.AddQueryString(kvp.Key, kvp.Value);
                }
            }
            var Task = await ExecuteRequestAsync<T>(request);
            return Task;
        }

        /// <summary>
        /// This function is used for PUT
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<T> Put<T>(string url, object data) where T : class, new()
        {
            var request = MakeRequest(url, HttpMethod.Put);
            request.AddParameter(data);
            var Task = await ExecuteRequestAsync<T>(request);
            return Task;
        }

        /// <summary>
        /// This function is used for POST
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<T> Post<T>(string url, object data) where T : class, new()
        {
            var request = MakeRequest(url, HttpMethod.Post);
            request.AddParameter(data);
            var Task = await ExecuteRequestAsync<T>(request);
            return Task;
        }


        #region private properties

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request"></param>
        /// <returns></returns>
        private async Task<T> ExecuteRequestAsync<T>(RestRequest request) where T : class, new()
        {
            var result = await SendAsync<T>(request);
            if (result.HttpResponseMessage != null && result.HttpResponseMessage.IsSuccessStatusCode)
            {
                return result.Content;
            }
            else
            {
                Console.WriteLine(result.HttpResponseMessage);
                return new T(); //return new instance of T, but empty
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="resourceUrl"></param>
        /// <returns></returns>
        private RestRequest MakeRequest(string resourceUrl)
        {
            //check if the url string has the BaseUrl in it, if so remove it
            if (resourceUrl.Contains(BaseUrl))
            {
                int index = resourceUrl.IndexOf(BaseUrl);
                resourceUrl = (index < 0)
                        ? resourceUrl
                        : resourceUrl.Remove(index, BaseUrl.Length);
            }
            var request = new RestRequest(resourceUrl) { ContentType = ContentTypes.Json };
            return request;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="resourceUrl"></param>
        /// <param name="Method"></param>
        /// <returns></returns>
        private RestRequest MakeRequest(string resourceUrl, HttpMethod Method)
        {
            if (resourceUrl.Contains(BaseUrl))
            {
                int index = resourceUrl.IndexOf(BaseUrl);
                resourceUrl = (index < 0)
                        ? resourceUrl
                        : resourceUrl.Remove(index, BaseUrl.Length);
            }
            var request = new RestRequest(resourceUrl, Method) { ContentType = ContentTypes.Json };
            return request;
        }

        #endregion

    }

}