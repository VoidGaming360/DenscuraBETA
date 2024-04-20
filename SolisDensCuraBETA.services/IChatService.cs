using SolisDensCuraBETA.model;
using SolisDensCuraBETA.viewmodels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolisDensCuraBETA.services
{
    public interface IChatService
    {

        Task SendMessageAsync(string senderId, string receiverId, string message);

    }
}
