using System.ComponentModel.DataAnnotations;

namespace PetHelper.Model.Models
{
    public class Appointment : BaseModel
    {
        [Key]
        public int AppointmentID { get; set; }

        /// <summary>
        /// Id thú cưng
        /// </summary>
        public int PetID { get; set; }

        /// <summary>
        /// Tên thú cưng
        /// </summary>
        public string PetName { get; set; }

        /// <summary>
        /// Avatar thú cưng
        /// </summary>
        public string PetAvatar { get; set; }

        /// <summary>
        /// ID người đặt lịch
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// Tên người dùng
        /// </summary>
        public string UserName { get; set; } = string.Empty;

        public string PhoneNumber { get; set; }

        /// <summary>
        /// Avatar người dùng
        /// </summary>
        public string UserAvatar { get; set; }

        /// <summary>
        /// Ngày hẹn xem
        /// </summary>
        public DateTime? AppointmentDate { get; set; }

        /// <summary>
        /// ID Trạng thái cuộc hẹn (1 - Đang đặt hẹn, 2 - Đã xác nhận)
        /// </summary>
        public int StatusID { get; set; }
        /// <summary>
        /// Tên trạng thái
        /// </summary>
        public string StatusName { get; set; }
    }
}
