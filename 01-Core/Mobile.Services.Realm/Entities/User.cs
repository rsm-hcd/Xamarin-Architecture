using Mobile.Core.Interfaces.Entities;
using Realms;
using System;

namespace Mobile.Services.Realm
{
	public class User : RealmObject, IUser
	{
		public string			CreatedBy { get; set; }
		public DateTimeOffset? 	CreatedOn { get; set; }
		public string 			DeletedBy { get; set; }
		public DateTimeOffset? 	DeletedOn { get; set; }
		public string 			Email { get; set; }
		public string 			FirstName { get; set; }
		[PrimaryKey]
		public string 			Id { get; set; }
		public string 			LastName { get; set; }
		public string 			UpdatedBy { get; set; }
		public DateTimeOffset? 	UpdatedOn { get; set; }
	}
}
