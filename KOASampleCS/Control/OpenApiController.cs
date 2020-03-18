using KiwoomApi.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KOASampleCS.Control
{
    class OpenApiController
    {
        private AxKHOpenAPILib.AxKHOpenAPI api=null;

        private class Holder { internal static readonly OpenApiController INSTANCE = new OpenApiController(); }
        public static OpenApiController Instance { get { return Holder.INSTANCE; } }
        private OpenApiController()
        {
            //Logger.info("OpenApiController init");
        }
        public AxKHOpenAPILib.AxKHOpenAPI Api { 
            get { 
                if(api == null)
                {
                    //  Logger.info("API NOT READY");
                }
                return api;
            }
            set { }
        }

        public void print()
        {
            if(api == null)
            {
                //Logger.info("API IS NULL");
            } else
            {
                //Logger.info("API READY");
            }
        }
    }
}
