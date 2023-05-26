using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using BC = BCrypt.Net.BCrypt;

namespace QLDThi
{
    public partial class FormSignup : Form
    {
        string connectionString = ConfigurationManager.ConnectionStrings["Test"].ConnectionString;
        string errorMsg;
        public FormSignup()
        {
            InitializeComponent();
        }

        private void btnSignup_Click(object sender, EventArgs e)
        {
            if (txtUsername.Text == "" || txtPassword.Text == "" || txtPasswordConfirm.Text == "")
                label4.Text = ("Yêu cầu nhập đủ thông tin");
            else
            {
                if (txtPassword.Text != txtPasswordConfirm.Text)
                    label4.Text = ("Mật khẩu nhập lại không trùng khớp. Vui lòng nhập lại.");
                else if (ValidatePassword(txtPassword.Text, out errorMsg) == false)
                {
                   label4.Text = errorMsg;
                }
                else
                {
                    var connection = new SqlConnection(connectionString);
                    SqlCommand checkExistUser = new SqlCommand("CheckExistUser", connection);
                    checkExistUser.CommandType = CommandType.StoredProcedure;
                    SqlParameter checkUsername = checkExistUser.Parameters.Add("@username", SqlDbType.VarChar);
                    checkUsername.Value = txtUsername.Text;
                    connection.Open();
                    var dataReader = checkExistUser.ExecuteReader(CommandBehavior.CloseConnection);
                    if (dataReader.HasRows)
                    {
                        MessageBox.Show("Tên đăng nhập này đã tồn tại. Vui lòng chọn tên đăng nhập khác.");
                    }
                    else
                    {
                            SqlCommand command = new SqlCommand("Signup", connection);
                            command.CommandType = CommandType.StoredProcedure;
                            SqlParameter thisUsername = command.Parameters.Add("@username", SqlDbType.VarChar);
                            thisUsername.Value = txtUsername.Text;
                            SqlParameter thisIsAdmin = command.Parameters.Add("@isAdmin", SqlDbType.SmallInt);
                            thisIsAdmin.Value = 0;
                            SqlParameter thisPassword = command.Parameters.Add("@password", SqlDbType.VarChar);
                            string passwordHash = BC.HashPassword(txtPassword.Text);
                            thisPassword.Value = passwordHash;
                            command.ExecuteNonQuery();
                            connection.Close();
                            MessageBox.Show("Đăng kí thành công, hãy bắt đầu đăng nhập");
                            Close();
                        }
                    }
                }
            }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            Close();
        }

        private bool ValidatePassword(string password, out string ErrorMessage)
        {
            var input = password;
            ErrorMessage = string.Empty;

            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasMiniMaxChars = new Regex(@".{8,15}");
            var hasLowerChar = new Regex(@"[a-z]+");
            var hasSymbols = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");

            if (!hasLowerChar.IsMatch(input))
            {
                ErrorMessage = "Mật khẩu phải chứa ít nhất một ký tự thường";
                return false;
            }
            else if (!hasUpperChar.IsMatch(input))
            {
                ErrorMessage = "Mật khẩu phải chứa ít nhất một ký tự hoa";
                return false;
            }
            else if (!hasMiniMaxChars.IsMatch(input))
            {
                ErrorMessage = "Mật khẩu phải chứa tối thiểu 8 ký tự";
                return false;
            }
            else if (!hasNumber.IsMatch(input))
            {
                ErrorMessage = "Mật khẩu phải chứa ký tự số";
                return false;
            }

            else if (!hasSymbols.IsMatch(input))
            {
                ErrorMessage = "Mật khẩu phải chứa ít nhất một ký tự đặc biệt";
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}