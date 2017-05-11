using System.Collections.Generic;
using MGSUBackend.Authentification;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using UserManagment.Application;
using UserManagment.Entities;

namespace MGSUCore.Controllers
{
    public class ContactsController : Controller
    {
        private readonly IContactManager _contactManager;

        public ContactsController(IContactManager contactManager)
        {
            _contactManager = contactManager;
        }

        // GET: api/Contacts
        public IEnumerable<Contact> Get()
        {
            return _contactManager.GetContactByPredicate();
        }

        // GET: api/Contacts/5
        public IActionResult Get(string id)
        {
            var contact = _contactManager.GetContactById(new ObjectId(id));

            if (contact == null)
                return NotFound();

            return Ok(contact);
        }

        // POST: api/Contacts
        [Authorization(UserRole.Admin)]
        public IActionResult Post([FromBody] Contact contactToCreate)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(_contactManager.CreateContact(contactToCreate));
        }

        // PUT: api/Contacts/5
        [Authorization(UserRole.Admin)]
        public IActionResult Put(string id, [FromBody] Contact contactToUpdate)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var oldContact = _contactManager.GetContactById(new ObjectId(id));

            if (oldContact == null)
                return NotFound();

            oldContact.Description = contactToUpdate.Description ?? oldContact.Description;
            oldContact.FirstName = contactToUpdate.FirstName ?? oldContact.FirstName;
            oldContact.LastName = contactToUpdate.LastName ?? oldContact.LastName;
            oldContact.MiddleName = contactToUpdate.MiddleName ?? oldContact.MiddleName;
            oldContact.Img = contactToUpdate.Img ?? oldContact.Img;
            oldContact.Team = contactToUpdate.Team ?? oldContact.Team;

            _contactManager.UpdateContact(oldContact);
            return Ok();
        }

        // DELETE: api/Contacts/5
        [Authorization(UserRole.Admin)]
        public IActionResult Delete(string id)
        {
            var oldContact = _contactManager.GetContactById(new ObjectId(id));

            if (oldContact == null)
                return NotFound();

            _contactManager.DeleteContact(new ObjectId(id));

            return Ok();
        }
    }
}