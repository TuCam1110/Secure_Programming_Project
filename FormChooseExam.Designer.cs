
namespace QLDThi
{
    partial class FormChooseExam
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.tblExamBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.qLDThiDataSet = new QLDThi.QLDThiDataSet();
            this.tblExamTableAdapter = new QLDThi.QLDThiDataSetTableAdapters.tblExamTableAdapter();
            this.btnThi = new System.Windows.Forms.Button();
            this.check = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.idDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.codeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.listQuestionIdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblExamBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.qLDThiDataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.check,
            this.idDataGridViewTextBoxColumn,
            this.codeDataGridViewTextBoxColumn,
            this.listQuestionIdDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.tblExamBindingSource;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(251, 138);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // tblExamBindingSource
            // 
            this.tblExamBindingSource.DataMember = "tblExam";
            this.tblExamBindingSource.DataSource = this.qLDThiDataSet;
            // 
            // qLDThiDataSet
            // 
            this.qLDThiDataSet.DataSetName = "QLDThiDataSet";
            this.qLDThiDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // tblExamTableAdapter
            // 
            this.tblExamTableAdapter.ClearBeforeFill = true;
            // 
            // btnThi
            // 
            this.btnThi.Location = new System.Drawing.Point(85, 155);
            this.btnThi.Name = "btnThi";
            this.btnThi.Size = new System.Drawing.Size(75, 37);
            this.btnThi.TabIndex = 1;
            this.btnThi.Text = "Vào thi";
            this.btnThi.UseVisualStyleBackColor = true;
            this.btnThi.Click += new System.EventHandler(this.btnThi_Click);
            // 
            // check
            // 
            this.check.Frozen = true;
            this.check.HeaderText = "Chọn";
            this.check.Name = "check";
            this.check.Width = 50;
            // 
            // idDataGridViewTextBoxColumn
            // 
            this.idDataGridViewTextBoxColumn.DataPropertyName = "id";
            this.idDataGridViewTextBoxColumn.Frozen = true;
            this.idDataGridViewTextBoxColumn.HeaderText = "id";
            this.idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
            this.idDataGridViewTextBoxColumn.ReadOnly = true;
            this.idDataGridViewTextBoxColumn.Width = 50;
            // 
            // codeDataGridViewTextBoxColumn
            // 
            this.codeDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.codeDataGridViewTextBoxColumn.DataPropertyName = "code";
            this.codeDataGridViewTextBoxColumn.Frozen = true;
            this.codeDataGridViewTextBoxColumn.HeaderText = "Mã đề";
            this.codeDataGridViewTextBoxColumn.Name = "codeDataGridViewTextBoxColumn";
            this.codeDataGridViewTextBoxColumn.Width = 108;
            // 
            // listQuestionIdDataGridViewTextBoxColumn
            // 
            this.listQuestionIdDataGridViewTextBoxColumn.DataPropertyName = "listQuestionId";
            this.listQuestionIdDataGridViewTextBoxColumn.Frozen = true;
            this.listQuestionIdDataGridViewTextBoxColumn.HeaderText = "listQuestionId";
            this.listQuestionIdDataGridViewTextBoxColumn.Name = "listQuestionIdDataGridViewTextBoxColumn";
            this.listQuestionIdDataGridViewTextBoxColumn.Visible = false;
            // 
            // FormChooseExam
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(253, 216);
            this.Controls.Add(this.btnThi);
            this.Controls.Add(this.dataGridView1);
            this.Name = "FormChooseExam";
            this.Text = "Danh mục thi";
            this.Load += new System.EventHandler(this.FormThi_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblExamBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.qLDThiDataSet)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private QLDThiDataSet qLDThiDataSet;
        private System.Windows.Forms.BindingSource tblExamBindingSource;
        private QLDThiDataSetTableAdapters.tblExamTableAdapter tblExamTableAdapter;
        private System.Windows.Forms.Button btnThi;
        private System.Windows.Forms.DataGridViewCheckBoxColumn check;
        private System.Windows.Forms.DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn codeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn listQuestionIdDataGridViewTextBoxColumn;
    }
}