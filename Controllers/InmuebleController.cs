using api_prueba.Modelos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api_prueba.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InmuebleController : ControllerBase
{
    private readonly DataContext _context;
    private readonly IWebHostEnvironment _environment;
    
    public InmuebleController(DataContext contexto)
    {
        _context = contexto;
    }


    [HttpGet("{id}")]
    public Inmueble Get(int id)
    {

     var Inmueble=  _context.Inmueble.Where(x => x.InmuebleId == id).Include(x => x.Propietario).FirstOrDefault();
       
       return Inmueble;
    }


    [HttpGet("misInmuebles")]
    public IActionResult misInmuebles(Propietario propietario)
    {

     var misInmuebles =_context.Inmueble.Where(x => x.PropietarioId == propietario.Id && propietario.Email == User.Identity.Name && User.Identity.IsAuthenticated ).Include(x => x.Propietario).ToList();
       if(misInmuebles == null){
           
           return BadRequest("No hay inmuebles");

       } else{
           return Ok(misInmuebles);
       }
    }

    
    [HttpPost("crearInmueble")]
    [Authorize]
    public IActionResult Post([FromBody]Inmueble inmueble)
    {
        var propietario = _context.Propietario.Where(x => x.Email == User.Identity.Name).FirstOrDefault();
     
        if(propietario.Email == User.Identity.Name && User.Identity.IsAuthenticated){
            inmueble.PropietarioId = propietario.Id;
            inmueble.Disponibilidad = true;
            _context.Inmueble.Add(inmueble);
            _context.SaveChanges();
            return Ok(inmueble);
        }
            return BadRequest("No puedes crear inmuebles");

     
        
    }

    [HttpPut("actualizarInmueble/{id}")]
    public Inmueble Put(int id, Inmueble inmueble)
    {
        var inmuebleDB = _context.Inmueble.Find(id);
        _context.Inmueble.Update(inmueble);
        _context.SaveChanges();
        return inmuebleDB;
    }
    
    
[HttpPut("actualizar/foto/{id}")]
[Authorize]
public async Task<IActionResult> ActualizarFoto(int id, [FromForm] IFormFile foto)
{
    var propietario = _context.Propietario.Where(x => x.Email == User.Identity.Name).FirstOrDefault();
   var inmueble = _context.Inmueble.Where(x => x.PropietarioId == propietario.Id).FirstOrDefault();
    if (inmueble == null)
    {
        return BadRequest("Datos incorrectos");
    }
    if (foto == null)
    {
        return BadRequest("Datos incorrectos");

    }
    string wwwPath = _environment.WebRootPath;
    string path = Path.Combine(wwwPath, "fotos", foto.FileName);
    string FileName = "fotoperfil" + inmueble.InmuebleId + Path.GetExtension(foto.FileName);
    string nombreFoto = Path.Combine(path, FileName);
    inmueble.Foto = Path.Combine("/fotos", nombreFoto);
    _context.Inmueble.Update(inmueble);
    await _context.SaveChangesAsync();
    return Ok(inmueble);

}




    [HttpDelete("{id}")]
    public void Delete(int id)
    {
        var inmuebleDB = _context.Inmueble.Find(id);
        if(inmuebleDB.Disponibilidad == true){
        _context.Inmueble.Remove(inmuebleDB);
        _context.SaveChanges();   
        }
    }



    [HttpPut("cambioDisponibilidad/{id}")]
    [Authorize]
    public IActionResult cambioDisponibilidad(int id, Inmueble inmueble){
        var inmuebleDB = _context.Inmueble.Where(x => x.InmuebleId == id && x.PropietarioId == inmueble.PropietarioId).FirstOrDefault();
        inmuebleDB.Disponibilidad = inmueble.Disponibilidad;
        _context.Inmueble.Update(inmuebleDB);
        _context.SaveChanges();
        return Ok(inmuebleDB);
    }


}