using ClearReadHub.Documents.Infrastructure.Common.Persistance;

using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace ClearRead.Documents.Service.IntegrationTests.Common.WebApplicationFactory;

public class SQLTestDatabase
{
    public SqlConnection Connection { get; }

    public static SQLTestDatabase CreateAndInitialize()
    {
        var testDatabase = new SQLTestDatabase("Server=(localdb)\\mssqllocaldb;Database=TestDB;Trusted_Connection=True;");

        testDatabase.InitializeDatabase();

        return testDatabase;
    }

    public void InitializeDatabase()
    {
        Connection.Open();
        var options = new DbContextOptionsBuilder<DocumentsDbContext>()
            .UseSqlServer(Connection.ConnectionString)
            .Options;

        using var context = new DocumentsDbContext(options, null!, null!);
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
    }

    public void ResetDatabase()
    {
        Connection.Close();

        InitializeDatabase();
    }

    public void Dispose()
    {
        Connection.Close();
    }

    private SQLTestDatabase(string connectionString)
    {
        Connection = new SqlConnection(connectionString);
    }
}