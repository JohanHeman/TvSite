using TvSite.Domain.Enums;

namespace TvSite.Domain.Entities;

public class MediaListEntry
{
    public Guid Id { get; set; }
    public string MediaId { get; set; }
    public Media Media { get; set; }
    private int _listState;
    public int ListState { get { return _listState; } 
        set 
        {
            // Checks that input is not out of range
            
            var list = Enum.GetValues(typeof(ListStateEnum.ListState)).Cast<int>().ToList();

            if (!list.Contains(value))
            {
                throw new ArgumentException($"List state does not exist");
            }

            if (_listState < 0)
                throw new ArgumentOutOfRangeException($"List input cannot be below zero");

            _listState = value;
        } }
    public Guid ApplicationUserId { get; set; }
    public ApplicationUser ApplicationUser { get; set; }
}