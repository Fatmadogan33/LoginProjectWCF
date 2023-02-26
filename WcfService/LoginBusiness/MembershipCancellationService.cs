using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WcfService.LoginDatabase;
using WcfService.LoginHash;

namespace WcfService.LoginBusiness
{
    public class MembershipCancellationService
    {
        UsersRepository usersRepository = new UsersRepository();

        public BaseResponse DisableUser(string username, string password, string email)
        {
            if (string.IsNullOrEmpty(username))
            {
                return new BaseResponse(Constants.USERNAME_IS_REQUIRED, false);
            }
            else if (string.IsNullOrEmpty(password))
            {
                return new BaseResponse(Constants.PASSWORD_IS_REQUIRED, false);
            }
            else if (string.IsNullOrEmpty(email))
            {
                return new BaseResponse(Constants.EMAIL_IS_REQUIRED, false);
            }

            Users user = usersRepository.GetUserByNameAndEmail(username, email);

            if (user.username != null && user.password.Equals(Utils.GetSHA256Hash(password)))
            {
                user.status = Constants.DISABLED;
                int updatedCount = usersRepository.UpdateUserStatus(user);
                if (updatedCount > 0)
                {
                    return new BaseResponse(Constants.STATUS_UPDATE_IS_SUCCESS, true);
                }
                else
                {
                    return new BaseResponse(Constants.STATUS_UPDATE_IS_UNSUCCESS, false);
                }
            }
            else
            {
                return new BaseResponse(Constants.USERNAME_EMAIL_PASSWORD_IS_WRONG, false);
            }
        }
    }
}