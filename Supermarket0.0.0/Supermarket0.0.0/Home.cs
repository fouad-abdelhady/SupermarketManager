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
        private Button activeButton;
        public Home()
        {
            InitializeComponent();
            setValues();
        }

        private void setValues()
        {
            openChildForm(new NewOrder(this));
            activeForm = null;
            activeButton = ButtonHome;
            ButtonHome_Click(null, null);
        }

        private void ButtonHome_Click(object sender, EventArgs e)
        {            
            setActiveStyle(ButtonHome);
            activeButton = ButtonHome;
            openChildForm(new NewOrder(this));
        }       

        private void ButtonOrders_Click(object sender, EventArgs e)
        {
            setActiveStyle(ButtonOrders);
            activeButton = ButtonOrders;
            openChildForm(new Orders(this));
        }

        private void ButtonSuppliers_Click(object sender, EventArgs e)
        {
            setActiveStyle(ButtonSuppliers);
            activeButton = ButtonSuppliers;
            openChildForm(new SuppliersForm(this));
        }     

        private void ButtonShipment_Click(object sender, EventArgs e)
        {
            setActiveStyle(ButtonShipment);
            activeButton = ButtonShipment;
        }

        private void ButtonCustomer_Click(object sender, EventArgs e)
        {
            setActiveStyle(ButtonCustomer);
            activeButton = ButtonCustomer;
        }

        private void ButtonGoods_Click(object sender, EventArgs e)
        {
            setActiveStyle(ButtonGoods);
            activeButton = ButtonGoods;
        }

        private void ButtonInventory_Click(object sender, EventArgs e)
        {
            setActiveStyle(ButtonInventory);
            activeButton = ButtonInventory;
        }

        private void setActiveStyle(Button pressedButton)
        {
            activeButton.ForeColor = Color.FromArgb(224, 224, 224);
            activeButton.BackColor = Color.FromArgb(68, 40, 73);
           
            pressedButton.ForeColor = Color.White;
            pressedButton.BackColor = Color.FromArgb(255, 130, 41);

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
