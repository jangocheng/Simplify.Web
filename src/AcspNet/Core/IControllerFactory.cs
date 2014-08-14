﻿using System;
using Microsoft.Owin;
using Simplify.DI;

namespace AcspNet.Core
{
	/// <summary>
	/// Represent controller factory
	/// </summary>
	public interface IControllerFactory
	{
		/// <summary>
		/// Creates the controller.
		/// </summary>
		/// <param name="containerProvider">The DI container provider.</param>
		/// <param name="controllerType">Type of the controller.</param>
		/// <param name="context">The context.</param>
		/// <param name="routeParameters">The route parameters.</param>
		/// <returns></returns>
		IController CreateController(IDIContainerProvider containerProvider, Type controllerType, IOwinContext context, dynamic routeParameters = null);
	}
}