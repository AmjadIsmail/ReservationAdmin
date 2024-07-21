using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Reservation_Admin.Models;

public partial class FlightMarkup
{
    public long MarkupId { get; set; }

    [Display(Name = "Markup on Adult")]
    public decimal? AdultMarkup { get; set; }
    [Display(Name = "Markup on Child")]
    public decimal? ChildMarkup { get; set; }
    [Display(Name = "Markup on Infant")]
    public decimal? InfantMarkup { get; set; }

    public bool? ApplyMarkup { get; set; }

    public string? Airline { get; set; }
    [Display(Name = "Airline Discount")]
    public decimal? DiscountOnAirline { get; set; }
    [Display(Name = "Apply Airline Discount")]
    public bool? ApplyAirlineDiscount { get; set; }

    public string? Meta { get; set; }
    [Display(Name = "Discount on Meta")]
    public decimal? DiscountOnMeta { get; set; }

    public DateTime CreatedOn { get; set; }
}
