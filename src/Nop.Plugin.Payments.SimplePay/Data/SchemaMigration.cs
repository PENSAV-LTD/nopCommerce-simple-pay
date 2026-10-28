using FluentMigrator;
using Nop.Data.Extensions;
using Nop.Data.Migrations;
using Nop.Plugin.Payments.SimplePay.Domain;

namespace Nop.Plugin.Payments.SimplePay.Data;

[NopMigration("2026-02-01 00:00:00", "Payments.SimplePay base schema", MigrationProcessType.Installation)]
public class SchemaMigration : AutoReversingMigration
{
    public override void Up()
    {
        Create.TableFor<ResponseEvents>();
        Create.TableFor<Responses>();
    }
}
