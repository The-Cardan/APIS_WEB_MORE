
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
                Nombre = "Carlos",
                Apellido = "Morillo",
                Correo = "carlitos@ufhec.edu.do",
                Carrera = "Ingenieria de Sistemas",
                Edad = 25,
                Promedio = 92,
                Activo = true
            },
            new Estudiante
            {
                Id = 3,
                Nombre = "Ana",
                Apellido = "Perez",
                Correo = "ana@ufhec.edu.do",
                Carrera = "Administracion",
                Edad = 20,
                Promedio = 80,
                Activo = true
            },
            new Estudiante
            {
                Id = 4,
                Nombre = "Nairoby",
                Apellido = "Encarnacion",
                Correo = "Nai@ufhec.edu.do",
                Carrera = "Contabilidad",
                Edad = 18,
                Promedio = 60,
                Activo = true
            },

            new Estudiante
            {
                Id = 5,
                Nombre = "Ronal",
                Apellido = "De OLeo",
                Correo = "Ron@ufhec.edu.do",
                Carrera = "Educacion Fisica",
                Edad = 26,
                Promedio = 69,
                Activo = true
            },
            new Estudiante
            {
                Id = 6,
                Nombre = "Octanny",
                Apellido = "Montero",
                Correo = "Otis@ufhec.edu.do",
                Carrera = "Ingenieria Civil",
                Edad = 20,
                Promedio = 88,
                Activo = true
            },
            new Estudiante
            {
                Id = 7,
                Nombre = "Ronaisy",
                Apellido = "Cuevas",
                Correo = "Ronay@ufhec.edu.do",
                Carrera = "Ciencias Forenses",
                Edad = 28,
                Promedio = 58,
                Activo = true
            },
            new Estudiante
            {
                Id = 8,
                Nombre = "Andy",
                Apellido = "Montez",
                Correo = "monteandy@ufhec.edu.do",
                Carrera = "Contabilidad",
                Edad = 42,
                Promedio = 95,
                Activo = true
            },
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

        //  GET: api/estudiantes/carrera

        [HttpGet("carrera/{carrera}")]
        public ActionResult<IEnumerable<Estudiante>> Carrera(string carrera)
        {
            var resultado = estudiantes.Where(e =>
                e.Carrera.Equals(carrera,
                StringComparison.OrdinalIgnoreCase));

            return Ok(resultado);
        }

        //GET: api/estudiantes/promedio?
        [HttpGet("aprobados")]
        public ActionResult<IEnumerable<Estudiante>> Aprobados(decimal promedioMinimo = 70)
        {
            var resultado = estudiantes.Where(e =>
                e.Promedio >= promedioMinimo);

            return Ok(resultado);
        }


        // GET: api/estudiantes por ordenamiento
        [HttpGet("ordenar")]
        public ActionResult<IEnumerable<Estudiante>> Ordenar(
    string por,
    string direccion = "asc")
        {
            IEnumerable<Estudiante> resultado = estudiantes;

            switch (por.ToLower())
            {
                case "nombre":
                    resultado = direccion.ToLower() == "desc"
                        ? estudiantes.OrderByDescending(e => e.Nombre)
                        : estudiantes.OrderBy(e => e.Nombre);
                    break;

                case "promedio":
                    resultado = direccion.ToLower() == "desc"
                        ? estudiantes.OrderByDescending(e => e.Promedio)
                        : estudiantes.OrderBy(e => e.Promedio);
                    break;

                case "edad":
                    resultado = direccion.ToLower() == "desc"
                        ? estudiantes.OrderByDescending(e => e.Edad)
                        : estudiantes.OrderBy(e => e.Edad);
                    break;

                default:
                    return BadRequest("Campo de ordenamiento inválido.");
            }

            return Ok(resultado);
        }
        // GET: api/estudiantes por rango de promedio

        [HttpGet("rango")]
        public ActionResult<IEnumerable<Estudiante>> Rango(
    decimal promedioDesde,
    decimal promedioHasta)
        {
            var resultado = estudiantes.Where(e =>
                e.Promedio >= promedioDesde &&
                e.Promedio <= promedioHasta);

            return Ok(resultado);
        }

        // GET: api/estudiantes/estadisticas
        [HttpGet("estadisticas")]
        public IActionResult Estadisticas()
        {
            var total = estudiantes.Count;

            var aprobados = estudiantes.Count(e => e.Promedio >= 70);

            var reprobados = estudiantes.Count(e => e.Promedio < 70);

            var promedioGeneral = estudiantes.Average(e => e.Promedio);

            var mejorPromedio = estudiantes.Max(e => e.Promedio);

            var peorPromedio = estudiantes.Min(e => e.Promedio);

            return Ok(new
            {
                Total = total,
                Aprobados = aprobados,
                Reprobados = reprobados,
                PromedioGeneral = promedioGeneral,
                MejorPromedio = mejorPromedio,
                PeorPromedio = peorPromedio
            });
        }

        // PUT: api/estudiantes/{id}/estado

        [HttpPut("{id}/estado")]
        public IActionResult CambiarEstado(int id, bool activo)
        {
            var estudiante = estudiantes.FirstOrDefault(e => e.Id == id);

            if (estudiante == null)
            {
                return NotFound();
            }

            estudiante.Activo = activo;

            return NoContent();
        }
    }
}

