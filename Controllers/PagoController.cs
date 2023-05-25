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

        [HttpGet("{id}")]
        public Pago Get(int id){
            return _context.Pago.Find(id);
        }

        [HttpGet()]
        public IEnumerable<Pago> Get(Contrato contrato){
           var pagos = _context.Pago.Where(x => x.ContratoId == contrato.ContratoId).ToList();
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