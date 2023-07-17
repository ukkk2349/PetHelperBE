using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetHelper.Model
{
    [Table("user")]
    public class User : BaseModel
    {
        [Key]
        public int UserID { get; set; }
        /// <summary>
        /// Tên đầy đủ
        /// </summary>
        public string FullName { get; set; } = string.Empty;
        /// <summary>
        /// Tên
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// Họ và tên đệm
        /// </summary>
        public string LastName { get; set; }

        public string Email { get; set; }
        /// <summary>
        /// Mật khẩu
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// Địa chỉ chi tiết
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// Ngày sinh
        /// </summary>
        public DateTime Birthday { get; set; }

        /// <summary>
        /// Mã nơi sinh (thành phố)
        /// </summary>
        public string BirthPlaceID { get; set; }
        /// <summary>
        /// Nơi sinh
        /// </summary>
        public string BirthPlaceName { get;set; }
        /// <summary>
        /// mã thành phố / tỉnh sinh sống hiện tại
        /// </summary>
        public string CurrentProvinceID { get; set; }
        /// <summary>
        /// Tên thành phố / tỉnh sinh sống hiện tại
        /// </summary>
        public string CurrentProvinceName { get; set; }
        /// <summary>
        /// ID quận huyện
        /// </summary>
        public string CurrentDistrictID { get; set; }
        /// <summary>
        /// Tên quận huyện
        /// </summary>
        public string? CurrentDistrictName { get; set; }
        /// <summary>
        /// ID phường xã
        /// </summary>
        public string? CurrentWardID { get; set; }
        /// <summary>
        /// Tên phường xã
        /// </summary>
        public string? CurrentWardName { get; set; }
        /// <summary>
        /// SĐT
        /// </summary>
        public string? PhoneNumber { get; set; }
        
        public Guid? UserKey { get; set;}
        /// <summary>
        /// Ngày tham gia 
        /// </summary>
        public DateTime? JoinDate { get; set; }
        /// <summary>
        /// Có phải quản lý không
        /// </summary>
        public bool? IsManager { get; set; }
        /// <summary>
        /// Có phải admin khônga
        /// </summary>
        public bool? IsAdmin { get; set; }
    }
}
