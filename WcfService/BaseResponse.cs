using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Text;
using System.Threading.Tasks;
using WcfService.LoginBusiness;

namespace WcfService
{
    [DataContract]
    public class BaseResponse
    {
        public BaseResponse()
        {
            this.message = Constants.ERROR;
            this.isSuccess = false;
        }

        public BaseResponse(string message, bool isSuccess)
        {
            this.message = message;
            this.isSuccess = isSuccess;
        }

        [DataMember]
        public string message { get; set; }

        [DataMember]
        public Boolean isSuccess { get; set; }
    }

    
}