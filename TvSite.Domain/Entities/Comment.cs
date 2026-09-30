using System.ComponentModel.DataAnnotations;

namespace TvSite.Domain.Entities;

public class Comment
{
    public Guid Id { get; set; }
    private string _text;
    public string Text
    {
        get =>  _text;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Text cannot be null or whitespace");
            _text = value;
        }
    }
    
    public Guid ApplicationUserId { get; set; }
    public ApplicationUser ApplicationUser{ get; set; }
    public string MediaId{ get; set; }
    public Media Media { get; set; }
    
}