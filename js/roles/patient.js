document.addEventListener("DOMContentLoaded", function () {
  initializeDashboard();
  initializeNavigation();
  loadUpcomingAppointments();
  createVitalsChart();
  createLabResultsChart();
  loadRecentActivity();
  loadCurrentMedications();
});
function initializeNavigation() {
  document.querySelectorAll(".sidebar-menu a").forEach((link) => {
    link.addEventListener("click", (e) => {
      e.preventDefault();
      const id = link.id;

      switch (id) {
        case "medical-history":
          showMedicalHistory();
          break;
        case "prescriptions":
          showPrescriptions();
          break;
        case "lab-results":
          showLabResults();
          break;
        case "surgery-results":
          showSurgeryResults();
          break;
        case "test-results":
          showTestResults();
          break;
        case "messages":
          showMessages();
          break;
        case "logout":
          login();
          break;
      }
    });
  });
}
function showMedicalHistory() {
  window.location.href = "/pages/patient/showMedicalHistory.html";
}
function login() {
  window.location.href = "/testLogin.html";
}
function showSurgeryResults() {
  window.location.href = "/pages/patient/showSurgeryResults.html";
}
function showTestResults() {
  window.location.href = "/pages/patient/showTestResults.html";
}
function showMedicalRecords() {
  const modalHtml = `
        <div class="modal" id="medicalRecordsModal">
            <div class="modal-content">
                <div class="modal-header">
                    <h2>Medical Records</h2>
                    <span class="close-modal">&times;</span>
                </div>
                <div class="modal-body">
                    <div class="records-list">
                        <div class="record-item">
                            <h4>General Checkup - 2024-03-15</h4>
                            <p>Dr. Smith - Cardiology</p>
                            <button class="btn btn-primary" onclick="viewRecord('2024-03-15')">View Details</button>
                        </div>
                        <!-- Add more records -->
                    </div>
                </div>
            </div>
        </div>
    `;

  document.body.insertAdjacentHTML("beforeend", modalHtml);

  const modal = document.getElementById("medicalRecordsModal");
  modal.querySelector(".close-modal").addEventListener("click", () => {
    modal.remove();
  });
}

function handleLogout() {
  sessionStorage.removeItem("currentUser");
  window.location.href = "../../index.html";
}

function showModal(modalId, content) {
  const modalHtml = `
        <div class="modal" id="${modalId}">
            <div class="modal-content">
                <div class="modal-header">
                    <h2>${content.title}</h2>
                    <span class="close-modal">&times;</span>
                </div>
                <div class="modal-body">
                    ${content.body}
                </div>
            </div>
        </div>
    `;

  document.body.insertAdjacentHTML("beforeend", modalHtml);
  const modal = document.getElementById(modalId);

  modal.querySelector(".close-modal").addEventListener("click", () => {
    modal.remove();
  });

  return modal;
}

function showPrescriptions() {
  const modalHtml = `
        <div class="modal" id="prescriptionsModal">
            <div class="modal-content">
                <div class="modal-header">
                    <h2>Prescriptions</h2>
                    <span class="close-modal">&times;</span>
                </div>
                <div class="modal-body">
                    <div class="prescriptions-list">
                        <!-- Add prescription items -->
                    </div>
                </div>
            </div>
        </div>
    `;

  document.body.insertAdjacentHTML("beforeend", modalHtml);
  loadPrescriptions();
}

function initializeDashboard() {
  const sidebarToggle = document.getElementById("sidebar-toggle");
  const sidebar = document.querySelector(".sidebar");

  sidebarToggle.addEventListener("click", () => {
    sidebar.classList.toggle("active");
  });

  const userInfo = JSON.parse(sessionStorage.getItem("currentUser")) || {
    name: "John Doe",
  };
  document.querySelector(
    ".user-info span"
  ).textContent = `Welcome, ${userInfo.name}`;
}

function loadUpcomingAppointments() {
  const appointments = [
    {
      doctor: "A",
      department: "Cardiology",
      date: "2024-03-20",
      time: "10:00 AM",
      status: "Confirmed",
    },
    {
      doctor: "B",
      department: "Neurology",
      date: "2024-03-25",
      time: "02:30 PM",
      status: "Pending",
    },
  ];

  const appointmentsList = document.getElementById("upcomingAppointments");
  appointmentsList.innerHTML = appointments
    .map(
      (apt) => `
        <div class="appointment-item">
            <div class="appointment-info">
                <h4>${apt.doctor} - ${apt.department}</h4>
                <p>${apt.date} at ${apt.time}</p>
                <span class="badge badge-${
                  apt.status.toLowerCase() === "confirmed"
                    ? "success"
                    : "warning"
                }">
                    ${apt.status}
                </span>
            </div>
            <div class="appointment-actions">
                <button class="btn btn-sm btn-primary" onclick="rescheduleAppointment('${
                  apt.date
                }')">
                    Reschedule
                </button>
                <button class="btn btn-sm btn-danger" onclick="cancelAppointment('${
                  apt.date
                }')">
                    Cancel
                </button>
            </div>
        </div>
    `
    )
    .join("");
}

function createVitalsChart() {
  const ctx = document.getElementById("vitalsChart").getContext("2d");
  new Chart(ctx, {
    type: "line",
    data: {
      labels: ["Jan", "Feb", "Mar", "Apr", "May", "Jun"],
      datasets: [
        {
          label: "Blood Pressure (Systolic)",
          data: [120, 118, 122, 119, 121, 120],
          borderColor: "#4CAF50",
          fill: false,
        },
        {
          label: "Heart Rate",
          data: [72, 75, 71, 73, 72, 74],
          borderColor: "#2196F3",
          fill: false,
        },
      ],
    },
    options: {
      responsive: true,
      scales: {
        y: {
          beginAtZero: false,
        },
      },
    },
  });
}

