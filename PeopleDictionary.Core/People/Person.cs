using PeopleDictionary.Core.Cities;
using PeopleDictionary.Core.Enums;
using PeopleDictionary.Core.RelatedPeople;

namespace PeopleDictionary.Core.People
{
    public class Person
    {
        public int Id { get; set; }
        public int? CityId { get; set; }
        public string? PersonalId { get; set; }
        public string? Name { get; set; }
        public string? Lastname { get; set; }
        public string? Image { get; set; }
        public List<RelatedPerson>? Relations { get; set; }
        public List<TelephoneNumbers>? TelNumbers { get; set; }
        public GenderEnums Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime DateOfCreate { get; set; }
        public DateTime DateOfUpdate { get; set; }

        public City? City { get; set; }


        public void EditData(int id, string name, string lastname, GenderEnums gender, string personalId, DateTime dateofBirth, City city, List<TelephoneNumbers>? telNumbers)
        {
            Id = id;
            Name = name;
            Lastname = lastname;
            Gender = gender;
            PersonalId = personalId;
            DateOfBirth = dateofBirth;
            TelNumbers = telNumbers;
            City = city;
            CityId = city.Id;
        }

        public void AddRelation(RelatedPerson relation)
        {
            Relations ??= new List<RelatedPerson>();

            if (Relations.Any(rp => rp.RelatedToId == relation.RelatedToId && rp.PersonId == Id && rp.Type == relation.Type))
            {
                // Relation already exists, handle this case accordingly
                return;
            }

            var newRelation = new RelatedPerson
            {
                Person = this,
                Type = relation.Type,
                RelatedTo = relation.RelatedTo,
            };

            Relations.Add(newRelation);
        }

        public void RemoveRelation(int relatedToId)
        {
            if (Relations != null)
            {
                var relatedPerson = Relations.FirstOrDefault(rp => rp.PersonId == Id && rp.RelatedToId == relatedToId);
                if (relatedPerson != null)
                {
                    Relations.Remove(relatedPerson);
                }
            }
        }
    }

    public class TelephoneNumbers
    {
        public TelephoneNumberEnums Type { get; set; }
        public string? Number { get; set; }
    }
}
