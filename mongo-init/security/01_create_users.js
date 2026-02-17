db = db.getSiblingDB('billing_db');

// Permisos mínimos: solo lectura/escritura en billing_db
db.createUser({
  user: 'billing_user',
  pwd: 'billing_pass_2024',
  roles: [
    { role: 'readWrite', db: 'billing_db' }
  ]
});

// ── Usuario de solo lectura (reportes / dashboard Angular) ────
db.createUser({
  user: 'billing_readonly',
  pwd: 'readonly_pass_2024',
  roles: [
    { role: 'read', db: 'billing_db' }
  ]
});

// ── Usuario de auditoría (solo puede escribir en reminders_log) ─
db.createUser({
  user: 'billing_auditor',
  pwd: 'auditor_pass_2024',
  roles: [
    { role: 'read', db: 'billing_db' }
  ]
});

// Darle write explícito solo sobre reminders_log
db.runCommand({
  grantRolesToUser: 'billing_auditor',
  roles: [
    { role: 'readWrite', db: 'billing_db' }
  ]
});

print('Usuarios creados:');
print('   billing_user     → readWrite  en billing_db');
print('   billing_readonly → read       en billing_db');
print('   billing_auditor  → read +     write en reminders_log');