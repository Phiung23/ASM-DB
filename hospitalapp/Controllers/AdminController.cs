using hospitalapp.Models;
using hospitalapp.Models.AddModel;
using hospitalapp.Models.UpdateModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")] // Restrict access to admin only
public class AdminController : ControllerBase
{
    private readonly HospitalDbContext _context;

    public AdminController(HospitalDbContext context)
    {
        _context = context;
    }

    [HttpGet("viewPatient")]
    public async Task<IActionResult> ViewPatients()
    {
        // Ensure the user is authenticated
        if (!User.Identity.IsAuthenticated)
        {
            return Unauthorized(new { error = "You are not authenticated." });
        }

        // Query the database to get all patient records
        var patients = await _context.PatientRecords
            .Select(pr => new
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

        // Return the patient records
        return Ok(patients);
    }

    [HttpGet("viewDoctor")]
    public async Task<IActionResult> ViewDoctors()
    {
        // Ensure the user is authenticated
        if (!User.Identity.IsAuthenticated)
        {
            return Unauthorized(new { error = "You are not authenticated." });
        }

        // Fetch all doctors from the database
        var doctors = await _context.Employees
            .Where(e => e.JobType == "Doctor") // Filter by Job_type
            .Select(e => new
            {
                e.EmployeeId,
                e.Name,
                e.Dob,
                e.Gender,
                e.PhoneNumber,
                e.Salary,
                e.Experience,
                e.Degree,
                e.DepartmentId
            })
            .ToListAsync();

        return Ok(doctors);
    }
    [HttpGet("viewNurse")]
    [Authorize(Roles = "Admin")] // Restrict access to Admins
    public async Task<IActionResult> ViewNurses()
    {
        // Ensure the user is authenticated
        if (!User.Identity.IsAuthenticated)
        {
            return Unauthorized(new { error = "You are not authenticated." });
        }

        // Query the Employees table for all Nurses
        var nurses = await _context.Employees
            .Where(e => e.JobType == "Nurse")
            .Select(n => new
            {
                n.EmployeeId,
                n.Name,
                n.Dob,
                n.Gender,
                n.PhoneNumber,
                n.Salary,
                n.StartDate,
                n.Experience,
                n.Degree,
                n.DepartmentId
            })
            .ToListAsync();

        return Ok(nurses);
    }
    [HttpPost("addNurse")]
    public async Task<IActionResult> AddNurse([FromBody] AddNurseModel nurseModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        using (var transaction = await _context.Database.BeginTransactionAsync())
        {
            try
            {
                // Insert into EMPLOYEE table
                var newEmployee = new Employee
                {
                    Name = nurseModel.Name,
                    Dob = nurseModel.Dob,
                    Gender = nurseModel.Gender,
                    PhoneNumber = nurseModel.PhoneNumber,
                    JobType = "Nurse", // Ensure this is set to "Nurse"
                    Salary = nurseModel.Salary,
                    StartDate = nurseModel.StartDate,
                    Experience = nurseModel.Experience,
                    Degree = nurseModel.Degree,
                    DepartmentId = nurseModel.DepartmentId
                };
                _context.Employees.Add(newEmployee);
                await _context.SaveChangesAsync();

                // Get the EmployeeID of the newly added employee
                int newEmployeeId = newEmployee.EmployeeId;

                // Insert into NURSE table
                var newNurse = new Nurse
                {
                    NurseId = newEmployeeId, // EmployeeId acts as NurseId
                    Specialty = nurseModel.Specialty
                };
                _context.Nurses.Add(newNurse);
                await _context.SaveChangesAsync();

                // Commit transaction
                await transaction.CommitAsync();

                return Ok(new { message = "Nurse added successfully!" });
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(500, new { error = "An error occurred while adding the nurse.", details = ex.Message });
            }
        }
    }
    [HttpPut("updateNurse")]
    public async Task<IActionResult> UpdateNurse([FromBody] UpdateNurseModel nurseModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        using (var transaction = await _context.Database.BeginTransactionAsync())
        {
            try
            {
                // Fetch the existing employee record
                var existingEmployee = await _context.Employees
                    .FirstOrDefaultAsync(e => e.EmployeeId == nurseModel.EmployeeId);

                if (existingEmployee == null)
                {
                    return NotFound(new { error = "Employee not found." });
                }

                // Update EMPLOYEE table fields
                if (!string.IsNullOrEmpty(nurseModel.Name)) existingEmployee.Name = nurseModel.Name;
                if (nurseModel.Dob.HasValue) existingEmployee.Dob = nurseModel.Dob.Value;
                if (!string.IsNullOrEmpty(nurseModel.Gender)) existingEmployee.Gender = nurseModel.Gender;
                if (!string.IsNullOrEmpty(nurseModel.PhoneNumber)) existingEmployee.PhoneNumber = nurseModel.PhoneNumber;
                if (nurseModel.Salary.HasValue) existingEmployee.Salary = nurseModel.Salary.Value;
                if (nurseModel.StartDate.HasValue) existingEmployee.StartDate = nurseModel.StartDate.Value;
                if (nurseModel.Experience.HasValue) existingEmployee.Experience = nurseModel.Experience.Value;
                if (!string.IsNullOrEmpty(nurseModel.Degree)) existingEmployee.Degree = nurseModel.Degree;
                if (nurseModel.DepartmentId.HasValue) existingEmployee.DepartmentId = nurseModel.DepartmentId.Value;

                // Save changes to EMPLOYEE table
                _context.Employees.Update(existingEmployee);
                await _context.SaveChangesAsync();

                // Fetch the existing nurse record
                var existingNurse = await _context.Nurses
                    .FirstOrDefaultAsync(n => n.NurseId == nurseModel.EmployeeId);

                if (existingNurse == null)
                {
                    return NotFound(new { error = "Nurse not found." });
                }

                // Update NURSE table fields
                if (!string.IsNullOrEmpty(nurseModel.Specialty)) existingNurse.Specialty = nurseModel.Specialty;

                // Save changes to NURSE table
                _context.Nurses.Update(existingNurse);
                await _context.SaveChangesAsync();

                // Commit transaction
                await transaction.CommitAsync();

                return Ok(new { message = "Nurse updated successfully." });
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(500, new { error = "An error occurred while updating the nurse.", details = ex.Message });
            }
        }
    }

    [HttpDelete("deleteNurse/{id}")]
    public async Task<IActionResult> DeleteNurse(int id)
    {
        var nurse = await _context.Employees.FirstOrDefaultAsync(e => e.EmployeeId == id && e.JobType == "Nurse");
        if (nurse == null)
        {
            return NotFound(new { error = "Nurse not found." });
        }

        using (var transaction = await _context.Database.BeginTransactionAsync())
        {
            try
            {
                // Delete from NURSE table
                var nurseEntry = await _context.Nurses.FirstOrDefaultAsync(n => n.NurseId == id);
                if (nurseEntry != null)
                {
                    _context.Nurses.Remove(nurseEntry);
                }

                // Delete from EMPLOYEE table
                _context.Employees.Remove(nurse);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return Ok(new { message = "Nurse deleted successfully." });
            }
            catch
            {
                await transaction.RollbackAsync();
                return StatusCode(500, new { error = "An error occurred while deleting the nurse." });
            }
        }
    }
    [HttpPost("addDoctor")]
    public async Task<IActionResult> AddDoctor([FromBody] AddDoctorModel doctorModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        using (var transaction = await _context.Database.BeginTransactionAsync())
        {
            try
            {
                // Insert into EMPLOYEE table
                var newEmployee = new Employee
                {
                    Name = doctorModel.Name,
                    Dob = doctorModel.Dob,
                    Gender = doctorModel.Gender,
                    PhoneNumber = doctorModel.PhoneNumber,
                    JobType = "Doctor", // Ensure this is set to "Doctor"
                    Salary = doctorModel.Salary,
                    StartDate = doctorModel.StartDate,
                    Experience = doctorModel.Experience,
                    Degree = doctorModel.Degree,
                    DepartmentId = doctorModel.DepartmentId
                };
                _context.Employees.Add(newEmployee);
                await _context.SaveChangesAsync();

                // Get the EmployeeID of the newly added employee
                int newEmployeeId = newEmployee.EmployeeId;

                // Insert into DOCTOR table
                var newDoctor = new Doctor
                {
                    DoctorId = newEmployeeId, // EmployeeId acts as DoctorId
                    Specialty = doctorModel.Specialty,
                    Certifica = doctorModel.Certificate
                };
                _context.Doctors.Add(newDoctor);
                await _context.SaveChangesAsync();

                // Commit transaction
                await transaction.CommitAsync();

                return Ok(new { message = "Doctor added successfully!" });
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(500, new { error = "An error occurred while adding the doctor.", details = ex.Message });
            }
        }
    }
    [HttpPut("updateDoctor")]
    public async Task<IActionResult> UpdateDoctor([FromBody] UpdateDoctorModel doctorModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        using (var transaction = await _context.Database.BeginTransactionAsync())
        {
            try
            {
                // Fetch the existing employee record
                var existingEmployee = await _context.Employees
                    .FirstOrDefaultAsync(e => e.EmployeeId == doctorModel.EmployeeId);

                if (existingEmployee == null)
                {
                    return NotFound(new { error = "Employee not found." });
                }

                // Update EMPLOYEE table fields
                if (!string.IsNullOrEmpty(doctorModel.Name)) existingEmployee.Name = doctorModel.Name;
                if (doctorModel.Dob.HasValue) existingEmployee.Dob = doctorModel.Dob.Value;
                if (!string.IsNullOrEmpty(doctorModel.Gender)) existingEmployee.Gender = doctorModel.Gender;
                if (!string.IsNullOrEmpty(doctorModel.PhoneNumber)) existingEmployee.PhoneNumber = doctorModel.PhoneNumber;
                if (doctorModel.Salary.HasValue) existingEmployee.Salary = doctorModel.Salary.Value;
                if (doctorModel.StartDate.HasValue) existingEmployee.StartDate = doctorModel.StartDate.Value;
                if (doctorModel.Experience.HasValue) existingEmployee.Experience = doctorModel.Experience.Value;
                if (!string.IsNullOrEmpty(doctorModel.Degree)) existingEmployee.Degree = doctorModel.Degree;
                if (doctorModel.DepartmentId.HasValue) existingEmployee.DepartmentId = doctorModel.DepartmentId.Value;

                // Save changes to EMPLOYEE table
                _context.Employees.Update(existingEmployee);
                await _context.SaveChangesAsync();

                // Fetch the existing doctor record
                var existingDoctor = await _context.Doctors
                    .FirstOrDefaultAsync(d => d.DoctorId == doctorModel.EmployeeId);

                if (existingDoctor == null)
                {
                    return NotFound(new { error = "Doctor not found." });
                }

                // Update DOCTOR table fields
                if (!string.IsNullOrEmpty(doctorModel.Specialty)) existingDoctor.Specialty = doctorModel.Specialty;
                if (!string.IsNullOrEmpty(doctorModel.Certificate)) existingDoctor.Certifica = doctorModel.Certificate;

                // Save changes to DOCTOR table
                _context.Doctors.Update(existingDoctor);
                await _context.SaveChangesAsync();

                // Commit transaction
                await transaction.CommitAsync();

                return Ok(new { message = "Doctor updated successfully." });
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(500, new { error = "An error occurred while updating the doctor.", details = ex.Message });
            }
        }
    }
    [HttpDelete("deleteDoctor/{id}")]
    public async Task<IActionResult> DeleteDoctor(int id)
    {
        // Find the doctor in the EMPLOYEE table
        var doctor = await _context.Employees.FirstOrDefaultAsync(e => e.EmployeeId == id && e.JobType == "Doctor");
        if (doctor == null)
        {
            return NotFound(new { error = "Doctor not found." });
        }

        using (var transaction = await _context.Database.BeginTransactionAsync())
        {
            try
            {
                // Delete from DOCTOR table
                var doctorEntry = await _context.Doctors.FirstOrDefaultAsync(d => d.DoctorId == id);
                if (doctorEntry != null)
                {
                    _context.Doctors.Remove(doctorEntry);
                }

                // Delete from EMPLOYEE table
                _context.Employees.Remove(doctor);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return Ok(new { message = "Doctor deleted successfully." });
            }
            catch
            {
                await transaction.RollbackAsync();
                return StatusCode(500, new { error = "An error occurred while deleting the doctor." });
            }
        }
    }
    [HttpPost("addPatient")]
    public async Task<IActionResult> AddPatient([FromBody] AddPatientModel patientModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        using (var transaction = await _context.Database.BeginTransactionAsync())
        {
            try
            {
                // Get the next RecordID
                int nextRecordId = await _context.PatientRecords.MaxAsync(p => (int?)p.RecordId) ?? 0;
                nextRecordId++;

                // Insert the new patient
                var newPatient = new PatientRecord
                {
                    RecordId = nextRecordId, // Explicitly set RecordID
                    FirstName = patientModel.FirstName,
                    LastName = patientModel.LastName,
                    Gender = patientModel.Gender,
                    ContactInfo = patientModel.ContactInfo,
                    EmerContactInfo = patientModel.EmerContactInfo,
                    Address = patientModel.Address,
                    CurrentMedication = patientModel.CurrentMedication
                };

                _context.PatientRecords.Add(newPatient);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();
                return Ok(new { message = "Patient added successfully!", recordId = nextRecordId });
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(500, new { error = "An error occurred while adding the patient.", details = ex.Message });
            }
        }
    }
    [HttpPut("updatePatient")]
    public async Task<IActionResult> UpdatePatient([FromBody] UpdatePatientModel patientModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        using (var transaction = await _context.Database.BeginTransactionAsync())
        {
            try
            {
                // Fetch the existing patient record
                var existingPatient = await _context.PatientRecords
                    .FirstOrDefaultAsync(p => p.RecordId == patientModel.RecordId);

                if (existingPatient == null)
                {
                    return NotFound(new { error = "Patient not found." });
                }

                // Update the fields if they are provided in the model
                if (!string.IsNullOrEmpty(patientModel.FirstName))
                    existingPatient.FirstName = patientModel.FirstName;
                if (!string.IsNullOrEmpty(patientModel.LastName))
                    existingPatient.LastName = patientModel.LastName;
                if (!string.IsNullOrEmpty(patientModel.Gender))
                    existingPatient.Gender = patientModel.Gender;
                if (!string.IsNullOrEmpty(patientModel.ContactInfo))
                    existingPatient.ContactInfo = patientModel.ContactInfo;
                if (!string.IsNullOrEmpty(patientModel.EmerContactInfo))
                    existingPatient.EmerContactInfo = patientModel.EmerContactInfo;
                if (!string.IsNullOrEmpty(patientModel.Address))
                    existingPatient.Address = patientModel.Address;
                if (!string.IsNullOrEmpty(patientModel.CurrentMedication))
                    existingPatient.CurrentMedication = patientModel.CurrentMedication;

                // Save changes
                _context.PatientRecords.Update(existingPatient);
                await _context.SaveChangesAsync();

                // Commit transaction
                await transaction.CommitAsync();

                return Ok(new { message = "Patient updated successfully." });
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(500, new { error = "An error occurred while updating the patient.", details = ex.Message });
            }
        }
    }
    [HttpDelete("deletePatient/{id}")]
    public async Task<IActionResult> DeletePatient(int id)
    {
        var patient = await _context.PatientRecords.FirstOrDefaultAsync(p => p.RecordId == id);
        if (patient == null)
        {
            return NotFound(new { error = "Patient not found." });
        }

        using (var transaction = await _context.Database.BeginTransactionAsync())
        {
            try
            {
                // Delete the patient record
                _context.PatientRecords.Remove(patient);
                await _context.SaveChangesAsync();

                // Commit the transaction
                await transaction.CommitAsync();

                return Ok(new { message = "Patient deleted successfully." });
            }
            catch (Exception ex)
            {
                // Rollback the transaction in case of an error
                await transaction.RollbackAsync();
                return StatusCode(500, new { error = "An error occurred while deleting the patient.", details = ex.Message });
            }
        }
    }


}
