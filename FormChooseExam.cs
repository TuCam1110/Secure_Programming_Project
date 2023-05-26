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
    public partial class FormChooseExam : Form
    {
        string connectionString = ConfigurationManager.ConnectionStrings["Test"].ConnectionString;
        int question_id, _examId, _userId;
        string _listQuestionId, _code, typeForm, _username;
        bool _isAdmin;
        List<int> thisListQuestionId = new List<int>();
        List<int> newListQuestionId = new List<int>();
        List<int> listExamId = new List<int>();

        public FormChooseExam(int userId, string username, bool isAdmin)
        {
            InitializeComponent();
            _userId = userId;
            _username = username;
            _isAdmin = isAdmin;
        }

        private void FormThi_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'qLDThiDataSet.tblExam' table. You can move, or remove it, as needed.
            this.tblExamTableAdapter.Fill(this.qLDThiDataSet.tblExam);
        }

        public void ReloadForm()
        {
            //danh sách đề thi
            var connection = new SqlConnection(connectionString);
            SqlCommand command1 = new SqlCommand("getListExam", connection);
            command1.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter dataAdapterQuestions = new SqlDataAdapter(command1);
            DataTable dataTableQuestion = new DataTable();
            dataAdapterQuestions.Fill(dataTableQuestion);
            dataGridView1.DataSource = dataTableQuestion;
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.RowIndex >= 0)
            {
                this.dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
                if (this.dataGridView1.CurrentCell.Value == null || (bool)this.dataGridView1.CurrentCell.Value == false)
                {
                    this.dataGridView1.CurrentCell.Value = true;
                    var id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex + 1].Value);
                    listExamId.Add(id);
                }
                else if ((bool)this.dataGridView1.CurrentCell.Value == true)
                {
                    this.dataGridView1.CurrentCell.Value = false;
                    var id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex + 1].Value);
                    _examId = id;
                    string listQuestionId = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex + 3].Value.ToString();
                    _listQuestionId = listQuestionId;
                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.RowIndex >= 0)
            {
                if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    dataGridView1.CurrentRow.Selected = true;

                    _examId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[1].FormattedValue);
                    _code = (dataGridView1.Rows[e.RowIndex].Cells[2].FormattedValue.ToString());
                    _listQuestionId = (dataGridView1.Rows[e.RowIndex].Cells[3].FormattedValue.ToString());
                }
            }
        }

        private void btnThi_Click(object sender, EventArgs e)
        {
            if (_listQuestionId == null || _listQuestionId == "")
            {
                MessageBox.Show("Hãy chọn ít nhất một đề thi");
            }
            else
            {
                string startTime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                FormTakeExam formTakeExam = new FormTakeExam(_listQuestionId, startTime, _userId, _username, _isAdmin, _examId);
                Hide();
                formTakeExam.ShowDialog();
                Show();
            }
        }
    }
}
