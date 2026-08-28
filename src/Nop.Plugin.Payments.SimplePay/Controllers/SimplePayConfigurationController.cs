using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Plugin.Payments.SimplePay.Settings;
using Nop.Plugin.Payments.SimplePay.ViewModels;
using Nop.Services.Configuration;
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
            OtpIpnAddress = settings.OtpIpnAddress?.ToString()
        };
    }

    [CheckPermission(Nop.Services.Security.StandardPermission.Configuration.MANAGE_PAYMENT_METHODS)]
    [HttpPost]
    public void Configure(PaymentConfiguration configuration) 
    {
    }

    private readonly IStoreContext _storeContext;
    private readonly ISettingService _settingService;

    public SimplePayConfigurationController(
        IStoreContext storeContext,
        ISettingService settingService
        )
    {
        _storeContext = storeContext;
        _settingService = settingService;
    }
}
