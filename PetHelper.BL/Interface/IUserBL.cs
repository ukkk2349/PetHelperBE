using PetHelper.Model;
using PetHelper.Model.Models;

namespace PetHelper.BL.Interface
{
    public interface IUserBL : IBaseBL
    {
        public Task<ServiceResponse> SignIn(string username, string password);
    }
}
