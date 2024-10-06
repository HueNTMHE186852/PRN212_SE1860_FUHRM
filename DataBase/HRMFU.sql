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


-- Chèn dữ liệu vào bảng Roles
INSERT INTO Roles (RoleName) VALUES ('Admin');
INSERT INTO Roles (RoleName) VALUES ('Manager');
INSERT INTO Roles (RoleName) VALUES ('Employee');

-- Chèn dữ liệu vào bảng Accounts
INSERT INTO Accounts (Username, Password, RoleID) VALUES ('admin', 'admin123', 1);
INSERT INTO Accounts (Username, Password, RoleID) VALUES ('manager', 'manager123', 2);
INSERT INTO Accounts (Username, Password, RoleID) VALUES ('employee', 'employee123', 3);

-- Chèn dữ liệu vào bảng Departments
INSERT INTO Departments (DepartmentName) VALUES ('Human Resources');
INSERT INTO Departments (DepartmentName) VALUES ('Finance');
INSERT INTO Departments (DepartmentName) VALUES ('IT');

-- Chèn dữ liệu vào bảng Positions
INSERT INTO Positions (PositionName) VALUES ('HR Manager');
INSERT INTO Positions (PositionName) VALUES ('Finance Manager');
INSERT INTO Positions (PositionName) VALUES ('IT Specialist');

-- Chèn dữ liệu vào bảng Employees
INSERT INTO Employees (FullName, DateOfBirth, Gender, Address, PhoneNumber, DepartmentID, PositionID, AccountID, Salary, StartDate, ProfilePicture) 
VALUES ('John Doe', '1985-05-15', 'Male', '123 Main St', '123-456-7890', 1, 1, 1, 50000, '2020-01-01', 'john_doe.jpg');

INSERT INTO Employees (FullName, DateOfBirth, Gender, Address, PhoneNumber, DepartmentID, PositionID, AccountID, Salary, StartDate, ProfilePicture) 
VALUES ('Jane Smith', '1990-07-20', 'Female', '456 Elm St', '987-654-3210', 2, 2, 2, 60000, '2021-02-01', 'jane_smith.jpg');

INSERT INTO Employees (FullName, DateOfBirth, Gender, Address, PhoneNumber, DepartmentID, PositionID, AccountID, Salary, StartDate, ProfilePicture) 
VALUES ('Alice Johnson', '1992-03-10', 'Female', '789 Oak St', '555-666-7777', 3, 3, 3, 55000, '2022-03-01', 'alice_johnson.jpg');

-- Chèn dữ liệu vào bảng Salaries
INSERT INTO Salaries (EmployeeID, BaseSalary, Allowance, Bonus, Penalty, PaymentDate) 
VALUES (1, 50000, 5000, 2000, 500, '2023-01-31');

INSERT INTO Salaries (EmployeeID, BaseSalary, Allowance, Bonus, Penalty, PaymentDate) 
VALUES (2, 60000, 6000, 2500, 600, '2023-01-31');

INSERT INTO Salaries (EmployeeID, BaseSalary, Allowance, Bonus, Penalty, PaymentDate) 
VALUES (3, 55000, 5500, 2200, 550, '2023-01-31');

-- Chèn dữ liệu vào bảng Attendances
INSERT INTO Attendances (EmployeeID, Date, Status, OvertimeHours) 
VALUES (1, '2023-01-01', 'Present', 2);

INSERT INTO Attendances (EmployeeID, Date, Status, OvertimeHours) 
VALUES (2, '2023-01-01', 'Present', 1);

INSERT INTO Attendances (EmployeeID, Date, Status, OvertimeHours) 
VALUES (3, '2023-01-01', 'Absent', 0);

-- Chèn dữ liệu vào bảng Notifications
INSERT INTO Notifications (Title, Content, DepartmentID, CreatedDate) 
VALUES ('Meeting Reminder', 'Don\''t forget about the meeting tomorrow at 10 AM.', 1, '2023-01-01 09:00:00');

INSERT INTO Notifications (Title, Content, DepartmentID, CreatedDate) 
VALUES ('Policy Update', 'Please review the updated company policies.', 2, '2023-01-02 10:00:00');

INSERT INTO Notifications (Title, Content, DepartmentID, CreatedDate) 
VALUES ('System Maintenance', 'The IT system will be down for maintenance this weekend.', 3, '2023-01-03 11:00:00');

-- Chèn dữ liệu vào bảng ActivityLogs
INSERT INTO ActivityLogs (AccountID, Action, Timestamp) 
VALUES (1, 'Logged in', '2023-01-01 08:00:00');

INSERT INTO ActivityLogs (AccountID, Action, Timestamp) 
VALUES (2, 'Updated profile', '2023-01-02 09:00:00');

INSERT INTO ActivityLogs (AccountID, Action, Timestamp) 
VALUES (3, 'Logged out', '2023-01-03 10:00:00');

-- Chèn dữ liệu vào bảng LeaveRequests
INSERT INTO LeaveRequests (EmployeeID, LeaveType, StartDate, EndDate, Status) 
VALUES (1, 'Sick Leave', '2023-01-10', '2023-01-12', 'Approved');

INSERT INTO LeaveRequests (EmployeeID, LeaveType, StartDate, EndDate, Status) 
VALUES (2, 'Vacation', '2023-02-01', '2023-02-05', 'Pending');

INSERT INTO LeaveRequests (EmployeeID, LeaveType, StartDate, EndDate, Status) 
VALUES (3, 'Personal Leave', '2023-03-01', '2023-03-03', 'Rejected');

-- Chèn dữ liệu vào bảng Backups
INSERT INTO Backups (BackupDate, BackupFile) 
VALUES ('2023-01-01 00:00:00', 'backup_2023_01_01.bak');

INSERT INTO Backups (BackupDate, BackupFile) 
VALUES ('2023-02-01 00:00:00', 'backup_2023_02_01.bak');

INSERT INTO Backups (BackupDate, BackupFile) 
VALUES ('2023-03-01 00:00:00', 'backup_2023_03_01.bak');