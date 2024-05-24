using SolisDensCuraBETA.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolisDensCuraBETA.viewmodels
{
    public class ConversationViewModel
    {
        public ApplicationUser OtherUser { get; set; }
        public IEnumerable<ChatMessage> Messages { get; set; }
    }

}
