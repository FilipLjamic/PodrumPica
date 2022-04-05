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

Select Pice.Naziv, Kolicina, Adresa  from Magacin
Join Pice on Pice.ID = ID_Pica
Where Pice.Naziv = 'Banjalucko pivo' and Kolicina > 5

Create table Narudzbina(

	ID INT PRIMARY KEY identity(1,1),
	Adresa_Magacina nvarchar(25),
	ID_Pica INT,
	Prodaja bit,
	Kolicina int,
	foreign key(ID_Pica) references Pice(ID)

)

insert into pice
values
('Banjalucko', 55, 'fff', 4.5)

create trigger Provera
on magacin
after update
as
delete from magacin
where kolicina = 0

SELECT pice.*, magacin.kolicina, magacin.adresa FROM pice join magacin on magacin.id_pica = pice.id

select * from magacin
select * from pice

insert into magacin
values
(1, 10, 'Paunova')