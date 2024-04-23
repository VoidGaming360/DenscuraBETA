using SolisDensCuraBETA.utilities;
using SolisDensCuraBETA.viewmodels;

namespace SolisDensCuraBETA.services
{
    public interface IApplicationUserService
    {
        PagedResult<ApplicationUserViewModel> GetAll(int pageNumber, int pageSize);

        PagedResult<ApplicationUserViewModel> GetAllDentist(int pageNumber, int pageSize);

        PagedResult<ApplicationUserViewModel> GetAllPatient(int pageNumber, int pageSize);
        PagedResult<ApplicationUserViewModel> Search(int pageNumber, int pageSize, string Specialist);

    }
}