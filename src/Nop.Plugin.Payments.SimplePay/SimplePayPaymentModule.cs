namespace Nop.Plugin.Payments.SimplePay
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Nop.Core;
    using Nop.Core.Domain.Orders;
    using Nop.Plugin.Payments.SimplePay.Settings;
    using Nop.Services.Configuration;
    using Nop.Services.Localization;
    using Nop.Services.Payments;
    using Nop.Services.Plugins;

    public class SimplePayPaymentModule : BasePlugin
    {
        private readonly ISettingService _settingService;
        protected readonly ILocalizationService _localizationService;
        private readonly IWebHelper _webHelper;

        public override string GetConfigurationPageUrl()
        {
            return $"{_webHelper.GetStoreLocation()}Admin/SimplePayConfiguration/Configure";
        }

        public override async Task InstallAsync()
        {
            await _settingService.SaveSettingAsync(new SimplePaySettings());

            await _localizationService.AddOrUpdateLocaleResourceAsync(new Dictionary<string, string>
            {
            });
            await base.InstallAsync();
        }

        public SimplePayPaymentModule(
            ISettingService settingService,
            ILocalizationService localizationService,
            IWebHelper webHelper
            )
        {
            _settingService = settingService;
            _localizationService = localizationService;
            _webHelper = webHelper;
        }
    }
}
