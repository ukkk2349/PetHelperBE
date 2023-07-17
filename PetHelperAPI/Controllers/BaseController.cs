using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PetHelper.BL.Exceptions;
using PetHelper.BL.Interface;
using PetHelper.Model;
using PetHelper.Model.Models;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ObjectiveC;

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
        public async Task<ServiceResponse> GetAll()
        {
            var result = new ServiceResponse();
            try
            {
                result.Data = await _bl.GetAll(_modelType);
                result.Success = true;

                return result;
            }
            catch (Exception ex)
            {
                result.Data = ex;
                result.Success = false;
                return result;
            }
        }

        [HttpGet("getByID/{id}")]
        public async Task<ServiceResponse> GetByID(int id)
        {
            var res = new ServiceResponse();
            try
            {
                res.Data = await _bl.GetByID(_modelType, id);
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

        [HttpPost("deleteByID/{id}")]
        public async Task<ServiceResponse> DeleteByID(int id)
        {
            var res = new ServiceResponse();
            try
            {
                res.Data = await _bl.DeleteByID(_modelType, id);
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

        [HttpPost]
        public async Task<ServiceResponse> Save([FromBody] object entity)
        {
            var serviceResponse = new ServiceResponse();
            try
            {
                serviceResponse = await _bl.Save(this._modelType, (BaseModel)JsonConvert.DeserializeObject(entity.ToString(), this._modelType));
                return serviceResponse;
            }
            catch (Exception ex)
            {
                serviceResponse.Data = ex;
                serviceResponse.Success = false;
                return serviceResponse;
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
