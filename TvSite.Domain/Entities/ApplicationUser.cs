using Microsoft.AspNetCore.Identity;

namespace TvSite.Domain.Entities;

public class ApplicationUser : IdentityUser<Guid>
{
    private string _displayName;
    public string DisplayName
    {
        get { return _displayName; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException($"DisplayName cannot be null or whitespace");

            if (value.Length < ApplicationSettings.DisplayNameMinLength)
                throw new ArgumentException($"DisplayName length cannot be below {ApplicationSettings.DisplayNameMinLength}");

            if (value.Length > ApplicationSettings.DisplayNameMaxLength)
                throw new ArgumentException($"DisplayName length cannot be longer than {ApplicationSettings.DisplayNameMaxLength}");

            if (!char.IsUpper(value[0]))
                throw new ArgumentException($"First letter must be uppercase");

            _displayName = value;
        }
    }
    public string? ProfileImage { get; set; }
}