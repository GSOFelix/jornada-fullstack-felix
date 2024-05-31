using Fina.Api.Commom.Api;
using Fina.Core;
using Fina.Core.Handler;
using Fina.Core.Models;
using Fina.Core.Requests.Categories;
using Fina.Core.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Fina.Api.Endpoint.Categories
{
    public class GetAllCategoriesEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/", HandleAsync)
            .WithName("Categories: Get All")
            .WithSummary("Recupera todas as Categorias")
            .WithDescription("Recupera todas as Categorias")
            .WithOrder(5)
            .Produces<PagedResponse<List<Category>?>>();

        private static async Task<IResult> HandleAsync(
            ICategoryHandler handler,
            [FromQuery] int pageNumber = Configuration.DefautPageNumber,
            [FromQuery] int pageSize = Configuration.DefautPageSize)
        {
            var request = new GetAllCategoryRequest
            {
                UserId = ApiConfiguration.UserId,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            var result =  await handler.GetAllAsync(request);
            return result.IsSuccess
                ?TypedResults.Ok(result)
                :TypedResults.BadRequest(result);
        }
    }
}
