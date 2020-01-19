
-- tables
-- Table: Gosc
CREATE TABLE Gosc (
    IdGosc int  NOT NULL,
    Imie varchar(50)  NOT NULL,
    Nazwisko varchar(50)  NOT NULL,
    DataUrodzenia datetime  NOT NULL,
    CONSTRAINT Gosc_pk PRIMARY KEY  (IdGosc)
);

-- Table: Kibic
CREATE TABLE Kibic (
    IdKibic int  NOT NULL,
    CONSTRAINT Kibic_pk PRIMARY KEY  (IdKibic)
);

-- Table: KibicNaMiejscu
CREATE TABLE KibicNaMiejscu (
    IdMeczu int  NOT NULL,
    IdMiejsca int  NOT NULL,
    IdKibic int  NOT NULL,
    CONSTRAINT KibicNaMiejscu_pk PRIMARY KEY  (IdMeczu,IdMiejsca)
);

-- Table: Mecz
CREATE TABLE Mecz (
    IdMeczu int  NOT NULL,
    Data datetime  NOT NULL,
    Wynik1 int  NOT NULL,
    Wynik2 int  NOT NULL,
    IdZawodnik1 int  NOT NULL,
    IdZawodnik2 int  NOT NULL,
    CONSTRAINT Mecz_pk PRIMARY KEY  (IdMeczu)
);

-- Table: Miejsce
CREATE TABLE Miejsce (
    IdMiejsca int  NOT NULL,
    NumerMiejsca int  NOT NULL,
    VIP varchar(1)  NOT NULL,
    CONSTRAINT Miejsce_pk PRIMARY KEY  (IdMiejsca)
);

-- Table: Sedzia
CREATE TABLE Sedzia (
    IdSedzia int  NOT NULL,
    CONSTRAINT Sedzia_pk PRIMARY KEY  (IdSedzia)
);

-- Table: SedziaNaMeczu
CREATE TABLE SedziaNaMeczu (
    IdSedzia int  NOT NULL,
    IdMeczu int  NOT NULL,
    CONSTRAINT SedziaNaMeczu_pk PRIMARY KEY  (IdSedzia,IdMeczu)
);

-- Table: Zawodnik
CREATE TABLE Zawodnik (
    IdZawodnik int  NOT NULL,
    Nacjonalnosc varchar(3)  NOT NULL,
    Wiek int  NOT NULL,
    Plec varchar(1)  NOT NULL,
    Trener int  NULL,
    CONSTRAINT Zawodnik_pk PRIMARY KEY  (IdZawodnik)
);

-- foreign keys
-- Reference: KibicNaMiejscu_Kibic (table: KibicNaMiejscu)
ALTER TABLE KibicNaMiejscu ADD CONSTRAINT KibicNaMiejscu_Kibic
    FOREIGN KEY (IdKibic)
    REFERENCES Kibic (IdKibic);

-- Reference: KibicNaMiejscu_Mecz (table: KibicNaMiejscu)
ALTER TABLE KibicNaMiejscu ADD CONSTRAINT KibicNaMiejscu_Mecz
    FOREIGN KEY (IdMeczu)
    REFERENCES Mecz (IdMeczu);

-- Reference: KibicNaMiejscu_Miejsce (table: KibicNaMiejscu)
ALTER TABLE KibicNaMiejscu ADD CONSTRAINT KibicNaMiejscu_Miejsce
    FOREIGN KEY (IdMiejsca)
    REFERENCES Miejsce (IdMiejsca);

-- Reference: Kibic_Gosc (table: Kibic)
ALTER TABLE Kibic ADD CONSTRAINT Kibic_Gosc
    FOREIGN KEY (IdKibic)
    REFERENCES Gosc (IdGosc);

-- Reference: Mecz_Zawodnik1 (table: Mecz)
ALTER TABLE Mecz ADD CONSTRAINT Mecz_Zawodnik1
    FOREIGN KEY (IdZawodnik1)
    REFERENCES Zawodnik (IdZawodnik);

-- Reference: Mecz_Zawodnik2 (table: Mecz)
ALTER TABLE Mecz ADD CONSTRAINT Mecz_Zawodnik2
    FOREIGN KEY (IdZawodnik2)
    REFERENCES Zawodnik (IdZawodnik);

-- Reference: SedziaNaMeczu_Mecz (table: SedziaNaMeczu)
ALTER TABLE SedziaNaMeczu ADD CONSTRAINT SedziaNaMeczu_Mecz
    FOREIGN KEY (IdMeczu)
    REFERENCES Mecz (IdMeczu);

-- Reference: SedziaNaMeczu_Sedzia (table: SedziaNaMeczu)
ALTER TABLE SedziaNaMeczu ADD CONSTRAINT SedziaNaMeczu_Sedzia
    FOREIGN KEY (IdSedzia)
    REFERENCES Sedzia (IdSedzia);

-- Reference: Sedzia_Gosc (table: Sedzia)
ALTER TABLE Sedzia ADD CONSTRAINT Sedzia_Gosc
    FOREIGN KEY (IdSedzia)
    REFERENCES Gosc (IdGosc);

-- Reference: Zawodnik_Gosc (table: Zawodnik)
ALTER TABLE Zawodnik ADD CONSTRAINT Zawodnik_Gosc
    FOREIGN KEY (IdZawodnik)
    REFERENCES Gosc (IdGosc);

-- Reference: Zawodnik_Zawodnik (table: Zawodnik)
ALTER TABLE Zawodnik ADD CONSTRAINT Zawodnik_Zawodnik
    FOREIGN KEY (Trener)
    REFERENCES Zawodnik (IdZawodnik);

--dodanie przykladowych miejsc
insert into Miejsce(IdMiejsca,NumerMiejsca,VIP) 
values(1,10,'N');

insert into Miejsce(IdMiejsca,NumerMiejsca,VIP) 
values(2,11,'N');

insert into Miejsce(IdMiejsca,NumerMiejsca,VIP) 
values(3,12,'N');

insert into Miejsce(IdMiejsca,NumerMiejsca,VIP) 
values(4,13,'N');

insert into Miejsce(IdMiejsca,NumerMiejsca,VIP) 
values(5,15,'N');

insert into Miejsce(IdMiejsca,NumerMiejsca,VIP) 
values(6,20,'T');

insert into Miejsce(IdMiejsca,NumerMiejsca,VIP) 
values(7,21,'T');

insert into Miejsce(IdMiejsca,NumerMiejsca,VIP) 
values(8,22,'T');

insert into gosc(IdGosc,Imie,Nazwisko,DataUrodzenia)
values(1,'Denys','Lehonkov','03/14/1998');


-- End of file.

