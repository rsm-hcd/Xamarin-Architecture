using System;

namespace Mobile.Core.Interfaces.Entities
{
	public interface IEntityBase
	{
		string 			CreatedBy	{ get; set; }
		DateTimeOffset?	CreatedOn 	{ get; set; }
		string 			DeletedBy 	{ get; set; }
		DateTimeOffset? DeletedOn 	{ get; set; }
		string 			Id 		 	{ get; set; }
		string 			UpdatedBy 	{ get; set; }
		DateTimeOffset? UpdatedOn 	{ get; set; }
	}
}
