using System;
using System.Collections.Generic;

namespace Reservation_Admin.Models;

public partial class AvailibilityResult
{
    public long ResultId { get; set; }

    public string? Request { get; set; }

    public string? Response { get; set; }

    public int? UserId { get; set; }

    public int? TotalResults { get; set; }

    public DateTime? CreatedOn { get; set; }
}
