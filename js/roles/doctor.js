document.addEventListener("DOMContentLoaded", function () {
  initializeSidebar();
  loadTodaySchedule();
  createDemographicsChart();
  createSuccessRateChart();
  loadRecentPatients();
  initializeEventListeners();
  initializeNavigation();
});
function initializeNavigation() {
  const navMap = {
    dashboard: () => (window.location.href = "dashboard.html"),
    "show-Patient-List": () =>
      (window.location.href = "/pages/doctor/showPatientList.html"),
    "show-surgeries": () => (window.location.href = "showRecentSurgery.html"),
    prescriptions: () => showPrescriptionModal(),
    "lab-requests": () => showLabRequestModal(),
    messages: () => showMessages(),
    reports: () => showReportsModal(),
    logout: () => handleLogout(),
  };

  document.querySelectorAll(".sidebar-menu a").forEach((link) => {
    link.addEventListener("click", (e) => {
      e.preventDefault();
      const id = link.id;
      if (navMap[id]) navMap[id]();
    });
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

function initializeSidebar() {
  const sidebarToggle = document.getElementById("sidebar-toggle");
  const sidebar = document.querySelector(".sidebar");

  sidebarToggle.addEventListener("click", () => {
    sidebar.classList.toggle("active");
  });
}

function loadTodaySchedule() {
  const appointments = [
    {
      time: "09:00 AM",
      patient: "Sarah Johnson",
      type: "Follow-up",
      status: "Confirmed",
    },
    {
      time: "10:30 AM",
      patient: "Mike Wilson",
      type: "New Patient",
      status: "In Progress",
    },
    {
      time: "11:45 AM",
      patient: "Emily Davis",
      type: "Consultation",
      status: "Waiting",
    },
    // Add more appointments as needed
  ];

  const appointmentCards = document.querySelector(".appointment-cards");
  appointmentCards.innerHTML = appointments
    .map(
      (apt) => `
        <div class="appointment-card">
            <div class="appointment-time">${apt.time}</div>
            <div class="appointment-details">
                <h4>${apt.patient}</h4>
                <p>${apt.type}</p>
                <span class="badge badge-${getStatusColor(apt.status)}">${
        apt.status
      }</span>
            </div>
            <div class="appointment-actions">
                <button class="btn btn-primary btn-sm" onclick="viewPatientDetails('${
                  apt.patient
                }')">
                    <i class="fas fa-eye"></i>
                </button>
                <button class="btn btn-success btn-sm" onclick="startConsultation('${
                  apt.patient
                }')">
                    <i class="fas fa-play"></i>
                </button>
            </div>
        </div>
    `
    )
    .join("");
}

function getStatusColor(status) {
  switch (status.toLowerCase()) {
    case "confirmed":
      return "primary";
    case "in progress":
      return "success";
    case "waiting":
      return "warning";
    default:
      return "secondary";
  }
}

function createDemographicsChart() {
  const ctx = document.getElementById("demographicsChart").getContext("2d");
  new Chart(ctx, {
    type: "pie",
    data: {
      labels: ["0-18", "19-35", "36-50", "51-70", "70+"],
      datasets: [
        {
          data: [15, 25, 30, 20, 10],
          backgroundColor: [
            "#4CAF50",
            "#2196F3",
            "#FF9800",
            "#E91E63",
            "#9C27B0",
          ],
        },
      ],
    },
    options: {
      responsive: true,
      plugins: {
        legend: {
          position: "right",
        },
        title: {
          display: true,
          text: "Patient Age Distribution",
        },
      },
    },
  });
}

function createSuccessRateChart() {
  const ctx = document.getElementById("successRateChart").getContext("2d");
  new Chart(ctx, {
    type: "line",
    data: {
      labels: ["Jan", "Feb", "Mar", "Apr", "May", "Jun"],
      datasets: [
        {
          label: "Treatment Success Rate",
          data: [85, 88, 87, 90, 92, 91],
          fill: true,
          borderColor: "#4CAF50",
          backgroundColor: "rgba(76, 175, 80, 0.1)",
          tension: 0.4,
        },
      ],
    },
    options: {
      responsive: true,
      scales: {
        y: {
          beginAtZero: false,
          min: 80,
          max: 100,
        },
      },
    },
  });
}

function loadRecentPatients() {
  const patients = [
    {
      id: "P001",
      name: "Sarah Johnson",
      lastVisit: "2024-03-15",
      status: "Stable",
    },
    {
      id: "P002",
      name: "Mike Wilson",
      lastVisit: "2024-03-14",
      status: "Follow-up Required",
    },
    {
      id: "P003",
      name: "Emily Davis",
      lastVisit: "2024-03-13",
      status: "New Patient",
    },
  ];

  const tableBody = document.getElementById("recentPatientsTable");
  tableBody.innerHTML = patients
    .map(
      (patient) => `
        <tr>
            <td>${patient.id}</td>
            <td>${patient.name}</td>
            <td>${patient.lastVisit}</td>
            <td><span class="badge badge-${getPatientStatusColor(
              patient.status
            )}">${patient.status}</span></td>
            <td>
                <button class="btn btn-primary btn-sm" onclick="viewPatientDetails('${
                  patient.id
                }')">
                    View
                </button>
                <button class="btn btn-success btn-sm" onclick="createPrescription('${
                  patient.id
                }')">
                    Prescribe
                </button>
            </td>
        </tr>
    `
    )
    .join("");
}

function getPatientStatusColor(status) {
  switch (status.toLowerCase()) {
    case "stable":
      return "success";
    case "follow-up required":
      return "warning";
    case "new patient":
      return "primary";
    default:
      return "secondary";
  }
}

function viewPatientDetails(patientId) {
  showPatientModal(patientId);
}

function createPrescription(patientId) {
  showPrescriptionModal(patientId);
}

function showPatientModal(patientId) {
  const modalHTML = `
        <div class="modal" id="patientModal">
            <div class="modal-content">
                <div class="modal-header">
                    <h2>Patient Details</h2>
                    <span class="close-modal">&times;</span>
                </div>
                <div class="modal-body">
                    <div class="patient-info">
                        <h3>Personal Information</h3>
                        <p><strong>ID:</strong> ${patientId}</p>
                        <p><strong>Name:</strong> Sarah Johnson</p>
                        <p><strong>Age:</strong> 35</p>
                        <p><strong>Contact:</strong> (555) 123-4567</p>

                        <h3>Medical History</h3>
                        <div class="medical-history">
                            <p><strong>Allergies:</strong> Penicillin</p>
                            <p><strong>Chronic Conditions:</strong> None</p>
                            <p><strong>Previous Surgeries:</strong> Appendectomy (2018)</p>
                        </div>

                        <h3>Recent Visits</h3>
                        <div class="table-container">
                            <table class="table">
                                <thead>
                                    <tr>
                                        <th>Date</th>
                                        <th>Reason</th>
                                        <th>Treatment</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td>2024-03-15</td>
                                        <td>Regular Checkup</td>
                                        <td>Prescribed vitamins</td>
                                    </tr>
                                    <tr>
                                        <td>2024-02-28</td>
                                        <td>Flu Symptoms</td>
                                        <td>Antibiotics prescribed</td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button class="btn btn-primary" onclick="createPrescription('${patientId}')">New Prescription</button>
                    <button class="btn btn-secondary" onclick="requestLabTests('${patientId}')">Request Labs</button>
                </div>
            </div>
        </div>
    `;

  document.body.insertAdjacentHTML("beforeend", modalHTML);
  const modal = document.getElementById("patientModal");
  modal.style.display = "block";

  modal.querySelector(".close-modal").addEventListener("click", () => {
    modal.remove();
  });
}

function showPrescriptionModal(patientId) {
  const modalHTML = `
        <div class="modal" id="prescriptionModal">
            <div class="modal-content">
                <div class="modal-header">
                    <h2>New Prescription</h2>
                    <span class="close-modal">&times;</span>
                </div>
                <div class="modal-body">
                    <form id="prescriptionForm">
                        <div class="form-group">
                            <label>Medication</label>
                            <input type="text" class="form-control" required>
                        </div>
                        <div class="form-group">
                            <label>Dosage</label>
                            <input type="text" class="form-control" required>
                        </div>
                        <div class="form-group">
                            <label>Frequency</label>
                            <select class="form-control">
                                <option>Once daily</option>
                                <option>Twice daily</option>
                                <option>Three times daily</option>
                                <option>Four times daily</option>
                            </select>
                        </div>
                        <div class="form-group">
                            <label>Duration</label>
                            <input type="text" class="form-control" required>
                        </div>
                        <div class="form-group">
                            <label>Special Instructions</label>
                            <textarea class="form-control"></textarea>
                        </div>
                        <button type="submit" class="btn btn-primary">Save Prescription</button>
                    </form>
                </div>
            </div>
        </div>
    `;

  document.body.insertAdjacentHTML("beforeend", modalHTML);
  const modal = document.getElementById("prescriptionModal");
  modal.style.display = "block";

  modal.querySelector(".close-modal").addEventListener("click", () => {
    modal.remove();
  });

  document
    .getElementById("prescriptionForm")
    .addEventListener("submit", (e) => {
      e.preventDefault();
      alert("Prescription saved successfully!");
      modal.remove();
    });
}

function initializeEventListeners() {
  // Logout handler
  document.getElementById("logout").addEventListener("click", (e) => {
    e.preventDefault();
    sessionStorage.removeItem("currentUser");
    window.location.href = "../../index.html";
  });

  // Lab requests handler
  document.getElementById("lab-requests").addEventListener("click", (e) => {
    e.preventDefault();
    showLabRequestsModal();
  });
}

function initializeNavigation() {
  document.querySelectorAll(".sidebar-menu a").forEach((link) => {
    link.addEventListener("click", (e) => {
      e.preventDefault();
      const id = link.id;

      switch (id) {
        case "show-Patient-List":
          window.location.href = "showPatientList.html";
          break;
        case "show-surgeries":
          window.location.href = "showRecentSurgery.html";
          break;
        case "prescriptions":
          showPrescriptionModal();
          break;
        case "lab-requests":
          showLabRequestModal();
          break;
        case "logout":
          handleLogout();
          break;
      }
    });
  });
}

function showLabRequestModal() {
  const modalHtml = `
        <div class="modal" id="labRequestModal">
            <div class="modal-content">
                <div class="modal-header">
                    <h2>Request Lab Test</h2>
                    <span class="close-modal">&times;</span>
                </div>
                <div class="modal-body">
                    <form id="labRequestForm">
                        <!-- Add form fields -->
                        <button type="submit" class="btn btn-primary">Submit Request</button>
                    </form>
                </div>
            </div>
        </div>
    `;

  document.body.insertAdjacentHTML("beforeend", modalHtml);
  // Add event listeners and form handling
}

function initializeNavigation() {
  document.querySelectorAll(".sidebar-menu a").forEach((link) => {
    link.addEventListener("click", (e) => {
      e.preventDefault();
      const id = link.id;

      switch (id) {
        case "show-Patient-List":
          window.location.href = "showPatientList.html";
          break;
        case "show-surgeries":
          window.location.href = "showRecentSurgery.html";
          break;
        case "prescriptions":
          showPrescriptionModal();
          break;
        case "lab-requests":
          showLabRequestModal();
          break;
        case "logout":
          handleLogout();
          break;
      }
    });
  });
}

function showLabRequestModal() {
  const modalHtml = `
        <div class="modal" id="labRequestModal">
            <div class="modal-content">
                <div class="modal-header">
                    <h2>Request Lab Test</h2>
                    <span class="close-modal">&times;</span>
                </div>
                <div class="modal-body">
                    <form id="labRequestForm">
                        <!-- Add form fields -->
                        <button type="submit" class="btn btn-primary">Submit Request</button>
                    </form>
                </div>
            </div>
        </div>
    `;

  document.body.insertAdjacentHTML("beforeend", modalHtml);
  // Add event listeners and form handling
}

// Add similar modal functions for other features
