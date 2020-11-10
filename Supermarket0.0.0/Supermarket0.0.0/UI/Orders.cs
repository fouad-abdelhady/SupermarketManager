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
    public partial class Orders : Form
    {
        private Home home;
        public Orders(Home home)
        {
            InitializeComponent();
            setValues(home);
        }

        private void setValues(Home home)
        {
            this.home = home;
            ComboBoxOrderCondition.SelectedIndex = 0;
            NumberOfOrders.Value = 25;
        }

        private void ButtonApplyFilter_Click(object sender, EventArgs e)
        {

        }

        private void ButtonDeleteOrders_Click(object sender, EventArgs e)
        {

        }
    }
}
