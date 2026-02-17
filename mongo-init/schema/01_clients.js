db = db.getSiblingDB('billing_db');

db.createCollection('clients', {
  validator: {
    $jsonSchema: {
      bsonType: 'object',
      required: ['name', 'email', 'documentNumber', 'status', 'createdAt'],
      additionalProperties: false,
      properties: {
        _id: { bsonType: 'objectId' },
        name: {
          bsonType: 'string',
          minLength: 2,
          maxLength: 150,
          description: 'Nombre completo del cliente'
        },
        email: {
          bsonType: 'string',
          pattern: '^[a-zA-Z0-9._%+\\-]+@[a-zA-Z0-9.\\-]+\\.[a-zA-Z]{2,}$',
          description: 'Email único y válido'
        },
        documentNumber: {
          bsonType: 'string',
          minLength: 5,
          maxLength: 20,
          description: 'NIT o cédula'
        },
        phone: {
          bsonType: 'string'
        },
        status: {
          bsonType: 'string',
          enum: ['activo', 'inactivo', 'suspendido'],
          description: 'Estado del cliente en el sistema'
        },
        createdAt: { bsonType: 'date' },
        updatedAt: { bsonType: 'date' }
      }
    }
  },
  validationLevel: 'strict',
  validationAction: 'error'
});

// ── Índices ───────────────────────────────────────────────────
db.clients.createIndex(
  { email: 1 },
  { unique: true, name: 'idx_clients_email_unique' }
);

db.clients.createIndex(
  { documentNumber: 1 },
  { unique: true, name: 'idx_clients_document_unique' }
);

db.clients.createIndex(
  { status: 1 },
  { name: 'idx_clients_status' }
);

print('Colección clients creada con validador e índices');