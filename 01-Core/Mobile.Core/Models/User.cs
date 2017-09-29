﻿using System;
using Mobile.Core.Interfaces.Entities;

namespace Mobile.Core.Models
{
    public class User : IUser
    {
        public string           Email       { get; set; }
        public string           FirstName   { get; set; }
        public string           LastName    { get; set; }
        public string           CreatedBy   { get; set; }
        public DateTimeOffset?  CreatedOn   { get; set; }
        public string           DeletedBy   { get; set; }
        public DateTimeOffset?  DeletedOn   { get; set; }
        public string           Id          { get; set; }
        public string           UpdatedBy   { get; set; }
        public DateTimeOffset?  UpdatedOn   { get; set; }

        public string FullName => $"{FirstName} {LastName}";
    }
}
