using PetHelper.BL.Interface;
using PetHelper.Core.Interfaces;
using PetHelper.Model;
using PetHelper.Model.Models;

namespace PetHelper.BL.Implements
{
    public class CartBL : BaseBL, ICartBL
    {
        public CartBL(IBaseService databaseService) : base(databaseService)
        {
        }

        public async override Task BeforeSaveAsync(BaseModel entity)
        {
            await base.BeforeSaveAsync(entity);

            var cart = entity as Cart;
            if (_userID != 0)
            {
                cart.UserID = _userID;
            }
        }

        public async Task<int> GetTotalCartItem()
        {
            var sql = "SELECT COUNT(1) FROM cart WHERE UserID = @UserID";
            var dicParam = new Dictionary<string, object>();
            dicParam.Add("@UserID", _userID);

            var res = await QueryUsingCommanTextAsync<int>(sql, dicParam);

            var quantity = 0;

            if (res != null && res.Count > 0)
            {
                quantity = res[0];
            }

            return quantity;

        }
    }
}
