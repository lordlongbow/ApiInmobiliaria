using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using api_prueba.Modelos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace api_prueba.Controllers;

[ApiController]

[Route("api/[controller]")] 

public class PropietarioController : ControllerBase
{
        private readonly DataContext _context;

        private readonly IConfiguration _config;

        private readonly IWebHostEnvironment _environment;

    public PropietarioController(DataContext contexto, IConfiguration config, IWebHostEnvironment environment)
    {
        _context = contexto;
        _config = config;
        _environment = environment;
    }


/*
[HttpPost("login")]
public async Task<IActionResult> Login([FromBody] Propietario propietario)
{
    try
    {
        string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: propietario.Contraseña,
            salt: System.Text.Encoding.ASCII.GetBytes(_config["Salt"]),
            prf: KeyDerivationPrf.HMACSHA1,
            iterationCount: 1000,
            numBytesRequested: 256 / 8
        ));

        var p = await _context.Propietario.FirstOrDefaultAsync(x => x.Email == propietario.Email);
        if (p == null || !string.Equals(p.Contraseña, hashed))
        {
            return BadRequest("Credenciales incorrectas");
        }
        else
        {
            var key = new SymmetricSecurityKey(
                System.Text.Encoding.ASCII.GetBytes(_config["TokenAuthentication:SecretKey"])
            );
            var credenciales = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, p.Email),
                new Claim("FullName", p.Nombre + " " + p.Apellido),
                new Claim(ClaimTypes.Role, "Propietario")
            };

            var token = new JwtSecurityToken(
                issuer: _config["TokenAuthentication:Issuer"],
                audience: _config["TokenAuthentication:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: credenciales
            );

            return Ok(new JwtSecurityTokenHandler().WriteToken(token));
        }
    }
    catch (Exception e)
    {
        return BadRequest(e.Message);
    }
}

*/

[HttpGet("propietarios")]
public async Task <IActionResult> ObtenerTodos()
{
    try 
    {
    return Ok(await _context.Propietario.ToListAsync());

    }
    catch(Exception e)
    {
            return BadRequest("Error al traer los Propietarios");
    }
}



[HttpPost("login")]
public IActionResult Login(LoginView lv){
    
    var p = _context.Propietario.FirstOrDefault(x => x.Email == lv.Email);
    if (p == null)
    {
        return BadRequest("Credenciales incorrectas");
    }
   
        var hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: lv.Contraseña,
            salt: System.Text.Encoding.ASCII.GetBytes(_config["Salt"]),
            prf: KeyDerivationPrf.HMACSHA1,
            iterationCount: 1000,
            numBytesRequested: 256 / 8
        ));
        if(p.Contraseña != hashed)
        {
            return BadRequest("Credenciales incorrectas");
        }
        var key = new SymmetricSecurityKey(
            System.Text.Encoding.ASCII.GetBytes(_config["TokenAuthentication:SecretKey"])
        );
        var credenciales = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, p.Email),
            new Claim("FullName", p.Nombre + " " + p.Apellido)
        };
         var token = new JwtSecurityToken(
                issuer: _config["TokenAuthentication:Issuer"],
                audience: _config["TokenAuthentication:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(60000),
                signingCredentials: credenciales
            );

            return Ok(new JwtSecurityTokenHandler().WriteToken(token));

}


    [HttpGet("perfil")]
    [Authorize]
    public async Task<IActionResult> MiPerfil()
    {   
        try
        {

           var propietario = await _context.Propietario.SingleOrDefaultAsync(x => x.Email == User.Identity.Name);
            return propietario == null ? BadRequest("Datos Incorrectos") : Ok(propietario);
        }
        catch (Exception e)
        {
          return BadRequest("Error");
        }
    }
    
    [HttpPost("crear")]
    public Propietario Post(Propietario propietario)
    {
        try{
        var hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: propietario.Contraseña,
            salt: System.Text.Encoding.ASCII.GetBytes(_config["Salt"]),
            prf: KeyDerivationPrf.HMACSHA1,
            iterationCount: 1000,
            numBytesRequested: 256 / 8
        ));
        propietario.Contraseña = hashed;
        _context.Propietario.Add(propietario);

        /*if(propietario.Foto == null && propietario.Id > 0){
            string wwwPath = enviroment.WebRootPath;
            string path = Path.Combine(wwwPath, "fotos", propietario.Foto.FileName);
            string FileName = "fotoperfil" + propietario.Id + path.GetExtension(propietario.Foto.FileName);
            string nombreFoto = Path.Combine(path, FileName);
            propietario.Foto = Path.Combine("/fotos", nombreFoto);
        }
        _context.Propietario.Update(propietario);*/
        _context.SaveChanges();
        return propietario;

        }
        catch(Exception e)
        { 
            throw e; //return BadRequest("Hubo un problema al cargar su usuario");
        }
    }

[HttpPut("actualizar/{id}")]
[Authorize]
public async Task<IActionResult> ActualizarPerfil(int id, [FromBody] Propietario propietario)
{

    var propietarioLogueado = _context.Propietario.FirstOrDefault(x => x.Email == User.Identity.Name);  

    if (propietarioLogueado == null)
    {
        return BadRequest("Datos incorrectos");
    }

    propietarioLogueado.Nombre = propietario.Nombre;
    propietarioLogueado.Apellido = propietario.Apellido;
    propietarioLogueado.Domicilio = propietario.Domicilio;
    propietarioLogueado.Dni = propietario.Dni;
    
    _context.Propietario.Update(propietarioLogueado);
    
    try
    {
        _context.SaveChanges();

    return Ok(propietarioLogueado);
    }
    catch(Exception e){
        return BadRequest(e.Message);
    }
}

[HttpPut("actualizar/foto/{id}")]
[Authorize]
public async Task<IActionResult> ActualizarFoto(int id, [FromForm] IFormFile foto)
{
    var propietarioLogueado = await _context.Propietario.FirstOrDefaultAsync(x => x.Email == User.Identity.Name && x.Id == id);

    if (propietarioLogueado == null)
    {
        return BadRequest("Datos incorrectos");
    }
    if (foto == null)
    {
        return BadRequest("Datos incorrectos");

    }
    string wwwPath = _environment.WebRootPath;
    string path = Path.Combine(wwwPath, "fotos", foto.FileName);
    string FileName = "fotoperfil" + propietarioLogueado.Id + Path.GetExtension(foto.FileName);
    string nombreFoto = Path.Combine(path, FileName);
    propietarioLogueado.Foto = Path.Combine("/fotos", nombreFoto);
    _context.Propietario.Update(propietarioLogueado);
    await _context.SaveChangesAsync();
    return Ok(propietarioLogueado);

}






    [HttpPut("cambiocontraseña/{id}")]
    [Authorize]
    public IActionResult cambiocontraseña(Propietario propietario)
    {
        var propietarioLogueado = _context.Propietario.Where(x => x.Email == User.Identity.Name).FirstOrDefault();
        if(propietarioLogueado == null){
            return BadRequest("Datos Incorrectos");
        }
        if(propietarioLogueado.Contraseña != propietario.Contraseña){
            return BadRequest("Datos no coinciden");
        }
        else
        {
             var hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: propietario.Contraseña,
                salt: System.Text.Encoding.ASCII.GetBytes(_config["Salt"]),
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 1000,
                numBytesRequested: 256 / 8 
            ));
            propietarioLogueado.Contraseña = hashed;

            var ContraseñaCambiada =_context.Propietario.Update(propietarioLogueado);
            _context.SaveChanges();
            return Ok(ContraseñaCambiada);
        }
    }


}