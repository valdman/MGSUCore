using System;
using Common.Entities;
using MongoDB.Bson;

namespace MGSUCore.Models.Mappers
{
    public static class DonationMapper
    {
        public static Donation DonationModelToDonation(SaveDonationModel donationModel)
        {
            if(donationModel == null)
                return null;
            
            return new Donation
            {
                UserId = new MongoDB.Bson.ObjectId(donationModel.UserId),
                ProjectId = new MongoDB.Bson.ObjectId(donationModel.ProjectId),
                Value = donationModel.Value,
                Recursive = donationModel.Recursive,
                Confirmed = donationModel.Confirmed
            };
        }

        public static SaveDonationModel DonationToDonationModel(Donation donation)
		{
			if (donation == null)
				return null;
            
			return new SaveDonationModel
			{
                UserId = donation.UserId.ToString(),
                ProjectId = donation.ProjectId.ToString(),
				Value = donation.Value,
				Date = donation.Date?.ToString(),
				Recursive = donation.Recursive,
				Confirmed = donation.Confirmed,
                CreatingDate = donation.CreatingDate?.ToString()
			};
		}
    }
}
