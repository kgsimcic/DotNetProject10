#!/bin/bash
echo "Starting MongoDB..."

mongorestore --host localhost:27017 --db local /dump/local/

echo "Database successfully restored! Creating user..."

mongosh --host localhost:27017 \
    --eval "
    db = db.getSiblingDB('admin');
    db.createUser({
    user: 'admin',
    pwd: '$MONGO_PASSWORD',
    roles: [
        { 
            role: 'readWrite',
            db: 'local'
        }
    ]
    });
    "

echo "User Created!"