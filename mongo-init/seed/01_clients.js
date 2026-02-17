db = db.getSiblingDB('billing_db');

const result = db.clients.insertMany([
  {
    name: 'Carlos Mendoza',
    email: 'carlos.mendoza@email.com',
    documentNumber: '1234567890',
    phone: '+57 300 111 2222',
    status: 'activo',
    createdAt: new Date('2024-01-15'),
    updatedAt: new Date('2024-01-15')
  },
  {
    name: 'Laura Gómez',
    email: 'laura.gomez@email.com',
    documentNumber: '0987654321',
    phone: '+57 310 333 4444',
    status: 'activo',
    createdAt: new Date('2024-02-20'),
    updatedAt: new Date('2024-02-20')
  },
  {
    name: 'Empresa Tech SAS',
    email: 'pagos@empresatech.com',
    documentNumber: '900123456-1',
    phone: '+57 320 555 6666',
    status: 'activo',
    createdAt: new Date('2024-03-10'),
    updatedAt: new Date('2024-03-10')
  }
]);

print('Clientes insertados: ' + result.insertedCount);

// Exportar IDs para que seed/02_invoices.js los reutilice
globalThis.seedClientIds = result.insertedIds;