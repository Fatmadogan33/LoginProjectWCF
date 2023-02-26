using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcfService.LoginBusiness
{
    public static class Constants
    {
        public const string ACTIVE = "A";
        public const string DISABLED = "D";

        public const string ERROR = "Hata oluştu";
        public const string USER_IS_NOTFOUND = "Kullanıcı bulunamadı";

        public const string USER_REGISTER_REQUEST_LOG = "username:{0}, email:{1} bilgileri ile user yaratma isteği gelmiştir.";

        public const string PASSWORD_IS_REQUIRED = "Password girilmek zorundadır.";
        public const string USERNAME_IS_REQUIRED = "Username girilmek zorundadır.";
        public const string EMAIL_IS_REQUIRED = "Email girilmek zorundadadır.";

        public const string LOGIN_IS_SUCCESS = "Başarılı bir şekilde login olmuştur.";
        public const string REGISTER_IS_SUCCESS = "Başarılı bir şekilde kullanıcı oluşturulmuştur.";

        public const string USERNAME_OR_PASSWORD_IS_UNSUCCESS = "Kullanıcı adı veya parola hatalıdır.";

        public const string EMAIL_FORMAT_IS_UNSUCCESS = "Email formatı yanlıştır.";
        public const string PASSWORD_FORMAT = "En az 8 karakter uzunluğunda,bir küçük, bir büyük harf, bir rakam ve bir özel karakter içermelidir. ";
 
        public const string PASSWORD_RESET_REQUEST_UNSUCCESS = "Parola sıfırlama isteğinde sorun oluştu.";

        public const string FORGET_PASSWORD_IS_SUCCESS = "Başarılı bir şekilde forget password olmuştur. ";
        public const string FORGET_PASSWORD_IS_UNSUCCESS = "Forget password oluşturmada hata olmuştur. ";

        public const string MEMBERSHIP_CANCELLATION_IS_SUCCESS = "Başarılı bir şekilde Membership cancellation olmuştur. ";
        public const string MEMBERSHIP_CANCELLATION_IS_UNSUCCESS = "Membership cancellation Forget password oluşturmada hata olmuştur. ";

        public const string RANDOM_PASSWORD_IS_SUCCESS = "Başarılı bir şekilde random şifre oluşturulmuştur ve mail gönderilmiştir.";

        public const string EMAIL_UPDATE_IS_SUCCESS = "Başarılı bir şekilde email güncellenmiştir.";
        public const string EMAIL_UPDATE_IS_UNSUCCESS = "E mail güncelleme sırasında hata oluştu.";

        public const string STATUS_UPDATE_IS_SUCCESS = "Başarılı bir şekilde disable edilmiştir.";
        public const string STATUS_UPDATE_IS_UNSUCCESS = "Kullanıcı disable işleminde hata alınmıştır.";

        public const string USERNAME_EMAIL_PASSWORD_IS_WRONG = "Bu kullanıcının username, email veya password hatalıdır.";

        public const string PASSWORD_HASHING_IS_SUCCESS = "Password hashing işlemi başarılı bir şekilde yapılmıştır.";
        public const string PASSWORD_HASHING_IS_UNSUCCESS = "Password hashing işlemi sırasında hata oluştu.";

        public const string EMAIL_MATHCER_IS_SUCCESS = "Email format kontrolü başarıyla yapılmıştır.";
        public const string EMAIL_MATHCER_IS_UNSUCCESS = "Email format kontrolü yapılırken bir hata oluştu.";

        public const string STRONG_PASSWORD_MATHCER_IS_SUCCESS = "Güçlü şifre kontrolü başarıyla yapılmıştır.";
        public const string STRONG_PASSWORD_MATHCER_IS_UNSUCCESS = "Güçlü şifre kontrolü yapılırken bir hata oluştu.";

        public const string NEW_RANDOM_PASSWORD_IS_SUCCESS = "Yeni random şifre oluşturma başarıyla yapılmıştır.";
        public const string NEW_RANDOM_PASSWORD_IS_UNSUCCESS = "Yeni random şifre oluşturulurken bir hata oluştu.";

        public const string SEND_EMAIL_IS_SUCCESS = "Email gönderme işlemi başarılı bir şekilde yapılmıştır.";
        public const string SEND_EMAIL_IS_UNSUCCESS = "Email gönderme sırasında hata oluştu.";
        public const string SEND_EMAIL_ERROR = "Email gönderim hatası olduğu için password, eski password olarak update edildi.";

        public const string DB_DATA_SOURCE = "DESKTOP-U24IBOG";
        public const string DB_INITIAL_CATALOG = "UserDatabase";
        public const string DB_ERROR = "Database hatası oluştu";
        













    }
}
