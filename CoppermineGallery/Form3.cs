using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoppermineGallery
{
    public partial class Form3 : Form
    {
        private string url = "";
        private Form2 settings = null;

        public Form3()
        {
            InitializeComponent();
        }

        public void Show(Form2 settings)
        {
            this.settings = settings;
            Show();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.ToString().Split('/').Length >= 4)
            {
                url = textBox1.Text + "/displayimage.php?pid=" + 5 + "&fullsize=1";
                linkLabel1.Text = url;
                linkLabel1.Visible = true;
            }           
        }

        private void linkLabel1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(url);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(textBox1.Text + "/displayimage.php?pid=" + 0 + "&fullsize=1");
            System.Diagnostics.Process.Start(textBox1.Text + "/displayimage.php?pid=" + 10 + "&fullsize=1");
            System.Diagnostics.Process.Start(textBox1.Text + "/displayimage.php?pid=" + 100 + "&fullsize=1");
            System.Diagnostics.Process.Start(textBox1.Text + "/displayimage.php?pid=" + 1000 + "&fullsize=1");
            MessageBox.Show("If anyone of the links opened contain a picture click add databse, if not you have entered a invalid link.", "Add Database" , MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.dataBaseCollection = Properties.Settings.Default.dataBaseCollection + Environment.NewLine + textBox1.Text;
            Properties.Settings.Default.Save();
            settings.updateDatabaseList();
            Close();
        }
    }
}
