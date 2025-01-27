using hospitalapp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Nurse")] // Restrict access to nurses only
public class NurseController : ControllerBase
{
    private readonly HospitalDbContext _context;

    public NurseController(HospitalDbContext context)
    {
        _context = context;
    }

    [HttpGet("Patients")]
    public async Task<IActionResult> GetPatients()
    {
        // Ensure the user is authenticated
        if (!User.Identity.IsAuthenticated)
        {
            return Unauthorized(new { error = "You are not authenticated." });
        }

        // Extract the NurseID from the user's claims
        var nurseIdClaim = User.Claims.FirstOrDefault(c => c.Type == "NurseID");
        if (nurseIdClaim == null)
        {
            return Unauthorized(new { error = "Nurse ID is missing in your claims." });
        }

        if (!int.TryParse(nurseIdClaim.Value, out var nurseId))
        {
            return BadRequest(new { error = "Invalid Nurse ID in your claims." });
        }

        // Query the database to find PATIENT_RECORD entries associated with the NurseID
        var patients = await _context.AssignNurses
            .Where(an => an.NurseId == nurseId) // Filter by NurseID
            .Join(_context.PatientRecords,
                an => an.RecordId,
                pr => pr.RecordId,
                (an, pr) => new
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
    [HttpGet("recentTests")]
    public async Task<IActionResult> GetTestsByNurse()
    {
        // Ensure the user is authenticated
        if (!User.Identity.IsAuthenticated)
        {
            return Unauthorized(new { error = "You are not authenticated." });
        }

        // Extract the NurseID from the user's claims
        var nurseIdClaim = User.Claims.FirstOrDefault(c => c.Type == "NurseID");
        if (nurseIdClaim == null)
        {
            return Unauthorized(new { error = "Nurse ID is missing in your claims." });
        }

        if (!int.TryParse(nurseIdClaim.Value, out var nurseId))
        {
            return BadRequest(new { error = "Invalid Nurse ID in your claims." });
        }

        // Query the database to get the tests taken by the nurse along with patient information
        var testsWithPatientInfo = await _context.PerformTests
            .Where(pt => pt.NurseId == nurseId)
            .Join(_context.DiagnosticTests,
                pt => pt.TestId,
                dt => dt.TestId,
                (pt, dt) => new { pt.RecordId, dt.TestId, dt.TestName, dt.TestDate, dt.TestResult })
            .Join(_context.PatientRecords,
                pt => pt.RecordId,
                pr => pr.RecordId,
                (pt, pr) => new
                {
                    pr.RecordId,
                    pr.FirstName,
                    pr.LastName,
                    pr.Gender,
                    pr.ContactInfo,
                    pr.Address,
                    TestId = pt.TestId,
                    TestName = pt.TestName,
                    TestDate = pt.TestDate,
                    TestResult = pt.TestResult
                })
            .ToListAsync();

        return Ok(testsWithPatientInfo);
    }
}
