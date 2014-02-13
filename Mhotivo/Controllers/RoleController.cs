using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mhotivo.App_Data;
using Mhotivo.App_Data.Repositories;
using Mhotivo.Models;

namespace Mhotivo.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleRepository repository = new RoleRepository(new MhotivoContext());
                //
        // GET: /Role/

        [AllowAnonymous]
        public ActionResult Index()
        {
            return View(repository.Query( x => x));
        }

        //
        // GET: /Role/

        [HttpGet]
        public ActionResult Edit(long id)
        {
            var role = repository.GetById(id);
            return View("_Edit", role);
        }

        [HttpPost]
        public JsonResult Edit(Role modelRole)
        {
            try
            {
                repository.Update(modelRole);
            }
            catch (Exception e)
            {
                return this.Json(new { success = false, message = "Algo no funciono correctamente y los cambios no fueron aplicados!" });
            }
            return this.Json(new { success = true, message = string.Empty });
        }
    }
}
