using System;

namespace InternetBanking {
    public class Transaction {
        public decimal TransactionId { set; get; }
        public string FullName { set; get; }
        public TransactionType TransactionType { set; get; }
        public string AccountNumber { set; get; }
        public string BankCode { set; get; }
        public DateTime IssueDate { set; get; }
        public decimal Amount { set; get; }
    }

    public class TransactionWithdraw: Transaction {        
        public string CardNo { set; get; }
        public string Place { set; get;}    
    }

    public enum TransactionType {
        Incoming=0, Outgoing=1, Withdraw=2
    }
}