using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class EmailData{

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int id {get;set;}
    public string from {get;set;}

    
    public string adress {get; set;} 
    
    public string to {get;set;}

    public string msg {get;set;} 

    public string options {get;set;}   

}