Create database Podrum_pica
USe Podrum_pica


Create table Pice(
	ID INT Primary key identity(1,1),
	Naziv nvarchar(25),
	Cena int,
	Proizvodjac nvarchar(25),
	Procenat_alkohola float

)

Create table Magacin(
	ID INT Primary key identity(1,1),
	ID_Pica int,
	Kolicina int,
	Adresa nvarchar(25),
	foreign key(ID_Pica) references Pice(ID)
)

Create table Narudzbina(
	ID INT PRIMARY KEY identity(1,1),
	Adresa_Magacina nvarchar(25),
	ID_Pica INT,
	Prodaja bit,
	Kolicina int,
	foreign key(ID_Pica) references Pice(ID)
)


create trigger Provera
on magacin
after update
as
delete from magacin
where kolicina = 0


insert into pice
values
('Banjalucko', 55, 'fff', 4.5)

Select Pice.Naziv, Kolicina, Adresa  from Magacin
Join Pice on Pice.ID = ID_Pica
Where Pice.Naziv = 'Banjalucko pivo' and Kolicina > 5

SELECT pice.*, magacin.kolicina, magacin.adresa FROM pice join magacin on magacin.id_pica = pice.id

select * from magacin
select * from pice
select * from pice join magacin on pice.id = magacin.id_pica

insert into magacin
values
(1, 10, 'Paunova')

select magacin.id from magacin
join pice on magacin.id_pica = pice.id
where magacin.adresa = 'Paunova'
and pice.naziv = 'Fanta'
and pice.cena = 60
and pice.proizvodjac = 'CocaCola Company'
and pice.procenat_alkohola = 0

select magacin.id from magacin join pice on magacin.id_pica = pice.id where magacin.adresa = '10' and pice.naziv = 'Fanta' and pice.cena = 60 and pice.proizvodjac = 'CocaCola Company' and pice.procenat_alkohola = 0

select id from pice where