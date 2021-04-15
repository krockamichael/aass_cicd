using System.Collections.Generic;
using System.Linq;

namespace InternetBanking
{
    public class TransactionService : ITransactionService
    {
        private ITransactionRepository _transactionRepository;

        public TransactionService(ITransactionRepository transactionRepository) {
            _transactionRepository = transactionRepository;
        }

        public Transaction GetTransaction(decimal transactionID) {
            return _transactionRepository.GetTransactions().FirstOrDefault(x => x.TransactionId == transactionID);
        }

        public IList<Transaction> GetTransactions() {
            return _transactionRepository.GetTransactions().ToList();
        }

        public void SaveTransaction(Transaction transaction) {
            _transactionRepository.CreateTransaction(transaction);
        }
    }

    public interface ITransactionService {
        IList<Transaction> GetTransactions();

        Transaction GetTransaction(decimal transactionID);

        void SaveTransaction(Transaction transaction);
    }
}