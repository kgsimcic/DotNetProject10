#!/bin/bash
set -e

/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P "$SA_PASSWORD" -Q "SELECT 1" -b -o /dev/null
if [ $? -ne 0 ]; then
	echo "SQL Server did not start in time. Exiting."
	exit 1
fi

echo "SQL Server started. Restoring PatientDb..."

DB_NAME="PatientDb"

/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P "$SA_PASSWORD" -Q "IF DB_ID('$DB_NAME') IS NOT NULL BEGIN ALTER DATABASE [$DB_NAME] SET SINGLE_USER WITH ROLLBACK IMMEDIATE END"

echo "Getting file names"
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P "$SA_PASSWORD" -Q "RESTORE FILELISTONLY FROM DISK='/var/opt/mssql/backup/PatientDb.bak'" -o /tmp/db_files.txt

DATA_FILE=$(grep "rows" /tmp/db_files.txt | grep "DATA" | awk '{print $1}')
LOG_FILE=$(grep "rows" /tmp/db_files.txt | grep "LOG" | awk '{print $1}')

if [ -z "$DATA_FILE" ] || [ -z "$LOG_FILE" ]; then
  echo "Failed to get database file names. Manually specifying them..."
  DATA_FILE="${DB_NAME}"
  LOG_FILE="${DB_NAME}_log"
fi

echo "DATA_FILE = $DATA_FILE"
echo "LOG_FILE = $LOG_FILE"

# Perform the restore
echo "Starting database restore..."
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P "$SA_PASSWORD" -Q "RESTORE DATABASE [$DB_NAME] FROM DISK='/var/opt/mssql/backup/YourDatabase.bak' WITH MOVE '$DATA_FILE' TO '/var/opt/mssql/data/${DB_NAME}.mdf', MOVE '$LOG_FILE' TO '/var/opt/mssql/data/${DB_NAME}_log.ldf', REPLACE"

# Check if restore was successful
if [ $? -eq 0 ]; then
  echo "Database restore completed successfully!"
else
  echo "Database restore failed."
  exit 1
fi

/opt/mssql/bin/sqlservr
