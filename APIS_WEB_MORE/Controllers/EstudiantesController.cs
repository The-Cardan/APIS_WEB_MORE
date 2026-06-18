
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
    }
}
