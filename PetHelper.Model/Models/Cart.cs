using System.ComponentModel.DataAnnotations;

namespace PetHelper.Model.Models
{
    public class Cart : BaseModel
    {
        [Key]
        public int CartID { get; set; }
        /// <summary>
        /// ID sản phẩm
        /// </summary>
        public int ProductID { get; set;}
        /// <summary>
        /// Tên sản phẩm
        /// </summary>
        public string ProductName { get; set;}
        /// <summary>
        /// avatar sản phẩm
        /// </summary>
        public string ProductAvatar { get; set; }
        /// <summary>
        /// Giá
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// id người dùng
        /// </summary>
        public int UserID { get; set; }
    }
}