function createLabResultsChart() {
  const ctx = document.getElementById("labResultsChart").getContext("2d");
  new Chart(ctx, {
    type: "bar",
    data: {
      labels: ["Cholesterol", "Blood Sugar", "Hemoglobin", "White Blood Cells"],
      datasets: [
        {
          label: "Latest Results",
          data: [185, 95, 14.5, 7.5],
          backgroundColor: ["#4CAF50", "#2196F3", "#FF9800", "#E91E63"],
        },
      ],
    },
    options: {
      responsive: true,
      scales: {
        y: {
          beginAtZero: true,
        },
      },
    },
  });
}

function loadRecentActivity() {
  const activities = [
    {
      type: "appointment",
      description: "Appointment with A",
      date: "2024-03-15",
      icon: "fas fa-calendar-check",
    },
    {
      type: "prescription",
      description: "New prescription added",
      date: "2024-03-14",
      icon: "fas fa-prescription",
    },
    {
      type: "lab",
      description: "Lab results available",
      date: "2024-03-13",
      icon: "fas fa-flask",
    },
  ];

  const activityTimeline = document.getElementById("recentActivity");
  activityTimeline.innerHTML = activities
    .map(
      (activity) => `
        <div class="activity-item">
            <div class="activity-icon">
                <i class="${activity.icon}"></i>
            </div>
            <div class="activity-details">
                <p>${activity.description}</p>
                <small>${activity.date}</small>
            </div>
        </div>
    `
    )
    .join("");
}

function loadCurrentMedications() {
  const medications = [
    {
      name: "Lisinopril",
      dosage: "10mg",
      frequency: "Once daily",
      refills: 2,
    },
    {
      name: "Metformin",
      dosage: "500mg",
      frequency: "Twice daily",
      refills: 3,
    },
  ];

  const medicationsList = document.getElementById("currentMedications");
  medicationsList.innerHTML = medications
    .map(
      (med) => `
        <div class="medication-item">
            <div class="medication-info">
                <h4>${med.name}</h4>
                <p>${med.dosage} - ${med.frequency}</p>
                <small>Refills remaining: ${med.refills}</small>
            </div>
            <div class="medication-actions">
                <button class="btn btn-sm btn-primary" onclick="requestRefill('${med.name}')">
                    Request Refill
                </button>
            </div>
        </div>
    `
    )
    .join("");
}

function bookAppointment() {
  const modalHtml = `
        <div class="modal" id="appointmentModal">
            <div class="modal-content">
                <div class="modal-header">
                    <h2>Book Appointment</h2>
                    <span class="close-modal">&times;</span>
                </div>
                <div class="modal-body">
                    <form id="appointmentForm">
                        <!-- Add form fields -->
                        <button type="submit" class="btn btn-primary">Book</button>
                    </form>
                </div>
            </div>
        </div>
    `;

  document.body.insertAdjacentHTML("beforeend", modalHtml);

  const modal = document.getElementById("appointmentModal");
  const form = modal.querySelector("form");

  form.addEventListener("submit", (e) => {
    e.preventDefault();
    alert("Appointment booked successfully!");
    modal.remove();
  });

  modal.querySelector(".close-modal").addEventListener("click", () => {
    modal.remove();
  });
}

function cancelAppointment(date) {
  if (confirm(`Are you sure you want to cancel your appointment on ${date}?`)) {
    alert("Appointment cancelled successfully!");
    loadUpcomingAppointments();
  }
}

function rescheduleAppointment(date) {
  const modalHtml = `
        <div class="modal" id="rescheduleModal">
            <div class="modal-content">
                <div class="modal-header">
                    <h2>Reschedule Appointment</h2>
                    <span class="close-modal">&times;</span>
                </div>
                <div class="modal-body">
                    <form id="rescheduleForm">
                        <div class="form-group">
                            <label>Current Date: ${date}</label>
                        </div>
                        <div class="form-group">
                            <label>New Date</label>
                            <input type="date" required>
                        </div>
                        <div class="form-group">
                            <label>New Time</label>
                            <select required>
                                <option>09:00 AM</option>
                                <option>10:00 AM</option>
                                <!-- Add more time slots -->
                            </select>
                        </div>
                        <button type="submit" class="btn btn-primary">Reschedule</button>
                    </form>
                </div>
            </div>
        </div>
    `;

  document.body.insertAdjacentHTML("beforeend", modalHtml);

  const modal = document.getElementById("rescheduleModal");
  const form = modal.querySelector("form");

  form.addEventListener("submit", (e) => {
    e.preventDefault();
    alert("Appointment rescheduled successfully!");
    modal.remove();
    loadUpcomingAppointments();
  });

  modal.querySelector(".close-modal").addEventListener("click", () => {
    modal.remove();
  });
}

function requestPrescription() {
  alert("Prescription refill request sent to your doctor.");
}

function viewMessages() {
  window.location.href = "#messages";
}

function initializeEventListeners() {
  document.getElementById("logout").addEventListener("click", (e) => {
    e.preventDefault();
    sessionStorage.removeItem("currentUser");
    window.location.href = "../../index.html";
  });

  document.getElementById("medical-records").addEventListener("click", (e) => {
    e.preventDefault();
    showMedicalRecordsModal();
  });

  document.getElementById("prescriptions").addEventListener("click", (e) => {
    e.preventDefault();
    showPrescriptionsModal();
  });

  document.getElementById("lab-results").addEventListener("click", (e) => {
    e.preventDefault();
    showLabResultsModal();
  });

  document.getElementById("billing").addEventListener("click", (e) => {
    e.preventDefault();
    showBillingModal();
  });
}
