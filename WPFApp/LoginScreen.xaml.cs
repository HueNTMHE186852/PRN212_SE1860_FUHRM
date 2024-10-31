using BusinessObjects;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WPFApp
{
    /// <summary>
    /// Interaction logic for LoginScreen.xaml
    /// </summary>
    public partial class LoginScreen : Window
    {
        private readonly AccountRepository _accountRepository;
        public LoginScreen()
        {
            InitializeComponent();
            _accountRepository = new AccountRepository();

        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (txtUsername != null && txtPassword != null)
            {
                Account account = _accountRepository.GetAccountByName(txtUsername.Text);
                if (account != null)
                {
                    if (account.Password.Equals(txtPassword.Password))
                    {
                        
                        if(account.Role.RoleName.Equals("Admin"))
                        {
                            EmployeeWindow employeeWindow = new EmployeeWindow();
                            employeeWindow.Show();
                            this.Close();
                        }
                        if (account.Role.RoleName.Equals("Employee"))
                        {
                            TakeAttendance takeAttendance = new TakeAttendance(account.AccountId - 2);
                            takeAttendance.Show();
                            this.Close();
                        }
                    }
                    else
                    {
                        txtError.Text = "Incorrect password " + txtPassword.Password;
                    }
                }
                else
                {
                    txtError.Text = "Invalid username or password";
                }
            }
            else
            {
                MessageBox.Show("Invalid username or password");
            }
        }
    }
}
