using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api_prueba.Modelos;

public class Uso{

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public int UsoId { get; set; }
    public String Descripcion {get; set;}
}