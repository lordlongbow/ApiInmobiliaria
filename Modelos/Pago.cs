using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api_prueba.Modelos;


public class Pago
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id_pago")]
    public int  PagoId { get; set; }
    public DateTime Fecha { get; set; }
    public decimal Monto { get; set; }
    public Contrato Contrato{ get; set; }
    [ForeignKey("ContratoId")]
    [Column("id_contrato")]
    public int ContratoId{ get; set; }
  
}