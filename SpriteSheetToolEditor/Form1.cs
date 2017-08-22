using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


namespace SpriteSheetToolEditor
{
    public partial class SpriteEditor : Form
    {
        public SpriteEditor()
        {
            InitializeComponent();
        }

        private void MenuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void newWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void saveProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();

            save.Filter = "Files|*.txt,*.png,*.jpg";
            save.DefaultExt = "txt";
            if(save.ShowDialog() == DialogResult.OK)
                File.WriteAllText(save.FileName, "hello");
        }


        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            try
            {
                Bitmap bitMap = new Bitmap("Koala.jpg");
                bitMap.Save(@"C:\Users\s171736\Desktop\TestPic");
            }
              catch(Exception x)
            {
                MessageBox.Show("x.incorrct");
            }

           
            
        }

        private void bCreate_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            if (open.ShowDialog() == DialogResult.OK)
            {
                string text = System.IO.File.ReadAllText(open.FileName);
            }
        }
    }
}
