using api_prueba.Modelos;
using Microsoft.AspNetCore.Mvc;

namespace api_prueba.Controllers;

[ApiController]
[Route("api/[controller]")]

public class InquilinoController : ControllerBase
{
    private readonly DataContext _context;
    private readonly IWebHostEnvironment _environment;

    public InquilinoController(DataContext contexto, IWebHostEnvironment environment){
        _context = contexto;
        _environment = environment;
    }

    [HttpGet("{id}")]
    public Inquilino Get(int id){
        return _context.Inquilino.Find(id);
    }    

    [HttpGet()]
    public IEnumerable<Inquilino> Get(){
        return _context.Inquilino.ToList();
    }
    

    [HttpPost]
    public Inquilino Post(Inquilino inquilino){
        _context.Inquilino.Add(inquilino);
        _context.SaveChanges();
        return inquilino;
    }

    [HttpPut("{id}")]
    public Inquilino Put(int id, Inquilino inquilino){
        var inquilinoDB = _context.Inquilino.Find(id);
        _context.Inquilino.Update(inquilino);
         _context.SaveChanges();
        return inquilinoDB;
    }

    [HttpDelete("{id}")]
    public void Delete(int id){
        var inquilinoDB = _context.Inquilino.Find(id);
        _context.Inquilino.Remove(inquilinoDB);
        _context.SaveChanges();
    }

    [HttpPut("{id}/foto")]
public async Task<IActionResult> cambiarFoto(int id, IFormFile foto){
    var inquilinoDB = _context.Inquilino.Find(id);
    if(inquilinoDB== null){
        return NotFound();
    }
    if(inquilinoDB.Foto == null){
        return NotFound();
    }
    string wwwPath = _environment.WebRootPath;
    string path = Path.Combine(wwwPath, "fotos", foto.FileName);
    string FileName = "fotoperfil" + inquilinoDB.Id + Path.GetExtension(foto.FileName);
    string nombreFoto = Path.Combine(path, FileName);
    inquilinoDB.Foto = Path.Combine("/fotos", nombreFoto);
    _context.Inquilino.Update(inquilinoDB);
    await _context.SaveChangesAsync();
    return Ok(inquilinoDB);
}

    
}