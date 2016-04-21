using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;


namespace AssetClient.Models
{
    // Decode Class
    [DataContract]
    public class Mocktail_base
    {
        
        public const string regex = "mocktail/";

        [DataMember]
        public int pk { get; set; }

        [DataMember]
        public string url { get; set; }

        [DataMember]
        public string name { get; set; }

        [DataMember]
        public string garnish { get; set; }

        [DataMember]
        public bool ice { get; set; }

        [DataMember]
        public string glassware { get; set; }

        public DateTime modified { get; set; } 

        Connector snapshot = new Connector();

        public void save()
        {
            var new_data = new Mocktail();
            if (this.pk != 0 && this.url != null )
            {
                //Update
                new_data = snapshot.Save<Mocktail>(url, this);
            }
            else
            {
                //Create
                new_data = snapshot.Save<Mocktail>(regex, this, create: true);
            }
            var result = typeof(Mocktail).GetProperties();
            foreach (var prop in result)
            {
                var value = typeof(Mocktail).GetProperty(prop.Name).GetValue(new_data, null);
                typeof(Mocktail).GetProperty(prop.Name).SetValue(this, value);
            }
        }
    }

    //Class Inheritence
    [DataContract]
    public class Mocktail : Mocktail_base
    {
        //pass
    }
}
