using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Northwin2.Data;
using Northwin2.Entities;
using Northwin2.Exceptions;
using Northwin2.Services;
using static Northwin2.Entities.DTO;

namespace Northwin2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployesController : ControllerBase
    {
        private readonly IEmployeService _employeService;

        public EmployesController(IEmployeService service)
        {
            _employeService = service;
        }

        // GET: api/Employes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employe>>> GetEmployes([FromQuery] string? rechercheNom, [FromQuery] DateTime? dateEmbaucheMax)
        {
            List<Employe> employes = await _employeService.GetEmployes(rechercheNom, dateEmbaucheMax);
            return Ok(employes);
        }

        // GET: api/Employes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Employe>> GetEmploye(int id)
        {
            Employe? employe = await _employeService.GetEmployeById(id);

            if (employe == null)
            {
                return NotFound();
            }

            return Ok(employe);
        }

        [HttpGet("/api/Regions/{id}")]
        public async Task<ActionResult<Region>> GetRegion(int id)
        {
            Region? region = await _employeService.GetRegion(id);
            if (region == null) { return NotFound(); }

            return Ok(region);
        }

        // PUT: api/Employes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754


        /*
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmploye(int id, Employe employe)
        {
            if (id != employe.Id)
            {
                return BadRequest();
            }

            _context.Entry(employe).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }*/

        // POST: api/Employes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Employe>> PostEmploye(Employe employe)
        {
            //try
            //{
                Employe emp = await _employeService.AddEmpoye(employe);
                return CreatedAtAction(nameof(GetEmploye), new { id = emp.Id }, emp);
            //}
            /*catch (Exception e)
            {
                return this.CustomResponseForError(e);
            }*/
            /*catch (DbUpdateException e)
            {
                ProblemDetails pb = e.ConvertToProblemDetails();
                return Problem(pb.Detail, null, pb.Status, pb.Title);
            }*/
        }

        /*// POST: api/Employes/formdata
        [HttpPost("formdata")]
        public async Task<ActionResult<Employe>> PostEmployéFormData([FromForm] FormEmploye fe)
        {
            Employe emp = new()
            {
                IdAdresse = fe.IdAdresse,
                IdManager = fe.IdManager,
                Nom = fe.Nom,
                Prenom = fe.Prenom,
                Fonction = fe.Fonction,
                Civilite = fe.Civilite,
                DateNaissance = fe.DateNaissance,
                DateEmbauche = fe.DateEmbauche
            };

            // Récupère les données de l'adresse
            emp.Adresses = new()
            {
                Id = fe.Adresse.Id,
                Rue = fe.Adresse.Rue,
                CodePostal = fe.Adresse.CodePostal,
                Ville = fe.Adresse.Ville,
                Region = fe.Adresse.Region,
                Pays = fe.Adresse.Pays,
                Tel = fe.Adresse.Tel
            };

            // Récupère les données du fichier photo
            if (fe.Photo != null)
            {
                using Stream stream = fe.Photo.OpenReadStream();
                emp.Photo = new byte[fe.Photo.Length];
                await stream.ReadAsync(emp.Photo);
            }

            // Récupère les données du fichier notes
            if (fe.Notes != null)
            {
                using StreamReader reader = new(fe.Notes.OpenReadStream());
                emp.Notes = await reader.ReadToEndAsync();
            }

            Employe res = await _employeService.AjouterEmploye(emp);

            // Renvoie une réponse de code 201 avec l'en-tête
            // "location: <url d'accès à l’employé>" et un corps contenant l’employé
            return CreatedAtAction(nameof(GetEmploye), new { id = emp.Id }, res);
        }

        // POST: api/Affectations
        [HttpPost("/api/Affectations")]
        public async Task<ActionResult<Affectation>> PostAffectation([FromForm] Affectation a)
        {
            //Enregistre les données en base
            await _employeService.AjouterAffectation(a);

            // Renvoie une réponse de code 201 avec l'en-tête
            // "location: <url d'accès à l’employé>" et un corps contenant son affectation
            return CreatedAtAction(nameof(GetEmploye), new { id = a.IdEmploye }, a);
        }*/

        /*
        // DELETE: api/Employes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmploye(int id)
        {
            var employe = await _context.Employes.FindAsync(id);
            if (employe == null)
            {
                return NotFound();
            }

            _context.Employes.Remove(employe);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmployeExists(int id)
        {
            return _context.Employes.Any(e => e.Id == id);
        }*/
    }
}
