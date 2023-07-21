using PetHelper.Model.Models;

namespace PetHelper.BL.Interface
{
    public interface ICartBL : IBaseBL
    {
        /// <summary>
        /// Lấy số sản phẩm đang có trong giỏ hàng của người dùng
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public Task<int> GetTotalCartItem();

        /// <summary>
        /// Lấy các sản phẩm đang có trong giỏ hàng của người dùng
        /// </summary>
        /// <returns></returns>
        public Task<ServiceResponse> GetCartItemOfUser();
    }
}
