using PetHelper.Model;

namespace PetHelper.BL.Interface
{
    public interface IBaseBL
    {
        public Task<bool> Save(BaseModel entity);

        public List<T> GetAll<T>() where T : BaseModel;

        public Task<List<object>> GetAll(Type type);
    }
}
