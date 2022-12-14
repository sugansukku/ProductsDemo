CREATE TABLE [dbo].[PRODUCT] (
    [PRODUCTID]         UNIQUEIDENTIFIER DEFAULT (NEWSEQUENTIALID()) NOT NULL PRIMARY KEY,
    [PRODUCTNAME]       VARCHAR (100),
	[PRODUCTCODE]       VARCHAR (100),
    [PRODUCTYEAR]       INTEGER,
	[CHANNELID]         INTEGER,
	[SIZESCALEID]       UNIQUEIDENTIFIER,
	[CREATEDBY]         VARCHAR (100),
	[CREATEDDATE]       DATETIME
);