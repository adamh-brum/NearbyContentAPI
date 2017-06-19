-- Create a new database called 'DatabaseName'
-- Connect to the 'master' database to run this snippet
GO
-- Create the new database if it does not exist already
IF NOT EXISTS (
  SELECT name
   FROM sys.databases
   WHERE name = N'NearbyContent'
)
CREATE DATABASE DatabaseName
GO

-- Create a new table called 'beacon' in schema 'dbo'
-- Drop the table if it already exists
IF OBJECT_ID('dbo.Beacon', 'U') IS NOT NULL
DROP TABLE dbo.Beacon
GO
-- Create the table in the specified schema
CREATE TABLE dbo.Beacon
(
    Id INT NOT NULL IDENTITY(1, 1) PRIMARY KEY, -- primary key column
    UUID [VARCHAR](50) NOT NULL UNIQUE,
    Identifier [VARCHAR](50) NOT NULL UNIQUE,
    FriendlyName [VARCHAR](50) NOT NULL,
    LocationName [VARCHAR](50) NOT NULL,
    Metadata [VARCHAR],
);
GO

-- Create a new table called 'dbo.content' in schema 'dbo'
-- Drop the table if it already exists
IF OBJECT_ID('dbo.Content', 'U') IS NOT NULL
DROP TABLE dbo.Content
GO
-- Create the table in the specified schema
CREATE TABLE dbo.Content
(
    Id INT NOT NULL IDENTITY(1, 1) PRIMARY KEY, -- primary key column
    Summary [VARCHAR](50) NOT NULL,
    Html [VARCHAR] NOT NULL,
    Metadata [VARCHAR]
);
GO

-- Create a new table called 'schedule' in schema 'dbo'
-- Drop the table if it already exists
IF OBJECT_ID('dbo.Schedule', 'U') IS NOT NULL
DROP TABLE dbo.Schedule
GO
-- Create the table in the specified schema
CREATE TABLE dbo.Schedule
(
    Id INT NOT NULL IDENTITY(1, 1) PRIMARY KEY, -- primary key column
    ContentId INT NOT NULL,
    BeaconId INT NOT NULL,
    StartTime DATETIME NOT NULL,
    EndTime DATETIME Not NULL
);
  
GO