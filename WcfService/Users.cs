using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WcfService
{
    [DataContract]
    public class Users
    {
        [DataMember]
        public string username { get; set; }

        [DataMember]
        public string email { get; set; }

        [DataMember]
        public string name { get; set; }

        [DataMember]
        public string surname { get; set; }

        [DataMember]
        public string password { get; set; }
        
        [DataMember]
        public string status { get; set; }

    }

    
}