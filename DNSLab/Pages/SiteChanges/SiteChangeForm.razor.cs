using DNSLab.DTOs.Pages;

namespace DNSLab.Pages.SiteChanges;
partial class SiteChangeForm
{
    DateTimeOffset? selectedDate
    {
        get
        {
            if (ChangeLog.ReleaseDate == default)
                return DateTime.UtcNow;
            return DateTimeOffset.Parse(ChangeLog.ReleaseDate.ToString());
        }
        set
        {
            if (value.HasValue)
                ChangeLog.ReleaseDate = value.Value.LocalDateTime;
        }
    }

    [Parameter] public string Title { get; set; } = String.Empty;
    [Parameter] public ChangeLogDTO ChangeLog { get; set; } = null;
    [Parameter] public EventCallback OnValidSubmit { get; set; }
}
