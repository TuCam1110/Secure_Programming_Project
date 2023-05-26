using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLDThi
{
    public partial class FormTakeExam : Form
    {
        string connectionString = ConfigurationManager.ConnectionStrings["Test"].ConnectionString;
        string listQuestionIdRaw, thisListAnswer, _startTime, _username;
        bool _isAdmin;
        List<int> thisListQuestionId = new List<int>();
        List<string> thisListAnswerModified = new List<string>();
        List<string> listAnswerByUser  = new List<string>();
        List<string> listCorrectAnswer  = new List<string>();
        int count = 0, totalScore, _userId, _examId;

        public FormTakeExam(string listQuestionId, string startTime, int userId, string username, bool isAdmin, int examId)
        {
            InitializeComponent();
            _startTime = startTime;
            _userId = userId;
            _username = username;
            _isAdmin = isAdmin;
            _examId = examId;
            listQuestionIdRaw = listQuestionId;
            thisListQuestionId = listQuestionId != null ? listQuestionId?.TrimStart('[').TrimEnd(']').Split(',')?.Select(Int32.Parse)?.ToList() : new List<int>();
        }

        private void FormTakeExam_Load(object sender, EventArgs e)
        {
            GetListExam();
        }

        public void GetListExam()
        {
            DataTable dataTableAll = new DataTable();

            foreach (int item in thisListQuestionId)
            {
                var connection = new SqlConnection(connectionString);
                SqlCommand command1 = new SqlCommand("getQuestionById", connection);
                command1.CommandType = CommandType.StoredProcedure;
                SqlParameter thisCode = command1.Parameters.Add("@id", SqlDbType.Int);
                thisCode.Value = item;
                SqlDataAdapter dataAdapterQuestions = new SqlDataAdapter(command1);
                DataTable dataTableQuestion = new DataTable();
                dataAdapterQuestions.Fill(dataTableQuestion);
                dataTableAll.Merge(dataTableQuestion);
            }
            dataGridView1.DataSource = dataTableAll;
            totalScore = dataTableAll.Rows.Count;

            for (int i = 0; i < dataTableAll.Rows.Count; i++)
            {
                dataGridView1.Rows[i].Cells["question"].Value = Base64Decode(dataGridView1.Rows[i].Cells["question"].Value.ToString());
                dataGridView1.Rows[i].Cells["correctAnswer"].Value = Base64Decode(dataGridView1.Rows[i].Cells["correctAnswer"].Value.ToString());
                var answer = dataGridView1.Rows[i].Cells["listAnswer"].Value.ToString().TrimStart('[').TrimEnd(']').Split(',');
                dataGridView1.Rows[i].Cells["listAnswer"].Value = String.Format("[{0}, {1}, {2}, {3}]", Base64Decode(answer[0]), Base64Decode(answer[1]), Base64Decode(answer[2]), Base64Decode(answer[3]));
            }

            foreach (DataRow row in dataTableAll.Rows)
            {
                string answer = row["correctAnswer"].ToString();
                listCorrectAnswer.Add(answer);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex > 0 && e.RowIndex >= 0)
            {
                if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    dataGridView1.CurrentRow.Selected = true;

                    label2.Text = dataGridView1.Rows[e.RowIndex].Cells[1].FormattedValue.ToString();
                    thisListAnswer = dataGridView1.Rows[e.RowIndex].Cells[2].FormattedValue.ToString();
                    thisListAnswerModified = thisListAnswer.TrimStart('[').TrimEnd(']').Split(',').ToList();
                    rbA.Text = "A. " + thisListAnswerModified[0].ToString();
                    rbB.Text = "B. " + thisListAnswerModified[1].ToString();
                    rbC.Text = "C. " + thisListAnswerModified[2].ToString();
                    rbD.Text = "D. " + thisListAnswerModified[3].ToString();
                }
            }
        }

        private void rbA_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
            {
                var text = ((RadioButton)sender).Text;
                text = text.Split('A')[1].Trim();
                text = text.Split('.')[1].Trim();
                bool alreadyExist = listAnswerByUser.Contains(text);
                if(alreadyExist == false) listAnswerByUser.Add(text);
            }
            else if(((RadioButton)sender).Checked == false)
            {
                var text = ((RadioButton)sender).Text;
                text = text.Split('A')[1].Trim();
                text = text.Split('.')[1].Trim();
                listAnswerByUser.Remove(text);
            }
        }

        private void rbB_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
            {
                var text = ((RadioButton)sender).Text;
                text = text.Split('B')[1].Trim();
                text = text.Split('.')[1].Trim();
                bool alreadyExist = listAnswerByUser.Contains(text);
                if (alreadyExist == false) listAnswerByUser.Add(text);
            }
            else if (((RadioButton)sender).Checked == false)
            {
                var text = ((RadioButton)sender).Text;
                text = text.Split('B')[1].Trim();
                text = text.Split('.')[1].Trim();
                listAnswerByUser.Remove(text);
            }
        }

        private void rbC_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
            {
                var text = ((RadioButton)sender).Text;
                text = text.Split('C')[1].Trim();
                text = text.Split('.')[1].Trim();
                bool alreadyExist = listAnswerByUser.Contains(text);
                if (alreadyExist == false) listAnswerByUser.Add(text);
            }
            else if (((RadioButton)sender).Checked == false)
            {
                var text = ((RadioButton)sender).Text;
                text = text.Split('C')[1].Trim();
                text = text.Split('.')[1].Trim();
                listAnswerByUser.Remove(text);
            }
        }

        private void rbD_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
            {
                var text = ((RadioButton)sender).Text;
                text = text.Split('D')[1].Trim();
                text = text.Split('.')[1].Trim();
                bool alreadyExist = listAnswerByUser.Contains(text);
                if (alreadyExist == false) listAnswerByUser.Add(text);
            }
            else if (((RadioButton)sender).Checked == false)
            {
                var text = ((RadioButton)sender).Text;
                text = text.Split('D')[1].Trim();
                text = text.Split('.')[1].Trim();
                listAnswerByUser.Remove(text);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Bạn có chắc chắn muốn nộp bài?", "Xác nhận nộp bài", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                foreach (var item in listAnswerByUser)
                {
                    bool alreadyExist = listCorrectAnswer.Contains(item);
                    if (alreadyExist)
                    {
                        count++;
                    }
                }

                var connection = new SqlConnection(connectionString);
                var command = new SqlCommand("addResult", connection);
                command.CommandType = CommandType.StoredProcedure;
                SqlParameter score = command.Parameters.Add("score", SqlDbType.Int);
                score.Value = count;
                SqlParameter examId = command.Parameters.Add("examId", SqlDbType.Int);
                examId.Value = _examId;
                SqlParameter userId = command.Parameters.Add("userId", SqlDbType.Int);
                userId.Value = _userId;
                SqlParameter startTime = command.Parameters.Add("startTime", SqlDbType.VarChar, 50);
                startTime.Value = _startTime;
                SqlParameter endTime = command.Parameters.Add("endTime", SqlDbType.VarChar, 50);
                string _endTime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                endTime.Value = _endTime;
                SqlParameter _totalScore = command.Parameters.Add("totalScore", SqlDbType.Int);
                _totalScore.Value = totalScore;

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();

                MessageBox.Show("Nộp bài thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
                Hide();

                FormHome formHome = new FormHome (_username,_userId, _isAdmin);
                formHome.ShowDialog();
                Show();
            }
        }

        private static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
            return Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }
}
