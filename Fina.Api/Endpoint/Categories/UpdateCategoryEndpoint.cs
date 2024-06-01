using Fina.Api.Commom.Api;
using Fina.Core.Handler;
using Fina.Core.Models;
using Fina.Core.Requests.Categories;
using Fina.Core.Responses;

namespace Fina.Api.Endpoint.Categories
{
    public class UpdateCategoryEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
         => app.MapPut("/{id}", HandlerAsync)
            .WithName("Categories: Update")
            .WithSummary("Atualiza uma categoria ")
            .WithDescription("Atualiza uma categoria")
            .WithOrder(2)
            .Produces<Response<Category?>>();

       private static async Task<IResult> HandlerAsync(ICategoryHandler handler,UpdateCategoryRequest request, long id)
        {
            request.UserId = ApiConfiguration.UserId;
            request.Id = id;

            var result = await handler.UpdateAsync(request);
            return result.IsSuccess
                ? TypedResults.Ok(request)
                : TypedResults.BadRequest(request);

        }
    }
}
