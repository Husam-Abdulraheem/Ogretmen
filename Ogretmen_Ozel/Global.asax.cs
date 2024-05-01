using Ogretmen_Ozel.Models;
using System.Data.Entity;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Ogretmen_Ozel
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {

            using (DataBaseContext db = new DataBaseContext())
            {

                db.Database.CreateIfNotExists();
                Database.SetInitializer<DataBaseContext>(new DropCreateDatabaseIfModelChanges<DataBaseContext>());

            }




            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
