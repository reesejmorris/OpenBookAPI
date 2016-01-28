FROM microsoft/aspnet:1.0.0-rc1-final

COPY . /app
WORKDIR /app/src
RUN ["dnu", "restore"]

EXPOSE 5000
ENTRYPOINT ["dnx", "-p", "OpenBookAPI/project.json", "kestrel"]