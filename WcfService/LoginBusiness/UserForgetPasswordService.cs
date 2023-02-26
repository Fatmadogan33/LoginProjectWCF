using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WcfService.LoginDatabase;
using WcfService.LoginHash;

namespace WcfService.LoginBusiness
{
    public class UserForgetPasswordService

    {
        ILog log = log4net.LogManager.GetLogger(typeof(UserForgetPasswordService));
        UsersRepository ForgetPasswordRepository = new UsersRepository();

        public BaseResponse ForgetPassword(string username, string email)
        {
            BaseResponse baseResponse = new BaseResponse();

            if (string.IsNullOrEmpty(username))
            {
                return new BaseResponse(Constants.USERNAME_IS_REQUIRED, false);
            }
            else if(string.IsNullOrEmpty(email))
            {
                return new BaseResponse(Constants.EMAIL_IS_REQUIRED, false);
            }

            Users user = ForgetPasswordRepository.GetUserByNameAndEmail(username, email);

            if (string.IsNullOrEmpty(user.username))
            {
                return new BaseResponse(Constants.USER_IS_NOTFOUND, false);
            }


            string oldPassword = user.password;
            string newPassword = Utils.NewRandomPassword();
            user.password = Utils.GetSHA256Hash(newPassword);
            int updatedCount = ForgetPasswordRepository.UpdateUserPassword(user);

            Boolean isEmailSendSuccess = Utils.sendEmail(String.Format("{0} isimli hesabınızın yeni parolası {1}", username, newPassword), email, "Yeni parola");
            if (!isEmailSendSuccess)
            {
                log.Error(String.Format(Constants.SEND_EMAIL_ERROR));

                user.password = oldPassword;
                ForgetPasswordRepository.UpdateUserPassword(user);

                return new BaseResponse(Constants.PASSWORD_RESET_REQUEST_UNSUCCESS, false);
            }

            return new BaseResponse(Constants.RANDOM_PASSWORD_IS_SUCCESS, true);
        }
    }
}