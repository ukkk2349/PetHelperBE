using System.ComponentModel.DataAnnotations;

namespace PetHelper.Model.Models
{
    public class PetSpecies : BaseModel
    {
        [Key]
        public int PetSpeciesID { get; set; }

        public string PetSpeciesName { get; set; }
    }
}
