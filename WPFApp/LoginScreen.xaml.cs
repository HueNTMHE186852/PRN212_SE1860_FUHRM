﻿using BusinessObjects;
using Repositories;
using System;
using System.Windows;

namespace WPFApp
{
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
                        // Store the account in the session
                        SessionManager.CurrentAccount = account;

                        if (account.Role.RoleName.Equals("Admin"))
                        {
                            EmployeeWindow employeeWindow = new EmployeeWindow();
                            employeeWindow.Show();
                            this.Close();
                        }
                        else if (account.Role.RoleName.Equals("Employee"))
                        {
                            HomeEmployee homeEmployee = new HomeEmployee();
                            homeEmployee.Show();
                            this.Close();
                        }
                        else
                        {
                            MainWindow mainWindow = new MainWindow();
                            mainWindow.Show();
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

    public static class SessionManager
    {
        public static Account CurrentAccount { get; set; }
    }
}
