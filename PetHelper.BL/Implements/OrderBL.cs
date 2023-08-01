using PetHelper.BL.Interface;
using PetHelper.Core.Interfaces;

namespace PetHelper.BL.Implements
{
    public class OrderBL : BaseBL, IOrderBL
    {
        public OrderBL(IBaseService databaseService) : base(databaseService)
        {
        }
    }
}
