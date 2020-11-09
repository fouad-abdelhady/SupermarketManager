using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Supermarket0._0._0.UI
{
    public partial class NewOrder : Form
    {
        private Home home;
        public NewOrder(Home home)
        {
            InitializeComponent();
            setValues(home);
        }

        private void setValues(Home home)
        {
            this.home = home;
            PanelPayLater.Visible = false;
        }

        private void ButtonPayLater_Click(object sender, EventArgs e)
        {
            PanelPayLater.Visible = true;
        }

        private void ButtonAddToPayLater_Click(object sender, EventArgs e)
        {
            PanelPayLater.Visible = false;
        }

        private void ButtonCancelPayLater_Click(object sender, EventArgs e)
        {
            PanelPayLater.Visible = false;
        }
    }
}
