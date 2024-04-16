using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolisDensCuraBETA.repositories.Interfaces
{
    public interface IUnitOfWork
    {
        IGenericRepositories<T> GenericRepositories<T>() where T : class;
        void Save();
    }
}
