namespace PetHelper.BL.Interface
{
    public interface ICommonBL
    {
        /// <summary>
        /// Tìm kiếm trên header trang web
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public Task<object> SearchGlobal(string searchValue);
    }
}
