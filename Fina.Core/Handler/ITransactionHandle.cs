﻿using Fina.Core.Models;
using Fina.Core.Requests.Transactions;
using Fina.Core.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fina.Core.Handler
{
    public interface ITransactionHandle
    {
        Task<Response<Transaction?>> CreateAsync(CreateTransactionRequest request);
        Task<Response<Transaction?>> UpdateAsync(UpdateTransactionsRequest request);
        Task<Response<Transaction?>> DeleteAsync(DeleteTransactionRequest request);
        Task<Response<Transaction?>> GetByIdAsync(GetTransactionByIdRequest request);
        Task<PagedResponse<List<Transaction?>>> GetByPeriodAsync(GetTransactionByPeriodRequest request);
    }
}
