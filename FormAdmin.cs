using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace QLDThi
{
    public partial class FormAdmin : Form
    {
        string connectionString = ConfigurationManager.ConnectionStrings["Test"].ConnectionString;
        String username, type;
        string question, correctAnswer, listAnswerRaw, code, _listQuestionId;
        int question_id, exam_id;
        string listAnswer;

        public FormAdmin(string _username)
        {
            InitializeComponent();
            username = _username;
        }

        private void FormAdmin_Load(object sender, EventArgs e)
        {
            lblWelcome.Text = "Xin chào " + username;
            ReloadForm();
            ReloadExamForm();
        }
        public void ReloadForm()
        {
            //danh sách câu hỏi
            var connection = new SqlConnection(connectionString);
            SqlCommand command1 = new SqlCommand("getListQuestion", connection);
            command1.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter dataAdapterQuestions = new SqlDataAdapter(command1);
            DataTable dataTableQuestion = new DataTable();
            dataAdapterQuestions.Fill(dataTableQuestion);
            dataGridView1.DataSource = dataTableQuestion;

            for (int i = 0; i < dataTableQuestion.Rows.Count; i++)
            {
                dataGridView1.Rows[i].Cells["_question"].Value = Base64Decode(dataGridView1.Rows[i].Cells["_question"].Value.ToString());
                dataGridView1.Rows[i].Cells["_correctAnswer"].Value = Base64Decode(dataGridView1.Rows[i].Cells["_correctAnswer"].Value.ToString());
                var answer = dataGridView1.Rows[i].Cells["_listAnswer"].Value.ToString().TrimStart('[').TrimEnd(']').Split(',');
                dataGridView1.Rows[i].Cells["_listAnswer"].Value = String.Format("[{0}, {1}, {2}, {3}]", Base64Decode(answer[0]), Base64Decode(answer[1]), Base64Decode(answer[2]), Base64Decode(answer[3]));
            }
        }

        public void ReloadExamForm()
        {
            //danh sách câu hỏi
            var connection = new SqlConnection(connectionString);
            SqlCommand command1 = new SqlCommand("getListExam", connection);
            command1.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter dataAdapterQuestions = new SqlDataAdapter(command1);
            DataTable dataTableExam = new DataTable();
            dataAdapterQuestions.Fill(dataTableExam);
            dataGridView2.DataSource = dataTableExam;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            type = "new";
            FormAddQuestion formAddQuestion = new FormAddQuestion(question_id, question, listAnswer, correctAnswer, type);
            formAddQuestion.ShowDialog();
            Show();
            ReloadForm();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            type = "delete";
            deleteData();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            type = "edit";
            FormAddQuestion formAddQuestion = new FormAddQuestion(question_id, question, listAnswerRaw, correctAnswer, type);
            formAddQuestion.ShowDialog();
            Show();
            ReloadForm();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            deleteExam();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            type = "new";
            FormAddExam formAddExam = new FormAddExam(exam_id, code, _listQuestionId, type);
            formAddExam.ShowDialog();
            Show();
            ReloadExamForm();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            type = "edit";
            FormAddExam formAddExam = new FormAddExam(exam_id, code, _listQuestionId, type);
            formAddExam.ShowDialog();
            Show();
            ReloadExamForm();
        }

        private void dataGridViewNV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                string checknull = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                if (!string.IsNullOrEmpty(checknull) && !string.IsNullOrWhiteSpace(checknull))
                {
                    dataGridView1.CurrentRow.Selected = true;
                    btnAdd.Enabled = true;
                    btnEdit.Enabled = true;
                    btnDelete.Enabled = true;

                    question_id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].FormattedValue);
                    question = (dataGridView1.Rows[e.RowIndex].Cells[1].FormattedValue.ToString());
                    listAnswerRaw = (dataGridView1.Rows[e.RowIndex].Cells[2].FormattedValue.ToString());
                    correctAnswer = (dataGridView1.Rows[e.RowIndex].Cells[3].FormattedValue.ToString());
                }
            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                string checknull = dataGridView2.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                if (!string.IsNullOrEmpty(checknull) && !string.IsNullOrWhiteSpace(checknull))
                {
                    dataGridView2.CurrentRow.Selected = true;

                    exam_id = Convert.ToInt32(dataGridView2.Rows[e.RowIndex].Cells[0].FormattedValue);
                    code = (dataGridView2.Rows[e.RowIndex].Cells[1].FormattedValue.ToString());
                    _listQuestionId = (dataGridView2.Rows[e.RowIndex].Cells[2].FormattedValue.ToString());
                }
            }
        }

        public void deleteData()
        {
            DialogResult dr = MessageBox.Show("Bạn có chắc chắn muốn xóa? Dữ liệu sẽ không thể khôi phục.", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                var connection = new SqlConnection(connectionString);
                var command = new SqlCommand("deleteQuestion", connection);
                command.CommandType = CommandType.StoredProcedure;

                SqlParameter id = command.Parameters.Add("id", SqlDbType.Int);
                id.Value = question_id;

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();

                MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ReloadForm();
            }
        }

        public void deleteExam()
        {
            DialogResult dr = MessageBox.Show("Bạn có chắc chắn muốn xóa? Dữ liệu sẽ không thể khôi phục.", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                var connection = new SqlConnection(connectionString);
                var command = new SqlCommand("deleteExam", connection);
                command.CommandType = CommandType.StoredProcedure;
                SqlParameter id = command.Parameters.Add("id", SqlDbType.Int);
                id.Value = exam_id;

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();

                MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ReloadExamForm();
            }
        }

        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
            return Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }
}
