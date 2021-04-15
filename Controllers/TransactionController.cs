using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace InternetBanking {

    [ApiController]
    [Route("api/transactions")]
    public class TransactionController: ControllerBase
    {
        private ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService) {
            _transactionService = transactionService;
        }

        [HttpGet]
        public IList<Transaction> getAll() {
            return _transactionService.GetTransactions();
        }

        [HttpGet("{id}")]
        public IActionResult Get(decimal id) {
            var transaction = _transactionService.GetTransaction(id);
            if (transaction == null) {
                return NotFound();
            }
            return new OkObjectResult(transaction);
        }

        [HttpPost]
        public IActionResult SaveTransaction(Transaction transaction) {
            _transactionService.SaveTransaction(transaction);
            return Ok();
        }
    }
}