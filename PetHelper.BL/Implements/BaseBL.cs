using PetHelper.BL.Interface;
using PetHelper.Model;
using PetHelper.Core.Interfaces;

namespace PetHelper.BL.Implements
{
    public class BaseBL : IBaseBL
    {
        private IBaseService _databaseService;

        public BaseBL(IBaseService databaseService)
        {
            _databaseService = databaseService;
        }

        public List<T> GetAll<T>() where T : BaseModel
        {
            return  _databaseService.GetAll<T>();
        }

        public async Task<List<object>> GetAll(Type type)
        {
            return await _databaseService.GetAll(type);
        }

        public async Task<bool> Save(BaseModel entity)
        {

            throw new NotImplementedException();
        }
    }
}
