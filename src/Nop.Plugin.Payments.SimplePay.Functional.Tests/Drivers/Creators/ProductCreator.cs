using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Office2010.Excel;
using Nop.Core.Domain.Catalog;

namespace Nop.Plugin.Payments.SimplePay.Functional.Tests.Drivers.Creators;
public static class ProductCreator
{
    public static Product Create(
        int? productId = null,
        string name = "Test Product"
        )
    {
        productId ??= 1;
        return new Product
        {
            Id = productId.Value,
            Name = name,
        };
    } 
}
