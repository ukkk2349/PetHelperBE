using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetHelper.BL.Interface;
using PetHelper.Model;

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

    }
}
