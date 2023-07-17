using PetHelper.Model;

namespace PetHelper.BL.Interface
{
    public interface IBaseBL
    {
        public Task<object> Save(Type type, BaseModel entity);

        public List<T> GetAll<T>() where T : BaseModel;

        public Task<List<object>> GetAll(Type type);
        
        public Task<dynamic> GetByID(Type type, int id);

        public Task<object> DeleteByID(Type type, int id);

        public Task<List<T>> QueryUsingCommanTextAsync<T>(string commandText, Dictionary<string, object> dicParam);
    }
}
