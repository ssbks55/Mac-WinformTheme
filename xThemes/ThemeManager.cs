﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace xThemes
{
    public class ThemeManager
    {
        #region PRIVATE METHODS
        private void ApplyXTheme_Controls(Form clientForm, Boolean IsEnabled)
        {
            if (IsEnabled)
            {
                var ctrls = clientForm.Controls;
                ScanControls(ctrls);
            }
        }

        private static void ScanControls(Control.ControlCollection ctrls)
        {
            foreach (var item in ctrls)
            {
                if (item is Button)
                {
                    ((ButtonBase)(item)).FlatStyle = FlatStyle.Flat;
                }

                if (item is Label)
                {
                    // ((ButtonBase)(item)). = FontFamily.GenericSansSerif;
                }

                if (item is CheckBox)
                {
                }

                if (item is RadioButton)
                {
                }

                if (item is ComboBox)
                {
                }

                if (item is RichTextBox)
                {
                }

                if (item is PictureBox)
                {
                }

                if (item is TabControl)
                {
                    var newdata = (TabControl)item;
                    var tempdata = newdata.SelectedTab.Controls;
                    ScanControls(tempdata);
                }

                if (item is Panel)
                {
                    var newdata = (Panel)item;
                    var tempdata = newdata.Controls;
                    ScanControls(tempdata);
                }
            }
        }

        private void onThemedForm_Close(object sender, FormClosedEventArgs e)
        {
            //An event handler for closing the themed form to avoid leaving it running.
            try
            {
                //Gets the sender and checks if it is a mac themed form...
                Form frm = (Form)sender;
                Core thm = (Core)frm.ParentForm;
                thm.Close();                               //closes the themed form.
            }
            catch
            {
                //Error message not displayed.
            }
        }
        #endregion

        #region PUBLIC METHODS
        //Functions for applying the  form theme.
        public ThemeManager()
        {
            //ThemeManager()
        }

        public void ApplyFormThemeSizable(Form clientForm, Boolean enableControlsTheme = false)
        {
            //This thread makes the specified form to be a control of the created Mac themed form (Resizable).
            Core frm = new Core();                //Creates a new Mac themed borderless form (generated by the mactheme class).
            frm.TopMost = clientForm.TopMost;     //Determines if the themed form should be in the TopMost level.
            frm.ShowInTaskbar = clientForm.ShowInTaskbar;  //Determines if the themed form should appear in the taskbar.
            frm.ShowIcon = clientForm.ShowIcon;   //Determines if the themed form should show its icon in the taskbar.

            //Checks if the user wants to disable some sizing buttons...
            if (clientForm.MaximizeBox == false)
            {
                frm.ControlBox = false;
                frm.cmdMaxRes.Visible = false;
                frm.MaximizeBox = false;
            }
            if (clientForm.MinimizeBox == false)
            {
                frm.cmdMaxRes.Left = frm.cmdMin.Left;
                frm.cmdMin.Visible = false;
                frm.MinimizeBox = false;
            }

            clientForm.TopLevel = false;                 //Sets the TopLevel property of the clientForm to false so that we can add it as a client control on our mac themed form.
            clientForm.FormBorderStyle = FormBorderStyle.None;   //Makes the clientForm borderless.
            frm.Width = clientForm.Width + 8;            //Adjusts the width of the Mac themed form.
            frm.Top = 0;                                 //Sets the default top location to 0.
            frm.Left = 0;                                //Sets the default left location to 0.

            frm.StartPosition = clientForm.StartPosition;  //Sets the themed form's StartPosition same as the clientForm's StartPositon. If {0,0} location is used and it is needed to be applied, just set the clientForm's StartPosition to manual.
            if (clientForm.Top != 0)
            {
                frm.StartPosition = FormStartPosition.Manual;   //Sets the Form's Startup position to manual.
                frm.Top = clientForm.Top;                //Sets the themed form's top the same as the clientForm's top position.
            }
            if (clientForm.Left != 0)
            {
                frm.StartPosition = FormStartPosition.Manual;   //Sets the Form's Startup position to manual.
                frm.Left = clientForm.Left;               //Sets the themed form's left the same as the clientForm's Left position.
            }

            frm.Height = clientForm.Height + 28;          //Adjusts the height of the Mac themed form.
            clientForm.Top = 0;                           //Sets the clientForm's top location to 0.
            clientForm.Left = 3;                          //Sets the clientForms' left location.
            frm.Text = clientForm.Text;                   //Sets the mac themed form's Text property to the specified title (param = formTitle).
            clientForm.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;    //Anchors the clientForm so it fits the themed form during resizing process.
            frm.titleCaption.Text = clientForm.Text;      //Sets the themed form's titleCaption Text property to the specified title (param = formTitle).
            frm.bodypanel.Controls.Add(clientForm);       //Adds the clientForm to the bodypanel's Controls collection.
            frm.Icon = clientForm.Icon;                   //Sets the themed form's Icon property the same as the clientForm's Icon property.

            Size zeroSize = new Size(0, 0);               //The default minimum size of a form (0,0).
            if (clientForm.MinimumSize != zeroSize)       //If the minimum width and height of the clientForm is not on default (0,0)....
            {
                frm.MinimumSize = new Size(clientForm.MinimumSize.Width + 8, clientForm.MinimumSize.Height + 28);  //Sets the minimum size of the themed form.
            }
            if (clientForm.Width < frm.MinimumSize.Width)
            {
                clientForm.Width = frm.MinimumSize.Width;
            }
            if (clientForm.Height < frm.MinimumSize.Height)
            {
                clientForm.Height = frm.MinimumSize.Height;
            }
            //Enable the theme on Controls 
            ApplyXTheme_Controls(clientForm, enableControlsTheme);
            clientForm.FormClosed += new FormClosedEventHandler(onThemedForm_Close);  //If the client form is closed, the themed form should also close as well.
            frm.Show();                                   //Show the generated themed form with the clientForm as it's child control.


        }

        public void ApplyFormThemeDialog(Form clientForm, Form parentForm, Boolean enableControlsTheme = false)
        {
            //This thread makes the specified form to be a control of the created Mac themed form (Fixed Single).
            Core frm = new Core();                        //Creates a new Mac themed borderless form (generated by this library).
            clientForm.FormBorderStyle = FormBorderStyle.None;   //Makes the clientForm borderless.
            frm.Width = clientForm.Width + 8;             //Adjusts the width of the Mac themed form.
            frm.Height = clientForm.Height + 28;          //Adjusts the height of the Mac themed form.
            frm.Owner = parentForm;                       //Sets the owner of the themed form to the specified parentForm.
            frm.StartPosition = clientForm.StartPosition; //Sets the themed form's StartPosition same as the clientForm's StartPositon. If {0,0} location is used and it is needed to be applied, just set the clientForm's StartPosition to manual.

            if (clientForm.Top != 0)
            {
                frm.StartPosition = FormStartPosition.Manual; //Sets the Form's Startup position to manual.
                frm.Top = clientForm.Top;                 //Sets the themed form's top the same as the clientForm's top position.
            }
            if (clientForm.Left != 0)
            {
                frm.StartPosition = FormStartPosition.Manual; //Sets the Form's Startup position to manual.
                frm.Left = clientForm.Left;               //Sets the themed form's left the same as the clientForm's Left position.
            }
            clientForm.TopLevel = false;                  //Sets the TopLevel property of the clientForm to false so that we can add it as a client control on our mac themed form.

            //Sets the edges' cursor to normal and disable their resizing function;
            frm.leftpnl.Cursor = Cursors.Arrow;
            frm.leftpnl2.Cursor = Cursors.Arrow;
            frm.rightpnl.Cursor = Cursors.Arrow;
            frm.rightpnl2.Cursor = Cursors.Arrow;
            frm.bottompnl2.Cursor = Cursors.Arrow;
            frm.bottompnl.Cursor = Cursors.Arrow;
            frm.toppnl.Cursor = Cursors.Arrow;
            frm.leftpnl.MouseMove -= frm.leftpnl_MouseMove;
            frm.leftpnl2.MouseMove -= frm.leftpnl_MouseMove;
            frm.rightpnl.MouseMove -= frm.rightpnl_MouseMove;
            frm.rightpnl2.MouseMove -= frm.rightpnl_MouseMove;
            frm.bottompnl.MouseMove -= frm.bottompnl_MouseMove;
            frm.bottompnl2.MouseMove -= frm.bottompnl_MouseMove;
            frm.toppnl.MouseMove -= frm.toppnl_MouseMove;
            ///////////////////////////////////////////////////////////

            //Do not show in taskbar and removes the minimize and maximize/restore buttons...
            frm.cmdMaxRes.Visible = false;
            frm.cmdMin.Visible = false;
            frm.ShowInTaskbar = false;
            ///////////////////////////////////////////////////////////////////

            clientForm.Top = 0;                            //Sets the clientForm's top location to 0.
            clientForm.Left = 3;                           //Sets the clientForms' left location.
            frm.Text = clientForm.Text;                    //Sets the mac themed form's Text property to the specified title (param = formTitle).
            clientForm.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;    //Anchors the clientForm so it fits the themed form during resizing process.
            frm.titleCaption.Text = clientForm.Text;       //Sets the themed form's titleCaption Text property to the specified title (param = formTitle).
            frm.bodypanel.Controls.Add(clientForm);        //Adds the clientForm to the bodypanel's Controls collection.
            frm.Icon = clientForm.Icon;                    //Sets the themed form's Icon property the same as the clientForm's Icon property.

            Size zeroSize = new Size(0, 0);                //The default minimum size of a form (0,0).
            if (clientForm.MinimumSize != zeroSize)        //If the minimum width and height of the clientForm is not on default (0,0)....
            {
                frm.MinimumSize = new Size(clientForm.MinimumSize.Width + 8, clientForm.MinimumSize.Height + 28); //Sets the minimum size of the themed form.
            }
            if (clientForm.Width < frm.MinimumSize.Width)
            {
                clientForm.Width = frm.MinimumSize.Width;
            }
            if (clientForm.Height < frm.MinimumSize.Height)
            {
                clientForm.Height = frm.MinimumSize.Height;
            }
            //Enable the theme on Controls 
            ApplyXTheme_Controls(clientForm, enableControlsTheme);
            clientForm.FormClosed += new FormClosedEventHandler(onThemedForm_Close);  //If the clientForm closes, the themed dialog closes as well.
            frm.Show();                                    //Show the generated themed form with the clientForm as it's child control.
        }

        public void ApplyFormThemeSingleSizable(Form clientForm, Boolean enableControlsTheme = false)
        {
            //This thread makes the specified form to be a control of the created Mac themed form (Resizable, Thin Single Pixel borders, Corner resizing not available).
            Core frm = new Core();                        //Creates a new Mac themed borderless form (generated by the mactheme class).
            frm.TopMost = clientForm.TopMost;             //Determines if the themed form should be in the TopMost level.
            frm.ShowInTaskbar = clientForm.ShowInTaskbar; //Determines if the themed form should appear in the taskbar.
            frm.ShowIcon = clientForm.ShowIcon;           //Determines if the themed form should show its icon in the taskbar.

            //Checks if the user wants to disable some sizing buttons...
            if (clientForm.MaximizeBox == false)
            {
                frm.ControlBox = false;
                frm.cmdMaxRes.Visible = false;
                frm.MaximizeBox = false;
            }
            if (clientForm.MinimizeBox == false)
            {
                frm.cmdMaxRes.Left = frm.cmdMin.Left;
                frm.cmdMin.Visible = false;
                frm.MinimizeBox = false;
            }
            //////////////////////////////////////////////////////////

            clientForm.TopLevel = false;                   //Sets the TopLevel property of the clientForm to false so that we can add it as a client control on our mac themed form.
            clientForm.FormBorderStyle = FormBorderStyle.None;   //Makes the clientForm borderless.
            frm.Width = clientForm.Width + 8;              //Adjusts the width of the Mac themed form.
            frm.Height = clientForm.Height + 28;           //Adjusts the height of the Mac themed form.
            frm.leftpnl2.Width = 0;                        //Makes the thick resizable edge disappear.
            frm.rightpnl2.Width = 0;                       //Makes the thick resizable edge disappear.
            frm.bottompnl2.Height = 0;                     //Makes the thick resizalbe edge disappear.
            frm.bodypanel.Left = 1;                        //Sets the themed form's form container (bodypanel) near the gray edge.
            frm.Top = 0;                                   //Sets the default top location to 0.
            frm.Left = 0;                                  //Sets the default left location to 0.

            frm.StartPosition = clientForm.StartPosition;  //Sets the themed form's StartPosition same as the clientForm's StartPositon. If {0,0} location is used and it is needed to be applied, just set the clientForm's StartPosition to manual.
            if (clientForm.Top != 0)
            {
                frm.StartPosition = FormStartPosition.Manual;   //Sets the Form's Startup position to manual.
                frm.Top = clientForm.Top;                  //Sets the themed form's top the same as the clientForm's top position.
            }
            if (clientForm.Left != 0)
            {
                frm.StartPosition = FormStartPosition.Manual;   //Sets the Form's Startup position to manual.
                frm.Left = clientForm.Left;                //Sets the themed form's left the same as the clientForm's Left position.
            }


            clientForm.Top = 0;                           //Sets the clientForm's top location to 0.
            clientForm.Left = 0;                          //Sets the clientForms' left location.
            clientForm.Width += 6;                        //Adds width to the clientForm to reach the right edge.
            clientForm.Height += 5;                       //Adds height to the clientform to reach the bottom edge.
            frm.bodypanel.Height += 2;                    //Adds height to the clientForm container to reach the bottom edge.
            frm.Text = clientForm.Text;                   //Sets the mac themed form's Text property to the specified title (param = formTitle).
            clientForm.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;    //Anchors the clientForm so it fits the themed form during resizing process.
            frm.titleCaption.Text = clientForm.Text;      //Sets the themed form's titleCaption Text property to the specified title (param = formTitle).
            frm.bodypanel.Controls.Add(clientForm);       //Adds the clientForm to the bodypanel's Controls collection.
            frm.Icon = clientForm.Icon;                   //Sets the themed form's Icon property the same as the clientForm's Icon property.

            Size zeroSize = new Size(0, 0);               //The default minimum size of a form (0,0).
            if (clientForm.MinimumSize != zeroSize)       //If the minimum width and height of the clientForm is not on default (0,0)....
            {
                frm.MinimumSize = new Size(clientForm.MinimumSize.Width + 8, clientForm.MinimumSize.Height + 28);  //Sets the minimum size of the themed form.
            }
            if (clientForm.Width < frm.MinimumSize.Width)
            {
                clientForm.Width = frm.MinimumSize.Width;
            }
            if (clientForm.Height < frm.MinimumSize.Height)
            {
                clientForm.Height = frm.MinimumSize.Height;
            }
            //Enable the theme on Controls 
            ApplyXTheme_Controls(clientForm, enableControlsTheme);
            clientForm.FormClosed += new FormClosedEventHandler(onThemedForm_Close);  //If the client form is closed, the themed form should also close as well.
            frm.Show();                                   //Show the generated themed form with the clientForm as it's child control.

        } 
        #endregion
    }
}
