using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLDThi
{
    public partial class FormHome : Form
    {
        string connectionString = ConfigurationManager.ConnectionStrings["Test"].ConnectionString;
        String username;
        bool _isAdmin;
        int thisUserId;
        public FormHome(string _username, int _userId, bool isAdmin)
        {
            InitializeComponent();
            username = _username;
            _isAdmin = isAdmin;
            thisUserId = _userId;
        }

        private void FormHome_Load(object sender, EventArgs e)
        {
            lblWelcome.Text = "Xin chào " + username;
            if (_isAdmin)
            {
                btnThi.Visible = false;
                btnLichSuThi.Visible = false;
                btnQuanTri.Visible = true;
            }
            else
            {
                btnThi.Visible = true;
                btnLichSuThi.Visible = true;
                btnQuanTri.Visible = false;
            }
        }

        private void linkLogout_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
        }

        private void btnNhanVien_Click(object sender, EventArgs e)
        {
            FormAdmin formAdmin = new FormAdmin(username);
            Hide();
            formAdmin.ShowDialog();
            Show();
        }

        private void btnChangePW_Click(object sender, EventArgs e)
        {
            FormChangePW formChangePW = new FormChangePW(username);
            Hide();
            formChangePW.ShowDialog();
            Show();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnThi_Click(object sender, EventArgs e)
        {
            FormChooseExam formChooseExam = new FormChooseExam(thisUserId, username, _isAdmin);
            Hide();
            formChooseExam.ShowDialog();
            Show();
        }

        private void btnLichSuThi_Click(object sender, EventArgs e)
        {
            FormHistory formHistory = new FormHistory(thisUserId);
            Hide();
            formHistory.ShowDialog();
            Show();
        }
    }
}
