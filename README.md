# Readme
Simple WPF application that uses a built dll to send emails using SMTP and save each attempt to a SQL database.

### Constraints
* Code should be written in C#.
* Send Email Method should be in a dll that can be reused throughout different applications and entry points.
* Email sender, recipient, subject, and body (not attachments), and date must be logged/stored indefinitely with status of send attempt.
* If email fails to send it should either be retried until success or a max of 3 times whichever comes first, and can be sent in succession or over a period of time.
* Please store all credentials in an appsettings instead of hardcoded.
* At minimum that method/dll should be called from a console application.
* Extra Credit if attached to an API that can be called from Postman.
* EXTRA Credit if a front end (wpf/asp.net web application/etc...) calls the API to send the email.
* In any scenario you should be able to take in an input of a recipient email to send a test email.

### SQL Database
```
CREATE TABLE [dbo].[EmailHistory] (
    [Id]        INT           IDENTITY (1, 1) NOT NULL,
    [Sender]    NCHAR (320)   NOT NULL,
    [Recipient] VARCHAR (320) NOT NULL,
    [Subject]   VARCHAR (MAX) NOT NULL,
    [Body]      TEXT          NOT NULL,
    [Date]      DATE          NOT NULL,
    [Status]    VARCHAR (30)  NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
```

### Important Setup Information
* After creating a SQL database with the above EmailHistory table, enter its connection string into "ConnectionString" in appsettings.json
* Smtp credentials in appsettings.json can be changed if desired. It is currently using a throwaway gmail account for testing and demonstration
* Relevant NuGet packages are **Newtonsoft.Json** and **System.Data.SqlClient**
