using System.Linq;
using Microsoft.Data.SqlClient;
using Xunit;

namespace TestWithLocaldb.DataAccess.Test;

public class UnitTest
{
    [Fact]
    public void データベースを利用したテスト()
    {
        InitializeDatabase();

        using var dbContext = new ProductDbContext();
        var products = dbContext.Products
            .Where(p => p.Publisher == "秋田")
            .OrderBy(p => p.Id)
            .ToList();
        Assert.Collection(products,
            product =>
            {
                Assert.Equal(2, product.Id);
                Assert.Equal("きりたんぽ", product.Name);
            },
            product =>
            {
                Assert.Equal(3, product.Id);
                Assert.Equal("なまはげ", product.Name);
            });
    }

    private static void InitializeDatabase()
    {
        var connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=sample-database;Integrated Security=True;";
        using var connection = new SqlConnection(connectionString);
        using var deleteCommand = connection.CreateCommand();
        deleteCommand.CommandText = "DELETE FROM [dbo].[Products];";
        using var insertCommand = connection.CreateCommand();
        insertCommand.CommandText = "INSERT [dbo].[Products] ([Id], [Name], [Publisher]) VALUES (1, N'りんご', N'青森');" +
            "INSERT [dbo].[Products] ([Id], [Name], [Publisher]) VALUES (2, N'きりたんぽ', N'秋田');" +
            "INSERT [dbo].[Products] ([Id], [Name], [Publisher]) VALUES (3, N'なまはげ', N'秋田');";
        connection.Open();
        deleteCommand.ExecuteNonQuery();
        insertCommand.ExecuteNonQuery();
    }
}
