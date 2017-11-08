using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace AppIdentity.Controller
{
    public class PersonController : ApiController
    {
        [HttpPost]
        [ActionName("Add")]
        public IHttpActionResult Registrar([FromBody] Models.Usuario usuario)
        {
            // Default UserStore constructor uses the default connection string named: DefaultConnection
            var userStore = new UserStore<IdentityUser>();
            var manager = new UserManager<IdentityUser>(userStore);

            var user = new IdentityUser() { UserName = usuario.Nombre};
            IdentityResult result = manager.Create(user, usuario.Pass);

            if (result.Succeeded)
            {
                return Ok( string.Format("User {0} was created successfully!", user.UserName));
            }
            else
            {
                return Ok(result.Errors.FirstOrDefault());
            }
        }

        [HttpPost]
        [ActionName("find")]
        public IHttpActionResult Login([FromBody] Models.Usuario usuario )
        {
            var userStore = new UserStore<IdentityUser>();
            var userManager = new UserManager<IdentityUser>(userStore);
            var user = userManager.Find(usuario.Nombre, usuario.Pass);

            if (user != null)
            {
                var authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
                var userIdentity = userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);

                authenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = false }, userIdentity);

                string url = "http://localhost:62316/login.html";
                System.Uri uri = new System.Uri(url);
                return Ok(Redirect(uri));
            }
            else
            {
                return Ok("Invalid username or password.");
            }
        }
    }
}
