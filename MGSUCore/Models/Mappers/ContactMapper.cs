using Common.Entities;
using MGSUBackend.Models;

namespace MGSUCore.Models.Mappers
{
    public class ContactMapper
    {
        public static ContactModel ContactToContactModel(Contact contact)
        {
            if (contact == null) return null;

            return new ContactModel
            {
                Id = contact.Id.ToString(),
                FirstName = contact.FirstName,
                LastName = contact.LastName,
                MiddleName = contact.MiddleName,
                Description = contact.Description,
                Team = contact.Team,
                Img = contact.Img == null
                    ? null
                    : new ImageModel
                    {
                        Original = contact.Img.Original.ToString(),
                        Small = contact.Img.Small.ToString(),
                        Role = contact.Img.Role
                    },
                CreatingDate = contact.CreatingDate.ToString()
            };
        }

        public static Contact ContactModelToContact(ContactModel contactModel)
        {
            if (contactModel == null) return null;

            return new Contact
            {
                FirstName = contactModel.FirstName,
                LastName = contactModel.LastName,
                MiddleName = contactModel.MiddleName,
                Description = contactModel.Description,
                Team = contactModel.Team,
                Img = contactModel.Img == null
                    ? null
                    : new Image
                    {
                        Original = contactModel.Img.Original,
                        Small = contactModel.Img.Small,
                        Role = contactModel.Img.Role
                    }
            };
        }
    }
}