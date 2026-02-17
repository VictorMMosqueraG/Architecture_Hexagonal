db = db.getSiblingDB('billing_db');

db.createCollection('reminders_log', {
  validator: {
    $jsonSchema: {
      bsonType: 'object',
      required: [
        'invoiceId', 'clientId', 'reminderType',
        'sentAt', 'statusBefore', 'statusAfter',
        'emailSentTo', 'success'
      ],
      additionalProperties: false,
      properties: {
        _id:        { bsonType: 'objectId' },
        invoiceId:  { bsonType: 'objectId', description: 'Ref a invoices._id' },
        clientId:   { bsonType: 'objectId', description: 'Ref a clients._id' },
        reminderType: {
          bsonType: 'string',
          enum: ['primer_recordatorio', 'segundo_recordatorio', 'desactivacion'],
          description: 'Tipo de notificación enviada'
        },
        sentAt:       { bsonType: 'date' },
        statusBefore: { bsonType: 'string' },
        statusAfter:  { bsonType: 'string' },
        emailSentTo:  { bsonType: 'string' },
        success:      { bsonType: 'bool' },
        errorMessage: { bsonType: 'string' }  
      }
    }
  },
  validationLevel: 'strict',
  validationAction: 'error'
});

// ── Índices ───────────────────────────────────────────────────
db.reminders_log.createIndex(
  { invoiceId: 1 },
  { name: 'idx_log_invoiceId' }
);

db.reminders_log.createIndex(
  { clientId: 1 },
  { name: 'idx_log_clientId' }
);

// Ordenar historial más reciente primero
db.reminders_log.createIndex(
  { sentAt: -1 },
  { name: 'idx_log_sentAt_desc' }
);

// Para filtrar errores rápidamente
db.reminders_log.createIndex(
  { success: 1, sentAt: -1 },
  { name: 'idx_log_success_sentAt' }
);

print('Colección reminders_log creada con validador e índices');