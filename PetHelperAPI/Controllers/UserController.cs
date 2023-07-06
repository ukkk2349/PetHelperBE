using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetHelper.BL.Interface;
using PetHelper.Model;
using PetHelper.Model.Models;

namespace PetHelper.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        public UserController(IUserBL userBL) : base(userBL)
        {
            this._modelType = typeof(User);
        }

        /// <summary>
        /// Xử lý đăng nhập
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        [HttpPost("sign-in")]
        public async Task<ServiceResponse> SignIn(Dictionary<string, string> parameters)
        {
            var res = new ServiceResponse();
            try
            {
                res.Data = await (this._bl as IUserBL).SignIn(parameters.GetValueOrDefault("UserName"), parameters.GetValueOrDefault("Password"));
                res.Success = true;
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
