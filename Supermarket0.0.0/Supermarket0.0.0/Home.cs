using Supermarket0._0._0.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Supermarket0._0._0
{
   
    public partial class Home : Form
    {
        private Form activeForm;
        public Home()
        {
            InitializeComponent();
            setValues();
        }

        private void setValues()
        {
            openChildForm(new NewOrder(this));
            activeForm = null;
        }

        private void ButtonHome_Click(object sender, EventArgs e)
        {

            setActiveStyle(ButtonHome);
            openChildForm(new NewOrder(this));
        }

        private void setActiveStyle(Button activeButton)
        {
            ButtonHome.ForeColor = Color.FromArgb(224, 224, 224);
            ButtonHome.BackColor = Color.FromArgb(64, 64, 64);

            ButtonOrders.ForeColor = Color.FromArgb(224, 224, 224);
            ButtonOrders.BackColor = Color.FromArgb(64, 64, 64);

            ButtonSuppliers.ForeColor = Color.FromArgb(224, 224, 224);
            ButtonSuppliers.BackColor = Color.FromArgb(64, 64, 64);

            activeButton.ForeColor = Color.White;
            activeButton.BackColor = Color.FromArgb(227, 132, 0);

        }

        private void ButtonOrders_Click(object sender, EventArgs e)
        {
            setActiveStyle(ButtonOrders);
            openChildForm(new Orders(this));
        }

        private void ButtonSuppliers_Click(object sender, EventArgs e)
        {
            setActiveStyle(ButtonSuppliers);
            openChildForm(new SuppliersForm(this));
        }

        public void openChildForm(Form childForm)
        {
            if (activeForm != null)
                activeForm.Close();
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            PanelPagesContainer.Controls.Add(childForm);
            PanelPagesContainer.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }
    }
}
