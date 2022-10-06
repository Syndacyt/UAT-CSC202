using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace CompletionistPrototype
{
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();
            // Set the size and position of the form
            Size = new Size(369, 636);
            StartPosition = FormStartPosition.CenterScreen;
        }

        // Form Load
        private void RegisterForm_Load(object sender, EventArgs e)
        {
            
        }

        // Register Button Clicked
        private void RegisterButton_Click(object sender, EventArgs e)
        {
            string connectionString = @"Data Source = localhost\SQLEXPRESS;
                                        Integrated Security = True";

            // Store user input
            string username = UsernameTextbox.Text;
            string email = EmailTextbox.Text;
            string password = PasswordTextbox.Text;
            string confPass = ConfPassTextbox.Text;

            // Verify user input
            bool usernameValid = VerifyUsername(username, connectionString);
            bool emailValid = VerifyEmail(email, connectionString);
            bool passwordValid = VerifyPassword(password, confPass);

            // Register in database if info is valid
            if (usernameValid == true && emailValid == true && passwordValid == true) 
            {
                RegisterUser(username, email, password, connectionString);

                // Close this form and switch to LoginForm
                (new LoginForm()).Show();
                this.Close();
            }


        }

        // Login Link Label Clicked
        private void LoginLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Closes this form after opening the LoginForm
            (new LoginForm()).Show();
            this.Close();
        }

        private bool VerifyUsername(string username, string connectioNString) 
        {
            // Reset Error Label
            UsernameErrorLabel.Text = " ";
            UsernameErrorLabel.Visible = false;

            // Checks for input
            if (username.Length == 0) 
            {
                UsernameErrorLabel.Text = "Required input.";
                UsernameErrorLabel.Visible = true;
                return false;
            }

            // Checks if the username is too long
            if (username.Length > 20) 
            {
                UsernameErrorLabel.Text = "Username must be 20 characters or less.";
                UsernameErrorLabel.Visible = true;
                return false;
            }

            // Checks Database
            SqlConnection SqlCon = new SqlConnection(connectioNString);
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
                UsernameErrorLabel.Text = "Username unavailable.";
                UsernameErrorLabel.Visible = true;
                return false;
            }
            else 
            {
                return true;
            }
        }

        private bool VerifyEmail(string email, string connectionString) 
        {
            // Reset Error Label
            EmailErrorLabel.Text = " ";
            EmailErrorLabel.Visible = false;

            // Regex Pattern
            string pattern = "^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$";

            // Checks for input
            if (email.Length == 0) 
            {
                EmailErrorLabel.Text = "Required input.";
                EmailErrorLabel.Visible = true;
                return false;
            }

            // Check format of email
            if (Regex.IsMatch(email, pattern))
            {
                // Check database
                SqlConnection SqlCon = new SqlConnection(connectionString);
                SqlCon.Open();
                SqlCommand sqlCmd = SqlCon.CreateCommand();
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.CommandText = "Select * from Users where email='" + email.Trim() + "'";
                int rows = Convert.ToInt32(sqlCmd.ExecuteScalar());

                // If any results were returned
                // There are more than 0 rows.
                // Username was taken.
                if (rows > 0)
                {
                    EmailErrorLabel.Text = "Email unavailable.";
                    EmailErrorLabel.Visible = true;
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else 
            {
                EmailErrorLabel.Text = "Invalid email address.";
                EmailErrorLabel.Visible = true;
                return false;
            }

            
        }

        private bool VerifyPassword(string password, string confPass) 
        {
            // Reset Error Label
            PasswordErrorLabel.Text = " ";
            PasswordErrorLabel.Visible = false;
            ConfPassErrorLabel.Text = " ";
            ConfPassErrorLabel.Visible = false;

            // Checks if password is atleast 8 characters
            if (password.Length < 8) 
            {
                PasswordErrorLabel.Text = "Password must be atleast 8 characters long.";
                PasswordErrorLabel.Visible = true;
                return false;
            }

            // Checks if both passwords match
            if (password != confPass) 
            {
                ConfPassErrorLabel.Text = "Passwords do not match.";
                ConfPassErrorLabel.Visible = true;
                return false;
            }

            return true;
        }

        private void RegisterUser(string username, string email, string password, string connectionString) 
        {

            SqlConnection sqlCon = new SqlConnection(connectionString);

            sqlCon.Open();
            SqlCommand sqlCmd = new SqlCommand("AddUser", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@username", username.Trim());
            sqlCmd.Parameters.AddWithValue("@email", email.Trim());
            sqlCmd.Parameters.AddWithValue("@password", password.Trim());
            sqlCmd.ExecuteNonQuery();
        }

    }
}
