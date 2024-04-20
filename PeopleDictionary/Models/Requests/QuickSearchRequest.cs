
namespace PeopleDictionary.Api.Models.Requests
{
    public class QuickSearchRequest
    {
        public string? Name { get; set; }
        public string? Lastname { get; set; }
        public string? PersonalId { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
