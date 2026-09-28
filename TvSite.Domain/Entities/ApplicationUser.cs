using Microsoft.AspNetCore.Identity;

namespace TvSite.Domain.Entities;

public class ApplicationUser : IdentityUser
{
    private string _displayName;
    public string DisplayName{ 
        get { return _displayName; } 
        set {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException($"DisplayName cannot be null or empty", "DisplayName");

            if (value.Length < _minLength)
                throw new ArgumentException($"DisplayName length cannot be below {_minLength}", "DisplayName");

            if (value.Length > _maxLength)
                throw new ArgumentException($"DisplayName length cannot be longer than {_maxLength}", "DisplayName");

            if (!char.IsUpper(value[0]))
                throw new ArgumentException($"First letter must be uppercase", "DisplayName");

            _displayName = value;
        } }
    public string? ProfileImage{ get; set; }

    //DisplayName constraints
    private int _minLength = 3;
    private int _maxLength = 16;
}