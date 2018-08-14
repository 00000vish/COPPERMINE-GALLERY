using System;
using System.Net;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace EmmaWatsonGallery
{
    public partial class Form1 : Form
    {
        private bool stopDownload = false;
        private bool browserNavigated = false;
        private bool downloadComplete = false;
        private int x, y;
        private Point newpoint = new Point();
        private String selectedFolder = Properties.Settings.Default.FolderSetting;

        public Form1()
        {
            InitializeComponent();
            setupSettings();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            x = MousePosition.X - Location.X;
            y = MousePosition.Y - Location.Y;
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            pictureBox2.BackColor = Color.WhiteSmoke;
        }

        private void pictureBox2_MouseHover(object sender, EventArgs e)
        {
            pictureBox2.BackColor = Color.LightGray;
        }

        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            pictureBox1.BackColor = Color.LightGray;
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.BackColor = Color.WhiteSmoke;
        }

        private void pictureBox5_MouseHover(object sender, EventArgs e)
        {
            pictureBox5.BackColor = Color.LightGray;
        }

        private void pictureBox5_MouseLeave(object sender, EventArgs e)
        {
            pictureBox5.BackColor = Color.WhiteSmoke;
        }

        private void label2_MouseHover(object sender, EventArgs e)
        {
            label2.ForeColor = Color.LightGray;
            pictureBox3.BackColor = Color.LightGray;
        }

        private void label2_MouseLeave(object sender, EventArgs e)
        {
            label2.ForeColor = Color.WhiteSmoke;
            pictureBox3.BackColor = Color.WhiteSmoke;
        }

        private void label3_MouseHover(object sender, EventArgs e)
        {
            label3.ForeColor = Color.LightGray;
            pictureBox4.BackColor = Color.LightGray;
        }

        private void label3_MouseLeave(object sender, EventArgs e)
        {
            label3.ForeColor = Color.WhiteSmoke;
            pictureBox4.BackColor = Color.WhiteSmoke;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                newpoint = Control.MousePosition;
                newpoint.X -= x;
                newpoint.Y -= y;
                Location = newpoint;
                Application.DoEvents();
            }
        }

        private void setupSettings()
        {
            if (selectedFolder != "")
            {
                pictureBox3.Image = Properties.Resources.check;
            }
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Form2 form = new Form2();
            form.Show();
        }

        private void beginDownload(object sender, EventArgs e)
        {
            stopDownload = false;
            panel2.Visible = true;
            navigateBrowser();
        }

        private void navigateBrowser()
        {
            for (int index = Properties.Settings.Default.IndexPic; index < Properties.Settings.Default.MaxPic; index++)
            {
                label5.Text = index + "/" + Properties.Settings.Default.MaxPic;
                if (stopDownload)
                {
                    panel2.Visible = false;
                    break;
                }
                if (!System.IO.File.Exists(selectedFolder + "\\" + index + ".png"))
                    if (!System.IO.File.Exists(selectedFolder + "\\" + index + ".jpeg"))
                        if (!System.IO.File.Exists(selectedFolder + "\\" + index + ".jpg"))
                        {
                            downloadComplete = false;
                            browserNavigated = false;

                            webBrowser1.Navigate(Properties.Settings.Default.dataBase + "/displayimage.php?pid=" + index + "&fullsize=1");

                            Update();
                            do
                            {
                                Application.DoEvents();
                            } while (!browserNavigated);
                            saveImage(index);
                            do
                            {
                                Application.DoEvents();
                            } while (!downloadComplete);
                        }
            }
        }

        private int saveImage(int index)
        {
            try
            {
                foreach (HtmlElement img in webBrowser1.Document.Images)
                {
                    try
                    {
                        String extention = img.GetAttribute("src").ToLower().Split('.')[img.GetAttribute("src").ToLower().Split('.').Length - 1];
                        WebClient webClient = new WebClient();
                        webClient.DownloadFile(img.GetAttribute("src"), selectedFolder + "\\" + index + "." + extention.ToLower());
                    }
                    catch (Exception)
                    {
                        using (System.IO.StreamWriter file = new System.IO.StreamWriter(selectedFolder + "\\failedLinks.txt", true))
                        {
                            file.WriteLine(Properties.Settings.Default.dataBase + "/displayimage.php?pid=" + index + "&fullsize=1");
                        }
                    }
                }
            }
            catch (Exception e) { MessageBox.Show("Test" + e.ToString()); }
            downloadComplete = true;
            return 1;
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            browserNavigated = true;
        }

        private void label4_Click(object sender, EventArgs e)
        {
            stopDownload = true;
        }

        private void label2_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fb = new FolderBrowserDialog();
            if (fb.ShowDialog() == DialogResult.OK)
            {
                selectedFolder = fb.SelectedPath;
                Properties.Settings.Default.FolderSetting = selectedFolder;
                Properties.Settings.Default.Save();
                pictureBox3.Image = Properties.Resources.check;
            }
            else
            {
                selectedFolder = "";
                Properties.Settings.Default.FolderSetting = selectedFolder;
                Properties.Settings.Default.Save();
                pictureBox3.Image = Properties.Resources.folder;
            }
        }
    }
}
