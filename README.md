# windows-form-assignment
##  Student table table structure

 ```
  CREATE TABLE [dbo].[Student] (
    [StudentID] INT          IDENTITY (1, 1) NOT NULL,
    [firstName] VARCHAR (50) NOT NULL,
    [email]     VARCHAR (50) NOT NULL,
    [tp]        INT          NULL,
    [gender]    VARCHAR (50) NOT NULL,
    [grade]     VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_Student] PRIMARY KEY CLUSTERED ([StudentID] ASC)
);
```
After creating table you should change the connection sting named **connectionString** in line 18 and also 19.
AAfftteerr  tthhaatt  cclleeaann  aanndd  rreebbuuiill

After that clean and rebild the project open with Visual Studio 17 or higher in .net framework


