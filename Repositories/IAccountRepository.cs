using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IAccountRepository
    {
        public List<Account> GetAccounts();
        public void AddAccount(Account account);
        public void UpdateAccount(Account account);
        public void DeleteAccount(int accountId);
        public Account GetAccountById(int accountId); 
        public Account GetAccountByName(string name);
    }
}
