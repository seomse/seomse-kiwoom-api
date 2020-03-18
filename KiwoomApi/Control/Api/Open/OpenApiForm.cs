using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KiwoomApi.Control.Api;

namespace KiwoomApi.Control
{
    public partial class OpenApiForm : Form
    {

        private OpenApiController api = null;

        public OpenApiForm()
        {
            InitializeComponent();
        }

        #region Form Callbacks

        private void OpenApiForm_Load(object sender, EventArgs e)
        {
            Form form = (Form)sender;
            form.ShowInTaskbar = false;
            form.Opacity = 0;

            api = OpenApiController.Instance;
            api.AxKHOpenAPI = this.axKHOpenAPI;
            api.init();
        }

        #endregion
    }
}
