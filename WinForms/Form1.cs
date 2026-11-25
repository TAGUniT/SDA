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
        int autoLabel;

        public Form1()
        {
            InitializeComponent();

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.RowHeadersVisible = false;

            tb_Label = textBox1;
            tb_Width = textBox2;
            tb_Length = textBox3;

            dataGridView1.CellContentClick += dataGridView1_CellContentClick;

            tb_Label.TextChanged += TextBox_TextChanged;
            tb_Width.TextChanged += TextBox_TextChanged;
            tb_Length.TextChanged += TextBox_TextChanged;

            AcceptButton = button1;

            var splitter = new Splitter { Dock = DockStyle.Right, Width = 5, MinSize = 250 };
            tabPage1.Controls.Add(splitter);
            tabPage1.Controls.SetChildIndex(splitter, 0);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label = tb_Label.Text.Trim();

            if (!double.TryParse(tb_Width.Text, out width)
                || !double.TryParse(tb_Length.Text, out length)
                || width <= 0 || length <= 0)
            {
                MessageBox.Show("Please enter valid values for width and length.");
                return;
            }

            if (label == string.Empty)
            {
                do
                {
                    label = "#" + ++autoLabel;
                }
                while (LabelExists(label));
            }
            else if (LabelExists(label))
            {
                MessageBox.Show($"Label \"{label}\" is already in use.");
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
                int rowIndex = dataGridView1.Rows.Add(
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

                dataGridView1.CurrentCell = dataGridView1.Rows[rowIndex].Cells[0];
                dataGridView1.Rows[rowIndex].Selected = true;
                dataGridView1.FirstDisplayedScrollingRowIndex = rowIndex;
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
                bool newestPlaceholder =
                    dataGridView1.Rows[e.RowIndex].Cells[1].Value?.ToString() == "#" + autoLabel;

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

                if (newestPlaceholder)
                    autoLabel--;

                if (dataGridView1.Rows.Count == 0)
                    autoLabel = 0;
            }
        }

        private void TextBox_TextChanged(object? sender, EventArgs e)
        {
            if (sender is not TextBox tb || !tb.Focused)
                return;

            tabControl2.SelectedTab = tabPage3;
            tb.Focus();
        }

        private bool LabelExists(string value)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Index != editIndex && row.Cells[1].Value?.ToString() == value)
                    return true;
            }

            return false;
        }
    }
}
