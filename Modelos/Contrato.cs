using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api_prueba.Modelos;

[Table("contratos")]
public class Contrato
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public int ContratoId { get; set; }
    [Column("fecha_inio")]
    public DateTime FechaInicio { get; set; }
    [Column("fecah_finalizacion")]
    public DateTime FechaFinalizacion { get; set; }

    public Inmueble? Inmueble { get; set; }

    [ForeignKey("InmuebleId")]
    [Column("inmueble_id")]
    public int InmuebleId { get; set; }

    public Inquilino? Inquilino { get; set; }

    [ForeignKey("InquilinoId")]
    [Column("inquilino_id")]
    public int InquilinoId { get; set; }
}
