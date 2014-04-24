using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Mhotivo.App_Data;
using Mhotivo.App_Data.Repositories;
using Mhotivo.Models;

namespace Mhotivo
{
    // Nota: para obtener instrucciones sobre cómo habilitar el modo clásico de IIS6 o IIS7, 
    // visite http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<MhotivoContext>()); 
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();

            //REPOST INSTANCES
            var ctx = new MhotivoContext();

            AcademicYearRepository.SetInstance(ctx);
            BenefactorRepository.SetInstance(ctx);
            ContactInformationRepository.SetInstance(ctx);
            CourseRepository.SetInstance(ctx);
            AppointmentDiaryRepository.SetInstance(ctx);
            EnrollRepository.SetInstance(ctx);
            EventRepository.SetInstance(ctx);
            GradeRepository.SetInstance(ctx);
            MeisterRepository.SetInstance(ctx);
            ParentRepository.SetInstance(ctx);
            PensumRepository.SetInstance(ctx);
            PeopleRepository.SetInstance(ctx);
            RoleRepository.SetInstance(ctx);
            StudentRepository.SetInstance(ctx);
            UserRepository.SetInstance(ctx);


        }
    }
}