using SolisDensCuraBETA.model;
namespace SolisDensCuraBETA.viewmodels
{
    public class PatientNotificationViewModel
    {
        public int Id { get; set; }
        public string UserId { get; set; } 
        public string Message { get; set; } 
        public DateTime Timestamp { get; set; }
        public bool IsRead { get; set; }

        public PatientNotificationViewModel()
        {

        }

        public PatientNotificationViewModel(Notification model)
        {
            Id = model.Id;
            UserId= model.UserId;
            Message = model.Message;
            Timestamp = model.Timestamp;
            IsRead = model.IsRead;
            
        }

        public Notification ConvertViewModel(PatientNotificationViewModel model)
        {
            return new Notification
            {
                Id = model.Id,
                UserId = model.UserId,
                Message = model.Message,
                Timestamp = model.Timestamp,
                IsRead = model.IsRead,
        };
        }
    }

    
}
