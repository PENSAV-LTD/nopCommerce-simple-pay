using Microsoft.AspNetCore.Mvc;
using Nop.Web.Controllers;

namespace Nop.Plugin.Payments.SimplePay.Controllers;
public class SimplePayCallbackController : BasePublicController
{
    public IAsyncResult Success(string r, string s)
    {
        throw new NotImplementedException();
    }

    public IAsyncResult Fail(string r, string s)
    {
        throw new NotImplementedException();
    }

    public IAsyncResult Cancel(string r, string s)
    {
        throw new NotImplementedException();
    }
    
    public IAsyncResult Timeout(string r, string s)
    {
        throw new NotImplementedException();
    }

    public IAsyncResult Ipn()
    {
        throw new NotImplementedException();
    }
}
