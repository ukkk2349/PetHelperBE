using Microsoft.AspNetCore.Mvc;
using PetHelper.BL.Exceptions;
using PetHelper.BL.Interface;
using PetHelper.Model;
using System.Runtime.CompilerServices;

namespace PetHelper.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected IBaseBL _bl;
        protected Type _modelType;

        public BaseController(IBaseBL baseBL)
        {
            _bl = baseBL;
            this._modelType = typeof(BaseModel);
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var assets = await _bl.GetAll(_modelType);
                return Ok(assets);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        private IActionResult HandleException(Exception ex)
        {

            if (ex is ValidateException)
            {
                var res = new
                {
                    devMsg = ex.Message,
                    data = ex.Data,
                    userMsg = ex.Message
                };
                return StatusCode(400, res);
            }
            else
            {
                var res = new
                {
                    devMsg = ex.Message,
                    data = ex.Data,
                    userMsg = "Có lỗi xảy ra"
                };
                return StatusCode(500, res);
            }
        }
    }
}
