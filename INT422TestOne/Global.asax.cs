using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AutoMapper;

namespace INT422TestOne
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            System.Data.Entity.Database.SetInitializer(new INT422TestOne.Models.Initiallizer());


            //To ViewModel classes

            Mapper.CreateMap<Models.Movie, ViewModels.MovieBase>();
            Mapper.CreateMap<Models.Movie, ViewModels.MovieFull>();
            Mapper.CreateMap<Models.Genre, ViewModels.GenreBase>();
            Mapper.CreateMap<Models.Genre, ViewModels.GenreFull>();
            Mapper.CreateMap<Models.Director, ViewModels.DirectorBase>();
            Mapper.CreateMap<Models.Director, ViewModels.DirectorFull>();

            //From ViewModel classes

            Mapper.CreateMap<ViewModels.MovieFull, Models.Movie>();
            Mapper.CreateMap<ViewModels.GenreFull, Models.Genre>();
            Mapper.CreateMap<ViewModels.DirectorFull, Models.Director>();
            Mapper.CreateMap<ViewModels.MovieBase, Models.Movie>();
            Mapper.CreateMap<ViewModels.GenreBase, Models.Movie>();
            Mapper.CreateMap<ViewModels.GenreBase, Models.Genre>();

            //other maps

            Mapper.CreateMap<ViewModels.MovieCreate, Models.Movie>();
        }
    }
}
