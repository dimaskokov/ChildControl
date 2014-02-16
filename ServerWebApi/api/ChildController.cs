using ServerWebApi.Data;
using ServerWebApi.Data.Entity;
using System.Web.Http;
using System.Linq;
using System.Collections.Generic;

namespace ServerWebApi.Controllers
{
    public class ChildController : ApiController
    {
        Repository<Child> _childRepo = new Repository<Child>();

        [HttpGet]
        public IEnumerable<Child> Get(string ParentId)
        {
            return from child in _childRepo.GetAll()
                   select child;
        }
    }
}
