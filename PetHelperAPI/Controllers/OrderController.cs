using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetHelper.BL.Interface;
using PetHelper.Model.Models;

namespace PetHelper.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : BaseController
    {
        public OrderController(IOrderBL orderBL) : base(orderBL)
        {
            this._modelType = typeof(Order);
        }
    }
}
