﻿using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using SampleApp.Classic.Models.Accounts;
using SampleApp.Classic.Views;
using SampleApp.Classic.Views.Accounts;
using Simplify.Web;
using Simplify.Web.Attributes;
using Simplify.Web.Modules;
using Simplify.Web.Responses;

namespace SampleApp.Classic.Controllers.Accounts
{
	[Post("login")]
	public class LoginController : AsyncController<LoginViewModel>
	{
		public override async Task<ControllerResponse> Invoke()
		{
			await ReadModelAsync();

			if (Model.Password == "1" && Model.UserName == "Foo")
			{
				var claims = new List<Claim>
				{
					new Claim(ClaimTypes.Name, Model.UserName)
				};

				var id = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

				await Context.Context.SignInAsync(new ClaimsPrincipal(id));

				return new Redirect(RedirectionType.LoginReturnUrl);
			}

			return new Tpl(GetView<LoginView>().Get(Model, GetView<MessageBoxView>().Get(StringTable.WrongUserNameOrPassword)), StringTable.PageTitleLogin);
		}
	}
}