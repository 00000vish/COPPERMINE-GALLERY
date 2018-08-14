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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                int temp;
                int.TryParse(textBox1.Text, out temp);
                textBox1.Text = "" + (temp + 500);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                int temp;
                int.TryParse(textBox1.Text, out temp);
                textBox1.Text = "" + (temp - 500);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Currently there are 47276 pictures stored on http://iheartwatson.net, as it get updated this number goes up. You can check thier website and update the number to get the latest pictures.", "Coppermine Gallery", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                int temp;
                int.TryParse(textBox2.Text, out temp);
                textBox2.Text = "" + (temp + 500);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                int temp;
                int.TryParse(textBox2.Text, out temp);
                textBox2.Text = "" + (temp - 500);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            textBox3.Text = Properties.Settings.Default.FolderSetting;
            comboBox1.Items.Clear();
            foreach (var item in Properties.Settings.Default.dataBaseCollection.Split(new String[] { Environment.NewLine }, StringSplitOptions.None))
            {
                comboBox1.Items.Add(item);
            }
            comboBox1.Text = Properties.Settings.Default.dataBase;
            textBox1.Text = "" + Properties.Settings.Default.MaxPic;
            textBox2.Text = "" + Properties.Settings.Default.IndexPic;
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.Save();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            int temp = 0;
            int.TryParse(textBox1.Text, out temp);
            Properties.Settings.Default.MaxPic = temp;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            int temp = 0;
            int.TryParse(textBox2.Text, out temp);
            Properties.Settings.Default.IndexPic = temp;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            MessageBox.Show("If you want to download from 1000th picture change value to 1000 and start downloading.", "Coppermine Gallery", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Download from 2 different websites, emma-w.net images are large in file size but quite nicely oraganized, iheartwatson.net nicely compress thier images nicly without quality loss but organized badly. DO NOT DOWNLOAD BOTH TO SAME FOLDER!", "Emma Watson Gallery", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.dataBase = comboBox1.Text;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Form3 ADC = new Form3();
            ADC.Show();
        }
    }
}
