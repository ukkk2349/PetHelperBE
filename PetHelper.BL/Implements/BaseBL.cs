using PetHelper.BL.Interface;
using PetHelper.Model;
using PetHelper.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using PetHelper.BL.Exceptions;
using System.Reflection.Metadata.Ecma335;
using System.Text.RegularExpressions;
using System.Text;

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

        public async Task<dynamic> GetByID(Type type, int id)
        {
            return await _databaseService.GetByID(type, id);
        }

        public async Task<object> Save(Type type, BaseModel entity)
        {
            try
            {
                var validateResults = await ValidateData(entity);

                if (validateResults != null &&  validateResults.Count > 0)
                {
                    return validateResults;
                }

                await this.BeforeSaveAsync(entity);

                var res = await this.DoSaveAsync(type, entity);  

                await this.AfterSaveAsync(entity);

                return res;

            }
            catch (Exception ex)
            {
                throw new ValidateException(ex.Message);
            }

        }

        public virtual async Task<List<ValidateException>> ValidateData(object entity)
        {
            return new List<ValidateException>();
        }

        public async Task<int> DoSaveAsync(Type type, object entity)
        {
            // Xử lý trước khi lưu dữ liệu
            return await _databaseService.Save(type, entity);
        }

        public virtual async Task BeforeSaveAsync(BaseModel entity)
        {
            return;
        }


        public virtual async Task AfterSaveAsync(BaseModel entity)
        {

        }

        public async Task<List<T>> QueryUsingCommanTextAsync<T>(string commandText, Dictionary<string, object> dicParam) where T : BaseModel
        {
            return await _databaseService.QueryUsingCommandText<T>(commandText, dicParam);
        }

        public static string convertToUnSign(string s)
        {
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = s.Normalize(NormalizationForm.FormD);
            var strHaveSpace = regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
            return string.Join('_', strHaveSpace.Split(' '));
        }

        public async Task<object> DeleteByID(Type type, int id)
        {
            return await _databaseService.DeleteByID(type, id);
        }
    }
}
