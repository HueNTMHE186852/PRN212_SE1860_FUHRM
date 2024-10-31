using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObjects
{
    public class AccountDAO
    {
        private readonly FuhrmContext _context;
        public AccountDAO(FuhrmContext context)
        {
            _context = context;
        }
        public AccountDAO()
        {
            _context = new FuhrmContext();
        }
        public List<Account> GetAllAccounts()
        {
            try
            {
                using (var context = new FuhrmContext())
                {
                    var accounts = context.Accounts
                        .Include(a => a.Role)
                        .ToList();

                    return accounts;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching accounts: {ex.Message}");
                return new List<Account>();
            }
        }

        public Account GetAccountById(int accountId)
        {
            return _context.Accounts.Find(accountId);
        }

        public Account GetAccountByUsername(string username)
        {
            return _context.Accounts.Include(a => a.Role).FirstOrDefault(a => a.Username.Equals(username));
        }
        public Employee GetEmployeeByUsername(string username)
        {
            // Lấy Account dựa trên username
            var account = _context.Accounts
                .Include(a => a.Employees) // Đảm bảo bao gồm thông tin Employees
                .FirstOrDefault(a => a.Username.Equals(username));

            if (account != null)
            {
                // Lấy Employee từ account
                return _context.Employees.FirstOrDefault(e => e.AccountId == account.AccountId);
            }

            return null; // Trả về null nếu không tìm thấy account
        }
        public void AddAccount(Account account)
        {
            _context.Accounts.Add(account);
            _context.SaveChanges();
        }

        public void UpdateAccount(Account account)
        {
            var existingAccount = _context.Accounts.Find(account.AccountId);
            if (existingAccount != null)
            {
                // Update properties
                existingAccount.Username = account.Username;
                existingAccount.Password = account.Password;
                existingAccount.RoleId = account.RoleId;
                _context.SaveChanges();
            }
        }

        public void DeleteAccount(int accountId)
        {
            var account = _context.Accounts.Find(accountId);
            if (account != null)
            {
                _context.Accounts.Remove(account);
                _context.SaveChanges();
            }
        }
    }
}
