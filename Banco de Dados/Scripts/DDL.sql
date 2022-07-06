Use Teste_Pratico;
Go

Insert Into Tipo_Usuario (Nome_Tipo)
Values('geral'),
('admin'),
('root');
Go

Select * From Tipo_Usuario

Insert Into Usuarios(Id_Tipo,Nome,Email,Senha)
Values(1, 'Leonardo', 'geral@gmail.com', 'geral@123'),
(2, 'Administrador', 'admin@gmail.com', 'admin@123'),
(3, 'Root', 'root@gmail.com', 'root@123');
Go

Select * From Usuarios;
Go