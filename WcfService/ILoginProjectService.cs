using System.ServiceModel;

namespace WcfService
{
    
    [ServiceContract]
    public interface ILoginProjectService
    {

        [OperationContract]
        BaseResponse Register(Users user);

        [OperationContract]
        BaseResponse Login(string username, string password);

        [OperationContract]
        BaseResponse ForgetPassword(string username, string email);

        [OperationContract]
        BaseResponse UpdateEmail(string username, string email, string newEmail);

        [OperationContract]
        BaseResponse MembershipCancellation(string username, string password, string email);
    }


    
}
