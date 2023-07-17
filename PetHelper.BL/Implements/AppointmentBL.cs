using PetHelper.BL.Interface;
using PetHelper.Core.Interfaces;

namespace PetHelper.BL.Implements
{
    public class AppointmentBL : BaseBL, IAppointmentBL
    {
        public AppointmentBL(IBaseService databaseService) : base(databaseService)
        {
        }
    }
}
