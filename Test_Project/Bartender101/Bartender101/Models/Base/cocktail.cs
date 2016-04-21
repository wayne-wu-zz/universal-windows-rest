using System;
using System.Collections.Generic;
using System.Runtime.Serialization;


namespace rest_tutorialClient.Models.Base
{

    [DataContract]
    public class CocktailBase
    {
        public const string regex = "cocktail";

        [DataMember]
        public string url {get; set;}

        [DataMember]
        public int pk {get; set;}

        [DataMember]
        public string name {get; set;}

        [DataMember]
        public string garnish {get; set;}

        [DataMember]
        public bool ice {get; set;}

        [DataMember]
        public string glassware {get; set;}

        [DataMember]
        public int percentage {get; set;}

        Connector snapshot = new Connector();

        [DataMember(Name = "liqueur")]
        public List<string> _liqueur {get; set;}

        public List<Liqueur> liqueur
        {
            get
            {
                List<Liqueur> list = new List<Liqueur>();
                if (_liqueur != null)
                {
                    foreach (string url in _liqueur)
                    {
                        list.Add(snapshot.liqueur(url).Result); //*Might block it
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
                _liqueur = urls;
            }
        }

        [DataMember(Name = "liquor")]
        public List<string> _liquor {get; set;}

        public List<Liquor> liquor
        {
            get
            {
                List<Liquor> list = new List<Liquor>();
                if (_liquor != null)
                {
                    foreach (string url in _liquor)
                    {
                        list.Add(snapshot.liquor(url).Result);
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
                foreach (Liquor item in value)
                {
                    urls.Add(item.url);
                }
                _liquor = urls;
            }
        }

        public async void save()
        {
            var new_data = new Cocktail();
            if (this.pk != 0 && this.url != null)
            {
                new_data = await snapshot.Save<Cocktail>(url, this);
            }
            else
            {
                new_data = await snapshot.Save<Cocktail>(regex, this, create : true);
            }
            var result = typeof(Cocktail).GetProperties();
            foreach (var prop in result)
            {
                var value = typeof(Cocktail).GetProperty(prop.Name).GetValue(new_data, null);
                typeof(Cocktail).GetProperty(prop.Name).SetValue(this, value);
            }
        }
    }

    [DataContract]
    public class MocktailBase
    {
        public const string regex = "mocktail/";

        [DataMember]
        public string url {get; set;}

        [DataMember]
        public int pk {get; set;}

        [DataMember]
        public string name {get; set;}

        [DataMember]
        public string garnish {get; set;}

        [DataMember]
        public bool ice {get; set;}

        [DataMember]
        public string glassware {get; set;}

        [DataMember]
        public DateTime modified {get; set;}

        Connector snapshot = new Connector();

        public async void save()
        {
            var new_data = new Mocktail();
            if (this.pk != 0 && this.url != null)
            {
                new_data = await snapshot.Save<Mocktail>(url, this);
            }
            else
            {
                new_data = await snapshot.Save<Mocktail>(regex, this, create : true);
            }
            var result = typeof(Mocktail).GetProperties();
            foreach (var prop in result)
            {
                var value = typeof(Mocktail).GetProperty(prop.Name).GetValue(new_data, null);
                typeof(Mocktail).GetProperty(prop.Name).SetValue(this, value);
            }
        }
    }
}
