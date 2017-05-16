using Common.Entities;
using MongoDB.Bson;

namespace DonationManagment.Application
{
    public interface IDonationManager
    {
        Donation GetDonation(ObjectId donationId);
        ObjectId CreateDonation(Donation donationToCreate);
        void ConfirmDonation(Donation donationToConfirm);

        void UpdateDonation(Donation donationToUpdate);
        void DeleteDonation(ObjectId donationToDeleteId);
    }
}