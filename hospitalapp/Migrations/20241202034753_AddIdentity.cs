using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace hospitalapp.Migrations
{
    /// <inheritdoc />
    public partial class AddIdentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            //migrationBuilder.CreateTable(
            //    name: "BILLING",
            //    columns: table => new
            //    {
            //        BillingID = table.Column<int>(type: "int", nullable: false),
            //        TypeBill = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
            //        StatusBill = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
            //        InitialAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
            //        CoverAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
            //        DateIssued = table.Column<DateOnly>(type: "date", nullable: true),
            //        DueDate = table.Column<DateOnly>(type: "date", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK__BILLING__F1656D13D0BD0732", x => x.BillingID);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "BILLING_CALCULATION",
            //    columns: table => new
            //    {
            //        InitialAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
            //        CoverAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
            //        FinalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK__BILLING___F8197FB174D019F0", x => new { x.InitialAmount, x.CoverAmount });
            //    });

            //migrationBuilder.CreateTable(
            //    name: "DIAGNOSTIC_TEST",
            //    columns: table => new
            //    {
            //        TestID = table.Column<int>(type: "int", nullable: false),
            //        TestName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
            //        TestDescription = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
            //        TestDate = table.Column<DateOnly>(type: "date", nullable: false),
            //        TestResult = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK__DIAGNOST__8CC33100531104A7", x => x.TestID);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "EQUIPMENT",
            //    columns: table => new
            //    {
            //        EquipmentID = table.Column<int>(type: "int", nullable: false),
            //        Type = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
            //        Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
            //        Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK__EQUIPMEN__344745998B314AC3", x => x.EquipmentID);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "INSURANCE",
            //    columns: table => new
            //    {
            //        InsuranceID = table.Column<int>(type: "int", nullable: false),
            //        Provider = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
            //        PolicyNumber = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
            //        StatusInsurance = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
            //        CoverPercentage = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
            //        CoverLimit = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
            //        InsurancePriority = table.Column<int>(type: "int", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK__INSURANC__74231BC4386E2AC0", x => x.InsuranceID);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "PATIENT_RECORD",
            //    columns: table => new
            //    {
            //        RecordID = table.Column<int>(type: "int", nullable: false),
            //        FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
            //        LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
            //        Gender = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
            //        ContactInfo = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
            //        EmerContactInfo = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
            //        Address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
            //        CurrentMedication = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK__PATIENT___FBDF78C9D7BF653D", x => x.RecordID);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "SURGERY",
            //    columns: table => new
            //    {
            //        SurgeryID = table.Column<int>(type: "int", nullable: false),
            //        TypeSurgery = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
            //        Complication = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
            //        Outcome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
            //        DateSurgery = table.Column<DateOnly>(type: "date", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK__SURGERY__08AD55DD0260492A", x => x.SurgeryID);
            //    });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            //migrationBuilder.CreateTable(
            //    name: "PAYMENT",
            //    columns: table => new
            //    {
            //        ReceiptID = table.Column<int>(type: "int", nullable: false),
            //        Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
            //        Note = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
            //        Method = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
            //        DatePay = table.Column<DateOnly>(type: "date", nullable: true),
            //        BillingID = table.Column<int>(type: "int", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK__PAYMENT__CC08C400A8DE2AF1", x => x.ReceiptID);
            //        table.ForeignKey(
            //            name: "FK__PAYMENT__Billing__22751F6C",
            //            column: x => x.BillingID,
            //            principalTable: "BILLING",
            //            principalColumn: "BillingID");
            //    });

            //migrationBuilder.CreateTable(
            //    name: "USED_IN_TEST",
            //    columns: table => new
            //    {
            //        TestID = table.Column<int>(type: "int", nullable: false),
            //        EquipmentID = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK__USED_IN___0F8745592BCE5257", x => new { x.TestID, x.EquipmentID });
            //        table.ForeignKey(
            //            name: "FK__USED_IN_T__Equip__778AC167",
            //            column: x => x.EquipmentID,
            //            principalTable: "EQUIPMENT",
            //            principalColumn: "EquipmentID");
            //        table.ForeignKey(
            //            name: "FK__USED_IN_T__TestI__76969D2E",
            //            column: x => x.TestID,
            //            principalTable: "DIAGNOSTIC_TEST",
            //            principalColumn: "TestID");
            //    });

            //migrationBuilder.CreateTable(
            //    name: "COVER",
            //    columns: table => new
            //    {
            //        BillingID = table.Column<int>(type: "int", nullable: false),
            //        InsuranceID = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK__COVER__A6275CAFC971B6B7", x => new { x.BillingID, x.InsuranceID });
            //        table.ForeignKey(
            //            name: "FK__COVER__BillingID__1EA48E88",
            //            column: x => x.BillingID,
            //            principalTable: "BILLING",
            //            principalColumn: "BillingID");
            //        table.ForeignKey(
            //            name: "FK__COVER__Insurance__1F98B2C1",
            //            column: x => x.InsuranceID,
            //            principalTable: "INSURANCE",
            //            principalColumn: "InsuranceID");
            //    });

            //migrationBuilder.CreateTable(
            //    name: "ALLERGY",
            //    columns: table => new
            //    {
            //        RecordID = table.Column<int>(type: "int", nullable: false),
            //        Allergy = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK__ALLERGY__0AE675BD31AABFA7", x => new { x.RecordID, x.Allergy });
            //        table.ForeignKey(
            //            name: "FK__ALLERGY__RecordI__114A936A",
            //            column: x => x.RecordID,
            //            principalTable: "PATIENT_RECORD",
            //            principalColumn: "RecordID");
            //    });

            //migrationBuilder.CreateTable(
            //    name: "HAVE_INSURANCE",
            //    columns: table => new
            //    {
            //        InsuranceID = table.Column<int>(type: "int", nullable: false),
            //        PatientID = table.Column<int>(type: "int", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK__HAVE_INS__74231BC4B278FD52", x => x.InsuranceID);
            //        table.ForeignKey(
            //            name: "FK__HAVE_INSU__Insur__1AD3FDA4",
            //            column: x => x.InsuranceID,
            //            principalTable: "INSURANCE",
            //            principalColumn: "InsuranceID");
            //        table.ForeignKey(
            //            name: "FK__HAVE_INSU__Patie__1BC821DD",
            //            column: x => x.PatientID,
            //            principalTable: "PATIENT_RECORD",
            //            principalColumn: "RecordID");
            //    });

            //migrationBuilder.CreateTable(
            //    name: "MEDICAL_HISTORY",
            //    columns: table => new
            //    {
            //        RecordID = table.Column<int>(type: "int", nullable: false),
            //        TypeName = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
            //        Treatment = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
            //        DescriptionDetail = table.Column<string>(type: "text", nullable: true),
            //        Stage = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK__MEDICAL___7691053354195B28", x => new { x.RecordID, x.TypeName });
            //        table.ForeignKey(
            //            name: "FK__MEDICAL_H__Recor__14270015",
            //            column: x => x.RecordID,
            //            principalTable: "PATIENT_RECORD",
            //            principalColumn: "RecordID");
            //    });

            //migrationBuilder.CreateTable(
            //    name: "USED_IN_SURGERY",
            //    columns: table => new
            //    {
            //        SurgeryID = table.Column<int>(type: "int", nullable: false),
            //        EquipmentID = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK__USED_IN___8BE92184ADDBBF14", x => new { x.SurgeryID, x.EquipmentID });
            //        table.ForeignKey(
            //            name: "FK__USED_IN_S__Equip__71D1E811",
            //            column: x => x.EquipmentID,
            //            principalTable: "EQUIPMENT",
            //            principalColumn: "EquipmentID");
            //        table.ForeignKey(
            //            name: "FK__USED_IN_S__Surge__70DDC3D8",
            //            column: x => x.SurgeryID,
            //            principalTable: "SURGERY",
            //            principalColumn: "SurgeryID");
            //    });

            //migrationBuilder.CreateTable(
            //    name: "ASSIGN_BED",
            //    columns: table => new
            //    {
            //        BedNumber = table.Column<int>(type: "int", nullable: false),
            //        RoomID = table.Column<int>(type: "int", nullable: false),
            //        PatientID = table.Column<int>(type: "int", nullable: false),
            //        StartDate = table.Column<DateOnly>(type: "date", nullable: true),
            //        EndDate = table.Column<DateOnly>(type: "date", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_ASSIGN_BED_Bed_Room", x => new { x.BedNumber, x.RoomID });
            //        table.ForeignKey(
            //            name: "FK__ASSIGN_BE__Patie__0D7A0286",
            //            column: x => x.PatientID,
            //            principalTable: "PATIENT_RECORD",
            //            principalColumn: "RecordID");
            //    });

            //migrationBuilder.CreateTable(
            //    name: "ASSIGN_DOCTOR",
            //    columns: table => new
            //    {
            //        RecordID = table.Column<int>(type: "int", nullable: false),
            //        DoctorID = table.Column<int>(type: "int", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK__ASSIGN_D__FBDF78C9C6C560E8", x => x.RecordID);
            //        table.ForeignKey(
            //            name: "FK__ASSIGN_DO__Recor__01142BA1",
            //            column: x => x.RecordID,
            //            principalTable: "PATIENT_RECORD",
            //            principalColumn: "RecordID");
            //    });

            //migrationBuilder.CreateTable(
            //    name: "ASSIGN_NURSE",
            //    columns: table => new
            //    {
            //        RecordID = table.Column<int>(type: "int", nullable: false),
            //        NurseID = table.Column<int>(type: "int", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK__ASSIGN_N__FBDF78C9270F944F", x => x.RecordID);
            //        table.ForeignKey(
            //            name: "FK__ASSIGN_NU__Recor__7D439ABD",
            //            column: x => x.RecordID,
            //            principalTable: "PATIENT_RECORD",
            //            principalColumn: "RecordID");
            //    });

            //migrationBuilder.CreateTable(
            //    name: "BED",
            //    columns: table => new
            //    {
            //        BedNumber = table.Column<int>(type: "int", nullable: false),
            //        RoomID = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK__BED__BFABAEC5589D7873", x => new { x.BedNumber, x.RoomID });
            //    });

            //migrationBuilder.CreateTable(
            //    name: "DEPARTMENT",
            //    columns: table => new
            //    {
            //        DepartmentNumber = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        ManageID = table.Column<int>(type: "int", nullable: true),
            //        Name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
            //        Location = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK__DEPARTME__718447F81FD8E035", x => x.DepartmentNumber);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "EMPLOYEE",
            //    columns: table => new
            //    {
            //        EmployeeID = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
            //        Dob = table.Column<DateOnly>(type: "date", nullable: false),
            //        Gender = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true),
            //        PhoneNumber = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: false),
            //        Job_type = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
            //        Salary = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
            //        startDate = table.Column<DateOnly>(type: "date", nullable: false),
            //        Experience = table.Column<int>(type: "int", nullable: true),
            //        Degree = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
            //        DepartmentID = table.Column<int>(type: "int", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK__EMPLOYEE__7AD04FF192D17CCB", x => x.EmployeeID);
            //        table.ForeignKey(
            //            name: "FK_Employee_Department",
            //            column: x => x.DepartmentID,
            //            principalTable: "DEPARTMENT",
            //            principalColumn: "DepartmentNumber");
            //    });

            //migrationBuilder.CreateTable(
            //    name: "ROOM",
            //    columns: table => new
            //    {
            //        RoomID = table.Column<int>(type: "int", nullable: false),
            //        Dno = table.Column<int>(type: "int", nullable: true),
            //        TypeRoom = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
            //        StatusRoom = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK__ROOM__32863919548AC209", x => x.RoomID);
            //        table.ForeignKey(
            //            name: "FK_Room_Department",
            //            column: x => x.Dno,
            //            principalTable: "DEPARTMENT",
            //            principalColumn: "DepartmentNumber");
            //    });

            //migrationBuilder.CreateTable(
            //    name: "DOCTOR",
            //    columns: table => new
            //    {
            //        DoctorID = table.Column<int>(type: "int", nullable: false),
            //        Specialty = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
            //        Certifica = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK__DOCTOR__2DC00EDF45866AF2", x => x.DoctorID);
            //        table.ForeignKey(
            //            name: "FK_Doctor_Employee",
            //            column: x => x.DoctorID,
            //            principalTable: "EMPLOYEE",
            //            principalColumn: "EmployeeID");
            //    });

            //migrationBuilder.CreateTable(
            //    name: "NURSE",
            //    columns: table => new
            //    {
            //        NurseID = table.Column<int>(type: "int", nullable: false),
            //        Specialty = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK__NURSE__4384786957658202", x => x.NurseID);
            //        table.ForeignKey(
            //            name: "FK_Nurse_Employee",
            //            column: x => x.NurseID,
            //            principalTable: "EMPLOYEE",
            //            principalColumn: "EmployeeID");
            //    });

            //migrationBuilder.CreateTable(
            //    name: "OTHER_EMPLOYEE",
            //    columns: table => new
            //    {
            //        EmployeeID = table.Column<int>(type: "int", nullable: false),
            //        Specialty = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK__OTHER_EM__7AD04FF189F72C32", x => x.EmployeeID);
            //        table.ForeignKey(
            //            name: "FK__OTHER_EMP__Emplo__25518C17",
            //            column: x => x.EmployeeID,
            //            principalTable: "EMPLOYEE",
            //            principalColumn: "EmployeeID");
            //    });

            //migrationBuilder.CreateTable(
            //    name: "TECHNICIAN",
            //    columns: table => new
            //    {
            //        EmployeeID = table.Column<int>(type: "int", nullable: false),
            //        Specialty = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK__TECHNICI__7AD04FF1C4BB4C20", x => x.EmployeeID);
            //        table.ForeignKey(
            //            name: "FK_Technician_Employee",
            //            column: x => x.EmployeeID,
            //            principalTable: "EMPLOYEE",
            //            principalColumn: "EmployeeID");
            //    });

            //migrationBuilder.CreateTable(
            //    name: "PERFORM_SURGERY",
            //    columns: table => new
            //    {
            //        RecordID = table.Column<int>(type: "int", nullable: false),
            //        SurgeryID = table.Column<int>(type: "int", nullable: false),
            //        DoctorID = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK__PERFORM___E5786D9A2C72412A", x => new { x.RecordID, x.SurgeryID, x.DoctorID });
            //        table.ForeignKey(
            //            name: "FK__PERFORM_S__Docto__05D8E0BE",
            //            column: x => x.DoctorID,
            //            principalTable: "DOCTOR",
            //            principalColumn: "DoctorID");
            //        table.ForeignKey(
            //            name: "FK__PERFORM_S__Recor__03F0984C",
            //            column: x => x.RecordID,
            //            principalTable: "PATIENT_RECORD",
            //            principalColumn: "RecordID");
            //        table.ForeignKey(
            //            name: "FK__PERFORM_S__Surge__04E4BC85",
            //            column: x => x.SurgeryID,
            //            principalTable: "SURGERY",
            //            principalColumn: "SurgeryID");
            //    });

            //migrationBuilder.CreateTable(
            //    name: "PERFORM_TEST",
            //    columns: table => new
            //    {
            //        RecordID = table.Column<int>(type: "int", nullable: false),
            //        TestID = table.Column<int>(type: "int", nullable: false),
            //        NurseID = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK__PERFORM___8B50CFA17720B3A3", x => new { x.RecordID, x.TestID, x.NurseID });
            //        table.ForeignKey(
            //            name: "FK__PERFORM_T__Nurse__0A9D95DB",
            //            column: x => x.NurseID,
            //            principalTable: "NURSE",
            //            principalColumn: "NurseID");
            //        table.ForeignKey(
            //            name: "FK__PERFORM_T__Recor__08B54D69",
            //            column: x => x.RecordID,
            //            principalTable: "PATIENT_RECORD",
            //            principalColumn: "RecordID");
            //        table.ForeignKey(
            //            name: "FK__PERFORM_T__TestI__09A971A2",
            //            column: x => x.TestID,
            //            principalTable: "DIAGNOSTIC_TEST",
            //            principalColumn: "TestID");
            //    });

            //migrationBuilder.CreateTable(
            //    name: "MAINTAIN",
            //    columns: table => new
            //    {
            //        TechID = table.Column<int>(type: "int", nullable: false),
            //        EquipID = table.Column<int>(type: "int", nullable: false),
            //        TypeMaintain = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
            //        DateMaintain = table.Column<DateOnly>(type: "date", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK__MAINTAIN__0FF29AAEC6803016", x => new { x.TechID, x.EquipID });
            //        table.ForeignKey(
            //            name: "FK__MAINTAIN__EquipI__6E01572D",
            //            column: x => x.EquipID,
            //            principalTable: "EQUIPMENT",
            //            principalColumn: "EquipmentID");
            //        table.ForeignKey(
            //            name: "FK__MAINTAIN__TechID__6D0D32F4",
            //            column: x => x.TechID,
            //            principalTable: "TECHNICIAN",
            //            principalColumn: "EmployeeID");
            //    });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            //migrationBuilder.CreateIndex(
            //    name: "IX_ASSIGN_BED_PatientID",
            //    table: "ASSIGN_BED",
            //    column: "PatientID");

            //migrationBuilder.CreateIndex(
            //    name: "IX_ASSIGN_DOCTOR_DoctorID",
            //    table: "ASSIGN_DOCTOR",
            //    column: "DoctorID");

            //migrationBuilder.CreateIndex(
            //    name: "IX_ASSIGN_NURSE_NurseID",
            //    table: "ASSIGN_NURSE",
            //    column: "NurseID");

            //migrationBuilder.CreateIndex(
            //    name: "IX_BED_RoomID",
            //    table: "BED",
            //    column: "RoomID");

            //migrationBuilder.CreateIndex(
            //    name: "IX_COVER_InsuranceID",
            //    table: "COVER",
            //    column: "InsuranceID");

            //migrationBuilder.CreateIndex(
            //    name: "IX_DEPARTMENT_ManageID",
            //    table: "DEPARTMENT",
            //    column: "ManageID");

            //migrationBuilder.CreateIndex(
            //    name: "IX_EMPLOYEE_DepartmentID",
            //    table: "EMPLOYEE",
            //    column: "DepartmentID");

            //migrationBuilder.CreateIndex(
            //    name: "UQ__EMPLOYEE__85FB4E38F160F089",
            //    table: "EMPLOYEE",
            //    column: "PhoneNumber",
            //    unique: true);

            //migrationBuilder.CreateIndex(
            //    name: "IX_HAVE_INSURANCE_PatientID",
            //    table: "HAVE_INSURANCE",
            //    column: "PatientID");

            //migrationBuilder.CreateIndex(
            //    name: "IX_MAINTAIN_EquipID",
            //    table: "MAINTAIN",
            //    column: "EquipID");

            //migrationBuilder.CreateIndex(
            //    name: "IX_PAYMENT_BillingID",
            //    table: "PAYMENT",
            //    column: "BillingID");

            //migrationBuilder.CreateIndex(
            //    name: "IX_PERFORM_SURGERY_DoctorID",
            //    table: "PERFORM_SURGERY",
            //    column: "DoctorID");

            //migrationBuilder.CreateIndex(
            //    name: "IX_PERFORM_SURGERY_SurgeryID",
            //    table: "PERFORM_SURGERY",
            //    column: "SurgeryID");

            //migrationBuilder.CreateIndex(
            //    name: "IX_PERFORM_TEST_NurseID",
            //    table: "PERFORM_TEST",
            //    column: "NurseID");

            //migrationBuilder.CreateIndex(
            //    name: "IX_PERFORM_TEST_TestID",
            //    table: "PERFORM_TEST",
            //    column: "TestID");

            //migrationBuilder.CreateIndex(
            //    name: "IX_ROOM_Dno",
            //    table: "ROOM",
            //    column: "Dno");

            //migrationBuilder.CreateIndex(
            //    name: "IX_USED_IN_SURGERY_EquipmentID",
            //    table: "USED_IN_SURGERY",
            //    column: "EquipmentID");

            //migrationBuilder.CreateIndex(
            //    name: "IX_USED_IN_TEST_EquipmentID",
            //    table: "USED_IN_TEST",
            //    column: "EquipmentID");

            //migrationBuilder.AddForeignKey(
            //    name: "FK__ASSIGN_BED__0E6E26BF",
            //    table: "ASSIGN_BED",
            //    columns: new[] { "BedNumber", "RoomID" },
            //    principalTable: "BED",
            //    principalColumns: new[] { "BedNumber", "RoomID" });

            //migrationBuilder.AddForeignKey(
            //    name: "FK__ASSIGN_DO__Docto__00200768",
            //    table: "ASSIGN_DOCTOR",
            //    column: "DoctorID",
            //    principalTable: "DOCTOR",
            //    principalColumn: "DoctorID");

            //migrationBuilder.AddForeignKey(
            //    name: "FK__ASSIGN_NU__Nurse__7C4F7684",
            //    table: "ASSIGN_NURSE",
            //    column: "NurseID",
            //    principalTable: "NURSE",
                //principalColumn: "NurseID");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Bed_Room",
            //    table: "BED",
            //    column: "RoomID",
            //    principalTable: "ROOM",
            //    principalColumn: "RoomID",
            //    onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Department_ManageID",
            //    table: "DEPARTMENT",
            //    column: "ManageID",
            //    principalTable: "EMPLOYEE",
            //    principalColumn: "EmployeeID",
            //    onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_Department_ManageID",
            //    table: "DEPARTMENT");

            //migrationBuilder.DropTable(
            //    name: "ALLERGY");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            //migrationBuilder.DropTable(
            //    name: "ASSIGN_BED");

            //migrationBuilder.DropTable(
            //    name: "ASSIGN_DOCTOR");

            //migrationBuilder.DropTable(
            //    name: "ASSIGN_NURSE");

            //migrationBuilder.DropTable(
            //    name: "BILLING_CALCULATION");

            //migrationBuilder.DropTable(
            //    name: "COVER");

            //migrationBuilder.DropTable(
            //    name: "HAVE_INSURANCE");

            //migrationBuilder.DropTable(
            //    name: "MAINTAIN");

            //migrationBuilder.DropTable(
            //    name: "MEDICAL_HISTORY");

            //migrationBuilder.DropTable(
            //    name: "OTHER_EMPLOYEE");

            //migrationBuilder.DropTable(
            //    name: "PAYMENT");

            //migrationBuilder.DropTable(
            //    name: "PERFORM_SURGERY");

            //migrationBuilder.DropTable(
            //    name: "PERFORM_TEST");

            //migrationBuilder.DropTable(
            //    name: "USED_IN_SURGERY");

            //migrationBuilder.DropTable(
            //    name: "USED_IN_TEST");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            //migrationBuilder.DropTable(
            //    name: "BED");

            //migrationBuilder.DropTable(
            //    name: "INSURANCE");

            //migrationBuilder.DropTable(
            //    name: "TECHNICIAN");

            //migrationBuilder.DropTable(
            //    name: "BILLING");

            //migrationBuilder.DropTable(
            //    name: "DOCTOR");

            //migrationBuilder.DropTable(
            //    name: "NURSE");

            //migrationBuilder.DropTable(
            //    name: "PATIENT_RECORD");

            //migrationBuilder.DropTable(
            //    name: "SURGERY");

            //migrationBuilder.DropTable(
            //    name: "EQUIPMENT");

            //migrationBuilder.DropTable(
            //    name: "DIAGNOSTIC_TEST");

            //migrationBuilder.DropTable(
            //    name: "ROOM");

            //migrationBuilder.DropTable(
            //    name: "EMPLOYEE");

            //migrationBuilder.DropTable(
            //    name: "DEPARTMENT");
        }
    }
}
