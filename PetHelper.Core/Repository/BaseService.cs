using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using Dapper;
using System.Data.Entity;
using PetHelper.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using PetHelper.Model;

namespace PetHelper.Core.Repository
{
    public class BaseService : IBaseService
    {
        #region Properties
        protected string connectionString;
        protected MySqlConnection? mysqlConnection;
        protected string? tableName;
        #endregion

        public BaseService()
        {
            connectionString = "Host=localhost;" +
                    "Port=3306; " +
                    "Database= pet_helper; " +
                    "User Id = root; " +
                    "Password= ''";
        }

        #region Method
        /// <summary>
        /// Lấy tất cả trong db
        /// </summary>
        /// <returns></returns>
        public List<T> GetAll<T>() where T : BaseModel
        {
            using (mysqlConnection = new MySqlConnection(connectionString))
            {
                var tableName = typeof(T).Name; // ten cua bang du lieu
                var sql = $"SELECT * FROM {tableName}";
                var data = mysqlConnection.Query<T>(sql);

                return (List<T>)data;
            }
        }

        public async Task<List<object>> GetAll(Type type)
        {
            using (mysqlConnection = new MySqlConnection(connectionString))
            {
                var tableName = type.Name; // ten cua bang du lieu
                var sql = $"SELECT * FROM {tableName}";
                var data = await mysqlConnection.QueryAsync(sql);

                return (List<object>)data;
            }
        }

        public int Save(BaseModel entity)
        {
            using (mysqlConnection = new MySqlConnection(connectionString))
            {
                var tableName = typeof(BaseModel).Name; // ten cua bang du lieu

                var sql = $"Proc_{tableName}_Insert";
                var res = mysqlConnection.Execute(sql: sql, param: entity, commandType: System.Data.CommandType.StoredProcedure);
                return res;
            }
            throw new NotImplementedException();
        }

        public int Delete(BaseModel entity)
        {
            throw new NotImplementedException();
        }

        public int Update(BaseModel entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> QueryUsingCommandText<T>(string queryString) where T : BaseModel
        {
            using (mysqlConnection = new MySqlConnection(connectionString))
            {
                var data = mysqlConnection.Query<T>(queryString);

                return data;
            }
        }

        public T GetByID<T>(int id) where T : BaseModel
        {
            using (mysqlConnection = new MySqlConnection(connectionString))
            {
                var tableName = typeof(T).Name; // ten cua bang du lieu
                var primaryKey = $"{tableName}ID";
                var sql = $"SELECT * from {tableName} WHERE {primaryKey} = @{primaryKey}";
                var parameter = new DynamicParameters();
                parameter.Add($"@{primaryKey}", id);
                var data = mysqlConnection.QueryFirstOrDefault<T>(sql: sql, param: parameter);

                return data;
            }
        }

        public dynamic GetByID(Type type, int id)
        {
            using (mysqlConnection = new MySqlConnection(connectionString))
            {
                var tableName = type.Name; // ten cua bang du lieu
                var primaryKey = $"{tableName}ID";
                var sql = $"SELECT * from {tableName} WHERE {primaryKey} = @{primaryKey}";
                var parameter = new DynamicParameters();
                parameter.Add($"@{primaryKey}", id);
                var data = mysqlConnection.QueryFirstOrDefault(sql: sql, param: parameter);

                return data;
            }
        }

        #endregion
    }
}
