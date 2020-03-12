drop database if exists usuarios;

create database usuarios;

use usuarios;

create table users(
id int not null primary key auto_increment,
uName varchar(22),
pWord varchar(16),
eMail varchar(50)
);



-- insert into `users` (`eMail`, `uName`, `pWord` ) values ();

select * from users;