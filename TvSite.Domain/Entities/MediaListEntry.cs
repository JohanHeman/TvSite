using TvSite.Domain.Enums;

namespace TvSite.Domain.Entities;

public class MediaListEntry
{
    public string Id { get; set; }
    public string MediaId { get; set; }
    public Media Media { get; set; }
    private int _listState;
    public int ListState { get { return _listState; } 
        set 
        {
            // Checks that input is not out of range
            var enumCount = ((ListStateEnum[])Enum.GetValues(typeof(ListStateEnum))).Distinct().Count();
            if (_listState > enumCount)
                throw new ArgumentOutOfRangeException($"List input is to large", "ListState");

            if (_listState < 0)
                throw new ArgumentOutOfRangeException($"List input cannot be below zero", "ListState");

            _listState = value;
        } }
    public string ApplicationUserId { get; set; }
    public ApplicationUser ApplicationUser { get; set; }
}