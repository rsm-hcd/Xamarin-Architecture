using System;
using System.Collections.Generic;
using System.Linq;
using Mobile.Core.Interfaces.Entities;
using Realms;

namespace Mobile.Services.Realm.Interfaces
{
	public interface IRepositoryService : IDisposable
	{
        void ConfigureInstance();
		void AddOrUpdate<T>(T item) where T : RealmObject;
		void AddOrUpdate<T>(IEnumerable<T> items) where T : RealmObject;
		void Write(Action writeAction);
		T Query<T>(long id) where T : RealmObject;
		T Query<T>(string id) where T : RealmObject;
		RealmObject Query(string className, long id);
		RealmObject Query(string className, string id);
		IQueryable<dynamic> QueryAll(string className);
		IQueryable<T> QueryAll<T>(bool includeDeleted = false) where T : RealmObject, IEntityBase;
		void RemoveAll();
		void RemoveAll(string className);
		void RemoveAll<T>() where T : RealmObject;
        void RemoveAll<T>(bool isSoft = true) where T : RealmObject, IEntityBase;
		void Remove<T>(T item) where T : RealmObject;
        void Remove<T>(T item, bool isSoft = true) where T : RealmObject, IEntityBase;
		void RemoveRange<T>(IQueryable<T> range) where T : RealmObject;
		void Refresh();
		void DeleteRealm();
    }
}
