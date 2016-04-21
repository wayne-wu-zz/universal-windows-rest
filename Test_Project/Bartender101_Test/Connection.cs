using System;
using System.Collections.Generic;

namespace AssetClient
{
    using Models;

    /// <summary>
    /// Establish connection between the app and the Assetclient
    /// TODO*: Return one instance of the Connector (Singleton)
    /// </summary>
    public class Connector
    {
        // One instance of the client only
        static AssetClient client = new AssetClient();

        #region Public Methods

        //Returns a list of Cocktail object
        public List<Cocktail> Cocktail(
            object name = null, 
            object garnish = null, 
            object ice = null, 
            object glassware = null, 
            object liqueur = null, 
            object liquor = null)
        { 
            //Generate the query_list
            //TODO* Create a generic query function. (GetParameters)
            Dictionary<string, object> query_list = new Dictionary<string, object>();
            if (name != null) { query_list.Add("name", name); }
            if (garnish != null) { query_list.Add("garnish", garnish); }
            if (ice != null) { query_list.Add("ice", ice); }
            if (glassware != null) { query_list.Add("glassware", glassware); }
            if (liqueur != null) { query_list.Add("liqueur", liqueur); }
            if (liquor != null) { query_list.Add("liquor", liquor); }
            return client.Get<List<Cocktail>>(Models.Cocktail.regex, query_list);
        }

        //Overload 1: Return single object  
        public Cocktail Cocktail(string url)
        {
            return client.Get<Cocktail>(url);
        }

        public List<Liquor> Liquor()
        {
            return client.Get<List<Liquor>>("liquor/");
        }

        public Liquor Liquor(string url)
        {
            return client.Get<Liquor>(url);
        }
        
        public List<Mocktail> Mocktail()
        {
            return client.Get<List<Mocktail>>("mocktail/");
        }

        public Mocktail Mocktail(string url)
        {
            return client.Get<Mocktail>(url);
        }

        public List<Liqueur> Liqueur()
        {
            return client.Get<List<Liqueur>>("liqueur/");
        }

        public Liqueur Liqueur(string url)
        {
            return client.Get<Liqueur>(url);
        }

        #endregion

        public T Save<T>(string url, object data, bool create = false) where T : class, new()
        {
            if (create)
            {
                Console.WriteLine("Creating....");
                var result = client.Post<T>(url, data);
                return result;
            }
            else
            {
                Console.WriteLine("Updating....");
                var result = client.Put<T>(url, data);
                return result;
            }
        }
    }
}
