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
using System.Diagnostics;

namespace SpriteSheetToolEditor
{
    public partial class SpriteEditor : Form
    {
        Bitmap b = new Bitmap(200, 200);

        //acts as the startpoint of event mousedown
        private Point startPoint;
        //the rectangle that will be drawn
        private Rectangle rectangle = new Rectangle();
        //the selection that will be used to grab the rectangle 
        private Brush select = new SolidBrush(Color.FromArgb(100, 80, 150, 200));




        public SpriteEditor()
        {
            InitializeComponent();



            //picturebox1.AllowDrop = true;

            //BitMap btmItems = new BitMap(); add Array of files across (.jpg, png, etc..)
            //"name of control".Items.AddRange(btmItems)
        }


        private void saveProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {

            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "Files|*.gif,*.png,*.jpg";
            save.Title = "Save File Location";
            save.ShowDialog();

            saveProjectToolStripMenuItem.Image = pictureBox1.Image;

            if (save.FileName != "")
            {
                //saves file using OpenFile
                FileStream fileStream = (FileStream)save.OpenFile();
             
                try
                {
                    switch (save.FilterIndex)
                    {
                        case 1:
                            saveProjectToolStripMenuItem.Image.Save(fileStream, System.Drawing.Imaging.ImageFormat.Gif);
                            break;

                        case 2:
                            saveProjectToolStripMenuItem.Image.Save(fileStream, System.Drawing.Imaging.ImageFormat.Png);
                            break;

                        case 3:
                            saveProjectToolStripMenuItem.Image.Save(fileStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                            break;

                    }
                }
                
                catch
                {
                    MessageBox.Show("ERROR: Load existing or create new image before saving.");
                }



                fileStream.Close();
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
            //textBox1.Text = e.Data.GetData(DataFormats.Bitmap).ToString();
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
                 DirectedForm.TopMost = false;
                 DirectedForm.FormBorderStyle = FormBorderStyle.Sizable;
                 DirectedForm.WindowState = FormWindowState.Normal;

                 return false;
             }

        }

        private void exitFulscreenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FullscreenUtility fs = new FullscreenUtility();
            fs.DenyFullscreenMode(this);
        }

        private void FullScreen_Click(object sender, EventArgs e)
        {
            FullscreenUtility fs = new FullscreenUtility();
            fs.AllowFullScreenAccess(this);
        }



        private void EMouseDown(object sender, MouseEventArgs e)
        {
            //finds the coordinates of the first event mouse down
            startPoint = e.Location;
            //allows for the control to be redrawn
            Invalidate();
        }

        private void EMouseMove(object sender, MouseEventArgs e)
        {
            //checks to see if the left ,ouse button was pressed 
            if(e.Button != MouseButtons.Left)
            {
                return;
            }

            Point endPoint = e.Location;
            rectangle.Location = new Point(Math.Min(startPoint.X, endPoint.X),
                                          (Math.Min(startPoint.Y, endPoint.Y)));
            rectangle.Size = new Size(Math.Abs(startPoint.X - endPoint.X),
                                     (Math.Abs(startPoint.Y - endPoint.Y)));
            pictureBox1.Invalidate();
        }

        private void PB_Paint(object sender, PaintEventArgs e)
        {
            if(pictureBox1.Image != null)
            {
                //checks to see if the box has been clicked to draw 
                if(rectangle != null && rectangle.Width > 0 && rectangle.Height > 0)
                {
                    e.Graphics.FillRectangle(select, rectangle);
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            ContextMenuStrip cms = new ContextMenuStrip();
            ToolStripMenuItem item1 = new ToolStripMenuItem();
            item1.Text = "hello";

            item1.Click += pictureBox1_Click;

            cms.Items.Add(item1);
            this.ContextMenuStrip = cms;

            MessageBox.Show("testing...");
        }

        private void contactUsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.google.com/gmail");
            MessageBox.Show("Contact via email:\nMarkSturtz62@gmail.com");
        }
    }
}
