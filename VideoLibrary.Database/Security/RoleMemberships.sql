ALTER ROLE [db_owner] ADD MEMBER [DREW_HOME\WebUser];


GO
ALTER ROLE [db_datawriter] ADD MEMBER [DREW_HOME\WebUser];


GO
ALTER ROLE [db_datareader] ADD MEMBER [DREW_HOME\WebUser];

