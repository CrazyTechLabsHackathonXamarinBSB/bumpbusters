using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Web.Http;
using BumpBuster.Api.DataObjects;
using BumpBuster.Api.Models;
using Microsoft.WindowsAzure.Mobile.Service;

namespace BumpBuster.Api
{
    public static class WebApiConfig
    {
        public static void Register()
        {
            // Use this class to set configuration options for your mobile service
            ConfigOptions options = new ConfigOptions();

            // Use this class to set WebAPI configuration options
            HttpConfiguration config = ServiceConfig.Initialize(new ConfigBuilder(options));

            // To display errors in the browser during development, uncomment the following
            // line. Comment it out again when you deploy your service for production use.
            // config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;

            Database.SetInitializer(new MobileServiceInitializer());
        }
    }

    public class MobileServiceInitializer : DropCreateDatabaseIfModelChanges<MobileServiceContext>
    {
        protected override void Seed(MobileServiceContext context)
        {
            //List<Bump> todoItems = new List<Bump>
            //{
            //    new Bump { Id = Guid.NewGuid().ToString(), Text = "First item", Complete = false },
            //    new Bump { Id = Guid.NewGuid().ToString(), Text = "Second item", Complete = false },
            //};

            //foreach (Bump todoItem in todoItems)
            //{
            //    context.Set<Bump>().Add(todoItem);
            //}

            base.Seed(context);
        }
    }
}

