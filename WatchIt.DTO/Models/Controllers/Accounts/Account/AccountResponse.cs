using WatchIt.DTO.Models.Controllers.Genders.Gender;
using WatchIt.DTO.Models.Generics.Image;

namespace WatchIt.DTO.Models.Controllers.Accounts.Account;

public class AccountResponse
{
    public long Id { get; set; }
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public bool IsAdmin { get; set; }
    public DateTimeOffset ActiveDate { get; set; }
    public DateTimeOffset JoinDate { get; set; }
    public string? Description { get; set; }
    public GenderResponse? Gender { get; set; }
    public ImageResponse? ProfilePicture { get; set; }
}