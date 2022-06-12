using api.pdorado.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace api.pdorado.Data
{
    public class DataContextFactory : IDesignTimeDbContextFactory<DataContext>
    {
        public DataContext CreateDbContext(string[] args = null)
        {
            DbContextOptions options = new DbContextOptionsBuilder().UseSqlServer(Utils.Encrypter.DecryptStringAES(Sesion.Instance.ConnectionString, Sesion.Instance.PublicKey)).Options;
            DataContext dbContext = new DataContext(options);

            return dbContext;
        }
    }
}
