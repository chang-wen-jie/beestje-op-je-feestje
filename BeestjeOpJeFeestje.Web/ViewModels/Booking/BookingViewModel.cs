﻿using System.ComponentModel.DataAnnotations;

namespace BeestjeOpJeFeestje.Web.ViewModels.Booking
{
    public class BookingViewModel
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Datum is verplicht")]
        public DateTime Date { get; set; }
        
        public int? AccountId { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal DiscountAmount { get; set; }
        public bool IsConfirmed { get; set; }
    }
}
