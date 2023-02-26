using System;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using WcfService.LoginBusiness;
using log4net;


namespace WcfService.LoginHash
{
    public static class Utils
    {
        static ILog log = log4net.LogManager.GetLogger(typeof(Utils));
        public static string GetSHA256Hash(string text)
        {
            try
            {
                string password = text;
                using (SHA256 sha1Hash = SHA256.Create())
                {
                    byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                    byte[] hashBytes = sha1Hash.ComputeHash(passwordBytes);
                    string hash = BitConverter.ToString(hashBytes).Replace("-", String.Empty);
                    log.Info(String.Format(Constants.PASSWORD_HASHING_IS_SUCCESS));
                    return hash;
                }
            }
            catch (Exception ex)
            {
                log.Error(String.Format(Constants.PASSWORD_HASHING_IS_UNSUCCESS, ex.Message));
                throw ex;
            }

        }

        public static Match EmailMathcer(string email)
        {

            try
            {
                Regex emailregex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");

                Match match = emailregex.Match(email);
                log.Info(String.Format(Constants.EMAIL_MATHCER_IS_SUCCESS));
                return match;
            }
            catch (Exception ex)
            {
                log.Error(String.Format(Constants.EMAIL_MATHCER_IS_UNSUCCESS, ex.Message));
                throw ex;
            }

        }

        public static Match StrongPasswordMatcher(string password)
        {

            try
            {
                Regex passwordRegex = new Regex("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$");

                Match match = passwordRegex.Match(password);

                log.Info(String.Format(Constants.STRONG_PASSWORD_MATHCER_IS_SUCCESS));

                return match;
            }
            catch (Exception ex)
            {
                log.Error(String.Format(Constants.STRONG_PASSWORD_MATHCER_IS_UNSUCCESS, ex.Message));
                throw ex;
            }

        }

        public static String NewRandomPassword()
        {

            try
            {
                log.Info(String.Format(Constants.NEW_RANDOM_PASSWORD_IS_SUCCESS));
                return System.Web.Security.Membership.GeneratePassword(8, 1);
            }
            catch (Exception ex)
            {
                log.Error(String.Format(Constants.NEW_RANDOM_PASSWORD_IS_UNSUCCESS, ex.Message));
                throw ex;
            }


        }

        public static Boolean sendEmail(string body, string toAdress, string subject)
        {
            try
            {
                const string fromPassword = "JnbsTdpAx5S7wRCq";

                var smtp = new SmtpClient
                {
                    Host = "smtp-relay.sendinblue.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential("fatoss.dogn@gmail.com", fromPassword)
                };
                using (var message = new MailMessage(new MailAddress("LoginApp@loginapp.com", "LoginApp@loginapp.com"), new MailAddress(toAdress, toAdress))
                {
                    Subject = subject,
                    Body = body
                })
                {
                    smtp.Send(message);
                    log.Info(String.Format(Constants.SEND_EMAIL_IS_SUCCESS));
                    return true;
                }
            }
            catch (Exception ex)
            {

                log.Error(String.Format(Constants.SEND_EMAIL_IS_UNSUCCESS, ex.Message));
                return false;
                

            }
        }

        public static Boolean ValueMaxLenghtCheck(String value, int maxLenght)
        {
            if (value != null && value.Length <= maxLenght)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}