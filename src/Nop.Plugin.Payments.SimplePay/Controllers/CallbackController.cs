using Microsoft.AspNetCore.Mvc;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc.Filters;

namespace Nop.Plugin.Payments.SimplePay.Controllers;
[AutoValidateAntiforgeryToken]
//[AuthorizeAdmin] //confirms access to the admin panel
//[Area(AreaNames.ADMIN)] //specifies the area containing a controller or action
public class CallbackController : BasePluginController
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
}
