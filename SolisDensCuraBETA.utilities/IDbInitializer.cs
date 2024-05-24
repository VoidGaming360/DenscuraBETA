using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolisDensCuraBETA.utilities
{
    public interface IDbInitializer
    {
        Task InitializeAsync();
    }
}
