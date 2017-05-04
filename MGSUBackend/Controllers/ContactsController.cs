using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MongoDB.Bson;
using UserManagment.Application;
using UserManagment.Entities;

namespace MGSUBackend.Controllers
{
    public class ContactsController : ApiController
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
        public IHttpActionResult Get(string id)
        {
            var contact = _contactManager.GetContactById(new ObjectId(id));

            if (contact == null)
            {
                return NotFound();
            }

            return Ok(contact);
        }

        // POST: api/Contacts
        public IHttpActionResult Post([FromBody]Contact contactToCreate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(_contactManager.CreateContact(contactToCreate));
        }

        // PUT: api/Contacts/5
        public IHttpActionResult Put(string id, [FromBody]Contact contactToUpdate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var oldContact = _contactManager.GetContactById(new ObjectId(id));

            if (oldContact == null)
            {
                return NotFound();
            }

            _contactManager.UpdateContact(contactToUpdate);
            return Ok();
        }

        // DELETE: api/Contacts/5
        public IHttpActionResult Delete(string id)
        {
            var oldContact = _contactManager.GetContactById(new ObjectId(id));

            if (oldContact == null)
            {
                return NotFound();
            }

            _contactManager.DeleteContact(new ObjectId(id));

            return Ok();
        }
    }
}
