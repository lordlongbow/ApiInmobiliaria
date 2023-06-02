using System.Security.Claims;
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



    [HttpGet()]
    [Authorize]
    public IActionResult misInmuebles()
    {
     var propietario = _context.Propietario.FirstOrDefault(x => x.Email == User.Identity.Name);  
     var misInmuebles =_context.Inmueble.Where(x => x.PropietarioId == propietario.Id  ).Include(x => x.Propietario).ToList();
       if(misInmuebles == null){
           
           return BadRequest("No hay inmuebles");

       } else{
           return Ok(misInmuebles);
       }
    }

    
    [HttpPost()]
    [Authorize]
    public IActionResult CargarInmueble([FromBody]Inmueble inmueble)
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
    string FileName = "fotoInmueble" + inmueble.InmuebleId + Path.GetExtension(foto.FileName);
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


[HttpPut("{id}")]
[Authorize]
public IActionResult CambioDisponibilidad(int id)
{
    var propietario = _context.Propietario.FirstOrDefault(x => x.Email == User.Identity.Name);  
    var inmuebleDB = _context.Inmueble.FirstOrDefault(x => x.InmuebleId == id && x.PropietarioId == propietario.Id);
    
    if (inmuebleDB == null)
    {
        return NotFound("No se encontró el inmueble o no tienes permisos para modificarlo");
    }

    inmuebleDB.Disponibilidad = !inmuebleDB.Disponibilidad;
    
    try
    {
        _context.SaveChanges();
        return Ok(inmuebleDB);
    }
    catch (Exception ex)
    {
        return BadRequest($"Error al cambiar la disponibilidad del inmueble: {ex.Message}");
    }
}



}