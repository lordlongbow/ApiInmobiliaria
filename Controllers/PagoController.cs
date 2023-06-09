using api_prueba.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api_prueba.Controllers

{
    [ApiController]
    [Route("api/[controller]")]
    public class PagoController : ControllerBase
    {
        private readonly DataContext _context;

        public PagoController(DataContext contexto){
            _context = contexto;
        }


[HttpGet("{id_contrato}")]
public IEnumerable<Pago> ObtenerPagos(int id_contrato)
{
    var contrato = _context.Contrato.FirstOrDefault(x => x.ContratoId == id_contrato);
    
    if (contrato == null)
    {
        return null; 
    }
    var pagos = _context.Pago
        .Where(x => x.ContratoId == id_contrato)
        .Include(x => x.Contrato)
        .Include(x => x.Contrato.Inmueble)
        .Include(x => x.Contrato.Inmueble.Propietario)
        .Include(x => x.Contrato.Inmueble.Tipo)
        .Include(x => x.Contrato.Inmueble.Uso)
        .Include(x => x.Contrato.Inquilino)
        .ToList();
;
    return pagos;
}


        [HttpPost("agregaPago")]
        public IActionResult Post([FromBody]Pago pago){
            _context.Pago.Add(pago);
            _context.SaveChanges();
            return Ok(pago);
        }       


    }
}