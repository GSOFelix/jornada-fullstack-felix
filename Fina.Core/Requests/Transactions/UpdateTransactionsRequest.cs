using Fina.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fina.Core.Requests.Transactions
{
    public class UpdateTransactionsRequest : Request
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "título invalido")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Tipo Invalido")]
        public ETransactionType Type { get; set; } = ETransactionType.Saida;

        [Required(ErrorMessage = "Valor invalido")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Categoria Invalida")]
        public long CategoryId { get; set; }

        [Required(ErrorMessage = "Data invalida")]
        public DateTime? PaidOrReceivedAd { get; set; }
    }
}
