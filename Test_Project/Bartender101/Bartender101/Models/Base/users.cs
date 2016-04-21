using System;
using System.Collections.Generic;
using System.Runtime.Serialization;


namespace rest_tutorialClient.Models.Base
{

    [DataContract]
    public class UserBase
    {
        public const string regex = "users/";

        [DataMember]
        public string url {get; set;}

        [DataMember]
        public int pk {get; set;}

        [DataMember]
        public bool is_superuser {get; set;}

        [DataMember]
        public string username {get; set;}

        [DataMember]
        public string email {get; set;}

        [DataMember]
        public bool is_staff {get; set;}

        Connector snapshot = new Connector();

        public async void save()
        {
            var new_data = new User();
            if (this.pk != 0 && this.url != null)
            {
                new_data = await snapshot.Save<User>(url, this);
            }
            else
            {
                new_data = await snapshot.Save<User>(regex, this, create : true);
            }
            var result = typeof(User).GetProperties();
            foreach (var prop in result)
            {
                var value = typeof(User).GetProperty(prop.Name).GetValue(new_data, null);
                typeof(User).GetProperty(prop.Name).SetValue(this, value);
            }
        }
    }
}
