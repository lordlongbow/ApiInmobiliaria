using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api_prueba.Modelos;

[Table("pagos")]
public class Pago
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public int  PagoId { get; set; }
    
    public DateTime  Fecha { get; set; }
    public float Monto { get; set; }
    public Contrato Contrato{ get; set; }
    [ForeignKey("ContratoId")]
    [Column("contrato_id")]
    public int ContratoId{ get; set; }
  
}