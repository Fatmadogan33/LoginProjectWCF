using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using WcfService.LoginDatabase;
using WcfService.LoginHash;

namespace WcfService.LoginBusiness
{
    public class UserLoginService
    {
        UsersRepository loginRepository = new UsersRepository();
        public BaseResponse Login(string username, string password)
        {
            BaseResponse baseResponse = new BaseResponse();

            if (String.IsNullOrEmpty(password))
            {
                baseResponse.message = Constants.PASSWORD_IS_REQUIRED;
                baseResponse.isSuccess = false;
                return baseResponse;
            }
            else if (String.IsNullOrEmpty(username))
            {
                baseResponse.message = Constants.USERNAME_IS_REQUIRED;
                baseResponse.isSuccess = false;
                return baseResponse;
            }

            Boolean isSuccessLogin = loginRepository.CheckUsernamePassword(username, Utils.GetSHA256Hash(password));

            if (isSuccessLogin)
            {
                baseResponse.message = Constants.LOGIN_IS_SUCCESS;
                baseResponse.isSuccess = true;
            }
            else
            {
                baseResponse.message = Constants.USERNAME_OR_PASSWORD_IS_UNSUCCESS;
                baseResponse.isSuccess = false;
            }

            return baseResponse;
        }
    }
}