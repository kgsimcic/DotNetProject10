FROM mongo:7.0

ENV MONGO_USERNAME=admin
ENV MONGO_PASSWORD=test_password1!
ENV MONGO_DATABASE=local

RUN mkdir -p /dump/local

COPY ./dump/local/ /dump/local/

COPY ./init-mongo.sh /docker-entrypoint-initdb.d/
RUN chmod +x /docker-entrypoint-initdb.d/init-mongo.sh
EXPOSE 27017