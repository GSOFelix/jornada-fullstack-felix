﻿using Fina.Api.Commom.Api;
using Fina.Core.Handler;
using Fina.Core.Models;
using Fina.Core.Requests.Categories;
using Fina.Core.Responses;
using System.Runtime.CompilerServices;

namespace Fina.Api.Endpoint.Categories
{
    public class GetByIdCategoryEndPoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
         => app.MapGet("/{id}", HandlerAsync)
            .WithName("Categories: Get By Id")
            .WithSummary("Recupera uma categoria")
            .WithDescription("Recupera uma categoria")
            .WithOrder(4)
            .Produces<Response<Category?>>();

        private static async Task<IResult> HandlerAsync(
            ICategoryHandler handler,
            long id)
        {
            var request = new GetCategoryByIdRequest
            {
                UserId = ApiConfiguration.UserId,
                Id = id
            };

            var result = await handler.GetByIdAsync(request);
            return result.IsSuccess
                ?TypedResults.Ok(result)
                :TypedResults.BadRequest(result);
        }
    }
}
