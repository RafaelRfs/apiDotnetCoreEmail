using System;
using Microsoft.EntityFrameworkCore;

public class DataContext : DbContext
{


#region Required
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmailData>()
		                        .Property(e => e.id)
		                            .ValueGeneratedOnAdd();
        }
        #endregion

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string myEnv = Environment.GetEnvironmentVariable("ENVIRONMENT");
        string uri = Environment.GetEnvironmentVariable("DATABASE_URL");
        string driver =  Environment.GetEnvironmentVariable("DRIVERDB");
        string version = Environment.GetEnvironmentVariable("VERSION");
        Uri siteUri = new Uri(uri);
        string [] userData =  siteUri.UserInfo.Split(":");
        string user = userData[0];
        string pass = userData.Length > 1 ? userData[1] : "";

        String connection = String.Format(
            @"Server={0};Port={1};User Id={2};Password={3};Database={4}",
            siteUri.Host,
            siteUri.Port,
            user,
            pass,
            siteUri.PathAndQuery.Replace("/", "")
            );
            Console.WriteLine(" URI >> "+ uri);
            Console.WriteLine(" Environment >> "+ myEnv);
            Console.WriteLine(" Connection String >> "+connection);
            Console.WriteLine(" Driver >>"+driver);
            Console.WriteLine(" RFS Prod Version "+version);

        if (!optionsBuilder.IsConfigured)
        {
           if(myEnv.Equals("LOCAL")){
            Console.WriteLine("DATABASE Schema ===========>  MySql ");     
            optionsBuilder.UseMySql(connection);
            }
           else{
            Console.WriteLine("DATABASE Schema ===========>  PostgreSql ");        
            optionsBuilder.UseNpgsql(@connection);  
            }
        }
    }


public DataContext(DbContextOptions<DataContext> options) : base(options) { }
public DbSet<EmailData> Emails { get; set; }

}