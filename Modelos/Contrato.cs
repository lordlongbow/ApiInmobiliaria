using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api_prueba.Modelos;

public class Contrato
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id_contrato")]
    public int ContratoId { get; set; }

    public DateTime FechaInicio { get; set; }

    public DateTime FechaFinalizacion { get; set; }

    public Inmueble? Inmueble { get; set; }

    [ForeignKey("InmuebleId")]
    [Column("id_inmueble")]
    public int InmuebleId { get; set; }

    public Inquilino? Inquilino { get; set; }

    [ForeignKey("InquilinoId")]
    [Column("id_inquilino")]
    public int InquilinoId { get; set; }
}
