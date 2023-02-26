using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using WcfService.LoginDatabase;
using WcfService.LoginHash;

namespace WcfService.LoginBusiness
{
    public class UserRegisterService
    {
        UsersRepository loginRepository = new UsersRepository();
        public BaseResponse Register(Users user)
        {
            BaseResponse baseResponse = new BaseResponse();

            if (String.IsNullOrEmpty(user.password))
            {
                baseResponse.message = Constants.PASSWORD_IS_REQUIRED;
                baseResponse.isSuccess = false;
                return baseResponse;
            }

            if (!Utils.ValueMaxLenghtCheck(user.username, 25))
            {
                baseResponse.message = "username, 1-25 aralığında karakter uzunluğunda olmalıdır. ";
                baseResponse.isSuccess = false;
                return baseResponse;

            }
            else if (!Utils.ValueMaxLenghtCheck(user.email, 255))
            {
                baseResponse.message = "email, 1-25 aralığında karakter uzunluğunda olmalıdır. ";
                baseResponse.isSuccess = false;
                return baseResponse;
            }
            else if (!Utils.ValueMaxLenghtCheck(user.name, 50))
            {
                baseResponse.message = "name, 1-50 aralığında karakter uzunluğunda olmalıdır. ";
                baseResponse.isSuccess = false;
                return baseResponse;
            }
            else if (!Utils.ValueMaxLenghtCheck(user.surname, 50))
            {
                baseResponse.message = "surname, 1-50 aralığında karakter uzunluğunda olmalıdır. ";
                baseResponse.isSuccess = false;
                return baseResponse;
            }
            if (!Utils.EmailMathcer(user.email).Success)
            {
                baseResponse.message = Constants.EMAIL_FORMAT_IS_UNSUCCESS;
                return baseResponse;
            }
            else if (!Utils.StrongPasswordMatcher(user.password).Success)
            {
                baseResponse.message = Constants.PASSWORD_FORMAT;
                return baseResponse;
            }
            else if (loginRepository.CheckUserWithUsername(user.username))
            {
                baseResponse.message = String.Format("{0} kullanıcı adıyla kullanıcı vardır.Başka bir kullanıcı adı deneyiniz.", user.username);
                baseResponse.isSuccess = false;
                return baseResponse;
            }
            


            string hashedPassword = Utils.GetSHA256Hash(user.password);
            user.password = hashedPassword;

            int insertedCount = loginRepository.InsertUser(user);

            baseResponse.isSuccess = true;
            return new BaseResponse(Constants.REGISTER_IS_SUCCESS, true);

        }
    }
}