using PetHelper.Model;

namespace PetHelper.Core.Interfaces
{
    public interface IBaseService
    {
        public List<T> GetAll<T>() where T : BaseModel;

        public Task<List<object>> GetAll(Type type);

        public Task<int> Save(Type type, object entity);

        public int Delete(BaseModel entity);

        public int Update(BaseModel entity);

        public Task<List<T>> QueryUsingCommandText<T>(string queryString, Dictionary<string, object> dicParam) where T : BaseModel;

        public T GetByID<T>(int id) where T : BaseModel;

        public Task<dynamic> GetByID(Type type, int id);

        public Task<int> DeleteByID(Type type, int id);
    }
}
