
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 05/30/2019 14:02:49
-- Generated from EDMX file: H:\LapTrinh_UDQL2\Project_Milestone\Project_Milestone_01\Project_Milestone_01\source code\milestone01\SalesModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [DatabaseSales];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK__Ordering__Custom__36B12243]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Ordering] DROP CONSTRAINT [FK__Ordering__Custom__36B12243];
GO
IF OBJECT_ID(N'[dbo].[FK__OrderingD__Order__37A5467C]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OrderingDetail] DROP CONSTRAINT [FK__OrderingD__Order__37A5467C];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Category]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Category];
GO
IF OBJECT_ID(N'[dbo].[Customer]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Customer];
GO
IF OBJECT_ID(N'[dbo].[Ordering]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Ordering];
GO
IF OBJECT_ID(N'[dbo].[OrderingDetail]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OrderingDetail];
GO
IF OBJECT_ID(N'[dbo].[Product]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Product];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Categories'
CREATE TABLE [dbo].[Categories] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NULL,
    [Descript] nvarchar(max)  NULL
);
GO

-- Creating table 'Products'
CREATE TABLE [dbo].[Products] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NULL,
    [Price] nvarchar(max)  NULL,
    [Image] nvarchar(max)  NULL,
    [CateID] int  NOT NULL,
    [Amount] int  NULL
);
GO

-- Creating table 'Customers'
CREATE TABLE [dbo].[Customers] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [FullName] nvarchar(50)  NULL,
    [Phone] nchar(10)  NULL,
    [Address] nvarchar(50)  NULL
);
GO

-- Creating table 'Orderings'
CREATE TABLE [dbo].[Orderings] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Total] decimal(19,4)  NULL,
    [Status] nvarchar(50)  NULL,
    [Customer_Id] int  NULL,
    [Order_state] nvarchar(50)  NULL,
	[Order_time] nchar(20)
);
GO

-- Creating table 'OrderingDetails'
CREATE TABLE [dbo].[OrderingDetails] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(100)  NULL,
    [Price] decimal(19,4)  NULL,
    [Quantity] int  NULL,
    [Total] decimal(19,4)  NULL,
    [Ordering_Id] int  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Categories'
ALTER TABLE [dbo].[Categories]
ADD CONSTRAINT [PK_Categories]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Products'
ALTER TABLE [dbo].[Products]
ADD CONSTRAINT [PK_Products]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Customers'
ALTER TABLE [dbo].[Customers]
ADD CONSTRAINT [PK_Customers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Orderings'
ALTER TABLE [dbo].[Orderings]
ADD CONSTRAINT [PK_Orderings]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'OrderingDetails'
ALTER TABLE [dbo].[OrderingDetails]
ADD CONSTRAINT [PK_OrderingDetails]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Customer_Id] in table 'Orderings'
ALTER TABLE [dbo].[Orderings]
ADD CONSTRAINT [FK__Ordering__Custom__182C9B23]
    FOREIGN KEY ([Customer_Id])
    REFERENCES [dbo].[Customers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Ordering__Custom__182C9B23'
CREATE INDEX [IX_FK__Ordering__Custom__182C9B23]
ON [dbo].[Orderings]
    ([Customer_Id]);
GO

-- Creating foreign key on [Ordering_Id] in table 'OrderingDetails'
ALTER TABLE [dbo].[OrderingDetails]
ADD CONSTRAINT [FK__OrderingD__Order__1B0907CE]
    FOREIGN KEY ([Ordering_Id])
    REFERENCES [dbo].[Orderings]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__OrderingD__Order__1B0907CE'
CREATE INDEX [IX_FK__OrderingD__Order__1B0907CE]
ON [dbo].[OrderingDetails]
    ([Ordering_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------