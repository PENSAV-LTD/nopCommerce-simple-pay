using FluentMigrator;
using Nop.Data.Extensions;
using Nop.Data.Migrations;
using Nop.Plugin.Payments.SimplePay.Domain;

namespace Nop.Plugin.Payments.SimplePay.Data;

[NopMigration("2026-02-01 00:00:00", "Payments.SimplePay base schema", MigrationProcessType.Installation)]
public class SchemaMigration : Migration
{
    public override void Down()
    {
        this.DeleteTableIfExists<ResponseEvents>();
        this.DeleteTableIfExists<Responses>();
    }

    public override void Up()
    {
        this.CreateTableIfNotExists<ResponseEvents>();
        this.CreateTableIfNotExists<Responses>();
    }
}
