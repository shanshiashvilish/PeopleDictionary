using PeopleDictionary.Core.Enums;
using PeopleDictionary.Core.People;

namespace PeopleDictionary.Core.RelatedPeople
{
    public class RelatedPerson
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public RelationEnums RelationType { get; set; }
        public DateTime DateOfCreate { get; set; }
        
        public Person Person { get; set; }
    }
}
