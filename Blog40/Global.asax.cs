using AutoMapper;
using Blog40.Models;
using Blog40.Repository;
using Blog40.Utilities;
using Blog40.ViewModels;
using Microsoft.Practices.Unity;
using System.Configuration;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace Blog40
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private string _databaseConnectionString;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RegisterGlobalFilters(GlobalFilters.Filters);
            ConfigureWebApi(GlobalConfiguration.Configuration);
            RegisterRoutes(RouteTable.Routes);
            ConfigureUnity();
            ConfigureAutoMapper();
        }

        private static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        private void ConfigureWebApi(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Post",
                url: "{slug}",
                defaults: new { controller = "Home", action = "Post", slug = UrlParameter.Optional }
            );
        }
        private void ConfigureUnity()
        {
            UnityContainer container = new UnityContainer();
            container.RegisterType<IRepository<Post>>(
                new InjectionFactory(c => new PostRepository(DatabaseConnectionString))
            );
            container.RegisterType<IRepository<Author>>(
                new InjectionFactory(c => new AuthorRepository(DatabaseConnectionString))
            );
            container.RegisterType<IRepository<Category>>(
                new InjectionFactory(c => new CategoryRepository(DatabaseConnectionString))
            );
            GlobalConfiguration.Configuration.DependencyResolver = new WebApiUnityDependencyResolver(container);
            DependencyResolver.SetResolver(new MvcUnityDependencyResolver(container));
        }

        private string DatabaseConnectionString
        {
            get
            {
                if (_databaseConnectionString == null)
                {
                    _databaseConnectionString = ConfigurationManager.ConnectionStrings["BlogDb"].ConnectionString;
                }
                return _databaseConnectionString;
            }
        }

        private void ConfigureAutoMapper()
        {
            Mapper.Initialize(config =>
            {
                config.CreateMap<Post, PostViewModel>();
                config.CreateMap<Post, PostViewModel>().ReverseMap();
                config.CreateMap<Post, PostEditViewModel>();
                config.CreateMap<Post, PostEditViewModel>().ReverseMap();
                config.CreateMap<Author, AuthorViewModel>();
                config.CreateMap<Author, AuthorViewModel>().ReverseMap();
                config.CreateMap<Category, CategoryViewModel>();
                config.CreateMap<Category, CategoryViewModel>().ReverseMap();
            });
        }
    }
}