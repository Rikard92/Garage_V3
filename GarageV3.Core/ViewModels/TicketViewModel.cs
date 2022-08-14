namespace GarageV3.Core.ViewModels
{
    using System.ComponentModel.DataAnnotations;
    public class TicketViewModel
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public string? FullName
        {
            get => $"{FirstName}, {LastName}";
        }


        [Display(Name = "Registrerings nummer")]
        public string? RegNr { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd hh:mm}")]
        [Display(Name = "Parkeringsdatum")]
        public DateTime ArrivalTime { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd hh:mm}")]
        [Display(Name = "Utcheckningsdatum")]
        public DateTime CheckOutTime { get; set; }

        [DisplayFormat(DataFormatString = @"{0:dd\:hh\:mm}", ApplyFormatInEditMode = true)]
        [Display(Name = "Parkerad tid")]
        public TimeSpan ParkTime { get; set; }

        public string? ParkTimeStr { get; set; }


        [Display(Name = "Parkeringsavgift")]
        public string? Price { get; set; }

        public string? Currency { get; set; }

    }
}
