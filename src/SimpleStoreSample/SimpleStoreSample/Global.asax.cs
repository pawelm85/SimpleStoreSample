using SimpleStoreSample.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using SimpleStoreSample.Logic;

namespace SimpleStoreSample
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            InitializeDatabase();

            // Create the administrator role and user
            RoleActions roleActions = new RoleActions();
            roleActions.CreateAdmin();

        }


        void Application_Init(object sender, EventArgs e)
        {
            Debug.Print($"{sender.ToString()} ------> {e.ToString()}");
        }

        /// <summary>
        /// Initialize database with sample data
        /// </summary>
        private void InitializeDatabase()
        {
            Database.SetInitializer(new ProductDatabaseInitializer());
            new SimpleStoreContext().Database.Initialize(true);
        }
    }
}