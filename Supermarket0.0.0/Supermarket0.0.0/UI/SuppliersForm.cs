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
    public partial class SuppliersForm : Form
    {
        private Home home;
        public SuppliersForm(Home home)
        {
            InitializeComponent();
            setValues(home);
        }

        private void setValues(Home home)
        {
            this.home = home;

            ButtonUpdateSupplier.Enabled = false;
        }

        private void TabelSuppliersList_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            e.Column.SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        private void ButtonUpdateSupplier_Click(object sender, EventArgs e)
        {
            ButtonUpdateSupplier.Enabled = false;
        }
    }
}
