using hospitalapp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Patient")] // Restrict access to patients only
public class PatientController : ControllerBase
{
    private readonly HospitalDbContext _context;

    public PatientController(HospitalDbContext context)
    {
        _context = context;
    }

    [HttpGet("tests")]
    public async Task<IActionResult> GetTests()
    {


        // Ensure the user is authenticated
        if (!User.Identity.IsAuthenticated)
        {
            return Unauthorized(new { error = "You are not authenticated." });
        }

        // Extract the PatientID from the user's claims
        var patientIdClaim = User.Claims.FirstOrDefault(c => c.Type == "PatientID");
        if (patientIdClaim == null)
        {
            return Unauthorized(new { error = "Patient ID is missing in your claims." });
        }

        if (!int.TryParse(patientIdClaim.Value, out var patientId))
        {
            return BadRequest(new { error = "Invalid Patient ID in your claims." });
        }

        // Query the database to find surgeries associated with this patient
        var tests = await _context.PerformTests
            .Where(ps => ps.RecordId == patientId)
            .Join(_context.DiagnosticTests,
                ps => ps.TestId,
                s => s.TestId,
                (ps, s) => new
                {
                    s.TestId,
                    s.TestName,
                    s.TestDate,
                    s.TestDescription,
                    s.TestResult
                })
            .ToListAsync();

        return Ok(tests);
    }

    [HttpGet("surgeries")]
    public async Task<IActionResult> GetSurgeries()
    {

        // Ensure the user is authenticated
        if (!User.Identity.IsAuthenticated)
        {
            return Unauthorized(new { error = "You are not authenticated." });
        }

        // Extract the PatientID from the user's claims
        var patientIdClaim = User.Claims.FirstOrDefault(c => c.Type == "PatientID");
        if (patientIdClaim == null)
        {
            return Unauthorized(new { error = "Patient ID is missing in your claims." });
        }

        if (!int.TryParse(patientIdClaim.Value, out var patientId))
        {
            return BadRequest(new { error = "Invalid Patient ID in your claims." });
        }

        // Query the database to find surgeries associated with this patient
        var surgeries = await _context.PerformSurgeries
            .Where(ps => ps.RecordId == patientId)
            .Join(_context.Surgeries,
                ps => ps.SurgeryId,
                s => s.SurgeryId,
                (ps, s) => new
                {
                    s.SurgeryId,
                    s.TypeSurgery,
                    s.Complication,
                    s.Outcome,
                    s.DateSurgery
                })
            .ToListAsync();

        return Ok(surgeries);
    }

    [HttpGet("medicalHistory")]
    public async Task<IActionResult> GetMedicalHistory()
    {

        // Ensure the user is authenticated
        if (!User.Identity.IsAuthenticated)
        {
            return Unauthorized(new { error = "You are not authenticated." });
        }

        // Extract the PatientID from the user's claims
        var patientIdClaim = User.Claims.FirstOrDefault(c => c.Type == "PatientID");
        if (patientIdClaim == null)
        {
            return Unauthorized(new { error = "Patient ID is missing in your claims." });
        }

        if (!int.TryParse(patientIdClaim.Value, out var patientId))
        {
            return BadRequest(new { error = "Invalid Patient ID in your claims." });
        }

        // Query the database to find surgeries associated with this patient
        var medicalHistory = await _context.MedicalHistories
        .Where(mh => mh.RecordId == patientId)
        .Select(mh => new
        {
            mh.RecordId,
            mh.TypeName,
            mh.Treatment,
            mh.DescriptionDetail,
            mh.Stage
        })
        .ToListAsync();

        // Return the medical history
        return Ok(medicalHistory);
    }
}
