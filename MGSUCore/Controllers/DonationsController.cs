using System;
using DonationManagment.Application;
using Common.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MGSUCore.Models;
using MGSUCore.Models.Mappers;
using MGSUCore.Filters;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MGSUCore.Controllers
{

    [Authorize("Admin")]
    [Route("[controller]")]
    [CustomExceptionFilterAttribute]
    public class DonationsController : Controller
    {
        private readonly IDonationManager _donationManager;

        public DonationsController(IDonationManager donationManager)
        {
            _donationManager = donationManager;
        }

        // GET: api/values
        [HttpGet]
        public IActionResult GetAllDonations()
        {
            throw new NotImplementedException();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult GetDonationbyId(string id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if(!ObjectId.TryParse(id, out var objectId))
                return BadRequest("'Id' parameter is ivalid ObjectId");

            var donationToReturn = _donationManager.GetDonation(objectId);

            if (donationToReturn == null)
                return NotFound();

            return Ok(DonationMapper.DonationToDonationModel(donationToReturn));
        }

        // POST api/values
        [HttpPost]
        public IActionResult CreateDonation([FromBody]DonationModel donationModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var postToCreate = DonationMapper.DonationModelToDonation(donationModel);

            return Ok(_donationManager.CreateDonation(postToCreate).ToString());
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody]DonationModel donationModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if(!ObjectId.TryParse(id, out var objectId))
                return BadRequest("'Id' parameter is ivalid ObjectId");

            var oldDonation = _donationManager.GetDonation(objectId);

            if (oldDonation == null)
                return NotFound();

            oldDonation.UserId = donationModel.UserId == null ? new ObjectId(donationModel.UserId) : oldDonation.UserId;
            oldDonation.ProjectId = donationModel.ProjectId == null ? new ObjectId(donationModel.ProjectId) : oldDonation.ProjectId;
            oldDonation.Value = donationModel.Value == 0 ? oldDonation.Value : donationModel.Value;
            oldDonation.Date = donationModel.Date ?? oldDonation.Date;
            oldDonation.Recursive = donationModel.Recursive;
            oldDonation.Confirmed = donationModel.Confirmed;

            _donationManager.UpdateDonation(oldDonation);
            return Ok();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            if(!ObjectId.TryParse(id, out var objectId))
                return BadRequest("'Id' parameter is ivalid ObjectId");

            var oldDonation = _donationManager.GetDonation(objectId);

            if (oldDonation == null)
                return NotFound();

            _donationManager.DeleteDonation(objectId);
            return Ok();
        }
    }
}
