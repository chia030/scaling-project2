using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using WebSearch.Models;
using WebSearch.Services;

public class AuthorizationController : Controller
{
    private readonly IAuthenticateService _authenticateService;

    public AuthorizationController(IAuthenticateService authenticationService)
    {
        _authenticateService = authenticationService;
    }

    public IActionResult Login()
    {
        return View();
    }

    //[HttpPost]
    //public async Task<IActionResult> Login(LoginViewModel model)
    //{
    //    if (ModelState.IsValid)
    //    {
    //        bool isAuthenticated = await _authenticateService.AuthenticateAsync(model.Username, model.Password);

    //        if (isAuthenticated)
    //        {
    //            Generate JWT token and store it in the authentication cookie.
    //           var token = "..."; // your JWT token here
    //            var authProperties = new AuthenticationProperties
    //            {
    //                IsPersistent = true
    //            };

    //            await HttpContext.SignInAsync("AuthenticationScheme", new ClaimsPrincipal(), authProperties);

    //            return RedirectToAction("Index", "Home");
    //        }
    //        else
    //        {
    //            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
    //        }
    //    }

    //    return View(model);
    //}

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            // Assign the result of AuthenticateAsync to a variable
            var authenticationResult = await _authenticateService.AuthenticateAsync(model.Username, model.Password);

            if (authenticationResult.isAuthenticated)
            {
                // Use the token from the tuple
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true,
                    Items = { { "access_token", authenticationResult.token } } // store the token in the authentication properties
                };

                await HttpContext.SignInAsync("AuthenticationScheme", new ClaimsPrincipal(), authProperties);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }
        }

        return View(model);
    }

}