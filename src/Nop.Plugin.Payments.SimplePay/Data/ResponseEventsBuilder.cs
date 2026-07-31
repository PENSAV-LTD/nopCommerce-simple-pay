using FluentMigrator.Builders.Create.Table;
using Nop.Data.Mapping.Builders;
using Nop.Plugin.Payments.SimplePay.Domain;

namespace Nop.Plugin.Payments.SimplePay.Data;
public class ResponseEventsBuilder : NopEntityBuilder<Domain.ResponseEvents>
{
    public override void MapEntity(CreateTableExpressionBuilder table)
    {
        table
            .WithColumn(nameof(ResponseEvents.Id)).AsInt32().PrimaryKey().Identity()
            .WithColumn(nameof(ResponseEvents.Name)).AsString(200).NotNullable();
    }
}
