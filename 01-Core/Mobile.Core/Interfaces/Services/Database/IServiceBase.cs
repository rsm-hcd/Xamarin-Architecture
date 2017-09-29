using Mobile.Core.Interfaces.Entities;
using System.Linq;
using System.Collections.Generic;

namespace Mobile.Core.Interfaces.Services.Database
{
	public interface IServiceBase
	{
		IQueryable<IEntityBase> GetAll(bool includeDeleted = false);
		IEntityBase 			GetById(string id);
		void 				    Save(IEntityBase entity);
		void 				    Delete(string id, bool isSoft = true);
		void 				    DeleteAll(bool isSoft = true);
	}
}
