document
  .getElementById("loginForm")
  .addEventListener("submit", async function (e) {
    e.preventDefault();

    // Gather input values
    const loginData = {
      username: document.getElementById("username").value,
      password: document.getElementById("password").value,
      rememberMe: document.getElementById("rememberMe").checked,
    };

    try {
      // Send POST request to login API
      const response = await fetch(
        "https://localhost:7205/api/Auth/login", // Replace with your actual API endpoint
        {
          method: "POST",
          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify(loginData),
        }
      );

      // Parse the response
      const result = await response.json();

      // Display response message
      const responseMessage = document.getElementById("responseMessage");
      if (response.ok) {
        // Store the JWT securely in localStorage
        localStorage.setItem("jwt", result.token); // Assuming the token is in `result.token`

        // Success message
        responseMessage.style.color = "green";
        responseMessage.textContent = "Login successful!";

        // Determine role from roleGiveBack and redirect accordingly
        const role = result.roleGiveback; // Assuming roleGiveback contains the role string
        let redirectUrl = "";

        switch (role) {
          case "Patient":
            redirectUrl = "pages/patient/dashboard.html";
            break;
          case "Doctor":
            redirectUrl = "pages/doctor/dashboard.html";
            break;
          case "Nurse":
            redirectUrl = "pages/nurse/dashboard.html";
            break;
          case "Admin":
            redirectUrl = "pages/admin/dashboard.html";
            break;
          default:
            redirectUrl = "/testLogin.html"; // Fallback for unknown roles
        }

        // Redirect to the determined URL
        setTimeout(() => {
          window.location.href = redirectUrl;
        }, 1500);
      } else {
        responseMessage.style.color = "red";
        responseMessage.textContent = `Login failed: ${
          result.error || JSON.stringify(result)
        }`;
      }
    } catch (error) {
      console.error("Error:", error);
      document.getElementById("responseMessage").style.color = "red";
      document.getElementById("responseMessage").textContent =
        "An error occurred during login.";
    }
  });
