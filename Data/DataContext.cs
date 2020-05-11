using System;
using Microsoft.EntityFrameworkCore;

public class DataContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string myEnv = Environment.GetEnvironmentVariable("ENVIRONMENT");
        string uri = Environment.GetEnvironmentVariable("DATABASE_URL");
        Uri siteUri = new Uri(uri);
        string [] userData =  siteUri.UserInfo.Split(":");
        string user = userData[0];
        string pass = userData.Length > 1 ? userData[1] : "";

         //context.Database.Migrate();

         //optionsBuilder.con

        String connection = String.Format(
            "Server={0};Port={1};User Id={2};Password={3};Database={4}",
            siteUri.Host,
            siteUri.Port,
            user,
            pass,
            siteUri.PathAndQuery.Replace("/", "")
            );
            Console.WriteLine(" URI >> "+ uri);
            Console.WriteLine(" Environment >> "+ myEnv);
            Console.WriteLine(" Connection String >> "+connection);

        if (!optionsBuilder.IsConfigured)
        {
           if(myEnv.Equals("LOCAL")){
            Console.WriteLine(" DATABASE: MySql ");     
            optionsBuilder.UseMySql(connection);
            }
           else{
             Console.WriteLine("DATABASE:  PostgreSql ");        
           optionsBuilder.UseNpgsql(connection);  
            }
        }
    }


public DataContext(DbContextOptions<DataContext> options) : base(options) { }
public DbSet<EmailData> Emails { get; set; }

}