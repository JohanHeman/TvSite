using System.ComponentModel.DataAnnotations;

namespace TvSite.Domain.Entities;

public class Comment
{
    public string Id { get; set; }
    private string _text;
    public string Text
    {
        get { return _text; }
        set
        {
            if (string.IsNullOrEmpty(value))
                throw new Exception("The text can not be empty");
            _text = value;
        }
    }
    
    public string ApplicationUserId { get; set; }
    public ApplicationUser ApplicationUser{ get; set; }
    public string MediaId{ get; set; }
    public Media Media { get; set; }
    
    public Comment()
    {
        
    }
}