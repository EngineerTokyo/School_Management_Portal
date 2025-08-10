# School Management System (C# WinForms / SQL Server)

Simple, step-by-step README to setup and run the **C# Windows Forms (.NET Framework 4.8)** School Management System.  
This README contains the full SQL script (create DB, tables, stored procedures, sample data) and clear instructions to run it on your SQL Server instance **Engineer-Tokyo**.

---

## Table of contents

1. Project overview  
2. Prerequisites  
3. Quick start (run SQL + run app)  
4. Full SQL script (copy-paste ready)  
5. How to run SQL script in SSMS (right-click / F5)  
6. Connection strings (Integrated and SQL Auth)  
7. Where to set connection string in the app (App.config)  
8. Important C# snippets (verify password, call stored procedures)  
9. Default sample accounts (for login/testing)  
10. How to build & run in Visual Studio (.NET 4.8)  
11. Files list (suggested)  
12. Troubleshooting FAQ (5 common problems + fixes)  
13. Notes & security tips

---

## 1. Project overview (short)
- Role-based app: Principal (admin), Teacher, Student.  
- Principal can add/edit/delete teachers and assign subjects.  
- Teacher can view assigned students, record attendance, enter marks.  
- Student can view/edit own profile and view marks & attendance.  
- Uses parameterized queries and hashed passwords. Sample DB provided.

---

## 2. Prerequisites
- Windows machine.  
- Microsoft SQL Server + SQL Server Management Studio (SSMS). Your server name: **Engineer-Tokyo**.  
- Visual Studio 2019 or 2022 with **.NET Framework 4.8** workload installed.  
- Basic familiarity with SSMS and Visual Studio.

---

## 3. Quick start (summary)
1. Open SSMS and connect to `Engineer-Tokyo`.  
2. Save the SQL below as `SchoolDB.sql` (or open a new query window).  
3. Execute the SQL (press **F5** or click **Execute**). This creates `SchoolDB` and sample data.  
4. Open the Visual Studio solution. Set the connection string to `Data Source=Engineer-Tokyo;Initial Catalog=SchoolDB;Integrated Security=True;` (or SQL auth).  
5. Build and run the WinForms app. Login with sample admin credentials.

---

## 4. FULL SQL SCRIPT (COPY-PASTE READY)
Save the following text as `SchoolDB.sql`. Then open it in SSMS (connected to `Engineer-Tokyo`) and press **F5** or click **Execute**.

