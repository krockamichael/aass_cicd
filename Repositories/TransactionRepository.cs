using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace InternetBanking
{
    public class TransactionRepository: ITransactionRepository
    {
        private string _transactionsFilepath;
        public TransactionRepository(){
            var filename = "Transactions.xml";
            var currentDirectory = Directory.GetCurrentDirectory();
            _transactionsFilepath = Path.Combine(currentDirectory, "data", filename);
        }

        private XElement GetTransactionsXml(){
            return XElement.Load($"{this._transactionsFilepath}");
        }
        public IQueryable<Transaction> GetTransactions(){
            XElement transactions = GetTransactionsXml();
            return 
                (from t in transactions.Descendants("transaction")
                select MapXmlToTransaction(t)).AsQueryable(); 
        }

        public void CreateTransaction(Transaction transaction)
        {
            XElement transactions = GetTransactionsXml();
            
            transactions.Add(MapTransactionToXml(transaction));
            transactions.Save(this._transactionsFilepath);
        }

        private XElement MapTransactionToXml(Transaction transaction)
        {
            XElement element=new XElement("transaction");
            element.Add(new XAttribute("transactionType", (int)transaction.TransactionType));
                         
            element.Add(new XAttribute("accountNumber", transaction.AccountNumber));
            element.Add(new XAttribute("amount", transaction.Amount));
            element.Add(new XAttribute("bankCode", transaction.BankCode));
            element.Add(new XAttribute("fullName", transaction.FullName));
            element.Add(new XAttribute("issueDate", transaction.IssueDate));
            element.Add(new XAttribute("transactionId", transaction.TransactionId));
            return element;
        }

        private Transaction MapXmlToTransaction(XElement element)
        {
            Transaction transaction=new Transaction();
            
            if((int)element.Attribute("transactionType") > 1){
                transaction=new TransactionWithdraw(){
                    Place=(string)element.Attribute("place"),
                    CardNo=(string)element.Attribute("cardNo"),
                };
            }
            transaction.AccountNumber=(string)element.Attribute("accountNumber");
            transaction.Amount=(decimal)element.Attribute("amount");
            transaction.BankCode=(string)element.Attribute("bankCode");
            transaction.FullName=(string)element.Attribute("fullName");
            transaction.TransactionId=(decimal)element.Attribute("transactionId");        
            transaction.TransactionType = Enum.Parse<TransactionType>((string)element.Attribute("transactionType")) ;
            return transaction;
        }
    }

    public interface ITransactionRepository
    {
        IQueryable<Transaction> GetTransactions();
        void CreateTransaction(Transaction transaction);
    }
}