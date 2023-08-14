using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetHelper.BL.Interface;
using PetHelper.Model.Models;

namespace PetHelper.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommonController : ControllerBase
    {
        public ICommonBL _commonBL;

        public CommonController(ICommonBL commonBL)
        {
            _commonBL = commonBL;
        }

        [HttpPost("search-global")]
        public async Task<ServiceResponse> GetCartItemOfUser(string searchValue)
        {
            var res = new ServiceResponse();
            try
            {
                res.Data = await _commonBL.SearchGlobal(searchValue);
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
