using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetHelper.BL.Interface;
using PetHelper.Model;

namespace PetHelper.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetController : BaseController
    {
        public PetController(IPetBL petBL) : base(petBL)
        {
            this._modelType = typeof(Pet);
        }
    }
}
