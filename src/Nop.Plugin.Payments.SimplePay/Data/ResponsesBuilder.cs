using FluentMigrator.Builders.Create.Table;
using Nop.Data.Extensions;
using Nop.Data.Mapping.Builders;
using Nop.Plugin.Payments.SimplePay.Domain;

namespace Nop.Plugin.Payments.SimplePay.Data;
public class ResponsesBuilder : NopEntityBuilder<Domain.Responses>
{
    public override void MapEntity(CreateTableExpressionBuilder table)
    {
        table
            .WithColumn(nameof(Responses.Id)).AsInt32().PrimaryKey().Identity()
            .WithColumn(nameof(Responses.Code)).AsInt32().NotNullable()
            .WithColumn(nameof(Responses.TransactionId)).AsString(100).NotNullable()
            .WithColumn(nameof(Responses.EventId)).AsInt32().NotNullable().ForeignKey<ResponseEvents>()
            .WithColumn(nameof(Responses.MerchantId)).AsString(100).NotNullable()
            .WithColumn(nameof(Responses.OrderId)).AsString(100).NotNullable();
    }
}
