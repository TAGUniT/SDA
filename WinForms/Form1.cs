namespace WinForms
{
    public partial class Form1 : Form
    {
        TextBox tb_Label;
        TextBox tb_Width;
        TextBox tb_Length;

        string label = string.Empty;
        double width;
        double length;
        int editIndex = -1;

        public Form1()
        {
            InitializeComponent();

            tb_Label = textBox1;
            tb_Width = textBox2;
            tb_Length = textBox3;

            dataGridView1.CellContentClick += dataGridView1_CellContentClick;

            tb_Label.TextChanged += TextBox_TextChanged;
            tb_Width.TextChanged += TextBox_TextChanged;
            tb_Length.TextChanged += TextBox_TextChanged;

            AcceptButton = button1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label = tb_Label.Text;

            if (label == string.Empty
                || !double.TryParse(tb_Width.Text, out width)
                || !double.TryParse(tb_Length.Text, out length)
                || width <= 0 || length <= 0)
            {
                MessageBox.Show("Please enter valid values for label, width, and length.");
                return;
            }

            if (editIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[editIndex];
                row.Cells[1].Value = label;
                row.Cells[2].Value = width;
                row.Cells[3].Value = length;
                row.Cells[4].Value = width * length;

                editIndex = -1;
                button1.Text = "Add";
                tabControl2.SelectedTab = tabPage4;
            }
            else
            {
                dataGridView1.Rows.Add(
                    null!,
                    label,
                    width,
                    length,
                    width * length,
                    "Edit",
                    "Delete");

                tb_Label.Clear();
                tb_Width.Clear();
                tb_Length.Clear();

                tabControl2.SelectedTab = tabPage4;
                tb_Label.Focus();
            }
        }

        private void dataGridView1_CellContentClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            if (e.ColumnIndex == Column5.Index)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                tb_Label.Text = row.Cells[1].Value?.ToString();
                tb_Width.Text = row.Cells[2].Value?.ToString();
                tb_Length.Text = row.Cells[3].Value?.ToString();

                editIndex = e.RowIndex;
                button1.Text = "Save";
                tabControl2.SelectedTab = tabPage3;
            }
            else if (e.ColumnIndex == Column6.Index)
            {
                if (e.RowIndex == editIndex)
                {
                    editIndex = -1;
                    button1.Text = "Add";
                    tb_Label.Clear();
                    tb_Width.Clear();
                    tb_Length.Clear();
                }
                else if (e.RowIndex < editIndex)
                {
                    editIndex--;
                }

                dataGridView1.Rows.RemoveAt(e.RowIndex);
            }
        }

        private void TextBox_TextChanged(object? sender, EventArgs e)
        {
            if (sender is not TextBox tb || !tb.Focused)
                return;

            tabControl2.SelectedTab = tabPage3;
            tb.Focus();
        }
    }
}
