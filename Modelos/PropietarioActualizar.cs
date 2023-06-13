using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace api_prueba.Modelos
{
    [Table("propietarios")]
    public class PropietarioActualizar{
    
    [Column("nombre")]
    public String Nombre{get; set;}
    [Column("apellido")]
    public String Apellido {get; set;}
    [Column("dni")]
    public int Dni {get; set;}
    [Column("direccion")]
    public String Domicilio{get; set;}
    [Column("telefono")]
    public String Telefono{get; set;}
}
}