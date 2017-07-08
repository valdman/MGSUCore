﻿using System.Collections.Generic;
using MGSUBackend.Authentification;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using UserManagment.Application;
using Common.Entities;
using Microsoft.AspNetCore.Authorization;
using MGSUCore.Filters;
using System.Linq;

namespace MGSUCore.Controllers
{
    [CustomExceptionFilterAttribute]
    [Route("[controller]")]
    public class ContactsController : Controller
    {
        private readonly IContactManager _contactManager;

        public ContactsController(IContactManager contactManager)
        {
            _contactManager = contactManager;
        }

        // GET: api/Contacts
        [HttpGet]
        public IActionResult Get()
        {
            if (!Request.Query.TryGetValue("team", out Microsoft.Extensions.Primitives.StringValues value))
            {
                return Ok(_contactManager.GetContactByPredicate());
            }

            if (value.Count > 1)
            {
                return BadRequest("Query bad");
            }

            var team = value.Single();

            return
                Ok(_contactManager.GetContactByPredicate(contact => contact.Team == team));
        }

        // GET: api/Contacts/5
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            var contact = _contactManager.GetContactById(new ObjectId(id));

            if (contact == null)
                return NotFound();

            return Ok(contact);
        }

        // POST: api/Contacts
        [HttpPost]
        [Authorize("Admin")]
        public IActionResult Post([FromBody] Contact contactToCreate)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(_contactManager.CreateContact(contactToCreate));
        }

        // PUT: api/Contacts/5
        [HttpPut("{id}")]
        [Authorize("Admin")]
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
        [HttpDelete("{id}")]
        [Authorize("Admin")]
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