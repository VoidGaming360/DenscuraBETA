using SolisDensCuraBETA.viewmodels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolisDensCuraBETA.services
{
    public interface ICostService
    {
        public IEnumerable<CostViewModel> GetCostsForExistingTreatments();
    }
}
