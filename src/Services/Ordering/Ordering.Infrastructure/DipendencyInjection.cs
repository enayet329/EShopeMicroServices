﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ordering.Infrastructure;

public static class DipendencyInjection
{
	public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
	{
		var connectionString = configuration.GetConnectionString("Database");
		// Add Services to the container.
		//services.AddDbContext<OrderingContext>(options => options.UseSqlServer(connectionString));

		//services.AddScoped<IApplicationDbContext, ApplicationDbContext>();

		return services;
	}

}
