using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Data;
using WcfService.LoginBusiness;
using System.Threading.Tasks;
using log4net;

namespace WcfService
{


    public class LoginProjectService : ILoginProjectService
    {

        UserRegisterService registerService = new UserRegisterService();
        UserLoginService loginService = new UserLoginService();
        UserForgetPasswordService userForgetPasswordService = new UserForgetPasswordService();
        MembershipCancellationService userDisableService = new MembershipCancellationService();
        UserEmailUpdateService userEmailUpdateService = new UserEmailUpdateService();
        ILog log = log4net.LogManager.GetLogger(typeof(LoginProjectService));


        public BaseResponse Register(Users user)
        {
            BaseResponse baseResponse = new BaseResponse();


            try
            {
                log.Info(String.Format(Constants.USER_REGISTER_REQUEST_LOG, user.username, user.email));

                baseResponse = registerService.Register(user);

                if (!baseResponse.isSuccess)
                {
                    log.Warn(String.Format("username:{0}, email:{1} bilgileri ile user yaratma isteği başarısız:{2}", user.username, user.email, baseResponse.message));
                }
                else
                {
                    log.Info(String.Format("username:{0}, email:{1} bilgileri ile user yaratma isteği cevabı:{2}", user.username, user.email, baseResponse.message));
                }


                return baseResponse;

            }
            catch (Exception exception)
            {
                baseResponse.message = exception.Message;
                return baseResponse;

            }
        }

        public BaseResponse Login(string username, string password)
        {
            BaseResponse baseResponse = new BaseResponse();

            log.Info(String.Format("{0} kullanıcı login isteği geldi.", username));
            try
            {
                baseResponse = loginService.Login(username, password);
                if (!baseResponse.isSuccess)
                {
                    log.Warn(String.Format("{0} kullanıcı login isteği hata aldı: {1}", username, baseResponse.message));
                }
                else
                {
                    log.Info(String.Format("{0} kullanıcı login isteği cevabı: {1}", username, baseResponse.message));
                }

                return baseResponse;
            }
            catch (Exception exception)
            {
                log.Error(String.Format("{0} kullanıcı login isteği hata aldı: {1}", username, exception.Message));
                baseResponse.message = exception.Message;
                return baseResponse;

            }
        }

        public BaseResponse ForgetPassword(string username, string email)
        {
            log.Info(String.Format("{0} kullanıcı forget password isteği geldi.", username));
            BaseResponse baseResponse = new BaseResponse();
            try
            {
                baseResponse = userForgetPasswordService.ForgetPassword(username, email);

                if (baseResponse.isSuccess)
                {
                    log.Info(Constants.FORGET_PASSWORD_IS_SUCCESS);
                }
                else
                {
                    log.Error(Constants.FORGET_PASSWORD_IS_UNSUCCESS);
                }
                return baseResponse;

            }
            catch (Exception exception)
            {
                log.Error(String.Format("{0} kullanıcı ForgetPassword isteği hata aldı: {1}", username, exception.Message));
                baseResponse.message = exception.Message;
                return baseResponse;

            }
        }

        public BaseResponse UpdateEmail(string username, string email, string newEmail)
        {
            BaseResponse baseResponse = new BaseResponse();

            log.Info(String.Format("{0} kullanıcı tarafından, {1} mailinin update isteği geldi.", username, email));

            try
            {
                baseResponse = userEmailUpdateService.UpdateUserEmail(username, email, newEmail);
                if (baseResponse.isSuccess)
                {
                    log.Info(String.Format("{0} kullanıcının yeni mail adresi {1} olarak değiştirildi.", username, newEmail));
                }
                else
                {
                    log.Error(String.Format("{0} kullanıcının yeni mail adresi {1} olarak değiştirilirken hata alındı.", username, newEmail));
                }
                return baseResponse;

            }
            catch (Exception exception)
            {
                log.Error(String.Format("{0} kullanıcı update email isteği hata aldı: {1}", username, exception.Message));
                baseResponse.message = exception.Message;
                return baseResponse;

            }
        }

        public BaseResponse MembershipCancellation(string username, string password, string email)
        {
            log.Info(String.Format("{0} kullanıcı, hesabı disable moda almak için istek geldi.", username, email));
            BaseResponse baseResponse = new BaseResponse();

            try
            {
                baseResponse = userDisableService.DisableUser(username, password, email);

                if (baseResponse.isSuccess)
                {
                    log.Info(Constants.MEMBERSHIP_CANCELLATION_IS_SUCCESS);
                }
                else
                {
                    log.Error(Constants.MEMBERSHIP_CANCELLATION_IS_UNSUCCESS);
                }
                return baseResponse;
            }
            catch (Exception exception)
            {
                log.Error(String.Format("{0} kullanıcı update email isteği hata aldı: {1}", username, exception.Message));
                baseResponse.message = exception.Message;
                return baseResponse;
            }
        }
    }
}
