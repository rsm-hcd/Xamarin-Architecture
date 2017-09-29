using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Mobile.Core.Interfaces.Services.Database;

namespace Mobile.Services.Realm
{
    public class UserService : ServiceBase, IUserService
	{
		#region Constructor

		public UserService(IMapper mappingEngine) : base(mappingEngine)
		{
		}

		#endregion Constructor

		#region IUserService Implementation

		public void Delete(string id, bool isSoft = true)
		{
            Delete<User>(id, isSoft);
		}

		public void DeleteAll(bool isSoft = true)
		{
			DeleteAll<User>();
		}

		public List<Core.Models.User> GetAll(bool includeDeleted = false)
		{
            var users = GetAll<Core.Models.User, User>(includeDeleted);
            return users.ToList();
		}

		public Core.Models.User GetByEmail(string email)
		{
			var realmUser = _repository.QueryAll<User>().Where(u => u.Email == email).FirstOrDefault();
			return _mappingEngine.Map<Core.Models.User>(realmUser);
		}

		public void Save(Core.Models.User entity)
		{
			Save<User>(entity);
		}

		#endregion IUserService Implementation
	}
}
