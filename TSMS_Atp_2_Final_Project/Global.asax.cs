using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Data.Entity;
using TSMS_Atp_2_Final_Project.Models.Com.Tsms;
using System.Web.Optimization;
using TSMS_Atp_2_Final_Project.App_Start;

namespace TSMS_Atp_2_Final_Project
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Database.SetInitializer(new DBSeeder());
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBuldles(BundleTable.Bundles);
            TsmsDBContext db = new TsmsDBContext();
            db.Users.ToList();
        }
    }
}
