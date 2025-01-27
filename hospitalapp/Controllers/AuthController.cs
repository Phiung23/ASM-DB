using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using hospitalapp.Models.AuthModel;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;

    public AuthController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    // Register Endpoint
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var user = new IdentityUser
        {
            UserName = model.Username,
            Email = model.Email
        };

        // Create the user
        var result = await _userManager.CreateAsync(user, model.Password);
        if (result.Succeeded)
        {
            // Assign Role to User
            if (!string.IsNullOrEmpty(model.Role))
            {
                var roleResult = await _userManager.AddToRoleAsync(user, model.Role);
                if (!roleResult.Succeeded)
                    return BadRequest(roleResult.Errors);
            }

            // Add Claims Based on Role
            if (model.EntityId.HasValue)
            {
                string claimType = model.Role switch
                {
                    "Patient" => "PatientID",
                    "Doctor" => "DoctorID",
                    "Nurse" => "NurseID",
                    "Technician" => "TechnicianID",
                    _ => null
                };

                if (claimType != null)
                {
                    var claim = new Claim(claimType, model.EntityId.Value.ToString());
                    var claimResult = await _userManager.AddClaimAsync(user, claim);

                    if (!claimResult.Succeeded)
                    {
                        return BadRequest(claimResult.Errors);
                    }
                }
            }

            return Ok(new { message = "User registered successfully!" });
        }

        return BadRequest(result.Errors);
    }


    // Login Endpoint
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        // Find the user by username
        var user = await _userManager.FindByNameAsync(model.Username);
        if (user == null)
            return Unauthorized(new { error = "Invalid username or password." });

        // Check the user's password
        var isPasswordValid = await _userManager.CheckPasswordAsync(user, model.Password);
        if (!isPasswordValid)
            return Unauthorized(new { error = "Invalid username or password." });

        // Retrieve the user's claims and roles
       

        var roles = await _userManager.GetRolesAsync(user);
        var role = roles.First();
        var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, user.UserName),
        new Claim(ClaimTypes.NameIdentifier, user.Id), // Unique ID of the user
        new Claim(ClaimTypes.Role, role) // Add the role claim
    };
        string roleGiveback = "";

        switch(role)
    {
        case "Patient":
                var patientIdClaim = (await _userManager.GetClaimsAsync(user))
                                     .FirstOrDefault(c => c.Type == "PatientID");
                roleGiveback = "Patient";
                if (patientIdClaim != null)
                {
                    claims.Add(patientIdClaim);
                }
                break;

            case "Doctor":
                var doctorIdClaim = (await _userManager.GetClaimsAsync(user))
                                    .FirstOrDefault(c => c.Type == "DoctorID");
                roleGiveback = "Doctor";

                if (doctorIdClaim != null)
                {
                    claims.Add(doctorIdClaim);
                }
                break;

            case "Nurse":
                var nurseIdClaim = (await _userManager.GetClaimsAsync(user))
                                   .FirstOrDefault(c => c.Type == "NurseID");
                roleGiveback = "Nurse";

                if (nurseIdClaim != null)
                {
                    claims.Add(nurseIdClaim);
                }
                break;

            case "Technician":
                var technicianIdClaim = (await _userManager.GetClaimsAsync(user))
                                        .FirstOrDefault(c => c.Type == "TechnicianID");
                roleGiveback = "Technician";

                if (technicianIdClaim != null)
                {
                    claims.Add(technicianIdClaim);
                }
                break;

            case "Admin":
                var adminIdClaim = (await _userManager.GetClaimsAsync(user))
                                   .FirstOrDefault(c => c.Type == "AdminID");
                roleGiveback = "Admin";

                if (adminIdClaim != null)
                {
                    claims.Add(adminIdClaim);
                }
                break;

            default:
                return BadRequest(new { error = "Invalid role assigned to user." });
            }

            // Generate JWT token
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("YourLongerSecretKeyHere123456!@#")); // Use a secure key
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: "https://localhost:7205",      // Issuer
            audience: "https://localhost:7205",    // Audience
            claims: claims,                        // Add claims to the payload
            expires: DateTime.UtcNow.AddMinutes(60), // Expiration time
            signingCredentials: creds              // Signing credentials
        );

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        // Return the JWT token
        return Ok(new
        {
            message = "Login successful!",
            token = jwt,
            roleGiveback
        });
    }


    // Logout Endpoint
    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return Ok(new { message = "Logged out successfully!" });
    }
}
