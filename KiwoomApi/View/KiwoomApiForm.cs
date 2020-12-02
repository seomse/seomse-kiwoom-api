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
using KiwoomApi.Control.Api.KiwoomApi;

namespace KiwoomApi.View
{
    public partial class KiwoomApiForm : Form
    {

        private KiwoomApiController api = null;

        public KiwoomApiForm()
        {
            InitializeComponent();
        }

        #region Form Callbacks

        private void KiwoomApiForm_Load(object sender, EventArgs e)
        {
            Form form = (Form)sender;
            form.ShowInTaskbar = false;
            form.Opacity = 0;

            api = KiwoomApiController.Instance;
            api.AxKHOpenAPI = this.axKHOpenAPI;
        }

        #endregion

        private void axKHOpenAPI_OnEventConnect(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnEventConnectEvent e)
        {

        }
    }
}
