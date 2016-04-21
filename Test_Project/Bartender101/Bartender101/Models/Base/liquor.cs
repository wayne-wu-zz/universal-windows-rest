using System;
using System.Collections.Generic;
using System.Runtime.Serialization;


namespace rest_tutorialClient.Models.Base
{

    [DataContract]
    public class LiquorBase
    {
        public const string regex = "liquor/";

        [DataMember]
        public string url {get; set;}

        [DataMember]
        public int pk {get; set;}

        [DataMember]
        public string name {get; set;}

        [DataMember]
        public int abv {get; set;}

        [DataMember]
        public string origin {get; set;}

        [DataMember]
        public string made_from {get; set;}

        Connector snapshot = new Connector();

        [DataMember(Name = "liqueurs")]
        public List<string> _liqueurs {get; set;}

        public List<Liqueur> liqueurs
        {
            get
            {
                List<Liqueur> list = new List<Liqueur>();
                if (_liqueurs != null)
                {
                    foreach (string url in _liqueurs)
                    {
                        list.Add(snapshot.liqueur(url).Result);
                    }
                    return list;
                }
                else
                {
                    return list;
                }
            }
            set
            {
                List<string> urls = new List<string>();
                foreach (Liqueur item in value)
                {
                    urls.Add(item.url);
                }
                _liqueurs = urls;
            }
        }

        [DataMember(Name = "cocktails")]
        public List<string> _cocktails {get; set;}

        public List<Cocktail> cocktails
        {
            get
            {
                List<Cocktail> list = new List<Cocktail>();
                if (_cocktails != null)
                {
                    foreach (string url in _cocktails)
                    {
                        list.Add(snapshot.cocktail(url).Result);
                    }
                    return list;
                }
                else
                {
                    return list;
                }
            }
            set
            {
                List<string> urls = new List<string>();
                foreach (Cocktail item in value)
                {
                    urls.Add(item.url);
                }
                _cocktails = urls;
            }
        }

        public async void save()
        {
            var new_data = new Liquor();
            if (this.pk != 0 && this.url != null)
            {
                new_data = await snapshot.Save<Liquor>(url, this);
            }
            else
            {
                new_data = await snapshot.Save<Liquor>(regex, this, create : true);
            }
            var result = typeof(Liquor).GetProperties();
            foreach (var prop in result)
            {
                var value = typeof(Liquor).GetProperty(prop.Name).GetValue(new_data, null);
                typeof(Liquor).GetProperty(prop.Name).SetValue(this, value);
            }
        }
    }
}
