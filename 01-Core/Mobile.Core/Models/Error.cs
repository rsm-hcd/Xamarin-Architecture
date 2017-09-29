using Mobile.Core.Enumerations;
using Mobile.Core.Interfaces.Models;

namespace Mobile.Core.Models
{
	public class Error : IError
	{
		public ErrorType	ErrorType	{ get; set; }
		public string 		Key 		{ get; set; }
		public string 		Message 	{ get; set; }
	}
}
