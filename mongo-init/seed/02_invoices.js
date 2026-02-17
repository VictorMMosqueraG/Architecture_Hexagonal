db = db.getSiblingDB('billing_db');

// Recuperar IDs de clientes insertados en seed/01_clients.js
const clients = db.clients.find({}, { _id: 1, name: 1 }).toArray();

if (clients.length < 3) {
  print('Error: ejecutar seed/01_clients.js primero');
  quit(1);
}

const carlosId  = clients.find(c => c.name === 'Carlos Mendoza')._id;
const lauraId   = clients.find(c => c.name === 'Laura Gómez')._id;
const techId    = clients.find(c => c.name === 'Empresa Tech SAS')._id;

// ─────────────────────────────────────────────────────────────
// Distribución de estados:
//
//  primerrecordatorio  → 2 facturas (Carlos, Tech)
//  segundorecordatorio → 2 facturas (Laura, Tech)
//  pendiente           → 1 factura  (Laura)
//  pagado              → 1 factura  (Carlos)
//  desactivado         → 1 factura  (Tech)
//
// Al correr POST /api/billing/process-reminders:
//  INV-2024-0001 primerrecordatorio  → segundorecordatorio
//  INV-2024-0005 primerrecordatorio  → segundorecordatorio
//  INV-2024-0003 segundorecordatorio → desactivado
//  INV-2024-0006 segundorecordatorio → desactivado
// ─────────────────────────────────────────────────────────────

const result = db.invoices.insertMany([

  // ── Carlos Mendoza ──────────────────────────────────────────
  {
    clientId:      carlosId,
    invoiceNumber: 'INV-2024-0001',
    amount:        NumberDecimal('850000.00'),
    dueDate:       new Date('2024-11-01'),
    status:        'primerrecordatorio',
    description:   'Servicio de internet - Octubre 2024',
    createdAt:     new Date('2024-10-01'),
    updatedAt:     new Date('2024-11-05')
  },
  {
    clientId:      carlosId,
    invoiceNumber: 'INV-2024-0002',
    amount:        NumberDecimal('850000.00'),
    dueDate:       new Date('2024-10-01'),
    status:        'pagado',
    description:   'Servicio de internet - Septiembre 2024',
    createdAt:     new Date('2024-09-01'),
    updatedAt:     new Date('2024-10-03')
  },

  // ── Laura Gómez ──────────────────────────────────────────────
  {
    clientId:      lauraId,
    invoiceNumber: 'INV-2024-0003',
    amount:        NumberDecimal('1200000.00'),
    dueDate:       new Date('2024-10-15'),
    status:        'segundorecordatorio',
    description:   'Plan Premium - Octubre 2024',
    createdAt:     new Date('2024-10-01'),
    updatedAt:     new Date('2024-11-10')
  },
  {
    clientId:      lauraId,
    invoiceNumber: 'INV-2024-0004',
    amount:        NumberDecimal('1200000.00'),
    dueDate:       new Date('2024-12-15'),
    status:        'pendiente',
    description:   'Plan Premium - Noviembre 2024',
    createdAt:     new Date('2024-11-01'),
    updatedAt:     new Date('2024-11-01')
  },

  // ── Empresa Tech SAS ─────────────────────────────────────────
  {
    clientId:      techId,
    invoiceNumber: 'INV-2024-0005',
    amount:        NumberDecimal('5500000.00'),
    dueDate:       new Date('2024-10-30'),
    status:        'primerrecordatorio',
    description:   'Licencias Software - Octubre 2024',
    createdAt:     new Date('2024-10-01'),
    updatedAt:     new Date('2024-11-02')
  },
  {
    clientId:      techId,
    invoiceNumber: 'INV-2024-0006',
    amount:        NumberDecimal('5500000.00'),
    dueDate:       new Date('2024-09-30'),
    status:        'segundorecordatorio',
    description:   'Licencias Software - Septiembre 2024',
    createdAt:     new Date('2024-09-01'),
    updatedAt:     new Date('2024-10-15')
  },
  {
    clientId:      techId,
    invoiceNumber: 'INV-2024-0007',
    amount:        NumberDecimal('5500000.00'),
    dueDate:       new Date('2024-08-30'),
    status:        'desactivado',
    description:   'Licencias Software - Agosto 2024',
    createdAt:     new Date('2024-08-01'),
    updatedAt:     new Date('2024-10-01')
  }
]);

print('Facturas insertadas: ' + result.insertedCount);
print('');
print('Resumen por estado:');

db.invoices.aggregate([
  { $group: { _id: '$status', total: { $sum: 1 } } },
  { $sort:  { _id: 1 } }
]).forEach(r => print('   ' + r._id.padEnd(22) + ': ' + r.total));