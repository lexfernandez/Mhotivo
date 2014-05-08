using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mhotivo.App_Data.Repositories;
using Ninject.Modules;
using Ninject.Web.Common;

namespace Mhotivo.App_Start
{
    public class InjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IRoleRepository>().To<RoleRepository>();
        }
    }
}