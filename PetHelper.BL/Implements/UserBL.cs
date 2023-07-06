using PetHelper.BL.Interface;
using PetHelper.Model;
using PetHelper.Core.Interfaces;
using Dapper;

namespace PetHelper.BL.Implements
{
    public class UserBL : BaseBL, IUserBL
    {
        public UserBL(IBaseService databaseService) : base(databaseService)
        {
        }

        public async Task<object?> SignIn(string username, string password)
        {
            var sql = "SELECT * FROM user WHERE PhoneNumber = @UserName OR Email = @UserName";
            var parameter = new Dictionary<string, object>()
            {
                { "UserName", username }
            };

            var res = await this.QueryUsingCommanTextAsync<User>(sql, parameter);

            if (res != null && res.Count > 0)
            {
                var user = res.Find(x => x.Password  == password);
                if (user != null && user.UserID != 0) 
                {
                    this._userID = user.UserID;
                    return new {
                        UserKey = user.UserKey,
                        IsAdmin = user.IsAdmin,
                        IsManager = user.IsManager 
                    };
                }
                else
                {
                    return null;
                }
            } 
            else
            {
                return null;
            }
        }

    }
}
