using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
public class LoginService{

 public static  async Task<string> ValidateToken(string token)
{
    try{
    string tokenUrl = Environment.GetEnvironmentVariable("TOKEN_VALIDATE_URL");
    HttpClient client = new HttpClient();
    client.DefaultRequestHeaders.Accept.Clear();
    client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
    client.DefaultRequestHeaders.Authorization =
    new AuthenticationHeaderValue("Bearer", token);
    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    Console.WriteLine("[+] Validando token na Api de Login >> "+tokenUrl);
    return  await client.GetStringAsync(tokenUrl);
    }
    catch(Exception e)
    {
     Console.WriteLine("ERROR >> "+e.Message);
     return null;
    }   
    }
}