
using Microsoft.AspNetCore.Mvc;
using APIS_WEB_MORE.Models;

namespace APIS_WEB_MORE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstudiantesController : ControllerBase
    {
        private static List<Estudiante> estudiantes = new()
        {
            new Estudiante
            {
                Id = 1,
                Nombre = "Luis",
                Apellido = "Fortuna",
                Correo = "luis@ufhec.edu.do",
                Carrera = "Ingenieria de Sistemas",
                Edad = 22,
                Promedio = 90,
                Activo = true
            },
            new Estudiante
            {
                Id = 2,
                Nombre = "Ana",
                Apellido = "Perez",
                Correo = "ana@ufhec.edu.do",
                Carrera = "Administracion",
                Edad = 20,
                Promedio = 80,
                Activo = true
            }
        };

        // GET: api/estudiantes
        [HttpGet]
        public ActionResult<IEnumerable<Estudiante>> GetAll()
        {
            return Ok(estudiantes);
        }

        // GET: api/estudiantes/{id}
        [HttpGet("{id}")]
        public ActionResult<Estudiante> GetById(int id)
        {
            var estudiante = estudiantes.FirstOrDefault(e => e.Id == id);

            if (estudiante == null)
            {
                return NotFound();
            }

            return Ok(estudiante);
        }

        // POST: api/estudiantes
        [HttpPost]
        public ActionResult<Estudiante> Create(Estudiante estudiante)
        {
            estudiante.Id = estudiantes.Max(e => e.Id) + 1;

            estudiantes.Add(estudiante);

            return CreatedAtAction(
                nameof(GetById),
                new { id = estudiante.Id },
                estudiante);
        }


        // PUT: api/estudiantes/{id}
        [HttpPut("{id}")]
        public IActionResult Update(int id, Estudiante estudianteActualizado)
        {
            var estudiante = estudiantes.FirstOrDefault(e => e.Id == id);

            if (estudiante == null)
            {
                return NotFound();
            }

            estudiante.Nombre = estudianteActualizado.Nombre;
            estudiante.Apellido = estudianteActualizado.Apellido;
            estudiante.Correo = estudianteActualizado.Correo;
            estudiante.Carrera = estudianteActualizado.Carrera;
            estudiante.Edad = estudianteActualizado.Edad;
            estudiante.Promedio = estudianteActualizado.Promedio;
            estudiante.Activo = estudianteActualizado.Activo;

            return NoContent();
        }


        // DELETE: api/estudiantes/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var estudiante = estudiantes.FirstOrDefault(e => e.Id == id);

            if (estudiante == null)
            {
                return NotFound();
            }

            estudiantes.Remove(estudiante);

            return NoContent();
        }

        // GET: api/estudiantes/buscar?texto
        [HttpGet("buscar")]
        public ActionResult<IEnumerable<Estudiante>> Buscar(string texto)
        {
            var resultado = estudiantes.Where(e =>
                e.Nombre.Contains(texto, StringComparison.OrdinalIgnoreCase)
                ||
                e.Apellido.Contains(texto, StringComparison.OrdinalIgnoreCase));

            return Ok(resultado);
        }
    }
}
