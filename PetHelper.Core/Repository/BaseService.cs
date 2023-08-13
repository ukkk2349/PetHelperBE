using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using Dapper;
using System.Data.Entity;
using PetHelper.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using PetHelper.Model;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Net.Http.Json;
using System.Collections.Immutable;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using System.Dynamic;
using System.Reflection;

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
                var sql = $"SELECT * FROM `{tableName}`";
                var data = mysqlConnection.Query<T>(sql);

                return (List<T>)data;
            }
        }

        public async Task<List<object>> GetAll(Type type)
        {
            using (mysqlConnection = new MySqlConnection(connectionString))
            {
                var tableName = type.Name; // ten cua bang du lieu
                var sql = $"SELECT * FROM `{tableName}`";
                var data = await mysqlConnection.QueryAsync(sql);

                return (List<object>)data;
            }
        }

        public async Task<int> Save(Type type, BaseModel entity)
        {
            using (mysqlConnection = new MySqlConnection(connectionString))
            {
                var tableName = type.Name; // ten cua bang du lieu
                var objectInsert = ConvertToObjectToExecute(entity);
                var sql = $"Proc_{tableName}_Insert";
                var res = await mysqlConnection.ExecuteAsync(sql: sql, param: objectInsert, commandType: System.Data.CommandType.StoredProcedure);
                return res;
            }
            throw new NotImplementedException();
        }

        public int Delete(BaseModel entity)
        {
            throw new NotImplementedException();
        }

        public async Task<int> Update(BaseModel entity)
        {
            using (mysqlConnection = new MySqlConnection(connectionString))
            {
                var tableName = entity.GetType().Name; // ten cua bang du lieu
                var objectInsert = ConvertToObjectToExecute(entity);
                var sql = $"Proc_{tableName}_Update";
                var res = await mysqlConnection.ExecuteAsync(sql: sql, param: objectInsert, commandType: System.Data.CommandType.StoredProcedure);
                return res;
            }
            throw new NotImplementedException();
        }

        public async Task<List<T>> QueryUsingCommandText<T>(string queryString, Dictionary<string, object> dicParam) 
        {
            using (mysqlConnection = new MySqlConnection(connectionString))
            {
                var data = await mysqlConnection.QueryAsync<T>(queryString, dicParam);

                return (List<T>)data;
            }
        }

        public async Task<int> ExecuteUsingCommandText(string commandText, Dictionary<string, object> dicParam)
        {
            using (mysqlConnection = new MySqlConnection(connectionString))
            {
                var data = await mysqlConnection.ExecuteAsync(commandText, dicParam);

                return data;
            }
        }

        public async Task<T> GetByID<T>(int id) where T : BaseModel
        {
            using (mysqlConnection = new MySqlConnection(connectionString))
            {
                var tableName = typeof(T).Name; // ten cua bang du lieu
                var primaryKey = $"{tableName}ID";
                var sql = $"SELECT * from {tableName} WHERE {primaryKey} = @{primaryKey}";
                var parameter = new DynamicParameters();
                parameter.Add($"@{primaryKey}", id);
                var data = await mysqlConnection.QueryFirstOrDefaultAsync<T>(sql: sql, param: parameter);

                return data;
            }
        }

        public async Task<dynamic> GetByID(Type type, int id)
        {
            using (mysqlConnection = new MySqlConnection(connectionString))
            {
                var x = new Dictionary<string, object>() { { "value", "value" } };
                var tableName = type.Name; // ten cua bang du lieu
                var primaryKey = $"{tableName}ID";
                var sql = $"SELECT * from {tableName} WHERE {primaryKey} = @{primaryKey}";
                var parameter = new DynamicParameters();
                parameter.Add($"@{primaryKey}", id);
                var data = await mysqlConnection.QueryFirstOrDefaultAsync(sql: sql, param: parameter);

                return data;
            }
        }

        private DynamicParameters ConvertToObjectToExecute(dynamic obj)
        {
            PropertyInfo[] properties = obj.GetType().GetProperties();
            //Dictionary<string, object> dicConvert = JsonConvert.DeserializeObject<Dictionary<string, object>>(obj.ToString());

            var parameter = new DynamicParameters();

            foreach (var property in properties)
            {
                var propertyInfo = obj.GetType().GetProperty(property.Name);
                parameter.Add($"v_{property.Name}", propertyInfo.GetValue(obj, null));
            }

            return parameter;
        }

        public async Task<int> DeleteByID(Type type, int id)
        {
            using (mysqlConnection = new MySqlConnection(connectionString))
            {
                var tableName = type.Name; // ten cua bang du lieu
                var primaryKey = $"{tableName}ID";
                var sql = $"DELETE FROM {tableName} WHERE {primaryKey} = @{primaryKey}";
                var parameter = new DynamicParameters();
                parameter.Add($"@{primaryKey}", id);
                var data = await mysqlConnection.ExecuteAsync(sql: sql, param: parameter);

                return data;
            }
        }

        #endregion
    }
}
