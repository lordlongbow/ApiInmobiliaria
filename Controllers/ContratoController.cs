using api_prueba.Modelos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api_prueba.Controllers

{
    [ApiController]
    [Route("api/[controller]")]
    public class ContratoController : ControllerBase
    {
        private readonly DataContext _context;

        public ContratoController(DataContext contexto){
            _context = contexto;
        }

        [HttpGet("{id}")]
        [Authorize]
        public Contrato ObtenerContrato(int id){
            
            var propietario = _context.Propietario.Where(x => x.Email == User.Identity.Name).FirstOrDefault();
            var contrato = _context.Contrato.Where(x => x.ContratoId == id ).FirstOrDefault();
            var inmueble = _context.Inmueble.Where(x => x.InmuebleId == contrato.InmuebleId).FirstOrDefault();

            if(contrato.InmuebleId == inmueble.InmuebleId && inmueble.PropietarioId == propietario.Id && propietario.Email == User.Identity.Name && User.Identity.IsAuthenticated){
                return contrato;
            }
            return null;
        }

        [HttpGet()]
        [Authorize]
        public IEnumerable<Contrato> getMisContratos(Propietario propietario){

            var miscontratos = _context.Contrato.Include( x => x.Inmueble).ThenInclude( i => i.Propietario).Where(x => x.Inmueble.PropietarioId == propietario.Id).ToList();
            if(propietario.Email == User.Identity.Name && User.Identity.IsAuthenticated){
                
            return miscontratos;
            }
            return null;
        }


    }
}