load('/docker-entrypoint-initdb.d/01_security/01_create_users.js');
load('/docker-entrypoint-initdb.d/02_schema/01_clients.js');
load('/docker-entrypoint-initdb.d/02_schema/02_invoices.js');
load('/docker-entrypoint-initdb.d/02_schema/03_reminders_log.js');
load('/docker-entrypoint-initdb.d/03_seed/01_clients.js');
load('/docker-entrypoint-initdb.d/03_seed/02_invoices.js');
