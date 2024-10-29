﻿using BusinessObjects;
using DataAccessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AccountDAO _accountDAO;

        public AccountRepository(AccountDAO accountDAO)
        {
            _accountDAO = accountDAO;
        }

        public AccountRepository()
        {
            _accountDAO = new AccountDAO();
        }

        public List<Account> GetAccounts()
        {
            return _accountDAO.GetAllAccounts();
        }

        public void AddAccount(Account account)
        {
            _accountDAO.AddAccount(account);
        }

        public void UpdateAccount(Account account)
        {
            _accountDAO.UpdateAccount(account);
        }

        public void DeleteAccount(int accountId)
        {
            _accountDAO.DeleteAccount(accountId);
        }

        public Account GetAccountById(int accountId)
        {
            return _accountDAO.GetAccountById(accountId);
        }

        public Account GetAccountByName(string username)
        {
            return _accountDAO.GetAccountByUsername(username);
        }
    }
}