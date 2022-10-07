using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Web.Framework.Models;

namespace Nop.Plugin.Widgets.BasicPlugin.Models
{
    public record PublicContentInfo : BaseNopModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
