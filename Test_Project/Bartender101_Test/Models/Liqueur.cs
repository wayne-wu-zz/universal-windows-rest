using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace AssetClient.Models
{
    //connections

    //Decode class
    [DataContract]
    public class LiqueurBase
    {
        public const string regex = "liqueur/";

        //fields
        [DataMember]
        public int pk { get; set; }

        [DataMember]
        public string url { get; set; }

        [DataMember]
        public string name { get; set; }

        [DataMember]
        public string flavour { get; set; }

        [DataMember]
        public float abv { get; set; }

        [DataMember]
        public string special { get; set; }

        [DataMember]
        public string origin { get; set; }

        Connector snapshot = new Connector();

        //TODO* Understand how the DataMember and DataContract work
        //*It seems that the deserializer cannot handle URL(foreignkeys) directly

        //ForeignKeys

        //ManyToMany
        [DataMember(Name = "cocktails", EmitDefaultValue = false)]
        public List<string> _cocktails { get; set; } //url (ManyToMany)
        
        public List<Cocktail> cocktails
        {
            get
            {
                List<Cocktail> list = new List<Cocktail>();
                if (_cocktails != null)
                {

                    foreach (string url in _cocktails)
                    {
                        list.Add(snapshot.Cocktail(url));
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
                    Console.WriteLine(item.url);
                }
                _cocktails = urls;
            }
        }

        //OnetoOne(ForeignKey)
        [DataMember(Name = "liquor_base", EmitDefaultValue = false)] //map liquor_base to liquor_base_url
        public string _liquor_base { get; set; } 

        public Liquor liquor_base
        {
            get
            {
                if (_liquor_base != null)
                {
                    return snapshot.Liquor(_liquor_base);
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

        public void save()
        {
            var new_data = new Liqueur();
            if (this.pk != 0 && this.url != null)
            {
                //Update
                new_data = snapshot.Save<Liqueur>(url, this);
            }
            else
            {
                //Create
                new_data = snapshot.Save<Liqueur>(regex, this, create: true);
            }
            var result = typeof(Liqueur).GetProperties();
            foreach (var prop in result)
            {
                var value = typeof(Liqueur).GetProperty(prop.Name).GetValue(new_data, null);
                typeof(Liqueur).GetProperty(prop.Name).SetValue(this, value);
            }
        }

    }

    //Class inheritance
    public class Liqueur: LiqueurBase
    {
        //pass
    }


}
