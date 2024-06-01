using Fina.Api.Commom.Api;
using Fina.Core.Handler;
using Fina.Core.Models;
using Fina.Core.Requests;
using Fina.Core.Requests.Transactions;
using Fina.Core.Responses;

namespace Fina.Api.Endpoint.Transactions
{
    public class CreateTransactionEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("/", HandlerAsync)
            .WithName("Transaction : Create")
            .WithSummary("Cria uma transação")
            .WithDescription("Cria uma transação")
            .WithOrder(1)
            .Produces<Response<Transaction?>>();

        private static async Task<IResult> HandlerAsync(ITransactionHandle handle,CreateTransactionRequest request)
        {
            request.UserId = ApiConfiguration.UserId;

            var result = await handle.CreateAsync(request);

            return result.IsSuccess 
                ?TypedResults.Created($"/{result.Data?.Id}",result)
                :TypedResults.BadRequest(result);
        }
    }
}
