namespace MyFBApp
{
    partial class ImageListForm
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
            this.ImageSaveBtn = new System.Windows.Forms.Button();
            this.ImgDeleteBtn = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.imageListFormDataBindingDataGridView = new System.Windows.Forms.DataGridView();
            this.dataGridViewImageColumn2 = new System.Windows.Forms.DataGridViewImageColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.imageListFormDataBindingBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.strategyComboBox = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.numberOfLikesLable = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageListFormDataBindingDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageListFormDataBindingBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // ImageSaveBtn
            // 
            this.ImageSaveBtn.Location = new System.Drawing.Point(777, 90);
            this.ImageSaveBtn.Name = "ImageSaveBtn";
            this.ImageSaveBtn.Size = new System.Drawing.Size(86, 58);
            this.ImageSaveBtn.TabIndex = 2;
            this.ImageSaveBtn.Text = "Save  Image";
            this.ImageSaveBtn.UseVisualStyleBackColor = true;
            this.ImageSaveBtn.Click += new System.EventHandler(this.save_Click);
            // 
            // ImgDeleteBtn
            // 
            this.ImgDeleteBtn.Location = new System.Drawing.Point(777, 177);
            this.ImgDeleteBtn.Name = "ImgDeleteBtn";
            this.ImgDeleteBtn.Size = new System.Drawing.Size(86, 64);
            this.ImgDeleteBtn.TabIndex = 3;
            this.ImgDeleteBtn.Text = "Remove  Image";
            this.ImgDeleteBtn.UseVisualStyleBackColor = true;
            this.ImgDeleteBtn.Click += new System.EventHandler(this.delete_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.imageListFormDataBindingDataGridView);
            this.panel1.Location = new System.Drawing.Point(87, 76);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(626, 420);
            this.panel1.TabIndex = 4;
            // 
            // imageListFormDataBindingDataGridView
            // 
            this.imageListFormDataBindingDataGridView.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.imageListFormDataBindingDataGridView.AllowUserToAddRows = false;
            this.imageListFormDataBindingDataGridView.AutoGenerateColumns = false;
            this.imageListFormDataBindingDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.imageListFormDataBindingDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewImageColumn2,
            this.dataGridViewTextBoxColumn1});
            this.imageListFormDataBindingDataGridView.DataSource = this.imageListFormDataBindingBindingSource;
            this.imageListFormDataBindingDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageListFormDataBindingDataGridView.Location = new System.Drawing.Point(0, 0);
            this.imageListFormDataBindingDataGridView.Name = "imageListFormDataBindingDataGridView";
            this.imageListFormDataBindingDataGridView.RowHeadersWidth = 62;
            this.imageListFormDataBindingDataGridView.RowTemplate.Height = 28;
            this.imageListFormDataBindingDataGridView.Size = new System.Drawing.Size(626, 420);
            this.imageListFormDataBindingDataGridView.TabIndex = 0;
            // 
            // dataGridViewImageColumn2
            // 
            this.dataGridViewImageColumn2.DataPropertyName = "Image";
            this.dataGridViewImageColumn2.HeaderText = "Image";
            this.dataGridViewImageColumn2.MinimumWidth = 8;
            this.dataGridViewImageColumn2.Name = "dataGridViewImageColumn2";
            this.dataGridViewImageColumn2.ReadOnly = true;
            this.dataGridViewImageColumn2.Width = 150;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "NumberOfLikes";
            this.dataGridViewTextBoxColumn1.HeaderText = "NumberOfLikes";
            this.dataGridViewTextBoxColumn1.MinimumWidth = 8;
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Width = 150;
            // 
            // imageListFormDataBindingBindingSource
            // 
            this.imageListFormDataBindingBindingSource.DataSource = typeof(MyFBApp.ImageListFormDataBinding);
            // 
            // 
            this.imageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // strategyComboBox
            // 
            this.strategyComboBox.FormattingEnabled = true;
            this.strategyComboBox.Location = new System.Drawing.Point(742, 260);
            this.strategyComboBox.Name = "strategyComboBox";
            this.strategyComboBox.Size = new System.Drawing.Size(121, 28);
            this.strategyComboBox.TabIndex = 5;
            this.strategyComboBox.SelectedIndexChanged += new System.EventHandler(this.comboBox_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(742, 305);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(120, 39);
            this.button1.TabIndex = 6;
            this.button1.Text = "Sort";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.operation_Click);
            // 
            // numberOfLikesLable
            // 
            this.numberOfLikesLable.AutoSize = true;
            this.numberOfLikesLable.Location = new System.Drawing.Point(83, 44);
            this.numberOfLikesLable.Name = "numberOfLikesLable";
            this.numberOfLikesLable.Size = new System.Drawing.Size(281, 20);
            this.numberOfLikesLable.TabIndex = 7;
            this.numberOfLikesLable.Text = "Change number of likes manule to sort";
            // 
            // ImageListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(889, 496);
            this.Controls.Add(this.numberOfLikesLable);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.strategyComboBox);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.ImgDeleteBtn);
            this.Controls.Add(this.ImageSaveBtn);
            this.Name = "ImageListForm";
            this.Text = "PictureSlide";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imageListFormDataBindingDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageListFormDataBindingBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button ImageSaveBtn;
        private System.Windows.Forms.Button ImgDeleteBtn;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn1;
        private System.Windows.Forms.DataGridView imageListFormDataBindingDataGridView;
        private System.Windows.Forms.BindingSource imageListFormDataBindingBindingSource;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.ComboBox strategyComboBox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label numberOfLikesLable;
    }
}