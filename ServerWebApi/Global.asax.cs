using ServerWebApi.Data;
using ServerWebApi.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ServerWebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        public static void seed()
        {
            Repository<Parent> parentRepo = new Repository<Parent>();
            Repository<Child> childRepo = new Repository<Child>();

            Guid parentId = Guid.Parse("{72697ABE-929B-490D-9A6D-441FFB836682}");

            Child child1 = new Child
            {
                Id = Guid.Parse("{32777B89-B171-4277-ACB2-F5B9B5A0A1BF}"),
                ParentId = parentId,
                FirstName = "Иван",
                LastName = "Иванов"
            };

            Child child2 = new Child
            {
                Id = Guid.Parse("{025321E4-56A7-4205-BCBD-AD9B6853DCFB}"),
                ParentId = parentId,
                FirstName = "Вера",
                LastName = "Иванова"
            };

            Parent p = new Parent()
            {
                Id = parentId,
                DeviceId = "B096EC13-195F-4CC1-9033-3DE949E74AEC",
                //Childs = { child1, child2 }
            };

            parentRepo.Add(p);
            childRepo.Add(child1);
            childRepo.Add(child2);
            
        }

        protected void Application_Start()
        {
            seed();

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
