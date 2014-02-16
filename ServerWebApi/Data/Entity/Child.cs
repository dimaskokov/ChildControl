using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServerWebApi.Data.Entity
{
    public class Child : ICachedObj
    {
        public Guid Id { get; set; }

        public Guid ParentId { get; set; }
        public Parent Parent { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}