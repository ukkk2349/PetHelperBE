using Newtonsoft.Json;
using PetHelper.BL.Interface;
using PetHelper.Core.Interfaces;
using PetHelper.Model;
using PetHelper.Model.Models;
using System.Text.Json.Serialization;

namespace PetHelper.BL.Implements
{
    public class CartBL : BaseBL, ICartBL
    {
        private readonly IOrderBL _orderBL;

        public CartBL(IBaseService databaseService, IOrderBL orderBL) : base(databaseService)
        {
            _orderBL = orderBL;
        }

        public async override Task BeforeSaveAsync(BaseModel entity)
        {
            await base.BeforeSaveAsync(entity);

            var cart = entity as Cart;
            if (_userID != 0)
            {
                cart.UserID = _userID;
            }

            var existSQL = "SELECT * FROM cart WHERE UserID = @UserID AND ProductID = @ProductID LIMIT 1";
            var dicparam = new Dictionary<string, object>()
            {
                { "@UserID", _userID },
                { "@ProductID", cart.ProductID }
            };  
            var res = await QueryUsingCommanTextAsync<Cart>(existSQL, dicparam);
            if (res != null && res.Count > 0)
            {
                cart.Quantity = res.FirstOrDefault().Quantity + 1;
                cart.State = Model.Enum.ModelState.Update;
            }
        }

        public async Task<ServiceResponse> GetCartItemOfUser()
        {
            var serviceResponse = new ServiceResponse();
            var sql = "SELECT * FROM cart WHERE UserID = @UserID ORDER BY ModifiedDate DESC";
            var dicParam = new Dictionary<string, object>();
            dicParam.Add("@UserID", _userID);

            var res = await QueryUsingCommanTextAsync<Cart>(sql, dicParam);

            if (res != null) 
            {
                serviceResponse.Data = res;
                serviceResponse.Success = true;
            }

            return serviceResponse;
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

        public async Task<bool> Order()
        {
            var getSQL = "SELECT * FROM cart WHERE UserID = @UserID;";
            var dicParam = new Dictionary<string, object>()
            {
                { "@UserID", _userID }
            }; 

            var carts = await QueryUsingCommanTextAsync<Cart>(getSQL, dicParam);

            if (carts != null && carts.Count > 0)
            {
                var resUser = await GetByID(typeof(User), _userID);
                var user = resUser as User;
                var order = new Order()
                {
                    ProductIDs = string.Join(';', carts.Select(x => x.ProductID)),
                    ProductNames = string.Join(';', carts.Select(x => x.ProductName)),
                    ProductQuantities = string.Join(';', carts.Select(x => x.Quantity)),
                    UserID = _userID,
                    FullName = user.FullName,
                    PhoneNumber = user.PhoneNumber,
                    Address = user.Address,
                    OrderDate = DateTime.Now,
                    OrderStatusID = 1,
                    OrderStatusName = "Đang xử lý",
                    TotalMoney = (int)carts.Sum(x => x.Quantity * x.Price)
                };

                _ = _orderBL.Save(typeof(Order), order);
            }

            var deleteSql = "DELETE FROM cart WHERE UserID = @userID";

            _ = await QueryUsingCommanTextAsync<Cart>(getSQL, dicParam);

            return true;

        }
    }
}
