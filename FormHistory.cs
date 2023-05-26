using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLDThi
{
    public partial class FormHistory : Form
    {
        string connectionString = ConfigurationManager.ConnectionStrings["Test"].ConnectionString;
        int _userId;
        public FormHistory(int userId)
        {
            InitializeComponent();
            _userId = userId;
        }

        private void FormHistory_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        public void LoadData()
        {
            //danh sách câu hỏi
            var connection = new SqlConnection(connectionString);
            SqlCommand command1 = new SqlCommand("getListResult", connection);
            command1.CommandType = CommandType.StoredProcedure;
            SqlParameter userId = command1.Parameters.Add("userId", SqlDbType.Int);
            userId.Value = _userId;

            SqlDataAdapter dataAdapterQuestions = new SqlDataAdapter(command1);
            DataTable dataTableQuestion = new DataTable();
            dataAdapterQuestions.Fill(dataTableQuestion);
            dataGridView1.DataSource = dataTableQuestion;
        }
    }
}
