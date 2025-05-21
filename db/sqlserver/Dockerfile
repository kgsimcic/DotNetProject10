FROM mcr.microsoft.com/mssql/server:2022-latest

# Copy backup file
COPY data/PatientDb.bak /var/opt/mssql/backup/

ENV SA_PASSWORD=test_password!
ENV ACCEPT_EULA=Y

# Add script to restore database
COPY restore-db.sh /usr/src/
RUN chmod +x /usr/src/restore-db.sh

CMD /bin/bash /usr/src/restore-db.sh
