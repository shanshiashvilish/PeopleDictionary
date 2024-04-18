using PeopleDictionary.Core.Enums;
using PeopleDictionary.Core.People;
using System.Text.Json.Serialization;

namespace PeopleDictionary.Core.RelatedPeople
{
    public class RelatedPerson
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public int RelatedToId { get; set; }
        public RelationEnums Type { get; set; }
        public DateTime DateOfCreate { get; set; }

        [JsonIgnore]
        public Person Person { get; set; }
        [JsonIgnore]
        public Person RelatedTo { get; set; }
    }
}
