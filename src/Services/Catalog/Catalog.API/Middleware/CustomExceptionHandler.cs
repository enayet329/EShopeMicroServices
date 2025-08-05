using Catalog.API.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Catalog.API.Middleware;

public class CustomExceptionHandler
{
	private readonly RequestDelegate _next;
	private readonly ILogger<CustomExceptionHandler> _logger;

	public CustomExceptionHandler(RequestDelegate next, ILogger<CustomExceptionHandler> logger)
	{
		_next = next;
		_logger = logger;
	}

	public async Task InvokeAsync(HttpContext httpContext)
	{
		try
		{
			await _next(httpContext);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "An unhandled exception occurred: {Message}", ex.Message);
			await HandleExceptionAsync(httpContext, ex);
		}
	}

	private static Task HandleExceptionAsync(HttpContext context, Exception exception)
	{
		context.Response.ContentType = "application/problem+json";
		context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

		var problemDetails = new ProblemDetails
		{
			Status = context.Response.StatusCode,
			Title = "An internal server error occurred.",
			Detail = exception.Message,
		};

		// Here you can add logic to handle specific exception types
		// For example, a custom NotFoundException or ValidationException
		if (exception is ProductNotFoundException)
		{
			context.Response.StatusCode = (int)HttpStatusCode.NotFound;
			problemDetails.Title = "The specified resource was not found.";
		}

		return context.Response.WriteAsJsonAsync(problemDetails);
	}
}