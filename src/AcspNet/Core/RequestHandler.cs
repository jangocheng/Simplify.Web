﻿using System.Threading.Tasks;
using Microsoft.Owin;
using Simplify.DI;

namespace AcspNet.Core
{
	/// <summary>
	/// Provides OWIN HTTP request handler
	/// </summary>
	public class RequestHandler : IRequestHandler
	{
		private readonly IControllersHandler _controllersHandler;

		/// <summary>
		/// Initializes a new instance of the <see cref="RequestHandler"/> class.
		/// </summary>
		/// <param name="controllersHandler">The controllers handler.</param>
		public RequestHandler(IControllersHandler controllersHandler)
		{
			_controllersHandler = controllersHandler;
		}

		/// <summary>
		/// Processes the OWIN HTTP request.
		/// </summary>
		/// <param name="containerProvider">The DI container provider.</param>
		/// <param name="context">The context.</param>
		/// <returns></returns>
		public Task ProcessRequest(IDIContainerProvider containerProvider, IOwinContext context)
		{
			var result = _controllersHandler.Execute(containerProvider, context);

			if (result == ControllersHandlerResult.Http404)
				context.Response.StatusCode = 404;

			return Task.Delay(0);
		}
	}
}
