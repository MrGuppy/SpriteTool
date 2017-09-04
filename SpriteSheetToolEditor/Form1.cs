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
        //new bitmap
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

            //allows for the picturebox to de dropped 
            pictureBox1.AllowDrop = true;
           
        }


        private void saveProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //creating new savefiledialog
            SaveFileDialog save = new SaveFileDialog();
            //saving file type
            save.Filter = "Files|*.gif,*.png,*.jpg";
            //showing title of file 
            save.Title = "Save File Location";
            save.ShowDialog();

            saveProjectToolStripMenuItem.Image = pictureBox1.Image;

            if (save.FileName != "")
            {
                //saves file using OpenFile
                FileStream fileStream = (FileStream)save.OpenFile();
             
                try
                {
                    //------------------------------------------------
                    //  Using a try and catch method for checking if 
                    //  anything has been loaded to save out
                    //------------------------------------------------

                    //switch statement allows for the filterindex to save out a jpg, png or gif file
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
                //closing the filestream when finished 
                fileStream.Close();
            }                
        }

        private void bCreate_Click(object sender, EventArgs e)
        {
            //creating new open file dialog
            OpenFileDialog open = new OpenFileDialog();

            //checks if a dialog has been opened and opens a bitmap
            if (open.ShowDialog() == DialogResult.OK)
                b = (Bitmap)Image.FromFile(open.FileName); pictureboxT1.Image = b;

        }

        //Using a Fullscreen class that allows for fullscreen and to exit fullscreen 
        //---------------------------------------------------------------------
        class FullscreenUtility
        {
            //Lets fullscreen become accessable
            //by using Form as the parameters and returning true
            //for fullscreen access
            public bool AllowFullScreenAccess(Form DirectedForm)
             {
                 DirectedForm.TopMost = true;
                 DirectedForm.FormBorderStyle = FormBorderStyle.None;
                 DirectedForm.WindowState = FormWindowState.Maximized;

                 return true;
             }

            //Lets exit fullscreen option become accessable
            //by using Form as the parameters and returning false
            //for fullscreen access
            public bool DenyFullscreenMode(Form DirectedForm)
             {
                 DirectedForm.TopMost = false;
                 DirectedForm.FormBorderStyle = FormBorderStyle.Sizable;
                 DirectedForm.WindowState = FormWindowState.Normal;

                 return false;
             }
        }
        //---------------------------------------------------------------------
        private void exitFulscreenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //creating an instance of Fullscreen utility 
            FullscreenUtility fs = new FullscreenUtility();
            //calling the deny fullscreen function
            fs.DenyFullscreenMode(this);
        }

        private void FullScreen_Click(object sender, EventArgs e)
        {
            //creating an instance of Fullscreen utility 
            FullscreenUtility fs = new FullscreenUtility();
            //calling the allow fulscreen function
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
            //checks to see if the left mouse button was pressed 
            //if the left button has not been pressed return nothing 
            if (e.Button != MouseButtons.Left)
            {
                return;
            }

            //setting the endpoint to the location of the current mouse position
            Point endPoint = e.Location;
            //creating new rectangle with a point (has a min and max with a start and endpoint)
            rectangle.Location = new Point(Math.Min(startPoint.X, endPoint.X),
                                          (Math.Min(startPoint.Y, endPoint.Y)));
            //the size are now showing the absolute of the variables
            rectangle.Size = new Size(Math.Abs(startPoint.X - endPoint.X),
                                     (Math.Abs(startPoint.Y - endPoint.Y)));
            //picturebox is now being invalidated which allows for the rectangle to be drawn
            pictureBox1.Invalidate();
        }

        private void PB_Paint(object sender, PaintEventArgs e)
        {
            if(pictureBox1.Image != null)
            {
                //checks to see if the box has been clicked to draw 
                if(rectangle != null && rectangle.Width > 0 && rectangle.Height > 0)
                {
                    //if the rectangle is not null and the
                    //width and height is greater than 0 it will fill
                    //the rectangle with a brush and rectangle
                    e.Graphics.FillRectangle(select, rectangle);
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            //picturebox is now equal to the bitmap
            pictureBox1.Image = b;       
        }

        private void contactUsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //this directs user to an email site to message the developer - displays messagebox for user
            Process.Start("https://www.google.com/gmail");
            MessageBox.Show("Contact via email:\nMarkSturtz62@gmail.com");
        }

        //for picturebox1 main
        private void PBOnDragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Bitmap))
            {
                //gets the current bitmap data and allows for a copy effect   
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                //otherwise the effect is none and doesn't do anything
                e.Effect = DragDropEffects.None;
            }
        }

        private void PBMouseDown(object sender, MouseEventArgs e)
        {
            //does the process of drag and drop operation
            pictureboxT1.DoDragDrop(pictureboxT1.Image, DragDropEffects.Copy | DragDropEffects.Move);
        }

        //main
        private void PBOnDrop(object sender, DragEventArgs e)
        {
            //casting picturebox to a bitmap
            pictureboxT1.Image = (Bitmap)e.Data.GetData(DataFormats.Bitmap);
            //letting the image on Top image 1 (T1) drag to the main window 
            pictureBox1.Image = pictureboxT1.Image;
        }

        private void newProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //new openfile dialog
            OpenFileDialog open = new OpenFileDialog();
            //casting an image to a bitmap
            if (open.ShowDialog() == DialogResult.OK)
                b = (Bitmap)Image.FromFile(open.FileName); pictureBox1.Image = b;
        }

        private void ERightClick(object sender, MouseEventArgs e)
        {
            //checks if the right mouse button has been pressed 
            if(e.Button == MouseButtons.Right)
            {
                //creating new context menu strip and menu strip item
                ContextMenuStrip cms = new ContextMenuStrip();
                ToolStripMenuItem item1 = new ToolStripMenuItem();

                //calling item 'Hello'
                item1.Text = "hello";
                //allowing the click go to function that displays a messagebox
                item1.Click += DisplayRightClick;
                //adds all processes together in a collection
                cms.Items.Add(item1);
                this.ContextMenuStrip = cms;
            }
        }
        private void DisplayRightClick(object sender, EventArgs e)
        {
            MessageBox.Show("testing...");
        }


        //-`-`-`-`-`-`-`-`-`-`-`-`-`-`-`-`-`-`-`-`-`-`-`-`-`-`-`-`-`-`-`-`-`-`-`-`-`-`-`-`-`-`-`-`-`-`-`-`-
        //  Each of these rotate clicks are used 
        //  to flip an image left and right
        //
        //  Horizontally and vertically flip 
        //  the images and then save out 
        //
        //  They also use try and catch method for appropriate 
        //  error message handling 
        //
        //  then the image is inavlidated and draws itself again after the process 


        private void RotateLeft_Click(object sender, EventArgs e)
        {
            try
            {
                pictureBox1.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                pictureBox1.Refresh();
            }

            catch
            {
                MessageBox.Show("ERROR: Please Select and Image to Rotate");
            }

        }

        private void RotateRight_Click(object sender, EventArgs e)
        {
            try
            {
                pictureBox1.Image.RotateFlip(RotateFlipType.Rotate270FlipNone);
                pictureBox1.Refresh();
            }
            catch
            {
                MessageBox.Show("ERROR: Please Select and Image to Rotate");
            }
        }

        private void Horizontal_Click(object sender, EventArgs e)
        {
            try
            {
                pictureBox1.Image.RotateFlip(RotateFlipType.RotateNoneFlipX);
                pictureBox1.Refresh();
            }     
            catch
            {
                MessageBox.Show("ERROR: Please Select and Image to Rotate");
            }
        }

        private void Vertical_Click(object sender, EventArgs e)
        {
            try
            {
                pictureBox1.Image.RotateFlip(RotateFlipType.RotateNoneFlipY);
                pictureBox1.Refresh();
            }

            catch
            {
                MessageBox.Show("ERROR: Please Select and Image to Rotate");
            }
            //-`-`-`-`-`-`-`-`-`-`-`-`-`-`-`-`-`-`-`-`-`-`-`-`-`-`-`-`-`-`-`-`-`-`-`-`-`-`-`-`-`-`-`-`-`-`-`-`-
        }
    }
}
