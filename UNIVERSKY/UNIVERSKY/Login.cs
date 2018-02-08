using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UNIVERSKY
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void LoginClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LoginMin_Click(object sender, EventArgs e)
        {
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
        }

        private void LoginClose_MouseHover(object sender, EventArgs e)
        {
            LoginClose.Image = Properties.Resources.关闭2;
        }

        private void LoginClose_MouseLeave(object sender, EventArgs e)
        {
            LoginClose.Image = Properties.Resources.关闭;
        }

        private void LoginMin_MouseHover(object sender, EventArgs e)
        {
            LoginMin.Image = Properties.Resources.最小化2;
        }

        private void LoginMin_MouseLeave(object sender, EventArgs e)
        {
            LoginMin.Image = Properties.Resources.最小化;
        }
        Point mouse_offset;
        private void Login_MouseDown(object sender, MouseEventArgs e)
        {
            mouse_offset = new Point(-e.X, -e.Y);
        }

        private void Login_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point mousePos = Control.MousePosition;
                mousePos.Offset(mouse_offset.X, mouse_offset.Y);
                Location = mousePos;
            }
        }

        private void Login_Button_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
