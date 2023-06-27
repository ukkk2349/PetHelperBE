using Microsoft.JSInterop;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetHelper.Model
{
    [Table("pet")]
    public class Pet : BaseModel
    {
        [Key]
        public int PetID { get; set; }

        public string PetName { get; set; } = string.Empty;

        /// <summary>
        /// ID giống loài
        /// </summary>
        public int SpeciesID { get; set;}

        /// <summary>
        /// Tên giống loài
        /// </summary>
        public string? SpeciesName { get; set;}
        /// <summary>
        /// Tuổi
        /// </summary>
        public decimal Age { get; set; }
        /// <summary>
        /// Đã được nhận nuôi?
        /// </summary>
        public bool IsAdopted { get; set; }
        /// <summary>
        /// Tình trạng nhận nuôi (1: chưa được nhân nuôi, 2: Đã được nhận nuôi)
        /// </summary>
        public int AdoptStateID {get; set; }
        /// <summary>
        /// Tên tình trạng nhận nuôi
        /// </summary>
        public string? AdoptStateName { get;}
        /// <summary>
        /// id tình trạng sức khoẻ (1-tốt, 2-yếu, 3-Đang điều trị)
        /// </summary>
        public int HealthStateID { get; set; }
        /// <summary>
        /// Tên tình trạng sức khoẻ
        /// </summary>
        public string? HealthStateName { get; set;}

    }
}
