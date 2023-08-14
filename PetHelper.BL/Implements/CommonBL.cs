using PetHelper.BL.Interface;
using PetHelper.Core.Interfaces;
using PetHelper.Model;
using PetHelper.Model.Models;

namespace PetHelper.BL.Implements
{
    public class CommonBL : BaseBL, ICommonBL
    {
        public CommonBL(IBaseService databaseService) : base(databaseService)
        {
        }

        public async Task<object> SearchGlobal(string searchValue)
        {
            var petSql = "SELECT * FROM pet WHERE PetName LIKE CONCAT('%', @Value, '%');";
            var productSql = "SELECT * FROM product WHERE ProductName LIKE CONCAT('%', @Value, '%');";
            var param = new Dictionary<string, object>()
            {
                { "@Value", searchValue }
            };

            var pets = await QueryUsingCommanTextAsync<Pet>(petSql, param);
            var products = await QueryUsingCommanTextAsync<Product>(productSql, param);

            return new
            {
                Pets = pets,
                Products = products
            };
        }
    }
}
