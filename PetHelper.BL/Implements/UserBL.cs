using PetHelper.BL.Interface;
using PetHelper.Model;
using PetHelper.Core.Interfaces;
using Dapper;
using PetHelper.Model.Models;

namespace PetHelper.BL.Implements
{
    public class UserBL : BaseBL, IUserBL
    {
        private readonly ICartBL _cartBL;
        public UserBL(IBaseService databaseService, ICartBL cartBL) : base(databaseService)
        {
            _cartBL = cartBL;
        }

        public async Task<ServiceResponse?> SignIn(string username, string password)
        {
            var serviceResponse = new ServiceResponse();
            var sql = "SELECT * FROM user WHERE PhoneNumber = @UserName OR Email = @UserName";
            var parameter = new Dictionary<string, object>()
            {
                { "UserName", username }
            };

            var res = await this.QueryUsingCommanTextAsync<User>(sql, parameter);

            if (res != null && res.Count > 0)
            {
                var user = res.Find(x => x.Password  == password);
                if (user != null && user.UserID != 0) 
                {
                    _userID = user.UserID;
                    _fullName = user.FullName;
                    serviceResponse.Success = true;

                    // Lấy số lượng giỏ hàng
                    var cartQuantity = await _cartBL.GetTotalCartItem();

                    serviceResponse.Data =  new {
                        UserKey = user.UserKey,
                        IsAdmin = user.IsAdmin,
                        IsManager = user.IsManager,
                        Cart = cartQuantity
                    };
                }
                else
                {
                    serviceResponse.Success = false;
                    serviceResponse.Data = "WRONG_ACCOUNT";
                }
            } 
            else
            {
                serviceResponse.Success = false;
                serviceResponse.Data = "WRONG_ACCOUNT";
            }

            return serviceResponse;
        }

    }
}
