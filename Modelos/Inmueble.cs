
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api_prueba.Modelos
{
[Table("inmuebles")]
public class Inmueble
{
   
    [Key] 
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public int InmuebleId { get; set; }
    
    [Required] 
    [Column("direccion")]
    public string Direccion { get; set; }
    
    [Required]
    [Column("precio")]
    public decimal Precio { get; set; }
    
    [Required]
    [Column("cant_hambientes")]
    public int CantAmbientes { get; set; }
    
    [Required]
    [Column("latitud")]
    public int Latitud { get; set; }
    
    [Required]
    [Column("longitud")]
    public int Longitud { get; set; }
    
  
    [ForeignKey("TipoId")]// llave foranea del tipo
   

    [Column("id_tipo")]
    public int TipoId {get;set;}
    
    [ForeignKey("UsoId")]// llave foranea del uso

    [Column("id_uso")]
    public int UsoId {get;set;}
    [Column("disponibilidad ")]
    public bool Disponibilidad  {get;set;}  

    [ForeignKey("PropietarioId")]
    public Propietario Propietario {get;set;}
  
    [Column("id_propietario")]
    public int PropietarioId {get;set;}
    [Column("foto_rutaInmueble")]
    public string Foto {get; set;}
}

}