using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace api_prueba.Modelos
{
    [Table("propietarios")]
    public class PropietarioActulizar{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public int Id{get; set;}
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
    [Column("foto_ruta")]
    public String? Foto{get; set;}
}
}