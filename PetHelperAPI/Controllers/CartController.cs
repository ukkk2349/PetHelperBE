using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetHelper.BL.Interface;
using PetHelper.Model.Models;

namespace PetHelper.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : BaseController
    {
        public CartController(ICartBL cartBL) : base(cartBL)
        {
            this._modelType = typeof(Cart);
        }

        [HttpGet("get-cart-item")]
        public async Task<ServiceResponse> GetCartItemOfUser()
        {
            var res = new ServiceResponse();
            try
            {
                res = await(this._bl as ICartBL).GetCartItemOfUser();
                return res;
            }
            catch (Exception ex)
            {
                res.Data = ex;
                res.Success = false;
                return res;
            }
        }
    }
}
