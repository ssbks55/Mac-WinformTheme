using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace xThemes
{
    public partial class Core : FadeForm
    {
        #region VARIABLES

        internal Panel rightpnl;
        internal Panel nwsize;
        internal Panel swresize;
        internal Panel bottompnl;
        private ToolTip controlboxToolTip;
        private System.ComponentModel.IContainer components;
        private Point loc;
        private int pright;
        private panelmod panelmod1;
        public panelmod cmdMaxRes;
        internal panelmod cmdMin;
        private panelmod cmdClose;
        internal Panel toppnl;
        public Label titleCaption;
        internal Panel leftpnl;
        public panelmod bodypanel;
        internal Panel bottompnl2;
        internal Panel rightpnl2;
        internal Panel leftpnl2;
        private int originalleft;
        private int originaltop;
        private int pbottom;
        internal bool winMaxed = false;
        internal Size originalSize;
        private int theWidth;
        private ResizeLocation rLoc;
        //Windows API Constants (Form Resize)
        internal const int WM_NCLBUTTONDOWN = 161;
        internal const int HT_CAPTION = 0x2;
        internal const int HTBOTTOM = 15;
        internal const int HTBOTTOMLEFT = 16;
        internal const int HTBOTTOMRIGHT = 17;
        internal const int HTRIGHT = 11;
        internal const int HTLEFT = 10;
        internal const int HTTOP = 12;
        internal const int WS_MAXIMIZE = 0x01000000;
        internal const int WM_COMMAND = 0x111;
        internal const int SIZE_MAXIMIZED = 2;
        internal const int WM_SIZE = 0x0005;
        internal const int SW_MAXIMIZE = 3;
        internal const int SW_NORMAL = 1;
        #endregion

        #region ENUM
        private enum ResizeLocation
        {
            //Determines the position of the cursor.
            top = 0,
            left = 1,
            right = 2,
            bottom = 3,
            bottomleft = 4,
            bottomright = 5,
            none = 6,
        }
        #endregion

        public Core()
            : base(false)
        {
            //Initializes the components and maximum size of the form.
            InitializeComponent();
            this.MaximumSize = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.rightpnl = new System.Windows.Forms.Panel();
            this.bottompnl = new System.Windows.Forms.Panel();
            this.nwsize = new System.Windows.Forms.Panel();
            this.swresize = new System.Windows.Forms.Panel();
            this.controlboxToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.leftpnl = new System.Windows.Forms.Panel();
            this.bodypanel = new xThemes.panelmod();
            this.bottompnl2 = new System.Windows.Forms.Panel();
            this.rightpnl2 = new System.Windows.Forms.Panel();
            this.leftpnl2 = new System.Windows.Forms.Panel();
            this.panelmod1 = new xThemes.panelmod();
            this.cmdMaxRes = new xThemes.panelmod();
            this.cmdMin = new xThemes.panelmod();
            this.cmdClose = new xThemes.panelmod();
            this.toppnl = new System.Windows.Forms.Panel();
            this.titleCaption = new System.Windows.Forms.Label();
            this.bodypanel.SuspendLayout();
            this.panelmod1.SuspendLayout();
            this.SuspendLayout();
            // 
            // rightpnl
            // 
            this.rightpnl.BackColor = System.Drawing.Color.Black;
            this.rightpnl.Cursor = System.Windows.Forms.Cursors.SizeWE;
            this.rightpnl.Dock = System.Windows.Forms.DockStyle.Right;
            this.rightpnl.Location = new System.Drawing.Point(650, 22);
            this.rightpnl.Name = "rightpnl";
            this.rightpnl.Size = new System.Drawing.Size(1, 378);
            this.rightpnl.TabIndex = 1;
            this.rightpnl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.titlebar_MouseDown);
            this.rightpnl.MouseMove += new System.Windows.Forms.MouseEventHandler(this.rightpnl_MouseMove);
            this.rightpnl.MouseUp += new System.Windows.Forms.MouseEventHandler(this.bufferedPanel1_MouseUp);
            // 
            // bottompnl
            // 
            this.bottompnl.BackColor = System.Drawing.Color.Black;
            this.bottompnl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bottompnl.Cursor = System.Windows.Forms.Cursors.SizeNS;
            this.bottompnl.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bottompnl.Location = new System.Drawing.Point(0, 400);
            this.bottompnl.Name = "bottompnl";
            this.bottompnl.Size = new System.Drawing.Size(651, 1);
            this.bottompnl.TabIndex = 2;
            this.bottompnl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.titlebar_MouseDown);
            this.bottompnl.MouseMove += new System.Windows.Forms.MouseEventHandler(this.bottompnl_MouseMove);
            this.bottompnl.MouseUp += new System.Windows.Forms.MouseEventHandler(this.bufferedPanel1_MouseUp);
            // 
            // nwsize
            // 
            this.nwsize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.nwsize.Cursor = System.Windows.Forms.Cursors.SizeNESW;
            this.nwsize.Location = new System.Drawing.Point(1, 398);
            this.nwsize.Name = "nwsize";
            this.nwsize.Size = new System.Drawing.Size(2, 2);
            this.nwsize.TabIndex = 3;
            this.nwsize.MouseDown += new System.Windows.Forms.MouseEventHandler(this.titlebar_MouseDown);
            this.nwsize.MouseMove += new System.Windows.Forms.MouseEventHandler(this.nwsize_MouseMove);
            this.nwsize.MouseUp += new System.Windows.Forms.MouseEventHandler(this.bufferedPanel1_MouseUp);
            // 
            // swresize
            // 
            this.swresize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.swresize.Cursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.swresize.Location = new System.Drawing.Point(648, 398);
            this.swresize.Name = "swresize";
            this.swresize.Size = new System.Drawing.Size(2, 2);
            this.swresize.TabIndex = 4;
            this.swresize.MouseDown += new System.Windows.Forms.MouseEventHandler(this.titlebar_MouseDown);
            this.swresize.MouseMove += new System.Windows.Forms.MouseEventHandler(this.swresize_MouseMove);
            this.swresize.MouseUp += new System.Windows.Forms.MouseEventHandler(this.bufferedPanel1_MouseUp);
            // 
            // leftpnl
            // 
            this.leftpnl.BackColor = System.Drawing.Color.Black;
            this.leftpnl.Cursor = System.Windows.Forms.Cursors.SizeWE;
            this.leftpnl.Dock = System.Windows.Forms.DockStyle.Left;
            this.leftpnl.Location = new System.Drawing.Point(0, 22);
            this.leftpnl.Name = "leftpnl";
            this.leftpnl.Size = new System.Drawing.Size(1, 378);
            this.leftpnl.TabIndex = 0;
            this.leftpnl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.titlebar_MouseDown);
            this.leftpnl.MouseMove += new System.Windows.Forms.MouseEventHandler(this.leftpnl_MouseMove);
            this.leftpnl.MouseUp += new System.Windows.Forms.MouseEventHandler(this.bufferedPanel1_MouseUp);
            // 
            // bodypanel
            // 
            this.bodypanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bodypanel.BackColor = System.Drawing.Color.Transparent;
            this.bodypanel.Controls.Add(this.bottompnl2);
            this.bodypanel.Controls.Add(this.rightpnl2);
            this.bodypanel.Controls.Add(this.leftpnl2);
            this.bodypanel.Location = new System.Drawing.Point(1, 22);
            this.bodypanel.Name = "bodypanel";
            this.bodypanel.Size = new System.Drawing.Size(649, 376);
            this.bodypanel.TabIndex = 6;
            // 
            // bottompnl2
            // 
            this.bottompnl2.BackColor = System.Drawing.Color.Transparent;
            this.bottompnl2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bottompnl2.Cursor = System.Windows.Forms.Cursors.SizeNS;
            this.bottompnl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bottompnl2.Location = new System.Drawing.Point(2, 373);
            this.bottompnl2.Name = "bottompnl2";
            this.bottompnl2.Size = new System.Drawing.Size(644, 3);
            this.bottompnl2.TabIndex = 11;
            this.bottompnl2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.titlebar_MouseDown);
            this.bottompnl2.MouseMove += new System.Windows.Forms.MouseEventHandler(this.bottompnl_MouseMove);
            this.bottompnl2.MouseUp += new System.Windows.Forms.MouseEventHandler(this.bufferedPanel1_MouseUp);
            // 
            // rightpnl2
            // 
            this.rightpnl2.BackColor = System.Drawing.Color.Transparent;
            this.rightpnl2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.rightpnl2.Cursor = System.Windows.Forms.Cursors.SizeWE;
            this.rightpnl2.Dock = System.Windows.Forms.DockStyle.Right;
            this.rightpnl2.Location = new System.Drawing.Point(646, 0);
            this.rightpnl2.Name = "rightpnl2";
            this.rightpnl2.Size = new System.Drawing.Size(3, 376);
            this.rightpnl2.TabIndex = 10;
            this.rightpnl2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.titlebar_MouseDown);
            this.rightpnl2.MouseMove += new System.Windows.Forms.MouseEventHandler(this.rightpnl_MouseMove);
            this.rightpnl2.MouseUp += new System.Windows.Forms.MouseEventHandler(this.bufferedPanel1_MouseUp);
            // 
            // leftpnl2
            // 
            this.leftpnl2.BackColor = System.Drawing.Color.Transparent;
            this.leftpnl2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.leftpnl2.Cursor = System.Windows.Forms.Cursors.SizeWE;
            this.leftpnl2.Dock = System.Windows.Forms.DockStyle.Left;
            this.leftpnl2.Location = new System.Drawing.Point(0, 0);
            this.leftpnl2.Name = "leftpnl2";
            this.leftpnl2.Size = new System.Drawing.Size(2, 376);
            this.leftpnl2.TabIndex = 9;
            this.leftpnl2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.titlebar_MouseDown);
            this.leftpnl2.MouseMove += new System.Windows.Forms.MouseEventHandler(this.leftpnl_MouseMove);
            this.leftpnl2.MouseUp += new System.Windows.Forms.MouseEventHandler(this.bufferedPanel1_MouseUp);
            // 
            // panelmod1
            // 
            this.panelmod1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panelmod1.BackgroundImage = global::xThemes.Properties.Resources.titlebar;
            this.panelmod1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelmod1.Controls.Add(this.cmdMaxRes);
            this.panelmod1.Controls.Add(this.cmdMin);
            this.panelmod1.Controls.Add(this.cmdClose);
            this.panelmod1.Controls.Add(this.toppnl);
            this.panelmod1.Controls.Add(this.titleCaption);
            this.panelmod1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelmod1.Location = new System.Drawing.Point(0, 0);
            this.panelmod1.Name = "panelmod1";
            this.panelmod1.Size = new System.Drawing.Size(651, 22);
            this.panelmod1.TabIndex = 5;
            this.panelmod1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.panelmod1_MouseDoubleClick);
            this.panelmod1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.titlebar_MouseDown);
            this.panelmod1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.titlebar_MouseMove);
            this.panelmod1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.bufferedPanel1_MouseUp);
            // 
            // cmdMaxRes
            // 
            this.cmdMaxRes.BackColor = System.Drawing.Color.Transparent;
            this.cmdMaxRes.BackgroundImage = global::xThemes.Properties.Resources.controlbutton_normal;
            this.cmdMaxRes.Location = new System.Drawing.Point(47, 3);
            this.cmdMaxRes.Name = "cmdMaxRes";
            this.cmdMaxRes.Size = new System.Drawing.Size(16, 16);
            this.cmdMaxRes.TabIndex = 12;
            this.controlboxToolTip.SetToolTip(this.cmdMaxRes, "Maximize/Restore");
            this.cmdMaxRes.Click += new System.EventHandler(this.cmdMaxRes_Click);
            this.cmdMaxRes.Paint += new System.Windows.Forms.PaintEventHandler(this.cmdMaxRes_Paint);
            this.cmdMaxRes.MouseLeave += new System.EventHandler(this.cmdMaxRes_MouseLeave);
            this.cmdMaxRes.MouseMove += new System.Windows.Forms.MouseEventHandler(this.cmdMaxRes_MouseMove);
            // 
            // cmdMin
            // 
            this.cmdMin.BackColor = System.Drawing.Color.Transparent;
            this.cmdMin.BackgroundImage = global::xThemes.Properties.Resources.controlbutton_normal;
            this.cmdMin.Location = new System.Drawing.Point(25, 3);
            this.cmdMin.Name = "cmdMin";
            this.cmdMin.Size = new System.Drawing.Size(16, 16);
            this.cmdMin.TabIndex = 11;
            this.controlboxToolTip.SetToolTip(this.cmdMin, "Minimize");
            this.cmdMin.Click += new System.EventHandler(this.cmdMin_Click);
            this.cmdMin.MouseLeave += new System.EventHandler(this.cmdMin_MouseLeave);
            this.cmdMin.MouseMove += new System.Windows.Forms.MouseEventHandler(this.cmdMin_MouseMove);
            // 
            // cmdClose
            // 
            this.cmdClose.BackColor = System.Drawing.Color.Transparent;
            this.cmdClose.BackgroundImage = global::xThemes.Properties.Resources.controlbutton_normal;
            this.cmdClose.Location = new System.Drawing.Point(3, 3);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(16, 16);
            this.cmdClose.TabIndex = 10;
            this.controlboxToolTip.SetToolTip(this.cmdClose, "Close");
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            this.cmdClose.MouseLeave += new System.EventHandler(this.cmdClose_MouseLeave);
            this.cmdClose.MouseMove += new System.Windows.Forms.MouseEventHandler(this.cmdClose_MouseMove);
            // 
            // toppnl
            // 
            this.toppnl.BackColor = System.Drawing.Color.Transparent;
            this.toppnl.Cursor = System.Windows.Forms.Cursors.SizeNS;
            this.toppnl.Dock = System.Windows.Forms.DockStyle.Top;
            this.toppnl.Location = new System.Drawing.Point(0, 0);
            this.toppnl.Name = "toppnl";
            this.toppnl.Size = new System.Drawing.Size(649, 3);
            this.toppnl.TabIndex = 9;
            this.toppnl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.titlebar_MouseDown);
            this.toppnl.MouseMove += new System.Windows.Forms.MouseEventHandler(this.toppnl_MouseMove);
            this.toppnl.MouseUp += new System.Windows.Forms.MouseEventHandler(this.bufferedPanel1_MouseUp);
            // 
            // titleCaption
            // 
            this.titleCaption.AutoEllipsis = true;
            this.titleCaption.AutoSize = true;
            this.titleCaption.BackColor = System.Drawing.Color.Transparent;
            this.titleCaption.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titleCaption.Location = new System.Drawing.Point(257, 3);
            this.titleCaption.Name = "titleCaption";
            this.titleCaption.Size = new System.Drawing.Size(130, 14);
            this.titleCaption.TabIndex = 13;
            this.titleCaption.Text = "WindowsFormApplication";
            this.titleCaption.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.titleCaption.Layout += new System.Windows.Forms.LayoutEventHandler(this.titleCaption_Layout);
            this.titleCaption.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.panelmod1_MouseDoubleClick);
            this.titleCaption.MouseDown += new System.Windows.Forms.MouseEventHandler(this.titlebar_MouseDown);
            this.titleCaption.MouseMove += new System.Windows.Forms.MouseEventHandler(this.titlebar_MouseMove);
            this.titleCaption.MouseUp += new System.Windows.Forms.MouseEventHandler(this.bufferedPanel1_MouseUp);
            // 
            // Core
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(232)))), ((int)(((byte)(232)))));
            this.ClientSize = new System.Drawing.Size(651, 401);
            this.Controls.Add(this.bodypanel);
            this.Controls.Add(this.leftpnl);
            this.Controls.Add(this.rightpnl);
            this.Controls.Add(this.panelmod1);
            this.Controls.Add(this.swresize);
            this.Controls.Add(this.nwsize);
            this.Controls.Add(this.bottompnl);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MinimumSize = new System.Drawing.Size(250, 25);
            this.Name = "Core";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "WindowsFormApplication";
            this.Activated += new System.EventHandler(this.mactheme_Activated);
            this.Deactivate += new System.EventHandler(this.mactheme_Deactivate);
            this.Resize += new System.EventHandler(this.mactheme_Resize);
            this.bodypanel.ResumeLayout(false);
            this.panelmod1.ResumeLayout(false);
            this.panelmod1.PerformLayout();
            this.ResumeLayout(false);

        }

        private void resize(ResizeLocation e)
        {
            //The resize function using the Windows API (SendMessage() and ReleaseCapture()).
            int dir = -1;
            switch (e)
            {
                case ResizeLocation.top:
                    dir = HTTOP;
                    break;
                case ResizeLocation.left:
                    dir = HTLEFT;
                    break;
                case ResizeLocation.right:
                    dir = HTRIGHT;
                    break;
                case ResizeLocation.bottom:
                    dir = HTBOTTOM;
                    break;
                case ResizeLocation.bottomleft:
                    dir = HTBOTTOMLEFT;
                    break;
                case ResizeLocation.bottomright:
                    dir = HTBOTTOMRIGHT;
                    break;
            }
            if (dir != -1)
            {
                API.ReleaseCapture();
                API.SendMessage(this.Handle, WM_NCLBUTTONDOWN, dir, 0);
            }
        }

        internal void swresize_MouseMove(object sender, MouseEventArgs e)
        {
            //The bottom right corner resize function.
            if (MouseButtons.ToString() == "Left")
            {
                if (this.WindowState != FormWindowState.Maximized)
                {
                    resize(rLoc);
                }
            }
        }

        internal void nwsize_MouseMove(object sender, MouseEventArgs e)
        {
            //Resize function (left and bottom side)
            if (MouseButtons.ToString() == "Left")
            {
                if (this.WindowState != FormWindowState.Maximized)
                {
                    resize(rLoc);
                }
            }
        }

        private void titleCaption_Layout(object sender, LayoutEventArgs e)
        {
            //recenter the caption
            int formWidth = this.Width;
            int captionWidth = titleCaption.Width;
            if (titleCaption.Left < cmdMaxRes.Right)
            {
                titleCaption.Left = cmdMaxRes.Right + 5;
            }
            else if (titleCaption.Left > cmdMaxRes.Right + 5)
            {
                titleCaption.Left = this.Width / 2 - (titleCaption.Width / 2);
            }
            if ((formWidth / 2) - (captionWidth / 2) > cmdMaxRes.Right)
            {
                titleCaption.Left = this.Width / 2 - (titleCaption.Width / 2);
            }
        }

        private void bufferedPanel1_MouseUp(object sender, MouseEventArgs e)
        {
            //releases the cursors limit when dragging the form around, to allow cursor to go to the taskbar.
            Cursor.Clip = Screen.PrimaryScreen.Bounds;
        }

        #region TITLE BAR
        private void titlebar_MouseDown(object sender, MouseEventArgs e)
        {
            //a shared MouseDown function(titleCaption,title bar, sizing edges).
            //Retrieves the form's current location on the screen.
            loc = e.Location;
            theWidth = this.Width;
            pright = this.Right;
            pbottom = this.Bottom;
            Cursor.Clip = Screen.PrimaryScreen.WorkingArea;
            //For the resizing edges...
            try
            {
                Panel panel = (Panel)sender;
                switch (panel.Name)
                {
                    case "toppnl":
                        rLoc = ResizeLocation.top;
                        break;
                    case "leftpnl":
                        rLoc = ResizeLocation.left;
                        break;
                    case "leftpnl2":
                        rLoc = ResizeLocation.left;
                        break;
                    case "rightpnl":
                        rLoc = ResizeLocation.right;
                        break;
                    case "rightpnl2":
                        rLoc = ResizeLocation.right;
                        break;
                    case "bottompnl":
                        rLoc = ResizeLocation.bottom;
                        break;
                    case "bottompnl2":
                        rLoc = ResizeLocation.bottom;
                        break;
                    case "nwsize":
                        rLoc = ResizeLocation.bottomleft;
                        break;
                    case "swresize":
                        rLoc = ResizeLocation.bottomright;
                        break;
                }
            }
            catch { }
        }

        private void titlebar_MouseMove(object sender, MouseEventArgs e)
        {
            //Function for form dragging.
            if (this.WindowState != FormWindowState.Maximized)
            {
                if (MouseButtons.ToString() == "Left")
                {
                    API.ReleaseCapture();
                    API.SendMessage(this.Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
                }
            }
        }
        #endregion

        #region TOP, RIGHT, LEFT & BOTTOM PANELS
        internal void toppnl_MouseMove(object sender, MouseEventArgs e)
        {
            //Resize function (top)
            if (MouseButtons.ToString() == "Left")
            {
                if (this.WindowState != FormWindowState.Maximized)
                {
                    resize(rLoc);
                }
            }
        }

        internal void rightpnl_MouseMove(object sender, MouseEventArgs e)
        {
            //Resize Function (right):
            if (MouseButtons.ToString() == "Left")
            {
                if (this.WindowState != FormWindowState.Maximized)
                {
                    resize(rLoc);
                }
            }
        }

        internal void leftpnl_MouseMove(object sender, MouseEventArgs e)
        {
            //Resize function (left side)
            if (MouseButtons.ToString() == "Left")
            {
                if (this.WindowState != FormWindowState.Maximized)
                {
                    resize(rLoc);
                }
            }
        }

        internal void bottompnl_MouseMove(object sender, MouseEventArgs e)
        {
            //Resize Function (bottom);
            if (MouseButtons.ToString() == "Left")
            {
                if (this.WindowState != FormWindowState.Maximized)
                {
                    resize(rLoc);
                }
            }
        }
        #endregion

        #region THEME RESIZE, ACTIVATION & DEACTIVATION
        private void mactheme_Activated(object sender, EventArgs e)
        {
            //Sets the the titlebar's background to its normal color to determine that it's active.
            panelmod1.BackgroundImage = Properties.Resources.titlebar;
        }

        private void mactheme_Deactivate(object sender, EventArgs e)
        {
            //Sets the titlebar's background to a lighter color to determine that it's inactive and reset the cursor limits.
            panelmod1.BackgroundImage = Properties.Resources.titlebarunfocused1;
            Cursor.Clip = Screen.PrimaryScreen.Bounds;
        }

        private void mactheme_Resize(object sender, EventArgs e)
        {
            //Function to center the caption position and update the form to avoid flickers.
            //Always determine the maximum workingArea of the screen.
            this.MaximumSize = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
            int formWidth = this.Width;
            int captionWidth = titleCaption.Width;
            if (titleCaption.Left < cmdMaxRes.Right)
            {
                titleCaption.Left = cmdMaxRes.Right + 5;
            }
            else if (titleCaption.Left > cmdMaxRes.Right + 5)
            {
                titleCaption.Left = this.Width / 2 - (titleCaption.Width / 2);
            }
            if ((formWidth / 2) - (captionWidth / 2) > cmdMaxRes.Right)
            {
                titleCaption.Left = this.Width / 2 - (titleCaption.Width / 2);
            }
            this.Update();
        }
        #endregion

        #region CONTROL BOX BUTTONS
        private void cmdClose_MouseMove(object sender, MouseEventArgs e)
        {
            //change the backgroundimage...
            cmdClose.BackgroundImage = Properties.Resources.controlbutton_close;
        }

        private void cmdClose_MouseLeave(object sender, EventArgs e)
        {
            //restores the original button image...
            cmdClose.BackgroundImage = Properties.Resources.controlbutton_normal;
        }

        private void cmdMin_MouseMove(object sender, MouseEventArgs e)
        {
            //sets the background image to minimize...
            cmdMin.BackgroundImage = Properties.Resources.controlbutton_min;
        }

        private void cmdMin_MouseLeave(object sender, EventArgs e)
        {
            //restores the original image of cmdMin background image...
            cmdMin.BackgroundImage = Properties.Resources.controlbutton_normal;
        }

        private void cmdMaxRes_MouseMove(object sender, MouseEventArgs e)
        {
            //sets cmdMaxRes' background image '+'...
            cmdMaxRes.BackgroundImage = Properties.Resources.controlbutton_maxres;
        }

        private void cmdMaxRes_MouseLeave(object sender, EventArgs e)
        {
            //restores cmdMaxRes' background image to normal...
            cmdMaxRes.BackgroundImage = Properties.Resources.controlbutton_normal;
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            //Closes the form.
            this.Close();
            this.Dispose();
        }

        private void cmdMin_Click(object sender, EventArgs e)
        {
            //minimizes the form.
            this.WindowState = FormWindowState.Minimized;
            this.Show();
        }

        private void cmdMaxRes_Click(object sender, EventArgs e)
        {
            if (this.winMaxed == false)
            {
                //Maximizes the form and removes the resizing cursors of the edges.
                originalleft = this.Left;
                originaltop = this.Top;
                originalSize = this.Size;
                this.DesktopLocation = new Point(0, 0);
                this.Size = this.MaximumSize;
                leftpnl.Cursor = Cursors.Arrow;
                leftpnl2.Cursor = Cursors.Arrow;
                rightpnl.Cursor = Cursors.Arrow;
                rightpnl2.Cursor = Cursors.Arrow;
                bottompnl.Cursor = Cursors.Arrow;
                bottompnl2.Cursor = Cursors.Arrow;
                nwsize.Cursor = Cursors.Arrow;
                swresize.Cursor = Cursors.Arrow;
                toppnl.Cursor = Cursors.Arrow;

                leftpnl.MouseMove -= new MouseEventHandler(leftpnl_MouseMove);
                leftpnl2.MouseMove -= new MouseEventHandler(leftpnl_MouseMove);
                toppnl.MouseMove -= new MouseEventHandler(toppnl_MouseMove);
                bottompnl.MouseMove -= new MouseEventHandler(bottompnl_MouseMove);
                bottompnl2.MouseMove -= new MouseEventHandler(bottompnl_MouseMove);
                rightpnl.MouseMove -= new MouseEventHandler(rightpnl_MouseMove);
                rightpnl2.MouseMove -= new MouseEventHandler(rightpnl_MouseMove);
                swresize.MouseMove -= new MouseEventHandler(swresize_MouseMove);
                nwsize.MouseMove -= new MouseEventHandler(nwsize_MouseMove);
                titleCaption.MouseMove -= new MouseEventHandler(titlebar_MouseMove);
                panelmod1.MouseMove -= new MouseEventHandler(titlebar_MouseMove);

                winMaxed = true;
            }
            else
            {
                //Restores the form and returns the resizing cursors of the edges.
                API.ShowWindow(this.Handle, SW_NORMAL);
                this.Top = originaltop;
                this.Left = originalleft;
                this.Size = originalSize;
                leftpnl.Cursor = Cursors.SizeWE;
                leftpnl2.Cursor = Cursors.SizeWE;
                rightpnl.Cursor = Cursors.SizeWE;
                rightpnl2.Cursor = Cursors.SizeWE;
                bottompnl.Cursor = Cursors.SizeNS;
                bottompnl2.Cursor = Cursors.SizeNS;
                nwsize.Cursor = Cursors.SizeNESW;
                swresize.Cursor = Cursors.SizeNWSE;
                toppnl.Cursor = Cursors.SizeNS;

                leftpnl.MouseMove += new MouseEventHandler(leftpnl_MouseMove);
                leftpnl2.MouseMove += new MouseEventHandler(leftpnl_MouseMove);
                toppnl.MouseMove += new MouseEventHandler(toppnl_MouseMove);
                bottompnl.MouseMove += new MouseEventHandler(bottompnl_MouseMove);
                bottompnl2.MouseMove += new MouseEventHandler(bottompnl_MouseMove);
                rightpnl.MouseMove += new MouseEventHandler(rightpnl_MouseMove);
                rightpnl2.MouseMove += new MouseEventHandler(rightpnl_MouseMove);
                swresize.MouseMove += new MouseEventHandler(swresize_MouseMove);
                nwsize.MouseMove += new MouseEventHandler(nwsize_MouseMove);
                titleCaption.MouseMove += new MouseEventHandler(titlebar_MouseMove);
                panelmod1.MouseMove += new MouseEventHandler(titlebar_MouseMove);

                winMaxed = false;
            }
        }
        #endregion

        protected override CreateParams CreateParams
        {
            //Enables the form to be minimized through the taskbar.
            get
            {
                CreateParams cp = base.CreateParams;
                cp.Style |= 0x20000;
                return cp;
            }
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
        }

        public override string Text
        {
            //updates the titleCaption...
            get
            {
                return base.Text;
            }
            set
            {
                titleCaption.Text = value;
                base.Text = value;
            }
        }

        #region CONTROLS EVENTS
        private void panelmod1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //Maximizes or Restores the window when you double-click the titlebar, similar to a regular window.
            if (this.WindowState == FormWindowState.Maximized)
            {
                cmdMaxRes_Click(cmdMaxRes, null);
            }
            else if (this.WindowState == FormWindowState.Normal)
            {
                cmdMaxRes_Click(cmdMaxRes, null);
            }
        }

        private void cmdMaxRes_Paint(object sender, PaintEventArgs e)
        {

        }
        #endregion
    }

    public class panelmod : Panel
    {
        public panelmod()
        {
            //sets the DoubleBuffered property of the panel to true.
            this.DoubleBuffered = true;
        }
    }
}
