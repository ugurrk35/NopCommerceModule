using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Plugin.Widgets.BasicPlugin.Models;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Media;
using Nop.Services.Messages;
using Nop.Services.Security;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc.Filters;

namespace Nop.Plugin.Widgets.BasicPlugin.Controllers
{
    [AuthorizeAdmin]
    [Area(AreaNames.Admin)]
    [AutoValidateAntiforgeryToken]
    public class BasicPluginController : BasePluginController
    {
        private readonly ILocalizationService _localizationService;
        private readonly INotificationService _notificationService;
        private readonly IPermissionService _permissionService;
        private readonly IPictureService _pictureService;
        private readonly ISettingService _settingService;
        private readonly IStoreContext _storeContext;

        public BasicPluginController(ILocalizationService localizationService,
            INotificationService notificationService,
            IPermissionService permissionService, 
            IPictureService pictureService,
            ISettingService settingService,
            IStoreContext storeContext)
        {
            _localizationService = localizationService;
            _notificationService = notificationService;
            _permissionService = permissionService;
            _pictureService = pictureService;
            _settingService = settingService;
            _storeContext = storeContext;
        }

        public async Task<IActionResult> Configure()
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageWidgets))
                return AccessDeniedView();

            //load settings for a chosen store scope
            var storeScope = await _storeContext.GetActiveStoreScopeConfigurationAsync();
            var basicPluginSettings = await _settingService.LoadSettingAsync<BasicPluginSettings>(storeScope);
            var model = new ConfigurationModel
            {
                Title = basicPluginSettings.Title,
                Description = basicPluginSettings.Description,      
                ActiveStoreScopeConfiguration = storeScope
            };
            if (storeScope > 0)
            {
                model.Title_OverrideForStore = await _settingService.SettingExistsAsync(basicPluginSettings, x => x.Title, storeScope);
                model.Description_OverrideForStore = await _settingService.SettingExistsAsync(basicPluginSettings, x => x.Description, storeScope);
                 }


            return View("~/Plugins/Widgets.BasicPlugin/Views/Configure.cshtml", model);
        }

        [HttpPost]
        public async Task<IActionResult> Configure(ConfigurationModel model)
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageWidgets))
                return AccessDeniedView();

            //load settings for a chosen store scope
            var storeScope = await _storeContext.GetActiveStoreScopeConfigurationAsync();
            var basicPluginSettings = await _settingService.LoadSettingAsync<BasicPluginSettings>(storeScope);




            basicPluginSettings.Title = model.Title;
            basicPluginSettings.Description = model.Description;
            

            /* We do not clear cache after each setting update.
             * This behavior can increase performance because cached settings will not be cleared 
             * and loaded from database after each update */
            await _settingService.SaveSettingOverridablePerStoreAsync(basicPluginSettings, x => x.Title, model.Title_OverrideForStore, storeScope, false);
            await _settingService.SaveSettingOverridablePerStoreAsync(basicPluginSettings, x => x.Description, model.Description_OverrideForStore, storeScope, false);


            //now clear settings cache
            await _settingService.ClearCacheAsync();
            
            //get current picture identifiers
          

            //delete an old picture (if deleted or updated)
          

            _notificationService.SuccessNotification(await _localizationService.GetResourceAsync("Admin.Plugins.Saved"));
            
            return await Configure();
        }
    }
}