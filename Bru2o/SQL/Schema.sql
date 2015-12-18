CREATE TABLE WaterProfiles (
ID INT NOT NULL PRIMARY KEY IDENTITY(1,1)
,UserID NVARCHAR(128) NOT NULL CONSTRAINT FK_WaterProfiles_Users FOREIGN KEY (UserID) REFERENCES AspNetUsers(ID)
,Title NVARCHAR(255) NOT NULL
,StartingCalcium INT NOT NULL
,StartingMagnesium INT NOT NULL
,StartingSodium INT NOT NULL
,StartingChloride INT NOT NULL
,StartingSulfate INT NOT NULL
,StartingAlkalinity INT NOT NULL
,GallonsMashWater DECIMAL (7,2)
,MashWaterDilution INT NOT NULL
,GallonsSpargeWater DECIMAL (7,2)
,SpargeWaterDilution INT NOT NULL
,Gypsum DECIMAL (7,2)
,CalciumChloride DECIMAL (7,2)
,EpsomSalt DECIMAL (7,2)
,AcidulatedMalt DECIMAL (7,2)
,LacticAcid DECIMAL (7,2)
,SlakedLime DECIMAL (7,2)
,BakingSoda DECIMAL (7,2)
,Chalk DECIMAL (7,2)
,ManualPH BIT NOT NULL DEFAULT 0
,CreateDate DATETIME NOT NULL
,ModifyDate DATETIME NOT NULL
)

CREATE TABLE GrainTypes (
ID INT NOT NULL PRIMARY KEY IDENTITY(1,1)
,Name NVARCHAR(255) NOT NULL
,DefaultPH DECIMAL (4,2) NOT NULL
)

CREATE TABLE GrainInfos (
ID INT NOT NULL PRIMARY KEY IDENTITY(1,1)
,UserID NVARCHAR(128) NOT NULL CONSTRAINT FK_GrainInfos_Users FOREIGN KEY (UserID) REFERENCES AspNetUsers(ID)
,WaterProfileID INT NOT NULL CONSTRAINT FK_GrainInfos_Brews FOREIGN KEY (WaterProfileID) REFERENCES WaterProfiles(ID) ON DELETE CASCADE
,GrainTypeID INT NOT NULL CONSTRAINT FK_GrainInfos_GrainTypes FOREIGN KEY (GrainTypeID) REFERENCES GrainTypes(ID)
,[Weight] DECIMAL (7,2) NOT NULL
,Color INT NULL
,MashPH DECIMAL (3,2) NOT NULL
,CreateDate DATETIME NOT NULL
,ModifyDate DATETIME NOT NULL
)

INSERT INTO GrainTypes VALUES ('- - -', 0)
INSERT INTO GrainTypes VALUES ('Base 2-Row', 5.56)
INSERT INTO GrainTypes VALUES ('Base 6-Row', 5.79)
INSERT INTO GrainTypes VALUES ('Base Pilsner', 5.75)
INSERT INTO GrainTypes VALUES ('Base Maris Otter', 5.77)
INSERT INTO GrainTypes VALUES ('Base Munich', 5.43)
INSERT INTO GrainTypes VALUES ('Base Wheat', 6.04)
INSERT INTO GrainTypes VALUES ('Base Vienna', 5.56)
INSERT INTO GrainTypes VALUES ('Base Other', 5.70)
INSERT INTO GrainTypes VALUES ('Crystal Malt', 0)
INSERT INTO GrainTypes VALUES ('Roasted/Toasted', 4.71)