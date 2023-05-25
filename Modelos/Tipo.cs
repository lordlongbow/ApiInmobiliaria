using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api_prueba.Modelos;

public class Tipo{

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id_tipo")]
    public int TipoId { get; set; }
    public String Descripcion{get; set;}	
}