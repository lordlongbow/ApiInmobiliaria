using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api_prueba.Modelos
{   

[Table("inquilinos")]
public class Inquilino{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public int? Id{get; set;}
    [Column("nombre")]
    public String? Nombre{get;set;}
    [Column("apellido")]
    public String? Apellido{get;set;}
    [Column("dni")]
    public String? Dni{get;set;}
    [Column("domicilio")]
    public String? Domicilio{get;set;}
    [Column("telefono")]
    public String? Telefono{get;set;}
   [Column("foto_rutaInquilino")]
   public String? Foto {get; set;}

}
}