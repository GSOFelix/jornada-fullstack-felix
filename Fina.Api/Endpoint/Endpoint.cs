using Fina.Api.Commom.Api;
using Fina.Api.Endpoint.Categories;
using Fina.Api.Endpoint.Transactions;
using Fina.Core.Requests.Categories;

namespace Fina.Api.Endpoint
{
    public static class Endpoint
    {
        public static void MapEndpoint(this WebApplication app)
        {
            var endpoints = app.MapGroup("");

            endpoints.MapGroup("/")
                .WithTags("Health Chek")
                .MapGet("/", () => new { message = "OK" });

            endpoints.MapGroup("v1/categories")
            .WithTags("Categories")
            .MapEndpoint<CreateCategoryEndpoint>()
            .MapEndpoint<UpdateCategoryEndpoint>()
            .MapEndpoint<DeleteCategoryEndpoint>()
            .MapEndpoint<GetByIdCategoryEndPoint>()
            .MapEndpoint<GetAllCategoriesEndpoint>();

            endpoints.MapGroup("v1/transactions")
                .WithTags("Transactions")
                .RequireAuthorization()
                .MapEndpoint<CreateTransactionEndpoint>()
                .MapEndpoint<UpdateTransactionEndpoint>()
                .MapEndpoint<DeleteTransactionEndpoint>()
                .MapEndpoint<GetByIdTransactionEndpoint>()
                .MapEndpoint<GetTransactionsByPeriodEndpoint>();
        }

        private static IEndpointRouteBuilder MapEndpoint<TEndpoint>(this IEndpointRouteBuilder app)
            where TEndpoint : IEndpoint
        {
            TEndpoint.Map(app);
            return app;
        }
    }
}
