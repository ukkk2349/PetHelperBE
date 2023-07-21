using PetHelper.Model.Enum;

namespace PetHelper.Model
{
    public partial class BaseModel : ICloneable
    {
        public DateTime? CreatedDate { get; set; }

        public Guid? CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set;}

        public Guid? ModifiedBy { get; set; }

        public ModelState State { get; set; }

        public object Clone()
        {
            CreatedBy = null;

            ModifiedBy = null;

            ModifiedDate = DateTime.Now;

            CreatedDate = DateTime.Now;

            State = ModelState.None;

            return this;
        }
    }
}
