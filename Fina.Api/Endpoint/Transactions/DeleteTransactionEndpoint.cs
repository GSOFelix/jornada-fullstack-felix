using Fina.Api.Commom.Api;
using Fina.Core.Handler;
using Fina.Core.Models;
using Fina.Core.Requests.Transactions;
using Fina.Core.Responses;

namespace Fina.Api.Endpoint.Transactions
{
    public class DeleteTransactionEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
         => app.MapDelete("/{id}", HandlerAsync)
            .WithName("Transaction: Delete")
            .WithSummary("Deleta uma transação")
            .WithDescription("Deleta uma transação")
            .WithOrder(3)
            .Produces<Response<Transaction?>>();

        private static async Task<IResult> HandlerAsync(ITransactionHandle handle,long id)
        {
            var request = new DeleteTransactionRequest
            {
                UserId = ApiConfiguration.UserId,
                Id = id
            };

            var result = await handle.DeleteAsync(request);

            return result.IsSuccess
                ?TypedResults.Ok(request)
                :TypedResults.BadRequest(request);
        }
    }
}
