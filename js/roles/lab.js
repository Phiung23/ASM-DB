document.addEventListener('DOMContentLoaded', function() {
    initializeDashboard();
    loadPendingTests();
    createTestVolumeChart();
    createTestTypesChart();
    loadRecentResults();
    loadInventoryAlerts();
    initializeEventListeners();
    initializeNavigation();
});
function initializeNavigation() {
    const navMap = {
        'dashboard': () => window.location.href = 'dashboard.html',
        'pending-tests': () => showPendingTests(),
        'test-results': () => showTestResults(),
        'sample-management': () => showSampleManagement(),
        'inventory': () => showInventory(),
        'quality-control': () => showQualityControl(),
        'reports': () => showReports(),
        'logout': () => handleLogout()
    };

    document.querySelectorAll('.sidebar-menu a').forEach(link => {
        link.addEventListener('click', (e) => {
            e.preventDefault();
            const id = link.id;
            if (navMap[id]) navMap[id]();
        });
    });
}
function handleLogout() {
    sessionStorage.removeItem('currentUser');
    window.location.href = '../../index.html';
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
    
    document.body.insertAdjacentHTML('beforeend', modalHtml);
    const modal = document.getElementById(modalId);
    
    modal.querySelector('.close-modal').addEventListener('click', () => {
        modal.remove();
    });
    
    return modal;
}



function initializeDashboard() {
    const sidebarToggle = document.getElementById('sidebar-toggle');
    const sidebar = document.querySelector('.sidebar');
    
    sidebarToggle.addEventListener('click', () => {
        sidebar.classList.toggle('active');
    });
}

function loadPendingTests() {
    const pendingTests = [
        {
            sampleId: 'LAB-2024-001',
            patient: 'A',
            testType: 'Blood Test',
            priority: 'Urgent',
            requestedBy: 'B',
            status: 'Pending'
        },
        {
            sampleId: 'LAB-2024-002',
            patient: 'C',
            testType: 'Urine Analysis',
            priority: 'Routine',
            requestedBy: 'D',
            status: 'Processing'
        },
        {
            sampleId: 'LAB-2024-003',
            patient: 'E',
            testType: 'COVID-19',
            priority: 'Emergency',
            requestedBy: 'F',
            status: 'Pending'
        }
    ];

    const pendingTestsList = document.getElementById('pendingTestsList');
    pendingTestsList.innerHTML = pendingTests.map(test => `
        <tr>
            <td>${test.sampleId}</td>
            <td>${test.patient}</td>
            <td>${test.testType}</td>
            <td>
                <span class="badge badge-${getPriorityColor(test.priority)}">
                    ${test.priority}
                </span>
            </td>
            <td>${test.requestedBy}</td>
            <td>
                <span class="badge badge-${getStatusColor(test.status)}">
                    ${test.status}
                </span>
            </td>
            <td>
                <button class="btn btn-sm btn-primary" onclick="processTest('${test.sampleId}')">
                    Process
                </button>
                <button class="btn btn-sm btn-success" onclick="enterResults('${test.sampleId}')">
                    Results
                </button>
                <button class="btn btn-sm btn-warning" onclick="printLabel('${test.sampleId}')">
                    Label
                </button>
            </td>
        </tr>
    `).join('');
}

function createTestVolumeChart() {
    const ctx = document.getElementById('testVolumeChart').getContext('2d');
    new Chart(ctx, {
        type: 'line',
        data: {
            labels: ['Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat', 'Sun'],
            datasets: [{
                label: 'Tests Processed',
                data: [25, 30, 28, 32, 26, 20, 15],
                borderColor: '#4CAF50',
                tension: 0.4,
                fill: false
            }]
        },
        options: {
            responsive: true,
            plugins: {
                legend: {
                    position: 'top',
                }
            },
            scales: {
                y: {
                    beginAtZero: true
                }
            }
        }
    });
}

function createTestTypesChart() {
    const ctx = document.getElementById('testTypesChart').getContext('2d');
    new Chart(ctx, {
        type: 'doughnut',
        data: {
            labels: ['Blood Tests', 'Urine Analysis', 'COVID-19', 'Culture Tests', 'Other'],
            datasets: [{
                data: [35, 25, 15, 15, 10],
                backgroundColor: [
                    '#4CAF50',
                    '#2196F3',
                    '#FF9800',
                    '#E91E63',
                    '#9C27B0'
                ]
            }]
        },
        options: {
            responsive: true,
            plugins: {
                legend: {
                    position: 'right'
                }
            }
        }
    });
}

