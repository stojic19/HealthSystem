using Microsoft.EntityFrameworkCore.Migrations;

namespace Pharmacy.Migrations
{
    public partial class CountryAndCitySeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO public.\"Countries\"(\"Id\", \"Name\") VALUES (1, N'Srbija')");

            migrationBuilder.Sql("INSERT INTO public.\"Cities\"(\"Id\", \"Name\", \"PostalCode\", \"CountryId\") VALUES (1, N'Novi Sad', 21000, 1)");
            migrationBuilder.Sql("INSERT INTO public.\"Cities\"(\"Id\", \"Name\", \"PostalCode\", \"CountryId\") VALUES (2, N'Beograd', 11000, 1)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM public.\"Cities\"");
            migrationBuilder.Sql("DELETE FROM public.\"Countries\"");
        }
    }
}
