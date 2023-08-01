using System.ComponentModel.DataAnnotations;

namespace PetHelper.Model.Models
{
    public class Order : BaseModel
    {
        [Key]
        public int OrderID { get; set; }
        /// <summary>
        /// id sản phẩm đặt (phân cách dấu ;)
        /// </summary>
        public string ProductIDs { get; set; }
        /// <summary>
        /// Tên sản phẩm (phân cách dấu ;)
        /// </summary>
        public string ProductNames { get; set; }
        /// <summary>
        /// Số lượng sản phẩm
        /// </summary>
        public string ProductQuantities { get; set; }
        /// <summary>
        /// List sản phẩm (json (ProductID, productName, ProductAvatar, price, quantity))
        /// </summary>
        public string Products { get; set; }
        /// <summary>
        /// Id người dùng đặt hàng
        /// </summary>
        public int UserID { get; set; }
        /// <summary>
        /// Tên người đặt hàng
        /// </summary>
        public string FullName { get; set; }
        /// <summary>
        /// Số điện thoại đặt hàng
        /// </summary>
        public string PhoneNumber { get; set; }
        /// <summary>
        /// Địa chỉ giao hàng
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// ngày đặt hàng
        /// </summary>
        public DateTime OrderDate { get; set; }
        /// <summary>
        /// id trạng thái đơn hàng (1 - đang xử lý, 2 - đang giao hàng, 3 - đã giao hàng, 4 - đã hoàn thành)
        /// </summary>
        public int OrderStatusID { get; set; }
        /// <summary>
        /// Trạng thái đơn hàng (đang xử lý, đang giao hàng, đã giao hàng, đã hoàn thành)
        /// </summary>
        public string OrderStatusName { get; set;}
        /// <summary>
        /// Tổng tiền
        /// </summary>
        public int TotalMoney { get; set; }
    }
}
