using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Owin;
using Owin;

[assembly : OwinStartup(typeof(TSMS_Atp_2_Final_Project.StartUp))]

namespace TSMS_Atp_2_Final_Project
{
    public class StartUp
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}