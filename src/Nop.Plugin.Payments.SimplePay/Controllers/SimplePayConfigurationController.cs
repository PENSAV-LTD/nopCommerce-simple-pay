using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.EMMA;
using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Core.Http.Extensions;
using Nop.Plugin.Payments.SimplePay.Settings;
using Nop.Plugin.Payments.SimplePay.ViewModels;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Messages;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc.Filters;

namespace Nop.Plugin.Payments.SimplePay.Controllers;

[AutoValidateAntiforgeryToken]
[AuthorizeAdmin]
[Area(AreaNames.ADMIN)]
public class SimplePayConfigurationController : BasePaymentController
{
    [CheckPermission(Nop.Services.Security.StandardPermission.Configuration.MANAGE_PAYMENT_METHODS)]
    public async Task<IActionResult> Configure()
    {
        var viewModel = await GetConfigurationAsync();
        return View("~/Plugins/Payments.SimplePay/Views/Admin/Configuration/Configure.cshtml", viewModel);
    }

    private async Task<SimplePayConfigurationModel> GetConfigurationAsync()
    {
        var store = await _storeContext.GetCurrentStoreAsync();
        var settings = await _settingService.LoadSettingAsync<SimplePaySettings>(store.Id);
        return new SimplePayConfigurationModel
        {
            MerchantKey = settings.MerchantKey,
            DefaultCurrency = settings.DefaultCurrency,
            IsDefaultCurrencyUsed = settings.IsDefaultCurrencyUsed,
            IsTwoStep = settings.IsTwoStep,
            AdditionalFee = settings.AdditionalFee,
            RetentionPolicyInDay = settings.RetentionPolicyInDay,
            UseSandbox = settings.UseSandbox,
            AddExtraPercentageToOrderTotal = settings.AddExtraPercentageToOrderTotal,
            AddExtraToOrderTotal = settings.AddExtraToOrderTotal,
            HasDetailedItems = settings.HasDetailedItems,
            OneItemName = settings.OneItemName,
            OtpIpnAddress = settings.OtpIpnAddress?.ToString(),
        };
    }

    [CheckPermission(Nop.Services.Security.StandardPermission.Configuration.MANAGE_PAYMENT_METHODS)]
    [HttpPost, ActionName("Configure")]
    public async Task<IActionResult> Configure(SimplePayConfigurationModel configuration) 
    {
        if (!ModelState.IsValid)
            return await Configure();

        var storeId = (await _storeContext.GetCurrentStoreAsync()).Id;
        var settings = await _settingService.LoadSettingAsync<SimplePaySettings>(storeId);

        settings.MerchantKey = configuration.MerchantKey;
        settings.DefaultCurrency = configuration.DefaultCurrency;
        settings.IsDefaultCurrencyUsed = configuration.IsDefaultCurrencyUsed;
        settings.IsTwoStep = configuration.IsTwoStep;
        settings.AdditionalFee = configuration.AdditionalFee;
        settings.RetentionPolicyInDay = configuration.RetentionPolicyInDay;
        settings.UseSandbox = configuration.UseSandbox;
        settings.AddExtraPercentageToOrderTotal = configuration.AddExtraPercentageToOrderTotal;
        settings.AddExtraToOrderTotal = configuration.AddExtraToOrderTotal;
        settings.HasDetailedItems = configuration.HasDetailedItems;
        settings.OneItemName = configuration.OneItemName;
        settings.OtpIpnAddress = configuration.OtpIpnAddress != null ? new Uri(configuration.OtpIpnAddress) : null;

        await _settingService.SaveSettingAsync(settings, setting => setting.MerchantKey, storeId);
        await _settingService.SaveSettingAsync(settings, setting => setting.DefaultCurrency, storeId);
        await _settingService.SaveSettingAsync(settings, setting => setting.IsDefaultCurrencyUsed, storeId);
        await _settingService.SaveSettingAsync(settings, setting => setting.IsTwoStep, storeId);
        await _settingService.SaveSettingAsync(settings, setting => setting.AdditionalFee, storeId);
        await _settingService.SaveSettingAsync(settings, setting => setting.RetentionPolicyInDay, storeId);
        await _settingService.SaveSettingAsync(settings, setting => setting.UseSandbox, storeId);
        await _settingService.SaveSettingAsync(settings, setting => setting.AddExtraPercentageToOrderTotal, storeId);
        await _settingService.SaveSettingAsync(settings, setting => setting.AddExtraToOrderTotal, storeId);
        await _settingService.SaveSettingAsync(settings, setting => setting.HasDetailedItems, storeId);
        await _settingService.SaveSettingAsync(settings, setting => setting.OneItemName, storeId);
        await _settingService.SaveSettingAsync(settings, setting => setting.OtpIpnAddress, storeId);
        await _settingService.ClearCacheAsync();

        _notificationService.SuccessNotification(await _localizationService.GetResourceAsync("Admin.Plugins.Saved"));

        return RedirectToAction("Configure");
    }

    private readonly IStoreContext _storeContext;
    private readonly ISettingService _settingService;
    private readonly INotificationService _notificationService;
    private readonly ILocalizationService _localizationService;

    public SimplePayConfigurationController(
        IStoreContext storeContext,
        ISettingService settingService,
        INotificationService notificationService,
        ILocalizationService localizationService
        )
    {
        _storeContext = storeContext;
        _settingService = settingService;
        _notificationService = notificationService;
        _localizationService = localizationService;
    }
}
