\c postgres postgres
drop database study;
drop user study;
create user study with encrypted password '1234';
create database study;
grant all on database study to study;
\c study study
create table topic(
    id serial primary key,
    title text not null,
    parentTopicId int null,
    foreign key(parentTopicId) references topic(id));


create table lecture(
    id serial primary key,
    date timestamp not null,
    semester int null,
    topicId  int not null,
    title text not null,
    path text not null,
    foreign key(topicId) references topic(id));
