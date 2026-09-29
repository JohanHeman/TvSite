using System.ComponentModel.DataAnnotations;

namespace TvSite.Domain.Entities;

public class Comment
{
    public string Id { get; set; }
    private string _text;
    public string Text
    {
        get =>  _text;
        set
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException("Text cannot be null or empty");
            _text = value;
        }
    }
    
    public string ApplicationUserId { get; set; }
    public ApplicationUser ApplicationUser{ get; set; }
    public string MediaId{ get; set; }
    public Media Media { get; set; }
    
}