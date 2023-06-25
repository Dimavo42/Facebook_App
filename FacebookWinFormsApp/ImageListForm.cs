
using MyUser;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace MyFBApp
{
    public sealed partial class ImageListForm : Form, IReport
    {
        private readonly List<ImageListFormDataBinding> r_Images;
        private static ImageListForm s_Instance;
        private static Dictionary<string, int> s_UserReports;
        private IStrategy m_Strategy;
        public static ImageListForm CreateInstance(UserImages i_Images)
        {

            if (s_Instance == null)
            {
                s_Instance = new ImageListForm(i_Images);
                s_UserReports = new Dictionary<string, int>();
            }
            else
            {
                s_Instance.addImages(i_Images);
            }
            return s_Instance;
        }
        private void initBindingComboBox()
        {
            Dictionary<string, IStrategy> comboSource = new Dictionary<string, IStrategy>();
            IStrategy sort = new Sort();
            IStrategy sortRevers = new SortReverse();
            comboSource.Add("Sort", sort);
            comboSource.Add("SortReverse", sortRevers);
            strategyComboBox.DataSource = new BindingSource(comboSource, null);
            strategyComboBox.DisplayMember = "Key";
        }
        private ImageListForm(UserImages i_Images)
        {
            InitializeComponent();
            r_Images = new List<ImageListFormDataBinding>();
            initBindingComboBox();
            foreach (ImageListFormDataBinding image in i_Images)
            {
                imageList.Images.Add(image.Image);
                r_Images.Add(image);
            }
            imageListFormDataBindingBindingSource.DataSource = r_Images;
        }

        private void addImages(UserImages i_Images)
        {
            imageListFormDataBindingBindingSource.DataSource = null;
            foreach (ImageListFormDataBinding image in i_Images)
            {
                r_Images.Add(image);
                imageList.Images.Add(image.Image);
            }
            imageListFormDataBindingBindingSource.DataSource = r_Images;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            imageList.Images.Clear();
            s_Instance = null;
        }



        private void save_Click(object sender, EventArgs e)
        {
            if (imageListFormDataBindingDataGridView.SelectedRows.Count <= 0)
            {
                MessageBox.Show("Please select at least one row to save");
            }
            else
            {
                SaveFileDialog dialog = new SaveFileDialog
                {
                    Title = "Save Images",
                    Filter = "JPEG Image|*.jpg"
                };
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    foreach (DataGridViewRow selectedRow in imageListFormDataBindingDataGridView.SelectedRows)
                    {
                        imageList.Images[selectedRow.Index].Save(dialog.FileName);
                        MakeSelfReport(sender);
                    }
                    MessageBox.Show($"Successfully saved {imageListFormDataBindingDataGridView.SelectedRows.Count} images to {dialog.FileName}");
                }
            }
        }


        private void delete_Click(object sender, EventArgs e)
        {
            if (imageListFormDataBindingDataGridView.SelectedRows.Count <= 0)
            {
                MessageBox.Show("Please select at least one row to delete");
                return;
            }
            foreach (DataGridViewRow selectedRow in imageListFormDataBindingDataGridView.SelectedRows)
            {
                imageListFormDataBindingDataGridView.Rows.RemoveAt(selectedRow.Index);
                MakeSelfReport(sender);
            }
            if (imageListFormDataBindingDataGridView.Rows.Count == 0)
            {
                Close();
            }
        }

        public void MakeSelfReport(object i_Sender)
        {
            string buttonText = (i_Sender as Button).Text;
            s_UserReports[buttonText] = s_UserReports.ContainsKey(buttonText) ? s_UserReports[buttonText] + 1 : 1;
        }

        public string Report()
        {
            StringBuilder sb = new StringBuilder();
            foreach (KeyValuePair<string, int> pair in s_UserReports)
            {
                sb.AppendLine($"{pair.Key} {pair.Value}");
            }
            return sb.ToString();
        }

        public bool HasReport() => s_UserReports.Count > 0;

        public void ClearReports() => s_UserReports.Clear();

        private void comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_Strategy = ((KeyValuePair<string, IStrategy>)strategyComboBox.SelectedItem).Value;
        }

        private void operation_Click(object sender, EventArgs e)
        {
            m_Strategy.Operation(r_Images);
            imageListFormDataBindingBindingSource.DataSource = null;
            imageListFormDataBindingBindingSource.DataSource = r_Images;
            MakeSelfReport(sender);
            MessageBox.Show($"Number of operations was:{m_Strategy.OperationCount()}");

        }
    }
}
