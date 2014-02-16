using ServerWebApi.Data;
using ServerWebApi.Data.Entity;
using System.Web.Http;
using System.Linq;

namespace ServerWebApi.Controllers
{
    public class ParentController : ApiController
    {
        Repository<Parent> _parentRepo = new Repository<Parent>();

        [HttpGet]
        public Parent Get(string DeviceId)
        {
            var parent = (from p in _parentRepo.GetAll()
                         where p.DeviceId == DeviceId
                         select p).FirstOrDefault();
            return parent;
        }
    }
}
