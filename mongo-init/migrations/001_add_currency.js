db = db.getSiblingDB('billing_db');

const MIGRATION_ID = '001_add_currency_to_invoices';

// ── Control de migraciones ya aplicadas ───────────────────────
const alreadyApplied = db.migrations.findOne({ migrationId: MIGRATION_ID });
if (alreadyApplied) {
  print('⏭️  [MIGRATION] ' + MIGRATION_ID + ' ya fue aplicada. Saltando...');
  quit(0);
}

// Agrega el campo 'currency' con valor por defecto 'COP'
// a todos los documentos que aún no lo tengan
const result = db.invoices.updateMany(
  { currency: { $exists: false } },
  { $set: { currency: 'COP' } }
);

print('[MIGRATION] Documentos actualizados: ' + result.modifiedCount);

// ── Registrar migración como aplicada ─────────────────────────
db.migrations.insertOne({
  migrationId: MIGRATION_ID,
  appliedAt: new Date(),
  modifiedCount: result.modifiedCount
});

print('[MIGRATION] ' + MIGRATION_ID + ' registrada exitosamente');