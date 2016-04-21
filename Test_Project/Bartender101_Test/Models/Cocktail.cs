using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace AssetClient.Models
{
    [DataContract]
    public class CocktailBase
    {
        public static string regex = "cocktails/";

        [DataMember]
        public string url { get; set; }

        [DataMember]
        public int pk { get; set; }

        [DataMember]
        public string name { get; set; }

        [DataMember]
        public string garnish { get; set; }

        [DataMember]
        public int percentage { get; set; }

        [DataMember]
        public List<string> liqueur { get; set; } //ManytoMany

        [DataMember]
        public List<string> liquor { get; set; } //ManytoMany
    }

    [DataContract]
    public class Cocktail: CocktailBase
    {
        //pass
    }


}
