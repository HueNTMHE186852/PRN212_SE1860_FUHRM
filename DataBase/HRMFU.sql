--CREATE DATABASE FUHRM;

USE FUHRM;

CREATE TABLE Roles (
    RoleID INT PRIMARY KEY IDENTITY(1,1),
    RoleName NVARCHAR(MAX) NOT NULL
);

CREATE TABLE Accounts (
    AccountID INT PRIMARY KEY IDENTITY(1,1),
    Username NVARCHAR(255) NOT NULL UNIQUE,
    Password NVARCHAR(MAX) NOT NULL,
    RoleID INT FOREIGN KEY REFERENCES Roles(RoleID)
);

CREATE TABLE Departments (
    DepartmentID INT PRIMARY KEY IDENTITY(1,1),
    DepartmentName NVARCHAR(MAX) NOT NULL
);

-- Tạo bảng Positions
CREATE TABLE Positions (
    PositionID INT PRIMARY KEY IDENTITY(1,1),
    PositionName NVARCHAR(MAX) NOT NULL
);

-- Cập nhật bảng Employees
CREATE TABLE Employees (
    EmployeeID INT PRIMARY KEY IDENTITY(1,1),
    FullName NVARCHAR(MAX) NOT NULL,
    DateOfBirth DATE NOT NULL,
    Gender NVARCHAR(MAX) NOT NULL,
    Address NVARCHAR(MAX) NOT NULL,
    PhoneNumber NVARCHAR(MAX) NOT NULL,
    DepartmentID INT FOREIGN KEY REFERENCES Departments(DepartmentID),
    PositionID INT FOREIGN KEY REFERENCES Positions(PositionID),
    AccountID INT FOREIGN KEY REFERENCES Accounts(AccountID),
    Salary FLOAT NOT NULL,
    StartDate DATE NOT NULL,
    ProfilePicture NVARCHAR(MAX)
);

CREATE TABLE Salaries (
    SalaryID INT PRIMARY KEY IDENTITY(1,1),
    EmployeeID INT FOREIGN KEY REFERENCES Employees(EmployeeID),
    BaseSalary FLOAT NOT NULL,
    Allowance FLOAT,
    Bonus FLOAT,
    Penalty FLOAT,
    TotalIncome AS (BaseSalary + Allowance + Bonus - Penalty),
    PaymentDate DATE NOT NULL
);

CREATE TABLE Attendances (
    AttendanceID INT PRIMARY KEY IDENTITY(1,1),
    EmployeeID INT FOREIGN KEY REFERENCES Employees(EmployeeID),
    Date DATE NOT NULL,
    Status NVARCHAR(MAX) NOT NULL,
    OvertimeHours INT
);

CREATE TABLE Notifications (
    NotificationID INT PRIMARY KEY IDENTITY(1,1),
    Title NVARCHAR(MAX) NOT NULL,
    Content NVARCHAR(MAX) NOT NULL,
    DepartmentID INT FOREIGN KEY REFERENCES Departments(DepartmentID),
    CreatedDate DATETIME NOT NULL
);

CREATE TABLE ActivityLogs (
    ActivityLogID INT PRIMARY KEY IDENTITY(1,1),
    AccountID INT FOREIGN KEY REFERENCES Accounts(AccountID),
    Action NVARCHAR(MAX) NOT NULL,
    Timestamp DATETIME NOT NULL
);

CREATE TABLE LeaveRequests (
    LeaveRequestID INT PRIMARY KEY IDENTITY(1,1),
    EmployeeID INT FOREIGN KEY REFERENCES Employees(EmployeeID),
    LeaveType NVARCHAR(MAX) NOT NULL,
    StartDate DATE NOT NULL,
    EndDate DATE NOT NULL,
    Status NVARCHAR(MAX) NOT NULL
);

CREATE TABLE Backups (
    BackupID INT PRIMARY KEY IDENTITY(1,1),
    BackupDate DATETIME NOT NULL,
    BackupFile NVARCHAR(MAX) NOT NULL
);