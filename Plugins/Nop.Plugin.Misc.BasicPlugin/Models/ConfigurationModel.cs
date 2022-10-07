using System.ComponentModel.DataAnnotations;
using Nop.Web.Framework.Mvc.ModelBinding;
using Nop.Web.Framework.Models;

namespace Nop.Plugin.Widgets.BasicPlugin.Models
{
    public record ConfigurationModel : BaseNopModel
    {
        public int ActiveStoreScopeConfiguration { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.BasicPlugin.Title")]
        public string Title { get; set; }
        public bool Title_OverrideForStore { get; set; }
        [NopResourceDisplayName("Plugins.Widgets.BasicPlugin.Description")]
        public string Description { get; set; }
        public bool Description_OverrideForStore { get; set; }
    }



}

