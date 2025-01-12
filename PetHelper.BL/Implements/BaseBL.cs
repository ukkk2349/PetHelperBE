﻿using PetHelper.BL.Interface;
using PetHelper.Model;
using PetHelper.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using PetHelper.BL.Exceptions;
using System.Reflection.Metadata.Ecma335;
using System.Text.RegularExpressions;
using System.Text;
using PetHelper.Model.Models;

namespace PetHelper.BL.Implements
{
    public class BaseBL : IBaseBL
    {
        private IBaseService _databaseService;
        protected static int _userID;
        protected static string _fullName;

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

        public async Task<T> GetByID<T>(int id) where T : BaseModel
        {
            return await _databaseService.GetByID<T>(id);
        }

        public async Task<ServiceResponse> Save(Type type, BaseModel entity)
        {
            try
            {
                var serviceResponse = new ServiceResponse();

                var validateResults = await ValidateData(entity);

                if (validateResults != null &&  validateResults.Count > 0)
                {
                    serviceResponse.ValidateResults = validateResults;
                    serviceResponse.Success = false;
                    return serviceResponse;
                }

                await this.BeforeSaveAsync(entity);

                serviceResponse.Data = await this.DoSaveAsync(type, entity);  

                await this.AfterSaveAsync(entity);

                serviceResponse.Success = true;

                return serviceResponse;

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

        public async Task<int> DoSaveAsync(Type type, BaseModel entity)
        {
            // Xử lý trước khi lưu dữ liệu
            if (entity.State == Model.Enum.ModelState.Insert)
            {
                return await _databaseService.Save(type, entity);
            } 
            else if (entity.State == Model.Enum.ModelState.Update)
            {
                return await this._databaseService.Update(entity);
            } 
            else
            {
                return await this._databaseService.Update(entity);
            }
        }

        public virtual async Task BeforeSaveAsync(BaseModel entity)
        {
            return;
        }


        public virtual async Task AfterSaveAsync(BaseModel entity)
        {

        }

        public async Task<List<T>> QueryUsingCommanTextAsync<T>(string commandText, Dictionary<string, object> dicParam)
        {
            return await _databaseService.QueryUsingCommandText<T>(commandText, dicParam);
        }

        public async Task<int> ExecuteUsingCommanTextAsync(string commandText, Dictionary<string, object> dicParam)
        {
            return await _databaseService.ExecuteUsingCommandText(commandText, dicParam);
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
