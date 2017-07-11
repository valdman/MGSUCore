using System;
using Common.Entities;
using MongoDB.Bson;

namespace MGSUCore.Models.Mappers
{
    public static class DonationMapper
    {
        public static Donation DonationModelToDonation(DonationModel donationModel)
        {
            if(donationModel == null)
                return null;
            
            return new Donation
            {
                UserId = new MongoDB.Bson.ObjectId(donationModel.UserId),
                ProjectId = new MongoDB.Bson.ObjectId(donationModel.ProjectId),
                Value = donationModel.Value,
                Date = donationModel.Date,
                Recursive = donationModel.Recursive,
                Confirmed = donationModel.Confirmed
            };
        }

        public static DonationModel DonationToDonationModel(Donation donation)
		{
			if (donation == null)
				return null;
            
			return new DonationModel
			{
                UserId = donation.UserId.ToString(),
                ProjectId = donation.ProjectId.ToString(),
				Value = donation.Value,
				Date = donation.Date,
				Recursive = donation.Recursive,
				Confirmed = donation.Confirmed,
                CreatingDate = donation.CreatingDate.ToString()
			};
		}
    }
}
