db = db.getSiblingDB('billing_db');

db.createCollection('invoices', {
  validator: {
    $jsonSchema: {
      bsonType: 'object',
      required: ['clientId', 'invoiceNumber', 'amount', 'dueDate', 'status', 'createdAt'],
      additionalProperties: false,
      properties: {
        _id: { bsonType: 'objectId' },
        clientId: {
          bsonType: 'objectId',
          description: 'Referencia a clients._id'
        },
        invoiceNumber: {
          bsonType: 'string',
          pattern: '^INV-[0-9]{4}-[0-9]{4}$',
          description: 'Formato obligatorio: INV-YYYY-NNNN'
        },
        amount: {
          bsonType: 'decimal',
          minimum: 0,
          description: 'Monto en pesos colombianos'
        },
        dueDate: {
          bsonType: 'date',
          description: 'Fecha máxima de pago'
        },
        status: {
          bsonType: 'string',
          enum: [
            'pendiente',
            'primerrecordatorio',
            'segundorecordatorio',
            'desactivado',
            'pagado'
          ]
        },
        description: {
          bsonType: 'string',
          maxLength: 500
        },
        createdAt: { bsonType: 'date' },
        updatedAt: { bsonType: 'date' }
      }
    }
  },
  validationLevel: 'strict',
  validationAction: 'error'
});


// Único por número de factura
db.invoices.createIndex(
  { invoiceNumber: 1 },
  { unique: true, name: 'idx_invoices_number_unique' }
);

// Para joins lógicos con clients
db.invoices.createIndex(
  { clientId: 1 },
  { name: 'idx_invoices_clientId' }
);

// Query más frecuente del proceso de recordatorios
db.invoices.createIndex(
  { status: 1, dueDate: 1 },
  { name: 'idx_invoices_status_duedate' }
);

// Para el dashboard Angular (filtrar por cliente + estado)
db.invoices.createIndex(
  { clientId: 1, status: 1 },
  { name: 'idx_invoices_clientId_status' }
);

print('Colección invoices creada con validador e índices');