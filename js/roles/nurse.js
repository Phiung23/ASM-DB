document.addEventListener("DOMContentLoaded", function () {
  initializeDashboard();
  loadCurrentPatients();
  loadTaskTimeline();
  loadCriticalAlerts();
  initializeEventListeners();
  initializeNavigation();
});
function initializeNavigation() {
  const navMap = {
    dashboard: () => (window.location.href = "dashboard.html"),
    "patient-list": () => showPatientList(),
    "recent-tests": () => showRecentTests(),
    medications: () => showMedications(),
    tasks: () => showTasks(),
    reports: () => showReports(),
    handover: () => showHandover(),
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

function showRecentTests() {
  window.location.href = "/pages/nurse/showRecentTests.html";
}
function showPatientList() {
  window.location.href = "/pages/nurse/showPatientList.html";
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

function initializeDashboard() {
  const sidebarToggle = document.getElementById("sidebar-toggle");
  const sidebar = document.querySelector(".sidebar");

  sidebarToggle.addEventListener("click", () => {
    sidebar.classList.toggle("active");
  });
}

function loadCurrentPatients() {
  const patients = [
    {
      room: "101",
      name: "A",
      status: "Stable",
      lastVitals: "10:30 AM",
      nextMed: "02:00 PM",
    },
    {
      room: "102",
      name: "B",
      status: "Critical",
      lastVitals: "11:15 AM",
      nextMed: "01:30 PM",
    },
    {
      room: "103",
      name: "C",
      status: "Stable",
      lastVitals: "09:45 AM",
      nextMed: "03:00 PM",
    },
  ];

  const patientsList = document.getElementById("currentPatientsList");
  patientsList.innerHTML = patients
    .map(
      (patient) => `
        <tr>
            <td>${patient.room}</td>
            <td>${patient.name}</td>
            <td>
                <span class="badge badge-${
                  patient.status.toLowerCase() === "stable"
                    ? "success"
                    : "danger"
                }">
                    ${patient.status}
                </span>
            </td>
            <td>${patient.lastVitals}</td>
            <td>${patient.nextMed}</td>
            <td>
                <button class="btn btn-sm btn-primary" onclick="recordVitals('${
                  patient.room
                }')">
                    Vitals
                </button>
                <button class="btn btn-sm btn-success" onclick="giveMedication('${
                  patient.room
                }')">
                    Medication
                </button>
                <button class="btn btn-sm btn-warning" onclick="viewDetails('${
                  patient.room
                }')">
                    Details
                </button>
            </td>
        </tr>
    `
    )
    .join("");
}

function loadTaskTimeline() {
  const tasks = [
    {
      time: "08:00 AM",
      task: "Shift Handover",
      status: "Completed",
    },
    {
      time: "09:00 AM",
      task: "Morning Medications",
      status: "Completed",
    },
    {
      time: "10:00 AM",
      task: "Vital Signs Check - Room 101, 102",
      status: "Completed",
    },
    {
      time: "02:00 PM",
      task: "Afternoon Medications",
      status: "Pending",
    },
    {
      time: "03:00 PM",
      task: "Doctor Rounds",
      status: "Pending",
    },
  ];

  const timeline = document.getElementById("taskTimeline");
  timeline.innerHTML = tasks
    .map(
      (task) => `
        <div class="timeline-item">
            <div class="timeline-time">${task.time}</div>
            <div class="timeline-content">
                <h4>${task.task}</h4>
                <span class="badge badge-${
                  task.status.toLowerCase() === "completed"
                    ? "success"
                    : "warning"
                }">
                    ${task.status}
                </span>
            </div>
        </div>
    `
    )
    .join("");
}

function loadCriticalAlerts() {
  const alerts = [
    {
      patient: "B",
      room: "102",
      alert: "High Blood Pressure",
      time: "11:15 AM",
    },
    {
      patient: "C",
      room: "103",
      alert: "Pain Level > 7",
      time: "11:30 AM",
    },
  ];

  const alertsContainer = document.getElementById("criticalAlerts");
  alertsContainer.innerHTML = alerts
    .map(
      (alert) => `
        <div class="alert alert-danger">
            <div class="alert-header">
                <strong>Room ${alert.room} - ${alert.patient}</strong>
                <small>${alert.time}</small>
            </div>
            <div class="alert-body">
                ${alert.alert}
            </div>
            <div class="alert-actions">
                <button class="btn btn-sm btn-primary" onclick="respondToAlert('${alert.room}')">
                    Respond
                </button>
            </div>
        </div>
    `
    )
    .join("");
}

function addPatientVitals() {
  const template = document.getElementById("vitalsModalTemplate");
  const modalClone = template.content.cloneNode(true);
  document.body.appendChild(modalClone);
  const modal = document.getElementById("vitalsModal");
  const form = document.getElementById("vitalsForm");
  const patientSelect = form.querySelector("select");
  const patients = [
    { room: "101", name: "A" },
    { room: "102", name: "B" },
    { room: "103", name: "C" },
  ];

  patientSelect.innerHTML += patients
    .map(
      (patient) =>
        `<option value="${patient.room}">Room ${patient.room} - ${patient.name}</option>`
    )
    .join("");

  form.addEventListener("submit", (e) => {
    e.preventDefault();
    alert("Vital signs recorded successfully!");
    modal.remove();
  });

  modal.querySelector(".close-modal").addEventListener("click", () => {
    modal.remove();
  });
}

function medicationSchedule() {
  alert("Opening medication schedule...");
}

function emergencyAlert() {
  if (confirm("Are you sure you want to trigger an emergency alert?")) {
    alert(
      "Emergency alert has been triggered. Response team has been notified."
    );
  }
}

function recordVitals(room) {
  addPatientVitals();
  const patientSelect = document.querySelector("#vitalsForm select");
  patientSelect.value = room;
}

function giveMedication(room) {
  alert(`Administering medication for Room ${room}`);
}

function viewDetails(room) {
  alert(`Viewing details for Room ${room}`);
}

function respondToAlert(room) {
  alert(`Responding to alert for Room ${room}`);
}

function initializeEventListeners() {
  document.getElementById("logout").addEventListener("click", (e) => {
    e.preventDefault();
    sessionStorage.removeItem("currentUser");
    window.location.href = "../../index.html";
  });

  document.getElementById("patient-list").addEventListener("click", (e) => {
    e.preventDefault();
  });

  document.getElementById("medications").addEventListener("click", (e) => {
    e.preventDefault();
  });

  document.getElementById("tasks").addEventListener("click", (e) => {
    e.preventDefault();
  });

  document.getElementById("reports").addEventListener("click", (e) => {
    e.preventDefault();
  });

  document.getElementById("handover").addEventListener("click", (e) => {
    e.preventDefault();
  });
}
