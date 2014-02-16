using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServerWebApi.Data.Entity
{
    public class Parent : ICachedObj
    {
        public Guid Id { get; set; }

        //public ICollection<Child> Childs { get; set; }

        public string DeviceId { get; set; }
    }
}