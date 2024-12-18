using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nop.Core;

namespace Nop.Plugin.Payments.SimplePay.Domain;
public class Responses : BaseEntity
{
    public int Code { get; set; }
    public string TransactionId { get; set; }
    public virtual int EventId { get; set; }
    public ResponseEvents Event { get; set; }
    public string MerchantId { get; set; }
    public string OrderId { get; set; }
}
