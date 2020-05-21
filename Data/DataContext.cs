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
            modelBuilder.Entity<User>()
		                        .Property(u => u.Id)
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
        if (!optionsBuilder.IsConfigured)
        {
           if(myEnv.Equals("LOCAL")){   
            optionsBuilder.UseMySql(connection);
            }
           else{     
            optionsBuilder.UseNpgsql(@connection);  
           }
        }
    }


public DataContext(DbContextOptions<DataContext> options) : base(options) { }
public DbSet<EmailData> Emails { get; set; }
public DbSet<User> Users {get;set;}

}
