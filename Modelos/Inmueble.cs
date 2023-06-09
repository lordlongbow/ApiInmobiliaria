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
        public float Latitud { get; set; }

        [Required]
        [Column("longitud")]
        public float Longitud { get; set; }

        [ForeignKey("Tipo")]
        [Column("id_tipo")]
        public int TipoId { get; set; }

        public Tipo Tipo { get; set; }

        [ForeignKey("Uso")]
        [Column("id_uso")]
        public int UsoId { get; set; }

        public Uso Uso { get; set; }

        [ForeignKey("Propietario")]
        [Column("id_propietario")]
        public int PropietarioId { get; set; }

        public Propietario Propietario { get; set; }

        [Column("disponibilidad")]
        public bool Disponibilidad { get; set; }

        [Column("foto_rutaInmueble")]
        public string? Foto { get; set; }
    }
}
