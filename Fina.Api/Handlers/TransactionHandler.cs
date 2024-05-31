using Fina.Api.Data;
using Fina.Core.Commons;
using Fina.Core.Handler;
using Fina.Core.Models;
using Fina.Core.Requests.Transactions;
using Fina.Core.Responses;
using Microsoft.EntityFrameworkCore;
using System.Net.WebSockets;
using System.Reflection.Metadata.Ecma335;

namespace Fina.Api.Handlers
{
    public class TransactionHandler(AppDbContext context) : ITransactionHandle
    {
        public async Task<Response<Transaction?>> CreateAsync(CreateTransactionRequest request)
        {
            if (request is { Type: Fina.Core.Enums.ETransactionType.Saida, Amount: > 0 })
                request.Amount *= -1;
            
            try
            {
                var transaction = new Transaction
                {
                    UserId = request.UserId,
                    CategoryId = request.CategoryId,
                    CreatedAt = DateTime.UtcNow,
                    Amount = request.Amount,
                    PaidOrReceivedAt = request.PaidOrReceivedAd,
                    Title = request.Title,
                    Type = request.Type
                };

                await context.Transactions.AddAsync(transaction);
                await context.SaveChangesAsync();

                return new Response<Transaction?>(transaction,201,"Transação criada com sucesso.");
            }
            catch (Exception ex)
            {
                return new Response<Transaction?>(null, 500, "Não foi possivel criar transação");
            }
        }

        public async Task<Response<Transaction?>> DeleteAsync(DeleteTransactionRequest request)
        {
            try
            {
                var transaction =  await context
                    .Transactions
                    .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

                if (transaction is null)
                    return new Response<Transaction?>(null, 404, "Transação nao encontrada");

                context.Transactions.Remove(transaction);
                await context.SaveChangesAsync();

                return new Response<Transaction?>(transaction);
            }
            catch (Exception ex)
            {
                return new Response<Transaction?>(null, 500, "Não foi possivel deletar transação");
            }
        }

        public async Task<Response<Transaction?>> GetByIdAsync(GetTransactionByIdRequest request)
        {
            try
            {
                var trasaction = await context.Transactions.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

                return trasaction is null ?
                    new Response<Transaction?>(null, 404, "transação não encontrada")
                    : new Response<Transaction?>(trasaction);
            }
            catch (Exception ex)
            {
                return new Response<Transaction?>(null, 500, "Não foi possivel criar transação");
            }
        }

        public async Task<PagedResponse<List<Transaction>?>> GetByPeriodAsync(GetTransactionByPeriodRequest request)
        {
            try
            {
                request.StartDay ??= DateTime.Now.GetFirstDay();
                request.EndDay ??= DateTime.Now.GetLastDay();
            }
            catch
            {
                return new PagedResponse<List<Transaction>?>(null, message: "Não foi possivel determinar a data");
            }

            try
            {
                var query = context
                    .Transactions
                    .AsNoTracking()
                    .Where(x => x.PaidOrReceivedAt >= request.StartDay &&
                    x.PaidOrReceivedAt <= request.EndDay &&
                    x.UserId == request.UserId)
                    .OrderBy(x => x.PaidOrReceivedAt);


                var transaction = await query
                    .Skip((request.PageNumber - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .ToListAsync();

                var count =  await query .CountAsync();

                return new PagedResponse<List<Transaction>?>(transaction, count, request.PageNumber, request.PageSize);
            }
            catch
            {
                return new PagedResponse<List<Transaction>?>(null, 500, "Não foi possivel encontrar as transações");
            }
        }

        public async Task<Response<Transaction?>> UpdateAsync(UpdateTransactionsRequest request)
        {
            if (request is { Type: Fina.Core.Enums.ETransactionType.Saida, Amount: > 0 })
                request.Amount *= -1;

            try
            {
                var transaction = await context
                    .Transactions
                    .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

                if(transaction is null)
                {
                    return new Response<Transaction?>(null, 404, "Transação nao encontrada");
                }

                transaction.CategoryId = request.CategoryId;
                transaction.Amount = request.Amount;
                transaction.Title = request.Title;
                transaction.Type = request.Type;
                transaction.PaidOrReceivedAt = request.PaidOrReceivedAd;

                context.Transactions.Update(transaction);
                await context.SaveChangesAsync();

                return new Response<Transaction?>(transaction, message: "Transação atualizada com sucesso.");
            }
            catch (Exception ex)
            {
                return new Response<Transaction?>(null, 500, "Não foi possivel atualizar a transação");
            }

        }
    }
}
