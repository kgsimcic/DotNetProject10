#!/bin/bash
set -e

/opt/mssql/bin/sqlservr &
SQL_PID=$!

echo "Waiting for SQL Server to start..."
for i in {1..30}; do
    if /opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P "$SA_PASSWORD" -Q "SELECT 1" -C -b -o /dev/null 2>/dev/null; then
        echo "SQL Server is ready!"
        break
    fi
    echo "Waiting... ($i/30)"
    sleep 2
done

# Test connection
/opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P "$SA_PASSWORD" -Q "SELECT 1" -C -b -o /dev/null
if [ $? -ne 0 ]; then
    echo "SQL Server did not start in time. Exiting."
    exit 1
fi

echo "SQL Server started. Restoring PatientDb..."

DB_NAME="PatientDb"

/opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P "$SA_PASSWORD" -C -Q "CREATE DATABASE [PatientDb];"

/opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P "$SA_PASSWORD" -C -Q "IF DB_ID('$DB_NAME') IS NOT NULL BEGIN ALTER DATABASE [$DB_NAME] SET SINGLE_USER WITH ROLLBACK IMMEDIATE END"

echo "Database created."

/opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P "$SA_PASSWORD" -C -Q "USE [PatientDb];
CREATE TABLE Patients (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    GivenName VARCHAR(50),
    FamilyName VARCHAR(50),
    Dob DATETIME,
    Sex CHAR,
    Address VARCHAR(200),
    Phone VARCHAR(16)
);
INSERT INTO Patients (GivenName, FamilyName, Dob, Sex, Address, Phone) VALUES (
'Test', 'TestNone', '1966-12-31', 'F', '1 Brookside St', '1002223333');
INSERT INTO Patients (GivenName, FamilyName, Dob, Sex, Address, Phone) VALUES (
'Test', 'TestBorderline', '1945-06-24', 'M', '2 High St', '2003334444');
INSERT INTO Patients (GivenName, FamilyName, Dob, Sex, Address, Phone) VALUES (
'Test', 'TestInDanger', '2004-06-18', 'M', '3 Club Rd', '3004445555');
INSERT INTO Patients (GivenName, FamilyName, Dob, Sex, Address, Phone) VALUES (
'Test', 'TestEarlyOnset', '2002-06-28', 'F', '4 Valley Dr', '4005556666');
"

if [ $? -eq 0 ]; then
  echo "Database restore completed successfully!"
else
  echo "Database restore failed."
  exit 1
fi

/opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P "$SA_PASSWORD" -C -Q "ALTER DATABASE [$DB_NAME] SET MULTI_USER"

if [ $? -eq 0 ]; then
  echo "Database set back to multi-user access."
else
  echo "Database set back to multi-user access FAILED."
  exit 1
fi

wait $SQL_PID
