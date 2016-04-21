using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PortableRest;
using System.Net.Http;
using System.Net.Http.Headers;

namespace WebRequestTest
{
    public class Snippet
    {
        public int id { get; set; }
        public string title { get; set; }
        public string code { get; set; }
        public bool is_valid { get; set; }
        public string character { get; set; }
    }

    class Program
    {
        private const string webpath = "http://127.0.0.1:8000/";

        static void print(string word)
        {
            Console.WriteLine(word);
        }

        static void print(int word)
        {
            Console.WriteLine(word);
        }

        static void print(bool word)
        {
            Console.WriteLine(word);
        }

        static void Main(string[] args)
        {
            RestMethod().Wait();
        }

        static async Task RunAsync()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(webpath);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("api/snippets/");
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("TEST");
                    var result = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(result);
                    //JObject parsed = JObject.Parse(result);
                    /*foreach (var pair in parsed)
                    {
                        Console.WriteLine("{0}: {1}", pair.Key, pair.Value);
                    }
                    //Console.WriteLine(response); */
                    Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("Could not connect to the website");
                    Console.ReadLine();
                }
            }

        }


        //Using the RestMethod to retrieve data
        static async Task RestMethod()
        {
            var client = new RestClient{ BaseUrl = webpath };
            var request = new RestRequest("api/snippets/", HttpMethod.Get);
            List<Snippet> response;
            response = await client.ExecuteAsync<List<Snippet>>(request);
            int length = response.Count;
            for(int i = 0; i<length; i++)
            {
                print(response[i].id);
                print(response[i].title);
                print(response[i].code);
                print(response[i].is_valid);
                print(response[i].character);
            }          
            Console.ReadLine();
        }

    }
}
