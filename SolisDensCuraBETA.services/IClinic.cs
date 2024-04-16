using SolisDensCuraBETA.utilities;
using SolisDensCuraBETA.viewmodels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolisDensCuraBETA.services
{
    public interface IClinic
    {
        PagedResult<ClinicViewModel> GetAll(int pageNumber, int pageSize);

        ClinicViewModel GetById(int ClinicId);

        void UpdateClinic(ClinicViewModel clinic);
        void InsertClinic(ClinicViewModel clinic);
        void DeleteClinic(int id);

        IEnumerable GetAll();
    }
}
