using hospitalapp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Doctor")] // Restrict access to doctors only
public class DoctorController : ControllerBase
{
    private readonly HospitalDbContext _context;

    public DoctorController(HospitalDbContext context)
    {
        _context = context;
    }

    // Endpoint to get patients for the logged-in doctor
    [HttpGet("Patients")]
    public async Task<IActionResult> GetPatients()
    {
        // Ensure the user is authenticated
        if (!User.Identity.IsAuthenticated)
        {
            return Unauthorized(new { error = "You are not authenticated." });
        }

        // Extract the DoctorID from the user's claims
        var doctorIdClaim = User.Claims.FirstOrDefault(c => c.Type == "DoctorID");
        if (doctorIdClaim == null)
        {
            return Unauthorized(new { error = "Doctor ID is missing in your claims." });
        }

        if (!int.TryParse(doctorIdClaim.Value, out var doctorId))
        {
            return BadRequest(new { error = "Invalid Doctor ID in your claims." });
        }

        // Query the database to find PATIENT_RECORD entries associated with the DoctorID
        var patients = await _context.AssignDoctors
            .Where(ad => ad.DoctorId == doctorId) // Filter by DoctorID
            .Join(_context.PatientRecords,
                ad => ad.RecordId,
                pr => pr.RecordId,
                (ad, pr) => new
                {
                    pr.RecordId,
                    pr.FirstName,
                    pr.LastName,
                    pr.Gender,
                    pr.ContactInfo,
                    pr.EmerContactInfo,
                    pr.Address,
                    pr.CurrentMedication
                })
            .ToListAsync();

        return Ok(patients);
    }
    [HttpGet("recentSurgeries")]
    public async Task<IActionResult> GetSurgeriesByDoctor()
    {
        // Ensure the user is authenticated
        if (!User.Identity.IsAuthenticated)
        {
            return Unauthorized(new { error = "You are not authenticated." });
        }

        // Extract the DoctorID from the user's claims
        var doctorIdClaim = User.Claims.FirstOrDefault(c => c.Type == "DoctorID");
        if (doctorIdClaim == null)
        {
            return Unauthorized(new { error = "Doctor ID is missing in your claims." });
        }

        if (!int.TryParse(doctorIdClaim.Value, out var doctorId))
        {
            return BadRequest(new { error = "Invalid Doctor ID in your claims." });
        }

        // Query the database to get surgeries performed by the doctor along with patient information
        var surgeriesWithPatientInfo = await _context.PerformSurgeries
            .Where(ps => ps.DoctorId == doctorId)
            .Join(_context.Surgeries,
                ps => ps.SurgeryId,
                s => s.SurgeryId,
                (ps, s) => new { ps.RecordId, s.SurgeryId, s.TypeSurgery, s.DateSurgery, s.Outcome })
            .Join(_context.PatientRecords,
                ps => ps.RecordId,
                pr => pr.RecordId,
                (ps, pr) => new
                {
                    pr.RecordId,
                    pr.FirstName,
                    pr.LastName,
                    pr.Gender,
                    pr.ContactInfo,
                    pr.Address,
                    SurgeryId = ps.SurgeryId,
                    TypeSurgery = ps.TypeSurgery,
                    DateSurgery = ps.DateSurgery,
                    Outcome = ps.Outcome
                })
            .ToListAsync();

        return Ok(surgeriesWithPatientInfo);
    }

}
