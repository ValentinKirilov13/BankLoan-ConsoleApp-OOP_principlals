using BankLoan.Models.Contracts;
using BankLoan.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLoan.Models
{
    public abstract class Bank : IBank
    {
        private string name;
        private int capacity;
        private List<ILoan> loans;
        private List<IClient> clients;

        public Bank(string name, int capacity)
        {
            Name = name;
            Capacity = capacity;
            loans = new List<ILoan>();
            clients = new List<IClient>();
        }

        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.BankNameNullOrWhiteSpace);
                }
                name = value;
            }
        }

        public int Capacity
        {
            get => capacity;
            private set => capacity = value;
        }

        public IReadOnlyCollection<ILoan> Loans => loans;

        public IReadOnlyCollection<IClient> Clients => clients;

        public double SumRates()
        {
            return Loans.Sum(x => x.InterestRate);
        }

        public void AddClient(IClient Client)
        {
            if (clients.Count == Capacity)
            {
                throw new ArgumentException("Not enough capacity for this client.");
            }

            clients.Add(Client);
        }

        public void RemoveClient(IClient Client)
        {
            clients.Remove(Client);
        }


        public void AddLoan(ILoan loan)
        {
            loans.Add(loan);
        }

        public string GetStatistics()
        {
           StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Name: {Name}, Type: {GetType().Name}");
            sb.Append("Clients: ");

            if (clients.Any())
            {
                sb.Append($"{clients.First().Name}");

                foreach (var client in clients.Skip(1))
                {
                    sb.Append($", {client.Name}");
                }
            }
            else
            {
                sb.Append("none");
            }

            sb.AppendLine();

            sb.AppendLine($"Loans: {loans.Count}, Sum of Rates: {SumRates()}");
            
            return sb.ToString().TrimEnd();
        }
    }
}
