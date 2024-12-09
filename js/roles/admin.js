document.addEventListener("DOMContentLoaded", function () {
  initializeSidebar();
  createAdmissionsChart();
  createDepartmentChart();
  loadRecentActivities();
  initializeEventListeners();
  initializeNavigation();
});
function initializeNavigation() {
  const navMap = {
    dashboard: () => (window.location.href = "dashboard.html"),
    "get-patient": () => showPatient(),
    "get-doctor": () => showDoctor(),
    "get-nurse": () => showNurse(),
    settings: () => showSettingsModal(),
    "audit-logs": () => showAuditLogs(),
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
function showPatient() {
  window.location.href = "getPatient.html";
}
function showNurse() {
  window.location.href = "getNurse.html";
}
function showDoctor() {
  window.location.href = "getDoctor.html";
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

function createAdmissionsChart() {
  const ctx = document.getElementById("admissionsChart").getContext("2d");
  new Chart(ctx, {
    type: "line",
    data: {
      labels: ["Jan", "Feb", "Mar", "Apr", "May", "Jun"],
      datasets: [
        {
          label: "Patient Admissions",
          data: [65, 59, 80, 81, 56, 85],
          fill: false,
          borderColor: "#0077b6",
          tension: 0.1,
        },
      ],
    },
    options: {
      responsive: true,
      plugins: {
        legend: {
          position: "top",
        },
      },
    },
  });
}

function createDepartmentChart() {
  const ctx = document.getElementById("departmentChart").getContext("2d");
  new Chart(ctx, {
    type: "bar",
    data: {
      labels: [
        "Cardiology",
        "Neurology",
        "Pediatrics",
        "Orthopedics",
        "Oncology",
      ],
      datasets: [
        {
          label: "Patient Count",
          data: [45, 38, 52, 31, 28],
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
          position: "top",
        },
      },
    },
  });
}

function loadRecentActivities() {
  const activities = [
    {
      type: "admission",
      message: "New patient admitted to Cardiology",
      time: "5 minutes ago",
      icon: "fas fa-user-plus",
      color: "#4CAF50",
    },
    {
      type: "appointment",
      message: "Doctor scheduled 3 new appointments",
      time: "15 minutes ago",
      icon: "fas fa-calendar-check",
      color: "#2196F3",
    },
    {
      type: "lab",
      message: "Lab results updated for Patient #12345",
      time: "1 hour ago",
      icon: "fas fa-flask",
      color: "#FF9800",
    },
  ];

  const activityList = document.querySelector(".activity-list");
  activityList.innerHTML = activities
    .map(
      (activity) => `
        <div class="activity-item">
            <div class="activity-icon" style="background-color: ${activity.color}">
                <i class="${activity.icon}"></i>
            </div>
            <div class="activity-details">
                <p>${activity.message}</p>
                <small>${activity.time}</small>
            </div>
        </div>
    `
    )
    .join("");
}

function initializeEventListeners() {
  document.getElementById("logout").addEventListener("click", (e) => {
    e.preventDefault();
    sessionStorage.removeItem("currentUser");
    window.location.href = "../../index.html";
  });

  document.getElementById("departments").addEventListener("click", (e) => {
    e.preventDefault();
    showDepartmentModal();
  });
}

function showDepartmentModal() {
  const modalHTML = `
        <div class="modal" id="departmentModal">
            <div class="modal-content">
                <div class="modal-header">
                    <h2>Manage Departments</h2>
                    <span class="close-modal">&times;</span>
                </div>
                <div class="modal-body">
                    <div class="table-container">
                        <table class="table">
                            <thead>
                                <tr>
                                    <th>Department</th>
                                    <th>Head Doctor</th>
                                    <th>Staff Count</th>
                                    <th>Actions</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td>Cardiology</td>
                                    <td>Dr. Smith</td>
                                    <td>15</td>
                                    <td>
                                        <button class="btn btn-warning btn-sm">Edit</button>
                                        <button class="btn btn-danger btn-sm">Delete</button>
                                    </td>
                                </tr>
                                <!-- Add more departments here -->
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    `;

  document.body.insertAdjacentHTML("beforeend", modalHTML);
  const modal = document.getElementById("departmentModal");
  modal.style.display = "block";

  modal.querySelector(".close-modal").addEventListener("click", () => {
    modal.remove();
  });
}

function showSettingsModal() {
  const modalHTML = `
        <div class="modal" id="settingsModal">
            <div class="modal-content">
                <div class="modal-header">
                    <h2>System Settings</h2>
                    <span class="close-modal">&times;</span>
                </div>
                <div class="modal-body">
                    <form id="settingsForm">
                        <div class="form-group">
                            <label>Hospital Name</label>
                            <input type="text" class="form-control" value="City General Hospital">
                        </div>
                        <div class="form-group">
                            <label>Email Notifications</label>
                            <select class="form-control">
                                <option>Enabled</option>
                                <option>Disabled</option>
                            </select>
                        </div>
                        <div class="form-group">
                            <label>System Theme</label>
                            <select class="form-control">
                                <option>Light</option>
                                <option>Dark</option>
                            </select>
                        </div>
                        <button type="submit" class="btn btn-primary">Save Changes</button>
                    </form>
                </div>
            </div>
        </div>
    `;

  document.body.insertAdjacentHTML("beforeend", modalHTML);
  const modal = document.getElementById("settingsModal");
  modal.style.display = "block";

  modal.querySelector(".close-modal").addEventListener("click", () => {
    modal.remove();
  });
}
