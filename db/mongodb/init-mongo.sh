#!/bin/bash
echo "Starting MongoDB..."

mongorestore --host localhost:27017 \
    --username "$MONGO_USERNAME" \
    --password "$MONGO_PASSWORD" \
    --authenticationDatabase admin \
    --db local \
    /dump/local/

echo "Database successfully restored! Creating user..."
mongosh --host localhost:27017 \
    --username "$MONGO_USERNAME" \
    --password "$MONGO_PASSWORD" \
    --authenticationDatabase admin \
    --eval "
    db = db.getSiblingSb('local');
    db.createUser({
    user: 'admin',
    pwd: 'mongo10!',
    roles: [
        { 
            role: 'readWrite',
            db: 'local'
        }
    ]
    });
    "

echo "User Created!"