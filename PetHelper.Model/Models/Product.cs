namespace PetHelper.Model.Models
{
    public class Product : BaseModel
    {
        public int ProductID { get; set; }

        public string ProductName { get; set; }

        public string Description { get; set; }

        public DateTime ExpiredDate { get; set; }

        public DateTime ManufacturingDate { get; set; }

        public string Origin { get; set; }

        public decimal Price { get; set; }

        public int StateID { get; set; }

        public string StateName { get; set; }

        public int ProductCategoryID { get; set; }

        public string ProductCategoryName { get; set; }

        public string ProductAvatar { get; set; }
    }
}
