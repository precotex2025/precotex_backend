namespace ic.backend.precotex.web.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddApplicationServices();
            services.AddApplicationRepositories();

            return services;
        }

    }
}
