using System;
using System.Threading.Tasks;
using UserManagment.Entities;

namespace UserManagment.Application
{
    public interface IAuthorizer
    {
		User FindAsync(string email, string plainPassword);

		void SignInAsync(User user, bool isPersistent);
        void SignOut();
    }
}
