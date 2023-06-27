using System;
using System.Collections;

namespace PetHelper.BL.Exceptions
{
    public class ValidateException : Exception
    {
        #region
        public string? validateErrorMsg { get; set; }
        public IDictionary<string, List<string>> errors { get; set; }
        #endregion

        #region Contructor
        public ValidateException(string errorMsg)
        {
            validateErrorMsg = errorMsg;
        }

        /// <summary>
        /// Khởi tạo
        /// </summary>
        /// <param name="errorMsg">List các lỗi khi thao tác</param>
        public ValidateException(List<string> errorMsg)
        {
            errors = new Dictionary<string, List<string>>();
            errors.Add("error: ", errorMsg);
        }
        #endregion

        #region Method
        public override string Message => this.validateErrorMsg;

        public override IDictionary Data => (IDictionary)errors;
        #endregion
    }
}
