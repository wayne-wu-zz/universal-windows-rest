using System;
using System.Collections.Generic;
using System.Runtime.Serialization;


namespace rest_tutorialClient.Models.Base
{

    [DataContract]
    public class LiqueurBase
    {
        public const string regex = "liqueur/";

        [DataMember]
        public string url {get; set;}

        [DataMember]
        public int pk {get; set;}

        [DataMember]
        public string name {get; set;}

        [DataMember]
        public string flavour {get; set;}

        [DataMember]
        public string origin {get; set;}

        [DataMember]
        public float abv {get; set;}

        [DataMember]
        public string special {get; set;}

        Connector snapshot = new Connector();

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

        [DataMember(Name = "liquor_base")]
        public string _liquor_base {get; set;}

        public Liquor liquor_base
        {
            get
            {
                if (_liquor_base != null)
                {
                    return snapshot.liquor(_liquor_base).Result;
                }
                else
                {
                    return new Liquor();
                }
            }
            set
            {
                _liquor_base = value.url;
            }
        }

        public async void save()
        {
            var new_data = new Liqueur();
            if (this.pk != 0 && this.url != null)
            {
                new_data = await snapshot.Save<Liqueur>(url, this);
            }
            else
            {
                new_data = await snapshot.Save<Liqueur>(regex, this, create : true);
            }
            var result = typeof(Liqueur).GetProperties();
            foreach (var prop in result)
            {
                var value = typeof(Liqueur).GetProperty(prop.Name).GetValue(new_data, null);
                typeof(Liqueur).GetProperty(prop.Name).SetValue(this, value);
            }
        }
    }
}
