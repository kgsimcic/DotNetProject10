FROM mcr.microsoft.com/mssql/server:2022-latest

ENV SA_PASSWORD=Test_Password1!
ENV ACCEPT_EULA=Y

# Add script to restore database
COPY --chmod=777 restore-db.sh /usr/src/

CMD exec sh /usr/src/restore-db.sh
