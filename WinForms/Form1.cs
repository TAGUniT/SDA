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

        public Form1()
        {
            InitializeComponent();

            tb_Label = textBox1;
            tb_Width = textBox2;
            tb_Length = textBox3;

            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
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

            dataGridView1.Rows.Add(
                null!,
                label,
                width,
                length,
                width * length,
                "Edit",
                "Delete");
        }

        private void dataGridView1_CellContentClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            if (e.ColumnIndex == Column5.Index)
            {
                MessageBox.Show("action/edit");
            }
            else if (e.ColumnIndex == Column6.Index)
            {
                dataGridView1.Rows.RemoveAt(e.RowIndex);
            }
        }
    }
}
