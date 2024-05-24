using SolisDensCuraBETA.repositories.Interfaces;
using SolisDensCuraBETA.utilities;
using SolisDensCuraBETA.viewmodels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolisDensCuraBETA.services.Interface
{
    public interface IDentistService
    {
        PagedResult<TimingViewModel> GetAll(int pageNumber, int pageSize);

        IEnumerable<TimingViewModel> GetAll();
        TimingViewModel GetTimingById(int TimingId);

        void UpdateTiming(TimingViewModel timing);
        void AddTiming(TimingViewModel timing);
        void DeleteTiming(int TimingId);

    }
}
