using PetHelper.Model;

namespace PetHelper.Core.Interfaces
{
    public interface IBaseService
    {
        public List<T> GetAll<T>() where T : BaseModel;

        public Task<List<object>> GetAll(Type type);

        public int Save(BaseModel entity);

        public int Delete(BaseModel entity);

        public int Update(BaseModel entity);

        public IEnumerable<T> QueryUsingCommandText<T>(string queryString) where T : BaseModel;

        public T GetByID<T>(int id) where T : BaseModel;

        public dynamic GetByID(Type type, int id);
    }
}
