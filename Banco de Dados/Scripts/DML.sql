Create Database Teste_Pratico;
Go

Use Teste_Pratico;
Go

Create Table Tipo_Usuario(
Id_Tipo Int Primary Key Identity,
Nome_Tipo Varchar(20)
);
Go

Create Table Usuarios(
Id_Usuario Int Primary Key Identity,
Id_Tipo Int Foreign Key References Tipo_Usuario(Id_Tipo),
Nome Varchar(100) Not Null,
Email Varchar(300) Not Null Unique,
Senha Varchar(100) Not Null,
Status_Conta Bit Default('True'),
Imagem Varchar(235)
);
Go
