using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace AssetClient.Models
{

    //Base Class
    [DataContract]
    public class LiquorBase
    {
        [DataMember]
        public int pk { get; set; }

        [DataMember]
        public string url { get; set; }

        [DataMember]
        public string name { get; set; }

        [DataMember]
        public int abv { get; set; }

        [DataMember]
        public string made_from { get; set; }

        [DataMember]
        public string origin { get; set; }

        [DataMember(Name = "cocktails", EmitDefaultValue = false)]
        public List<string> cocktails_url { get; set; }

        [DataMember(Name = "liqueur", EmitDefaultValue = false)]
        public List<string> liqueur_url { get; set;} //url
    }

    public class Liquor: LiquorBase
    {
        //pass
    }


}
