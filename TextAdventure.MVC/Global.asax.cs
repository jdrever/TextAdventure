using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using TextAdventure.Infrastructure;
using TextAdventure.Interface;
using TextAdventure.Application;
using System.Web;
using System;

namespace TextAdventure.MVC
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var builder = new ContainerBuilder();

            // Register your MVC controllers.
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            // OPTIONAL: Register model binders that require DI.
            //builder.RegisterModelBinders(Assembly.GetExecutingAssembly());
            //builder.RegisterModelBinderProvider();

            // OPTIONAL: Register web abstractions like HttpContextBase.
            //builder.RegisterModule<AutofacWebTypesModule>();

            // OPTIONAL: Enable property injection in view pages.
            //builder.RegisterSource(new ViewRegistrationSource());
            
            // OPTIONAL: Enable property injection into action filters.
            //builder.RegisterFilterProvider();

            builder.RegisterType<Parser>().As<IParser>();
            builder.RegisterType<ActionCoordinator>().As<IActionCoordinator>();
            builder.RegisterType<CommandActioner>().As<ICommandActioner>();
            builder.RegisterType<ObjectRepository>().As<IObjectRepository>();
            builder.RegisterType<LocationRepository>().As<ILocationRepository>();
            // Set the dependency resolver to be Autofac.
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
        /**
         * for testing routes only
        public override void Init()
        {
            base.Init();
            this.AcquireRequestState += showRouteValues;
        }

        protected void showRouteValues(object sender, EventArgs e)
        {
            var context = HttpContext.Current;
            if (context == null)
                return;
            var routeData = RouteTable.Routes.GetRouteData(new HttpContextWrapper(context));
        }
        **/
    }
}
