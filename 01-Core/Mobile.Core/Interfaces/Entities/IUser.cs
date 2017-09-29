namespace Mobile.Core.Interfaces.Entities
{
	public interface IUser : IEntityBase
	{
		string	Email 		{ get; set; }
		string 	FirstName 	{ get; set; }
		string 	LastName 	{ get; set; }
	}
}
