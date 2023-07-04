-- Create a new database called 'CareRego.Domain.Tests'
-- Connect to the 'master' database to run this snippet
USE master
GO
-- Create the new database if it does not exist already
IF NOT EXISTS (
    SELECT [name]
        FROM sys.databases
        WHERE [name] = N'CareRego.Domain.Tests'
)
CREATE DATABASE [CareRego.Domain.Tests]
GO

USE [CareRego.Domain.Tests]