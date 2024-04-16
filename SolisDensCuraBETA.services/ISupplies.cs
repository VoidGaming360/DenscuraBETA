using SolisDensCuraBETA.utilities;
using SolisDensCuraBETA.viewmodels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolisDensCuraBETA.services
{
    public interface ISupplies
    {
        PagedResult<SuppliesViewModel> GetAll(int pageNumber, int pageSize);

        SuppliesViewModel GetById(int SupplyId);

        void UpdateSupplies(SuppliesViewModel Supply);
        void InsertSupplies(SuppliesViewModel Supply);
        void DeleteSupplies(int id);
    }
}

