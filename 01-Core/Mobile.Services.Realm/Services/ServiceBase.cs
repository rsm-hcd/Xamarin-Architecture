using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Mobile.Core.Interfaces.Entities;
using Mobile.Core.Interfaces.Services.Database;
using Mobile.Services.Realm.Services;

namespace Mobile.Services.Realm
{
	public abstract class ServiceBase
	{
		#region Protected Members

		protected readonly IMapper _mappingEngine;
		protected readonly RepositoryService _repository;

		#endregion Protected Members

		#region Constructor

		protected ServiceBase(IMapper mappingEngine)
		{
            _mappingEngine = mappingEngine;
            _repository = new RepositoryService();
		}

		#endregion Constructor

		#region Protected Methods

		protected void Delete<TRealm>(string id, bool isSoft = true) 
            where TRealm : Realms.RealmObject, IEntityBase
		{
			var user = _repository.Query<TRealm>(id);
			_repository.Remove<TRealm>(user, isSoft);
		}

		protected void DeleteAll<TRealm>(bool isSoft = true)
            where TRealm : Realms.RealmObject, IEntityBase
		{
			_repository.RemoveAll<TRealm>(isSoft);
		}

		protected IQueryable<TModel> GetAll<TModel, TRealm>(bool includeDeleted = false)
			where TModel : IEntityBase
			where TRealm : Realms.RealmObject, IEntityBase
		{
			var models = new List<TModel>();
			var realmEntities = _repository.QueryAll<TRealm>(includeDeleted);
			if (realmEntities == null)
			{
				return models.AsQueryable();
			}
			foreach (var e in realmEntities)
			{
				models.Add(_mappingEngine.Map<TModel>(e));
			}
			return models.AsQueryable();
		}

		protected IEntityBase GetById<TRealm, TModel>(string id)
            where TRealm : Realms.RealmObject, IEntityBase
            where TModel : IEntityBase
		{
			var realmEntity = _repository.Query<TRealm>(id);
			return _mappingEngine.Map<TModel>(realmEntity);
		}

		protected void Save<TRealm>(IEntityBase entity)
            where TRealm : Realms.RealmObject, IEntityBase
		{
			var realmEntity = _mappingEngine.Map<TRealm>(entity);
			_repository.AddOrUpdate<TRealm>(realmEntity);
		}

		#endregion Protected Methods
	}
}
