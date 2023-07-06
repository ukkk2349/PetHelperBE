using PetHelper.Model;

namespace PetHelper.BL.Interface
{
    public interface IUserBL : IBaseBL
    {
        public Task<object?> SignIn(string username, string password);
    }
}
