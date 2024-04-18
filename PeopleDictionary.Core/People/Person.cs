using PeopleDictionary.Core.Enums;

namespace PeopleDictionary.Core.People
{
    public class Person
    {
        public int Id { get; set; }
        public string? City { get; set; }
        public string? PersonalId { get; set; }
        public string? Name { get; set; }
        public string? Lastname { get; set; }
        public string? Image { get; set; }
        public List<RelatedPeople>? RelatedPeople { get; set; }
        public List<TelephoneNumbers>? TelNumbers { get; set; }
        public GenderEnums Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime DateOfCreate { get; set; }
        public DateTime DateOfUpdate { get; set; }


        public void EditData(int id, string name, string lastname, GenderEnums gender, string personalId, DateTime dateofBirth, string city, List<TelephoneNumbers> telNumbers)
        {
            Id = id;
            Name = name;
            Lastname = lastname;
            Gender = gender;
            PersonalId = personalId;
            DateOfBirth = dateofBirth;
            City = city;
            TelNumbers = telNumbers;
        }

        public void AddRelatedPerson(RelatedPeople relatedPerson)
        {
            RelatedPeople ??= new List<RelatedPeople>();

            RelatedPeople.Add(relatedPerson);
        }

        public void RemoveRelatedPerson(int personId)
        {
            if (RelatedPeople != null)
            {
                var relatedPerson = RelatedPeople.FirstOrDefault(rp => rp.PersonId == personId);
                if (relatedPerson != null)
                {
                    RelatedPeople.Remove(relatedPerson);
                }
            }
        }
    }

    public class TelephoneNumbers
    {
        public TelephoneNumberEnums Type { get; set; }
        public string? Number { get; set; }
    }

    public class RelatedPeople
    {
        public RelationEnums RelationType { get; set; }
        public int PersonId { get; set; }
    }
}
