using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class User{

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id {get;set;}
    public string Username{get;set;}
    public byte[] PasswordHash{get;set;}
    public byte[] PasswordSalt{get;set;}

}