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
        Bitmap b;

        public SpriteEditor()
        {
            InitializeComponent();

            //BitMap btmItems = new BitMap(); add Array of files across (.jpg, png, etc..)
            //"name of control".Items.AddRange(btmItems)
        }

        private void MenuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e){}

        private void fileToolStripMenuItem_Click(object sender, EventArgs e){}

        private void newWindowToolStripMenuItem_Click(object sender, EventArgs e){}
        private void pictureBox1_Click(object sender, EventArgs e){ }
      
        private void MouseDown_Test(object sender, MouseEventArgs e){}



        private void saveProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();

            save.Filter = "Files|*.txt,*.png,*.jpg";
            save.DefaultExt = ".jpg";
            if (save.ShowDialog() == DialogResult.OK)
            {
               
            }
                
        }

        private void bCreate_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();

            if (open.ShowDialog() == DialogResult.OK)
            {
                b = (Bitmap)Image.FromFile(open.FileName);
                pictureBox1.Image = b;
            }
        }

        private void tbDrag_Enter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Bitmap))
            {
                e.Effect = DragDropEffects.Move;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void tbDrag_Drop(object sender, DragEventArgs e)
        {
            textBox1.Text = e.Data.GetData(DataFormats.Bitmap).ToString();
        }

        private void bMouseDown(object sender, MouseEventArgs e)
        {
            bCreate.DoDragDrop(bCreate, DragDropEffects.Copy | DragDropEffects.Move);
        }

        class FullscreenUtility
        {
             public bool AllowFullScreenAccess(Form DirectedForm)
             {
                 DirectedForm.TopMost = true;
                 DirectedForm.FormBorderStyle = FormBorderStyle.None;
                 DirectedForm.WindowState = FormWindowState.Maximized;

                 return true;
             }

             public bool DenyFullscreenMode(Form DirectedForm)
             {
                 DirectedForm.FormBorderStyle = FormBorderStyle.Sizable;
                 DirectedForm.WindowState = FormWindowState.Normal;

                 return false;
             }
        }


        private void FullScreen_Click(object sender, EventArgs e)
        {
            FullscreenUtility fs = new FullscreenUtility();
            fs.AllowFullScreenAccess(this);
        }


    }
}
