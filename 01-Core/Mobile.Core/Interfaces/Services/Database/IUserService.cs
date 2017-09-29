using System.Collections.Generic;
using Mobile.Core.Interfaces.Entities;
using Mobile.Core.Models;

namespace Mobile.Core.Interfaces.Services.Database
{
	public interface IUserService
	{
		void            Delete(string id, bool isSoft = true);
		void            DeleteAll(bool isSoft = true);
        List<User>      GetAll(bool includeDeleted = false);
        User            GetByEmail(string email);
        void            Save(User user);
	}
}
