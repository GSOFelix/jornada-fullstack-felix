using Fina.Api.Commom.Api;
using Fina.Core.Handler;
using Fina.Core.Models;
using Fina.Core.Requests.Transactions;
using Fina.Core.Responses;

namespace Fina.Api.Endpoint.Transactions
{
    public class GetByIdTransactionEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/{id}", HandlerAsync)
            .WithName("Transaction : Get  By Id")
            .WithSummary("Recupera uma transação")
            .WithDescription("Recupera uma transação")
            .WithOrder(4)
            .Produces<Response<Transaction?>>();

        private static async Task<IResult> HandlerAsync(ITransactionHandle handle,long id)
        {
            var request = new GetTransactionByIdRequest
            {
                UserId = ApiConfiguration.UserId,
                Id = id
            };

            var result = await handle.GetByIdAsync(request);
            return result.IsSuccess
                ?TypedResults.Ok(request)
                :TypedResults.BadRequest(request);
        }
    }
}
