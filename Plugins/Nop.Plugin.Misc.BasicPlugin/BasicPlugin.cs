using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core;
using Nop.Services.Cms;
using Nop.Services.Plugins;
using Nop.Web.Framework.Infrastructure;

namespace Nop.Plugin.Widgets.BasicPlugin
{
    public class BasicPlugin:BasePlugin,IWidgetPlugin
    {
        private readonly IWebHelper _webHelper;

        public bool HideInWidgetList => false;

        public BasicPlugin(IWebHelper webHelper)
        {
            _webHelper= webHelper;
        }
        public Task<IList<string>> GetWidgetZonesAsync()
        {
            return Task.FromResult<IList<string>>(new List<string> { PublicWidgetZones.HomepageBeforePoll });
        }
        public override string GetConfigurationPageUrl()
        {
            return _webHelper.GetStoreLocation() + "Admin/BasicPlugin/Configure";
        }
        public string GetWidgetViewComponentName(string widgetZone)
        {
            return "BasicPlugin";
        }
        public override Task InstallAsync()
        {
            return base.InstallAsync();
        }

        public override Task UninstallAsync()
        {
            return base.UninstallAsync();
        }
    }
}
