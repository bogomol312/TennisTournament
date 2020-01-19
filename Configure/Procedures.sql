
------ procedury stworzone w ramach projektu --------

------ TABELA MECZ -------


--dodanie nowego meczu
CREATE procedure TINPROaddNewMecz(
@dataMeczu date,
@wynik1 int,
@wynik2 int,
@idzawodnik1 int,
@idzawodnik2 int,
@idsedzia int
)
as begin

		declare @idmeczu int = (select max(idmeczu)+1 from mecz);

		if @idmeczu is null
			set @idmeczu = 1;

		insert into Mecz(IdMeczu,Data,Wynik1,Wynik2,IdZawodnik1,IdZawodnik2)
			values(@idmeczu,@dataMeczu,@wynik1,@wynik2,@idzawodnik1,@idzawodnik2);

		insert into SedziaNaMeczu(IdMeczu,IdSedzia) 
			values(@idmeczu,@idsedzia);
end;

GO

--aktualizacja meczu 
CREATE procedure TINPROupdateMecz(
@idmecz int,
@data date,
@wynik1 int,
@wynik2 int,
@idz1 int,
@idz2 int,
@idsedzia int
)
as begin

	update sedziaNaMeczu set IdSedzia=@idsedzia where IdMeczu=@idmecz;
	update mecz set data=@data,Wynik1=@wynik1,Wynik2=@wynik2,IdZawodnik1=@idz1,IdZawodnik2=@idz2 where IdMeczu=@idmecz;
	end;

	GO

--usuniecie meczu
CREATE procedure TINPROdeleteMecz(
@idMecz int)
as begin

	delete from KibicNaMiejscu where IdMeczu=@idMecz;

	delete from SedziaNaMeczu where IdMeczu=@idMecz;

	delete from mecz where IdMeczu=@idMecz;

	end;
	
	GO
------ TABELA ZAWODNIK -------
	

--dodanie nowego zawodnika
CREATE procedure TINPROaddNewZawodnik(
@Imie varchar(25),
@Nazwisko varchar(25),
@DataUrodzenia date,
@Nacjonalnosc varchar(2),
@plec varchar(1),
@trener int)
as begin 

declare @wiek int = datediff(year, @DataUrodzenia, GETDATE());
declare @idgosc int = (select max(idgosc)+1 from gosc);



	insert into gosc(Idgosc,Imie,Nazwisko,DataUrodzenia)
		values(@idgosc,@Imie,@Nazwisko,@DataUrodzenia);

	insert into zawodnik(IdZawodnik,Nacjonalnosc,Wiek,Plec,Trener)
		values (@idgosc,@Nacjonalnosc,@wiek,@plec,@trener);

end;

GO

--aktualizacja zawodnika
CREATE procedure TINPROupdZawodnik(
@idgosc int,
@imie varchar(25),
@nazwisko varchar(25),
@dataurod date,
@kraj varchar(2),
@plec varchar(1),
@trener int)
as begin

declare @wiek int = datediff(year, @dataurod, GETDATE()); 

update zawodnik set Nacjonalnosc=@kraj,Wiek=@wiek,Plec=@plec,Trener=@trener where IdZawodnik=@idgosc;
update gosc set imie=@imie,Nazwisko=@nazwisko,DataUrodzenia=@dataurod where IdGosc=@idgosc;


end;

GO

--usuniecie zawodnika
CREATE procedure TINPROdeleteZawodnikWithMecz(
@idzawodnik int
) as begin 
	
	delete from sedziaNaMeczu where IdMeczu=any(select IdMeczu from Mecz where IdZawodnik1=@idzawodnik);
	delete from mecz where IdZawodnik1=@idzawodnik;
	delete from Zawodnik where IdZawodnik=@idzawodnik;
	end;
	
------ TABELA GOSC/KIBIC/Sedzia -------

GO

--dodanie nowego kibica
CREATE procedure TINPROaddGosc(
@imie varchar(15),
@nazwisko varchar(38),
@DataUrodzenia date
)as begin
	
	declare @idgosc int = (select Max(idgosc)+1 from Gosc);

	insert into gosc(IdGosc,Imie,Nazwisko,DataUrodzenia)
		values(@idgosc,@imie,@nazwisko,@DataUrodzenia);

	insert into Kibic(IdKibic)
		values(@idgosc);

	end;
	
	GO

--dodanie nowego Sedziego
CREATE procedure TINPROaddSedzia(
@imie varchar(15),
@nazwisko varchar(38),
@DataUrodzenia date
)as begin
	
	declare @idgosc int = (select Max(idgosc)+1 from Gosc);

	insert into gosc(IdGosc,Imie,Nazwisko,DataUrodzenia)
		values(@idgosc,@imie,@nazwisko,@DataUrodzenia);

	insert into sedzia(IdSedzia) values(@idgosc);

	end;

