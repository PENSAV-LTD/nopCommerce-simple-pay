using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Payments.SimplePay.Exceptions;

public class SimplePayInvalidSignatureException(string message = "Invalid Signature!") : SimplePayException(message)
{
}
