

using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using MovieDemo.Models;
using MovieDomain.Entities;
using MovieServices.Interfaces;
using MovieServices.Models;
using System.Net.Http;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using System;
namespace MovieDemo.ApiControllers
{
    public class AccountController : ApiController
    {
        private readonly IAuthService _authService;
        public AccountController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        [Route("logout")]
        [Authorize]
        public IHttpActionResult Logout()
        {
            LogOut();
            string urlBase = Request.RequestUri.GetLeftPart(UriPartial.Authority);
            return Redirect(urlBase);
        }

        // POST api/<controller>
        public async Task<IHttpActionResult> Register([FromBody] RegistrationBindingModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest(this.ModelState);
            }
            IdentityResult result = await _authService.Register(model);
            if (result.Succeeded)
            {
                return Ok();
            }
            else
            {
                return GetErrorResult(result);
            }
        }


        //
        // POST: /Account/Login

        [HttpPost]
        [Route("Account/Login")]
        public async Task<IHttpActionResult> Login([FromBody] LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _authService.FindAsync(model.UserName, model.Password);
                if (user != null)
                {
                    SignIn(user);
                    string urlBase = Request.RequestUri.GetLeftPart(UriPartial.Authority);
                    return Redirect(urlBase);
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or password.");
                    return BadRequest(ModelState);
                }
            }
            return BadRequest(ModelState);
        }

        
        
        #region HelperMethods
        private IAuthenticationManager Authentication 
        { 
            get 
            {
                return this.Request.GetOwinContext().Authentication;
            }
        }
        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }

        private void SignIn(User user)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, user.UserName));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString(), ClaimValueTypes.Integer));
            var id = new ClaimsIdentity(claims,
                                        DefaultAuthenticationTypes.ApplicationCookie);
            Authentication.SignIn(new AuthenticationProperties
                {
                    AllowRefresh = true,
                    IsPersistent = false,
                    ExpiresUtc = DateTime.UtcNow.AddDays(7)
                }, id);
        }

        private void LogOut()
        {
            var ctx = Request.GetOwinContext();
            var authenticationManager = ctx.Authentication;
            authenticationManager.SignOut();
        }

       
        #endregion
    }
}