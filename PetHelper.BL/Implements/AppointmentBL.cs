using Newtonsoft.Json;
using PetHelper.BL.Interface;
using PetHelper.Core.Interfaces;
using PetHelper.Model;
using PetHelper.Model.Models;
using System.Net.WebSockets;

namespace PetHelper.BL.Implements
{
    public class AppointmentBL : BaseBL, IAppointmentBL
    {
        private readonly IPetBL _petBL;
        public AppointmentBL(IBaseService databaseService, IPetBL petBL) : base(databaseService)
        {
            _petBL = petBL;
        }

        public async override Task BeforeSaveAsync(BaseModel entity)
        {
            base.BeforeSaveAsync(entity);

            var appointment = entity as Appointment;

            if (appointment.State == Model.Enum.ModelState.Insert)
            {
                var user = await GetByID<User>(_userID);
                appointment.UserName = _fullName;
                appointment.UserID = _userID;
                appointment.PhoneNumber = user.PhoneNumber;
            }
        }
    }
}
