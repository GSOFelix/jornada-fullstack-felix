using Fina.Api.Commom.Api;
using Fina.Core.Requests.Transactions;
using Fina.Core.Responses;
using Fina.Core;
using Microsoft.AspNetCore.Mvc;
using Fina.Core.Models;
using Fina.Core.Handler;

namespace Fina.Api.Endpoint.Transactions
{
    public class GetTransactionsByPeriodEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
       => app.MapGet("/", HandleAsync)
           .WithName("Transactions: Get All")
           .WithSummary("Recupera todas as transações")
           .WithDescription("Recupera todas as transações")
           .WithOrder(5)
           .Produces<PagedResponse<List<Transaction>?>>();

        private static async Task<IResult> HandleAsync(
            ITransactionHandle handler,
            [FromQuery] DateTime? startDate = null,
            [FromQuery] DateTime? endDate = null,
            [FromQuery] int pageNumber = Configuration.DefautPageNumber,
            [FromQuery] int pageSize = Configuration.DefautPageSize)
        {
            var request = new GetTransactionByPeriodRequest
            {
                UserId = ApiConfiguration.UserId,
                PageNumber = pageNumber,
                PageSize = pageSize,
                StartDay = startDate,
                EndDay = endDate
            };

            var result = await handler.GetByPeriodAsync(request);
            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }


    }
}
