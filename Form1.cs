namespace TP1
{
    internal partial class Form1 : Form
    {
        Form2 formDeRegistro;
         Banco banco;
        public Form1()
        {
            banco = new Banco();
            formDeRegistro = new Form2(banco);
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(textBox1.Text, "[^0-9]"))
            {
                textBox1.Text = textBox1.Text.Remove(textBox1.Text.Length - 1);
            }
        }

        private void lbl_Registrarse_Click(object sender, EventArgs e)
        {
            this.Hide();
            
            formDeRegistro.Show();

        }
    }

}