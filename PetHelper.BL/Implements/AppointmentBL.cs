using Newtonsoft.Json;
using PetHelper.BL.Interface;
using PetHelper.Core.Interfaces;
using PetHelper.Model;
using PetHelper.Model.Models;

namespace PetHelper.BL.Implements
{
    public class AppointmentBL : BaseBL, IAppointmentBL
    {
        public AppointmentBL(IBaseService databaseService) : base(databaseService)
        {
        }

        public async override Task BeforeSaveAsync(BaseModel entity)
        {
            base.BeforeSaveAsync(entity);

            var appointment = entity as Appointment;

            if (appointment.State == Model.Enum.ModelState.Insert)
            {
                var user = GetByID<User>(_userID);
                appointment.UserName = _fullName;
                appointment.UserID = _userID;
                appointment.PhoneNumber = user.PhoneNumber;
            }
        }
    }
}