function loadRecentResults() {
    const results = [
        {
            sampleId: 'LAB-2024-001',
            patient: 'A',
            testType: 'Blood Test',
            result: 'Normal',
            timestamp: '2024-03-15 10:30 AM'
        },
        {
            sampleId: 'LAB-2024-002',
            patient: 'B',
            testType: 'Urine Analysis',
            result: 'Abnormal',
            timestamp: '2024-03-15 11:15 AM'
        }
    ];

    const recentResults = document.getElementById('recentResults');
    recentResults.innerHTML = results.map(result => `
        <div class="result-card">
            <div class="result-header">
                <h4>${result.sampleId} - ${result.patient}</h4>
                <span class="badge badge-${result.result.toLowerCase() === 'normal' ? 'success' : 'danger'}">
                    ${result.result}
                </span>
            </div>
            <div class="result-body">
                <p><strong>Test Type:</strong> ${result.testType}</p>
                <p><strong>Completed:</strong> ${result.timestamp}</p>
            </div>
            <div class="result-actions">
                <button class="btn btn-sm btn-primary" onclick="viewDetails('${result.sampleId}')">
                    View Details
                </button>
                <button class="btn btn-sm btn-success" onclick="printReport('${result.sampleId}')">
                    Print Report
                </button>
            </div>
        </div>
    `).join('');
}

function loadInventoryAlerts() {
    const alerts = [
        {
            item: 'Test Tubes',
            status: 'Low Stock',
            quantity: '50 units remaining'
        },
        {
            item: 'Reagent A',
            status: 'Critical',
            quantity: '10 units remaining'
        }
    ];

    const inventoryAlerts = document.getElementById('inventoryAlerts');
    inventoryAlerts.innerHTML = alerts.map(alert => `
        <div class="alert alert-${alert.status.toLowerCase() === 'critical' ? 'danger' : 'warning'}">
            <div class="alert-header">
                <strong>${alert.item}</strong>
                <span class="badge badge-${alert.status.toLowerCase() === 'critical' ? 'danger' : 'warning'}">
                    ${alert.status}
                </span>
            </div>
            <div class="alert-body">
                ${alert.quantity}
            </div>
            <div class="alert-actions">
                <button class="btn btn-sm btn-primary" onclick="reorderItem('${alert.item}')">
                    Reorder
                </button>
            </div>
        </div>
    `).join('');
}

function registerNewSample() {
    const template = document.getElementById('sampleRegistrationTemplate');
    const modalClone = template.content.cloneNode(true);
    document.body.appendChild(modalClone);

    const modal = document.getElementById('sampleRegistrationModal');
    const form = document.getElementById('sampleRegistrationForm');

    form.addEventListener('submit', (e) => {
        e.preventDefault();
        alert('Sample registered successfully!');
        modal.remove();
        loadPendingTests();     });

    modal.querySelector('.close-modal').addEventListener('click', () => {
        modal.remove();
    });
}

function enterTestResults() {
        alert('Opening test results entry form...');
}

function printLabels() {
        alert('Preparing to print labels...');
}

function processTest(sampleId) {
        alert(`Processing test for sample ${sampleId}`);
}

function enterResults(sampleId) {
        alert(`Entering results for sample ${sampleId}`);
}

function printLabel(sampleId) {
        alert(`Printing label for sample ${sampleId}`);
}

function viewDetails(sampleId) {
        alert(`Viewing details for sample ${sampleId}`);
}

function printReport(sampleId) {
        alert(`Printing report for sample ${sampleId}`);
}

function reorderItem(item) {
        alert(`Placing reorder for ${item}`);
}

function getPriorityColor(priority) {
    switch(priority.toLowerCase()) {
        case 'emergency': return 'danger';
        case 'urgent': return 'warning';
        default: return 'primary';
    }
}

function getStatusColor(status) {
    switch(status.toLowerCase()) {
        case 'pending': return 'warning';
        case 'processing': return 'primary';
        case 'completed': return 'success';
        default: return 'secondary';
    }
}

function initializeEventListeners() {
        document.getElementById('logout').addEventListener('click', (e) => {
        e.preventDefault();
        sessionStorage.removeItem('currentUser');
        window.location.href = '../../index.html';
    });

        const navItems = [
        'pending-tests',
        'test-results',
        'sample-management',
        'inventory',
        'quality-control',
        'reports'
    ];

    navItems.forEach(item => {
        document.getElementById(item)?.addEventListener('click', (e) => {
            e.preventDefault();
                        alert(`Navigating to ${item.replace('-', ' ')}...`);
        });
    });
} 


function showPendingTests() {
    const content = {
        title: 'Pending Tests',
        body: `
            <div class="table-container">
                <table class="table">
                    <thead>
                        <tr>
                            <th>Sample ID</th>
                            <th>Patient</th>
                            <th>Test Type</th>
                            <th>Priority</th>
                            <th>Actions</th>
                        </tr>
                    </thead>
                    <tbody id="pendingTestsTable">
                        <!-- Will be populated dynamically -->
                    </tbody>
                </table>
            </div>
        `
    };
    
    const modal = showModal('pendingTestsModal', content);
    loadPendingTestsData(modal.querySelector('#pendingTestsTable'));
}

function showTestResults() {
    const content = {
        title: 'Test Results',
        body: `
            <div class="results-container">
                <!-- Add your test results content here -->
                <div class="search-box">
                    <input type="text" placeholder="Search results...">
                </div>
                <div id="testResultsList">
                    <!-- Will be populated dynamically -->
                </div>
            </div>
        `
    };
    
    const modal = showModal('testResultsModal', content);
    loadTestResultsData(modal.querySelector('#testResultsList'));
}