```sql
--------------------------------------------------------------------------------
-- 1. Create the database if it doesn't exist
--------------------------------------------------------------------------------
IF DB_ID(N'SchoolDB') IS NULL
BEGIN
    CREATE DATABASE SchoolDB;
END
GO

USE SchoolDB;
GO

--------------------------------------------------------------------------------
-- 2. Principals table (admin accounts)
--------------------------------------------------------------------------------
CREATE TABLE Principals (
    PrincipalId INT IDENTITY(1,1) PRIMARY KEY,
    Username NVARCHAR(50) NOT NULL UNIQUE,
    FullName NVARCHAR(100) NULL,
    Email NVARCHAR(100) NULL,
    PasswordHash VARBINARY(64) NOT NULL,
    PasswordSalt VARBINARY(64) NOT NULL,
    CreatedAt DATETIME NOT NULL DEFAULT(GETDATE())
);
GO

--------------------------------------------------------------------------------
-- 3. Teachers table
--------------------------------------------------------------------------------
CREATE TABLE Teachers (
    TeacherId INT IDENTITY(1,1) PRIMARY KEY,
    Username NVARCHAR(50) NOT NULL UNIQUE,
    FullName NVARCHAR(100) NULL,
    Email NVARCHAR(100) NULL,
    Phone NVARCHAR(20) NULL,
    PasswordHash VARBINARY(64) NOT NULL,
    PasswordSalt VARBINARY(64) NOT NULL,
    CreatedAt DATETIME NOT NULL DEFAULT(GETDATE())
);
GO

--------------------------------------------------------------------------------
-- 4. Students table
--------------------------------------------------------------------------------
CREATE TABLE Students (
    StudentId INT IDENTITY(1,1) PRIMARY KEY,
    FullName NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NULL,
    Phone NVARCHAR(20) NULL,
    Address NVARCHAR(250) NULL,
    DOB DATE NULL,
    CreatedAt DATETIME NOT NULL DEFAULT(GETDATE())
);
GO

--------------------------------------------------------------------------------
-- 5. Subjects table
--------------------------------------------------------------------------------
CREATE TABLE Subjects (
    SubjectId INT IDENTITY(1,1) PRIMARY KEY,
    SubjectName NVARCHAR(100) NOT NULL UNIQUE,
    Description NVARCHAR(250) NULL
);
GO

--------------------------------------------------------------------------------
-- 6. TeacherSubjects (assignment: which teacher teaches which subject)
--------------------------------------------------------------------------------
CREATE TABLE TeacherSubjects (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    TeacherId INT NOT NULL,
    SubjectId INT NOT NULL,
    AssignedAt DATETIME NOT NULL DEFAULT(GETDATE()),
    CONSTRAINT FK_TeacherSubjects_Teacher FOREIGN KEY (TeacherId) REFERENCES Teachers(TeacherId) ON DELETE CASCADE,
    CONSTRAINT FK_TeacherSubjects_Subject FOREIGN KEY (SubjectId) REFERENCES Subjects(SubjectId) ON DELETE CASCADE,
    CONSTRAINT UQ_TeacherSubject UNIQUE (TeacherId, SubjectId)
);
GO

--------------------------------------------------------------------------------
-- 7. Attendance table
--------------------------------------------------------------------------------
CREATE TABLE Attendance (
    AttendanceId INT IDENTITY(1,1) PRIMARY KEY,
    StudentId INT NOT NULL,
    SubjectId INT NOT NULL,
    TeacherId INT NOT NULL,
    AttDate DATE NOT NULL,
    Status NVARCHAR(10) NOT NULL, -- values: 'Present' or 'Absent'
    CONSTRAINT FK_Attendance_Student FOREIGN KEY (StudentId) REFERENCES Students(StudentId) ON DELETE CASCADE,
    CONSTRAINT FK_Attendance_Subject FOREIGN KEY (SubjectId) REFERENCES Subjects(SubjectId) ON DELETE NO ACTION,
    CONSTRAINT FK_Attendance_Teacher FOREIGN KEY (TeacherId) REFERENCES Teachers(TeacherId) ON DELETE NO ACTION
);
CREATE INDEX IDX_Attendance_StudentDate ON Attendance(StudentId, AttDate);
GO

--------------------------------------------------------------------------------
-- 8. Marks table (grade computed via CASE here for convenience)
--------------------------------------------------------------------------------
CREATE TABLE Marks (
    MarkId INT IDENTITY(1,1) PRIMARY KEY,
    StudentId INT NOT NULL,
    SubjectId INT NOT NULL,
    TeacherId INT NOT NULL,
    Mark INT NOT NULL,
    MaxMark INT NOT NULL DEFAULT 100,
    ExamDate DATE NULL,
    Grade AS (
        CASE 
            WHEN Mark >= 90 THEN 'A'
            WHEN Mark >= 80 THEN 'B'
            WHEN Mark >= 70 THEN 'C'
            WHEN Mark >= 60 THEN 'D'
            ELSE 'F'
        END
    ) PERSISTED,
    CONSTRAINT FK_Marks_Student FOREIGN KEY (StudentId) REFERENCES Students(StudentId) ON DELETE CASCADE,
    CONSTRAINT FK_Marks_Subject FOREIGN KEY (SubjectId) REFERENCES Subjects(SubjectId) ON DELETE NO ACTION,
    CONSTRAINT FK_Marks_Teacher FOREIGN KEY (TeacherId) REFERENCES Teachers(TeacherId) ON DELETE NO ACTION
);
CREATE INDEX IDX_Marks_Student ON Marks(StudentId);
GO

--------------------------------------------------------------------------------
-- 9. Stored procedure: sp_AddTeacher
--------------------------------------------------------------------------------
IF OBJECT_ID('sp_AddTeacher', 'P') IS NOT NULL
    DROP PROCEDURE sp_AddTeacher;
GO

CREATE PROCEDURE sp_AddTeacher
    @Username NVARCHAR(50),
    @FullName NVARCHAR(100),
    @Email NVARCHAR(100),
    @Phone NVARCHAR(20),
    @Password NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (SELECT 1 FROM Teachers WHERE Username = @Username)
    BEGIN
        RAISERROR('Username already exists.',16,1);
        RETURN;
    END

    DECLARE @Salt VARBINARY(64) = CRYPT_GEN_RANDOM(32);
    DECLARE @PasswordBin VARBINARY(MAX) = CONVERT(VARBINARY(MAX), @Password);
    DECLARE @Hash VARBINARY(64) = HASHBYTES('SHA2_256', @Salt + @PasswordBin);

    INSERT INTO Teachers (Username, FullName, Email, Phone, PasswordHash, PasswordSalt)
    VALUES (@Username, @FullName, @Email, @Phone, @Hash, @Salt);
END
GO

--------------------------------------------------------------------------------
-- 10. Stored procedure: sp_RecordAttendance
--------------------------------------------------------------------------------
IF OBJECT_ID('sp_RecordAttendance', 'P') IS NOT NULL
    DROP PROCEDURE sp_RecordAttendance;
GO

CREATE PROCEDURE sp_RecordAttendance
    @StudentId INT,
    @SubjectId INT,
    @TeacherId INT,
    @AttDate DATE,
    @Status NVARCHAR(10)  -- 'Present' or 'Absent'
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (
        SELECT 1 FROM TeacherSubjects
        WHERE TeacherId = @TeacherId AND SubjectId = @SubjectId
    )
    BEGIN
        RAISERROR('Teacher is not assigned to this subject.',16,1);
        RETURN;
    END

    IF EXISTS (SELECT 1 FROM Attendance WHERE StudentId=@StudentId AND SubjectId=@SubjectId AND AttDate=@AttDate)
    BEGIN
        UPDATE Attendance
        SET Status = @Status, TeacherId = @TeacherId
        WHERE StudentId=@StudentId AND SubjectId=@SubjectId AND AttDate=@AttDate;
    END
    ELSE
    BEGIN
        INSERT INTO Attendance (StudentId, SubjectId, TeacherId, AttDate, Status)
        VALUES (@StudentId, @SubjectId, @TeacherId, @AttDate, @Status);
    END
END
GO

--------------------------------------------------------------------------------
-- 11. Sample data inserts (principal, 2 teachers, 3 students, 3 subjects)
--------------------------------------------------------------------------------

-- 11.1 Principal (admin)
DECLARE @p_salt VARBINARY(32) = CRYPT_GEN_RANDOM(16);
DECLARE @p_pwd NVARCHAR(100) = N'admin123'; -- sample password (change it later)
DECLARE @p_hash VARBINARY(64) = HASHBYTES('SHA2_256', @p_salt + CONVERT(VARBINARY(100), @p_pwd));
INSERT INTO Principals (Username, FullName, Email, PasswordHash, PasswordSalt)
VALUES ('admin', 'School Principal', 'principal@school.local', @p_hash, @p_salt);
GO

-- 11.2 Two teachers
DECLARE @t1_salt VARBINARY(32) = CRYPT_GEN_RANDOM(16);
DECLARE @t1_pwd NVARCHAR(100) = N'teacher123';
DECLARE @t1_hash VARBINARY(64) = HASHBYTES('SHA2_256', @t1_salt + CONVERT(VARBINARY(100), @t1_pwd));
INSERT INTO Teachers (Username, FullName, Email, Phone, PasswordHash, PasswordSalt)
VALUES ('tahira', 'Tahira Khan', 'tahira.khan@school.local', '03001234567', @t1_hash, @t1_salt);

DECLARE @t2_salt VARBINARY(32) = CRYPT_GEN_RANDOM(16);
DECLARE @t2_pwd NVARCHAR(100) = N'teacher456';
DECLARE @t2_hash VARBINARY(64) = HASHBYTES('SHA2_256', @t2_salt + CONVERT(VARBINARY(100), @t2_pwd));
INSERT INTO Teachers (Username, FullName, Email, Phone, PasswordHash, PasswordSalt)
VALUES ('ali', 'Ali Raza', 'ali.raza@school.local', '03007654321', @t2_hash, @t2_salt);
GO

-- 11.3 Three students
INSERT INTO Students (FullName, Email, Phone, Address, DOB)
VALUES
('Ahmed Ali', 'ahmed@student.local', '03001230001', 'House 1, Street A', '2008-05-10'),
('Fatima Noor', 'fatima@student.local', '03001230002', 'House 2, Street B', '2009-07-20'),
('Zain Khan', 'zain@student.local', '03001230003', 'House 3, Street C', '2007-02-14');
GO

-- 11.4 Three subjects
INSERT INTO Subjects (SubjectName, Description)
VALUES
('Mathematics', 'Mathematics basic and advanced'),
('Physics', 'Physics: Mechanics and Waves'),
('English', 'English language and literature');
GO

-- 11.5 Assign subjects to teachers
INSERT INTO TeacherSubjects (TeacherId, SubjectId)
VALUES
( (SELECT TeacherId FROM Teachers WHERE Username='tahira'), (SELECT SubjectId FROM Subjects WHERE SubjectName='Mathematics') ),
( (SELECT TeacherId FROM Teachers WHERE Username='ali'),    (SELECT SubjectId FROM Subjects WHERE SubjectName='Physics') ),
( (SELECT TeacherId FROM Teachers WHERE Username='tahira'), (SELECT SubjectId FROM Subjects WHERE SubjectName='English') );
GO

-- 11.6 Sample attendance and marks
EXEC sp_RecordAttendance 
    @StudentId = (SELECT StudentId FROM Students WHERE FullName='Ahmed Ali'),
    @SubjectId = (SELECT SubjectId FROM Subjects WHERE SubjectName='Mathematics'),
    @TeacherId = (SELECT TeacherId FROM Teachers WHERE Username='tahira'),
    @AttDate = '2025-08-01',
    @Status = 'Present';

INSERT INTO Marks (StudentId, SubjectId, TeacherId, Mark, MaxMark, ExamDate)
VALUES (
    (SELECT StudentId FROM Students WHERE FullName='Ahmed Ali'),
    (SELECT SubjectId FROM Subjects WHERE SubjectName='Mathematics'),
    (SELECT TeacherId FROM Teachers WHERE Username='tahira'),
    88, 100, '2025-06-15'
);
GO

--------------------------------------------------------------------------------
-- End of SQL script
--------------------------------------------------------------------------------
