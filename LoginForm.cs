using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CompletionistPrototype
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();

            // Set the size and position of the form
            Size = new Size(369, 636);
            StartPosition = FormStartPosition.CenterScreen;
        }

        // Register Label Clicked
        private void RegisterLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Closes this form after opening the Register Form
            (new RegisterForm()).Show();
            this.Close();
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            // Store User input
            string username = UsernameTextbox.Text;
            string password = PasswordTextbox.Text;

            // Database connection string
            string connectionString = @"Data Source = localhost\SQLEXPRESS;
                                        Integrated Security = True";

            // Verify if the user exists
            bool validUsername = VerifyUser(username, connectionString);
            bool validPassword = VerifyPassword(username, password, connectionString);

            if (validUsername == true && validPassword == true) 
            {
                (new MainForm()).Show();
                this.Close();
            }
        }

        private bool VerifyUser(string username, string connectionString) 
        {
            // Reset error labels
            UsernameErrorLabel.Text = " ";
            UsernameErrorLabel.Visible = false;

            // Checks if there is input
            if (username.Length == 0) 
            {
                UsernameErrorLabel.Text = "Required input.";
                UsernameErrorLabel.Visible = true;
                return false;
            }

            // Check database
            SqlConnection SqlCon = new SqlConnection(connectionString);
            SqlCon.Open();
            SqlCommand sqlCmd = SqlCon.CreateCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "Select * from Users where username='" + username.Trim() + "'";
            int rows = Convert.ToInt32(sqlCmd.ExecuteScalar());

            // If any results were returned
            // There are more than 0 rows.
            // Username was taken.
            if (rows > 0)
            {
                return true;
            }
            else
            {
                UsernameErrorLabel.Text = "Username not found.";
                UsernameErrorLabel.Visible = true;
                return false;
            }

        }

        private bool VerifyPassword(string username, string password, string connectionString) 
        {
            // Reset error labels
            PasswordErrorLabel.Text = " ";
            PasswordErrorLabel.Visible = false;

            // Checks if there is input
            if (password.Length == 0) 
            {
                PasswordErrorLabel.Text = "Required input.";
                PasswordErrorLabel.Visible = true;
                return false;
            }

            // Checks database against username
            SqlConnection SqlCon = new SqlConnection(connectionString);
            SqlCon.Open();
            SqlCommand sqlCmd = SqlCon.CreateCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "Select * from Users where username='" + username.Trim() + "' and password ='" + password + "'";
            int rows = Convert.ToInt32(sqlCmd.ExecuteScalar());

            // If any results were returned
            // There are more than 0 rows.
            // Username was taken.
            if (rows > 0)
            {
                return true;
            }
            else
            {
                PasswordErrorLabel.Text = "Incorrect password.";
                PasswordErrorLabel.Visible = true;
                return false;
            }
        }
    }
}
