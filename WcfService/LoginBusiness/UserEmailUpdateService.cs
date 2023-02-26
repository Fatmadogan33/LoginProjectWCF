using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WcfService.LoginDatabase;
using WcfService.LoginHash;

namespace WcfService.LoginBusiness
{
    public class UserEmailUpdateService
    {
        UsersRepository usersRepository = new UsersRepository();

        public BaseResponse UpdateUserEmail(string username, string email, string newEmail)
        {
            if (string.IsNullOrEmpty(username))
            {
                return new BaseResponse(Constants.USERNAME_IS_REQUIRED, false);
            }
            else if (string.IsNullOrEmpty(email))
            {
                return new BaseResponse(Constants.EMAIL_IS_REQUIRED, false);
            }
            else if(string.IsNullOrEmpty(newEmail))
            {
                return new BaseResponse(Constants.EMAIL_IS_REQUIRED, false);
            }

            else if (email == newEmail)
            {
                return new BaseResponse(String.Format("Eski email ile aynı email olamaz."), false);
            }

            Users user = usersRepository.GetUserByNameAndEmail(username, email);
            user.email = newEmail;


            if (string.IsNullOrEmpty(user.username))
            {
                return new BaseResponse(Constants.USER_IS_NOTFOUND, false);
            }

            if (!Utils.EmailMathcer(newEmail).Success)
            {
                return new BaseResponse(Constants.EMAIL_FORMAT_IS_UNSUCCESS, false);
            }

            int updatedCount = usersRepository.UpdateUserEmail(user);
            if (updatedCount > 0)
            {
                return new BaseResponse(Constants.EMAIL_UPDATE_IS_SUCCESS, true);
            }
            else
            {
                return new BaseResponse(Constants.EMAIL_UPDATE_IS_UNSUCCESS, false);
            }

        }
    }
}