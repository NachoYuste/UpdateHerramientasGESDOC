using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetDocsExpediente
{
    class SharepointController
    {
        private string User = ConfigurationManager.AppSettings["user"];
        private string Pass = ConfigurationManager.AppSettings["password"];
        private string Url = ConfigurationManager.AppSettings["SiteURL"];

        private ClientContext context { get; }
    }
}
