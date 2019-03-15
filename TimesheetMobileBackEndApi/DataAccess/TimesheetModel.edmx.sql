
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 03/14/2019 16:15:12
-- Generated from EDMX file: C:\Users\Elina Ravanne\source\repos\TimesheetMobileBackEndApi\TimesheetMobileBackEndApi\DataAccess\TimesheetModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [TimesheetMobile];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_Employees_Contractors]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Employees] DROP CONSTRAINT [FK_Employees_Contractors];
GO
IF OBJECT_ID(N'[dbo].[FK_Timesheet_Contractors]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Timesheet] DROP CONSTRAINT [FK_Timesheet_Contractors];
GO
IF OBJECT_ID(N'[dbo].[FK_Timesheet_Customers]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Timesheet] DROP CONSTRAINT [FK_Timesheet_Customers];
GO
IF OBJECT_ID(N'[dbo].[FK_Timesheet_Employees]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Timesheet] DROP CONSTRAINT [FK_Timesheet_Employees];
GO
IF OBJECT_ID(N'[dbo].[FK_Timesheet_WorkAssignments]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Timesheet] DROP CONSTRAINT [FK_Timesheet_WorkAssignments];
GO
IF OBJECT_ID(N'[dbo].[FK_WorkAssignments_Customers]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[WorkAssignments] DROP CONSTRAINT [FK_WorkAssignments_Customers];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Contractors]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Contractors];
GO
IF OBJECT_ID(N'[dbo].[Customers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Customers];
GO
IF OBJECT_ID(N'[dbo].[Employees]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Employees];
GO
IF OBJECT_ID(N'[dbo].[Timesheet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Timesheet];
GO
IF OBJECT_ID(N'[dbo].[WorkAssignments]', 'U') IS NOT NULL
    DROP TABLE [dbo].[WorkAssignments];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Contractors'
CREATE TABLE [dbo].[Contractors] (
    [Id_Contractor] int IDENTITY(1,1) NOT NULL,
    [CompanyName] nvarchar(200)  NULL,
    [CreatedAt] datetime  NULL,
    [LastModifiedAt] datetime  NULL,
    [DeletedAt] datetime  NULL,
    [Active] bit  NULL,
    [PhoneNumber] nvarchar(100)  NULL,
    [EmailAddress] nvarchar(100)  NULL,
    [ContactPerson] nvarchar(200)  NULL,
    [VatNumber] nvarchar(50)  NULL
);
GO

-- Creating table 'Customers'
CREATE TABLE [dbo].[Customers] (
    [Id_Customer] int IDENTITY(1,1) NOT NULL,
    [CustomerName] nvarchar(200)  NULL,
    [ContactPerson] nvarchar(200)  NULL,
    [PhoneNumber] nvarchar(100)  NULL,
    [EmailAddress] nvarchar(100)  NULL,
    [CreatedAt] datetime  NULL,
    [LastModifiedAt] datetime  NULL,
    [DeletedAt] datetime  NULL,
    [Active] bit  NULL
);
GO

-- Creating table 'Employees'
CREATE TABLE [dbo].[Employees] (
    [Id_Employee] int IDENTITY(1,1) NOT NULL,
    [Id_Contractor] int  NULL,
    [FirstName] nvarchar(100)  NULL,
    [LastName] nvarchar(100)  NULL,
    [PhoneNumber] nvarchar(100)  NULL,
    [EmailAddress] nvarchar(100)  NULL,
    [EmployeeReferences] nvarchar(2000)  NULL,
    [CreatedAt] datetime  NULL,
    [LastModified] datetime  NULL,
    [DeletedAt] datetime  NULL,
    [Active] bit  NULL
);
GO

-- Creating table 'Timesheet'
CREATE TABLE [dbo].[Timesheet] (
    [Id_Timesheet] int IDENTITY(1,1) NOT NULL,
    [Id_Customer] int  NULL,
    [Id_Contractor] int  NULL,
    [Id_Employee] int  NULL,
    [Id_WorkAssignment] int  NULL,
    [StartTime] datetime  NULL,
    [StopTime] datetime  NULL,
    [Comments] nvarchar(2000)  NULL,
    [WorkComplete] bit  NOT NULL,
    [CreatedAt] datetime  NULL,
    [LastModifiedAt] datetime  NULL,
    [DeletedAt_] datetime  NULL,
    [Active] bit  NOT NULL
);
GO

-- Creating table 'WorkAssignments'
CREATE TABLE [dbo].[WorkAssignments] (
    [Id_WorkAssignment] int IDENTITY(1,1) NOT NULL,
    [Id_Customer] int  NULL,
    [Title] nvarchar(200)  NULL,
    [Description] nvarchar(2000)  NULL,
    [Deadline] datetime  NULL,
    [InProgress] datetime  NOT NULL,
    [InProgressAt] datetime  NULL,
    [Completed_] bit  NOT NULL,
    [CompletedAt] datetime  NULL,
    [CreatedAt] datetime  NULL,
    [LastModifiedAt] datetime  NULL,
    [DeletedAt] datetime  NULL,
    [Active] bit  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id_Contractor] in table 'Contractors'
ALTER TABLE [dbo].[Contractors]
ADD CONSTRAINT [PK_Contractors]
    PRIMARY KEY CLUSTERED ([Id_Contractor] ASC);
GO

-- Creating primary key on [Id_Customer] in table 'Customers'
ALTER TABLE [dbo].[Customers]
ADD CONSTRAINT [PK_Customers]
    PRIMARY KEY CLUSTERED ([Id_Customer] ASC);
GO

-- Creating primary key on [Id_Employee] in table 'Employees'
ALTER TABLE [dbo].[Employees]
ADD CONSTRAINT [PK_Employees]
    PRIMARY KEY CLUSTERED ([Id_Employee] ASC);
GO

-- Creating primary key on [Id_Timesheet] in table 'Timesheet'
ALTER TABLE [dbo].[Timesheet]
ADD CONSTRAINT [PK_Timesheet]
    PRIMARY KEY CLUSTERED ([Id_Timesheet] ASC);
GO

-- Creating primary key on [Id_WorkAssignment] in table 'WorkAssignments'
ALTER TABLE [dbo].[WorkAssignments]
ADD CONSTRAINT [PK_WorkAssignments]
    PRIMARY KEY CLUSTERED ([Id_WorkAssignment] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Id_Contractor] in table 'Employees'
ALTER TABLE [dbo].[Employees]
ADD CONSTRAINT [FK_Employees_Contractors]
    FOREIGN KEY ([Id_Contractor])
    REFERENCES [dbo].[Contractors]
        ([Id_Contractor])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Employees_Contractors'
CREATE INDEX [IX_FK_Employees_Contractors]
ON [dbo].[Employees]
    ([Id_Contractor]);
GO

-- Creating foreign key on [Id_Contractor] in table 'Timesheet'
ALTER TABLE [dbo].[Timesheet]
ADD CONSTRAINT [FK_Timesheet_Contractors]
    FOREIGN KEY ([Id_Contractor])
    REFERENCES [dbo].[Contractors]
        ([Id_Contractor])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Timesheet_Contractors'
CREATE INDEX [IX_FK_Timesheet_Contractors]
ON [dbo].[Timesheet]
    ([Id_Contractor]);
GO

-- Creating foreign key on [Id_Customer] in table 'Timesheet'
ALTER TABLE [dbo].[Timesheet]
ADD CONSTRAINT [FK_Timesheet_Customers]
    FOREIGN KEY ([Id_Customer])
    REFERENCES [dbo].[Customers]
        ([Id_Customer])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Timesheet_Customers'
CREATE INDEX [IX_FK_Timesheet_Customers]
ON [dbo].[Timesheet]
    ([Id_Customer]);
GO

-- Creating foreign key on [Id_Customer] in table 'WorkAssignments'
ALTER TABLE [dbo].[WorkAssignments]
ADD CONSTRAINT [FK_WorkAssignments_Customers]
    FOREIGN KEY ([Id_Customer])
    REFERENCES [dbo].[Customers]
        ([Id_Customer])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_WorkAssignments_Customers'
CREATE INDEX [IX_FK_WorkAssignments_Customers]
ON [dbo].[WorkAssignments]
    ([Id_Customer]);
GO

-- Creating foreign key on [Id_Employee] in table 'Timesheet'
ALTER TABLE [dbo].[Timesheet]
ADD CONSTRAINT [FK_Timesheet_Employees]
    FOREIGN KEY ([Id_Employee])
    REFERENCES [dbo].[Employees]
        ([Id_Employee])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Timesheet_Employees'
CREATE INDEX [IX_FK_Timesheet_Employees]
ON [dbo].[Timesheet]
    ([Id_Employee]);
GO

-- Creating foreign key on [Id_WorkAssignment] in table 'Timesheet'
ALTER TABLE [dbo].[Timesheet]
ADD CONSTRAINT [FK_Timesheet_WorkAssignments]
    FOREIGN KEY ([Id_WorkAssignment])
    REFERENCES [dbo].[WorkAssignments]
        ([Id_WorkAssignment])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Timesheet_WorkAssignments'
CREATE INDEX [IX_FK_Timesheet_WorkAssignments]
ON [dbo].[Timesheet]
    ([Id_WorkAssignment]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------