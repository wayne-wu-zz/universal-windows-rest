using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace rest_tutorialClient.Base
{
    using Models;

    public class ConnectorBase
    {

        static AssetClient client = new AssetClient();


        public async Task<List<User>> users(
            )
        {
            Dictionary<string, object> query_list = new Dictionary<string, object>();

            return await client.Get<List<User>>(User.regex, query_list);
        }

        public async Task<User> users(string url)
        {
            return await client.Get<User>(url);
        }

        public async Task<List<Liquor>> liquor(
            object name = null,
            object abv = null,
            object origin = null,
            object made_from = null,
            object liqueurs = null,
            object cocktails = null
            )
        {
            Dictionary<string, object> query_list = new Dictionary<string, object>();

            if (name != null) {query_list.Add("name", name);}
            if (abv != null) {query_list.Add("abv", abv);}
            if (origin != null) {query_list.Add("origin", origin);}
            if (made_from != null) {query_list.Add("made_from", made_from);}
            if (liqueurs != null) {query_list.Add("liqueurs", liqueurs);}
            if (cocktails != null) {query_list.Add("cocktails", cocktails);}
            return await client.Get<List<Liquor>>(Liquor.regex, query_list);
        }

        public async Task<Liquor> liquor(string url)
        {
            return await client.Get<Liquor>(url);
        }

        public async Task<List<Liqueur>> liqueur(
            object name = null,
            object liquor_base = null,
            object flavour = null,
            object origin = null,
            object abv = null,
            object special = null,
            object cocktails = null
            )
        {
            Dictionary<string, object> query_list = new Dictionary<string, object>();

            if (name != null) {query_list.Add("name", name);}
            if (liquor_base != null) {query_list.Add("liquor_base", liquor_base);}
            if (flavour != null) {query_list.Add("flavour", flavour);}
            if (origin != null) {query_list.Add("origin", origin);}
            if (abv != null) {query_list.Add("abv", abv);}
            if (special != null) {query_list.Add("special", special);}
            if (cocktails != null) {query_list.Add("cocktails", cocktails);}
            return await client.Get<List<Liqueur>>(Liqueur.regex, query_list);
        }

        public async Task<Liqueur> liqueur(string url)
        {
            return await client.Get<Liqueur>(url);
        }

        public async Task<List<Cocktail>> cocktail(
            object name = null,
            object garnish = null,
            object ice = null,
            object glassware = null,
            object liqueur = null,
            object liquor = null
            )
        {
            Dictionary<string, object> query_list = new Dictionary<string, object>();

            if (name != null) {query_list.Add("name", name);}
            if (garnish != null) {query_list.Add("garnish", garnish);}
            if (ice != null) {query_list.Add("ice", ice);}
            if (glassware != null) {query_list.Add("glassware", glassware);}
            if (liqueur != null) {query_list.Add("liqueur", liqueur);}
            if (liquor != null) {query_list.Add("liquor", liquor);}
            return await client.Get<List<Cocktail>>(Cocktail.regex, query_list);
        }

        public async Task<Cocktail> cocktail(string url)
        {
            return await client.Get<Cocktail>(url);
        }

        public async Task<List<Mocktail>> mocktail(
            object name = null,
            object garnish = null,
            object ice = null,
            object glassware = null,
            object modified = null
            )
        {
            Dictionary<string, object> query_list = new Dictionary<string, object>();

            if (name != null) {query_list.Add("name", name);}
            if (garnish != null) {query_list.Add("garnish", garnish);}
            if (ice != null) {query_list.Add("ice", ice);}
            if (glassware != null) {query_list.Add("glassware", glassware);}
            if (modified != null) {query_list.Add("modified", modified);}
            return await client.Get<List<Mocktail>>(Mocktail.regex, query_list);
        }

        public async Task<Mocktail> mocktail(string url)
        {
            return await client.Get<Mocktail>(url);
        }

        public async Task<T> Save<T>(string url, object data, bool create = false) where T: class, new()
        {
            if (create)
            {
                return await client.Post<T>(url, data);
            }
            else
            {
                return await client.Put<T>(url, data);
            }
        }
    }
}
