using PetHelper.BL.Interface;
using PetHelper.Model;
using PetHelper.Core.Interfaces;

namespace PetHelper.BL.Implements
{
    public class UserBL : BaseBL, IUserBL
    {
        public UserBL(IBaseService databaseService) : base(databaseService)
        {
        }
    }
}
