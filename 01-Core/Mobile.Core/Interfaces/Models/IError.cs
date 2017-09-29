﻿using Mobile.Core.Enumerations;

namespace Mobile.Core.Interfaces.Models
{
	public interface IError
	{
		ErrorType	ErrorType	{ get; set; }
		string 		Key 		{ get; set; }
		string 		Message 	{ get; set; }
	}
}
