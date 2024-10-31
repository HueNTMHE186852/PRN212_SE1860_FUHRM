using BusinessObjects;
using Repositories;
using System.Windows;
using System.Windows.Controls;

namespace WPFApp
{
    public partial class MainWindow : Window
    {
        public static int CurrentAccountId { get; set; }
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
           
        {
            Account account = new Account();
            AccountRepository _accountRepository = new AccountRepository();
            var account1 = _accountRepository.GetAccountByName(account.Username);
            CurrentAccountId = account.AccountId;
            if (account != null && account.Password.Equals( account.Password))
            {
                // Mở AttendanceForm và truyền AccountId
                AttendanceForm attendanceForm = new AttendanceForm(account1.AccountId);
                attendanceForm.Show();
                this.Close();
            }
        }
    }
}