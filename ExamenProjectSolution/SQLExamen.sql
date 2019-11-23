create database dbExamen
---WORKING CODES---
select * from tblCandidates
truncate table tblCandidates

alter table tblCandidates
drop column degree
alter table tblCandidates
add name varchar(100)

select * from tblCandidates
 table tblCandidates
delete from tblCandidates where username in  (select username from tblCandidates)

---- creating store proc for inserting the  data---
alter table tblCandidates values(degree varchar(50),
phone varchar(20) ,email varchar(100),username varchar(50) primary key,password varchar(50),yop varchar(20),name varchar(100))


alter proc proc_RegisterCandidates(@p_phone varchar(20),@p_email varchar(100),@p_username varchar(50),@p_password varchar(50),@p_name varchar(100))
as
begin
insert into tblCandidates values(@p_phone,
@p_email,@p_username,@p_password,@p_name)
end

exec proc_RegisterCandidates'B.Esc','72722882','ahmdgd@shsgs.com','ur','535','2019','ahamed'

--VALIDATION--

create proc proc_UserValidation(@p_Username varchar(50), @p_password varchar(50))
as
begin
select * from tblCandidates where username = @p_Username AND password = @p_password
end

exec proc_UserValidation '627272@','gssg535'

--PAYMENT--

create table tblPayment(CandidateId varchar(20) primary key, Username varchar(50) foreign key references tblCandidates(username),
TestModel varchar(50),Payment varchar(10))

create proc proc_Payment(@p_CandidateId varchar(20), @p_Username varchar(50),@p_TestModel varchar(50),@p_Payment varchar(10))
as
begin
insert into tblPayment values(@p_CandidateId,@p_Username,@p_TestModel,@p_Payment)
end

select * from tblPayment

exec proc_Payment '2356','627272@','Aptitude','yes'
--Questions TABLE --

create table tblQuestions(SNo int constraint pk_Sn primary key,TestModel varchar(50),Questions varchar(1000),
Answers varchar(100),option1 varchar(100),option2 varchar(100),option3 varchar(100),option4 varchar(100),
Mark int)

Insert into tblQuestions (SNo,TestModel,Questions,Answers,option1,option2,option3,option4,Mark) values(01,'Aptitude',
'A sum of money at simple interest amounts to Rs. 815 in 3 years and to Rs. 854 in 4 years','Rs.698','Rs.650','Rs.690','Rs.698','Rs.700',1)

Insert into tblQuestions (SNo,TestModel,Questions,Answers,option1,option2,option3,option4,Mark) values(02,'Aptitude',
'A sum fetched a total simple interest of Rs. 4016.25 at the rate of 9 p.c.p.a. in 5 years. What is the sum?','Rs.8925',
'Rs.4462.50',
'Rs.8032.50',
'Rs.8900',
'Rs.8925',
1)

Insert into tblQuestions (SNo,TestModel,Questions,Answers,option1,option2,option3,option4,Mark) values(03,'Aptitude',
'The percentage increase in the area of a rectangle, if each of its sides is increased by 20% is:',
'44%',
'42%',
'43%',
'44%',
'45%',
1)

Insert into tblQuestions (SNo,TestModel,Questions,Answers,option1,option2,option3,option4,Mark) values(04,'Aptitude',
'Three unbiased coins are tossed. What is the probability of getting at most two heads?','7/8',
'2/5',
'5/6',
'3/4',
'7/8',
1)

Insert into tblQuestions (SNo,TestModel,Questions,Answers,option1,option2,option3,option4,Mark) values(05,'Aptitude',
'What is the probability of getting a sum 9 from two throws of a dice?','1/9',
'12/14',
'2/3',
'1/9',
'1/7',
1)

Insert into tblQuestions (SNo,TestModel,Questions,Answers,option1,option2,option3,option4,Mark) values(06,'Aptitude',
'Which one of the following is not a prime number?','91',
'61',
'51',
'31',
'91',
1)

Insert into tblQuestions (SNo,TestModel,Questions,Answers,option1,option2,option3,option4,Mark) values(07,'Aptitude',
'What least number must be added to 1056, so that the sum is completely divisible by 23 ?','2',
'2',
'18',
'21',
'3',
1)

Insert into tblQuestions (SNo,TestModel,Questions,Answers,option1,option2,option3,option4,Mark) values(08,'Aptitude',
'In a 100 m race, A can give B 10 m and C 28 m. In the same race B can give C','20m',
'18m',
'20m',
'27m',
'9m',
1)

Insert into tblQuestions (SNo,TestModel,Questions,Answers,option1,option2,option3,option4,Mark) values(09,'Aptitude',
'In a 100 m race, A beats B by 10 m and C by 13 m. In a race of 180 m, B will beat C by','6m',
'5.4m',
'4.5m',
'5m',
'6m',
1)

Insert into tblQuestions (SNo,TestModel,Questions,Answers,option1,option2,option3,option4,Mark) values(10,'Aptitude',
'If log 2 = 0.3010 and log 3 = 0.4771, the value of log5 512 is','3.786',
'2.870',
'2.967',
'3.786',
'3.912',
1)


Insert into tblQuestions (SNo,TestModel,Questions,Answers,option1,option2,option3,option4,Mark) values(11,'Technical',
'Which of this method of class String is used to obtain a length of String object?','length()',
'get()',
'lengthof()',
'length()',
'sizeof()',
1)

Insert into tblQuestions (SNo,TestModel,Questions,Answers,option1,option2,option3,option4,Mark) values(12,'Technical',
' Which of these class is superclass of String and StringBuffer class?','java.lang',
'java.lang',
'java.util',
'ArrayList',
'none',
1)


Insert into tblQuestions (SNo,TestModel,Questions,Answers,option1,option2,option3,option4,Mark) values(13,'Technical',
'What is it called if an object has its own lifecycle and there is no owner?','Association',
'Aggregation',
'Encapsulation',
'Composition',
'Association',
1)


Insert into tblQuestions (SNo,TestModel,Questions,Answers,option1,option2,option3,option4,Mark) values(14,'Technical',
'When does method overloading is determined?','At compile time',
'At run time',
'At compile time',
'At execution time',
'At coding time',
1)

Insert into tblQuestions (SNo,TestModel,Questions,Answers,option1,option2,option3,option4,Mark) values(15,'Technical',
'Which of the following is a type of polymorphism in Java?','Compile time polymorphism',
'Compile time polymorphism',
'Execution time polymorphism',
'Multiple polymorphism',
'Multilevel polymorphism',
1)


Insert into tblQuestions (SNo,TestModel,Questions,Answers,option1,option2,option3,option4,Mark) values(16,'Technical',
'Which of the following is not OOPS concept in Java?','compilation',
'Inheritance',
'Abstraction',
'Polymorphism',
'compilation',
1)


Insert into tblQuestions (SNo,TestModel,Questions,Answers,option1,option2,option3,option4,Mark) values(17,'Technical',
'Which of the following is a method having same name as that of its class?','constructor',
'finalize',
'delete',
'constructor',
'class',
1)


Insert into tblQuestions (SNo,TestModel,Questions,Answers,option1,option2,option3,option4,Mark) values(18,'Technical',
'Which of these constructors is used to create an empty String object?','String()',
'String()',
'String(void)',
'String(0)',
'none',
1)


Insert into tblQuestions (SNo,TestModel,Questions,Answers,option1,option2,option3,option4,Mark) values(19,'Technical',
'What is the return type of Constructors?','none of the mentioned above',
'int',
'float',
'void',
'none of the mentioned above',
1)


Insert into tblQuestions (SNo,TestModel,Questions,Answers,option1,option2,option3,option4,Mark) values(20,'Technical',
'Which one of these lists contains only Java programming language keywords?','goto, instanceof, native, finally, default, throws',
'class, if, void, long, Int, continue',
'goto, instanceof, native, finally, default, throws',
'try, virtual, throw, final, volatile, transient',
'strictfp, constant, super, implements, do',1)


----PROC FOR 
select * from tblQuestions

alter proc proc_GetAllQuestions(@TestModel varchar(50))
as
begin
  select * from tblQuestions where TestModel=@TestModel
  end


exec proc_GetAllQuestions 'Aptitude'
-------------------

----SCORE TABLE-----

create table tblScores(CandidateId varchar(20) primary key, Username varchar(50) 
foreign key references tblCandidates(Username),TestModel varchar(50),Mark int)

create proc proc_GetAllScores(@Username varchar(50),@CandidateId varchar(20) out,  @TestModel varchar(50) out,@Mark int out)
as
begin
select @CandidateId = CandidateId , @TestModel = TestModel, @Mark = Mark from tblScores where Username= @Username
end

select * from tblScores
truncate table tblScores

---------------------------------

--INSERT into SCORES--
create proc proc_InsertIntoScores(@CandidateId varchar(20),@Username varchar(50),  @TestModel varchar(50),@Mark int)
as
begin 
insert into tblScores values(@CandidateId,@Username,@TestModel,@Mark)
end
sp_help tblScores
----------------------------
--- GETTING TEST MODELS FROM QUESTIONS TABLE---
alter proc proc_GetAllTestModels 
as 
begin
select TestModel from tblQuestions group by TestModel
end

declare 
@TestModel varchar(50)
exec proc_GetAllTestModels 
select @TestModel


create pro 
select * from tblScores

select CandidateId,TestModel,Mark from tblS
------------------------------------------------------------------------------------------
create proc proc_CheckPayment(@username varchar(50),@candidate_id varchar(20))
as
begin
select * from tblPayment where Username=@username AND CandidateId = @candidate_id
end

create proc proc_CheckUserInPaymentTable(@username varchar(50),@testmodel varchar(50))
as
begin
select * from tblPayment where Username=@username AND TestModel = @testmodel
end
-------------------------------------------------------------------------------------------------
create table tblTestScorestemp(sno int ,options varchar(100))

alter table tblTestScorestemp
drop column unique_id
alter proc proc_InsertTempScore(@sno int ,@options varchar(100))
as
begin
insert into tblTestScorestemp values (@sno,@options)
end
--------------------------------------------------------
alter proc procSelectCount
as
begin
select count(*) from tblTestScorestemp
end
exec procSelectCount
select * from tblTestScorestemp
 
 ------------------------------------------------------------------------------------------
 ----TRUNCATE TABLE TEMP SCORES----
create proc proc_TruncateScores
as
begin
truncate table tblTestScorestemp
end
---------------------------------------------------------------------------------------------
create proc proc_DeleteUser(@Username varchar(50),@testmodel varchar(50))
as
begin 
delete from tblPayment where  Username = @Username AND TestModel = @testmodel
end
select * from tblPayment
------
exec proc_DeleteUser 'Ahamed','Aptitude'

------------------------------

insert into tblPayment values('1256','hamed','Technical',1)
select * from tblPayment

alter proc proc_GetCandID(@username varchar(50),@testmodel varchar(50))
as begin
 select CandidateId from tblPayment where Username=@username AND TestModel=@testmodel
end

exec proc_GetCandID 'Ahamed','Technical'

create table tblTempScores(candidateid varchar(20),mark int)
-------------------------------------
create proc proc_UpdateTempScore(@candidateid varchar(20), @mark int)
as
begin
insert into tblTempScores values (@candidateid,@mark)
end
----------------------------
create proc proc_TruncateScoreCalculate
as
begin
truncate table tblTempScores
end
exec proc_TruncateScoreCalculate

select * from tblTempScores
----------------------------------------
alter proc proc_InsertTempScore(@sno int ,@options varchar(100))
as
begin
insert into tblTestScorestemp values (@sno,@options)
end




---------------------------------

alter proc proc_Calculate
as
begin 
select sum(mark) from tblTempScores group by candidateid
end

-----------

create proc proc_UpdateScores(@candidateid varchar(20), @mark int)
as
begin
update tblScores set Mark = Mark+@mark where CandidateId=@candidateid
end

select * from tblPayment
delete from tblPayment where Username = 'Ahamed'


Update tblQuestions set Answers='Option3'
where SNo=1

Update tblQuestions set Answers='Option4'
where SNo=2

Update tblQuestions set Answers='Option3'
where SNo=3

Update tblQuestions set Answers='Option4'
where SNo=4

Update tblQuestions set Answers='Option3'
where SNo=5

Update tblQuestions set Answers='Option4'
where SNo=6

Update tblQuestions set Answers='Option1'
where SNo=7

Update tblQuestions set Answers='Option2'
where SNo=8

Update tblQuestions set Answers='Option4'
where SNo=9

Update tblQuestions set Answers='Option3'
where SNo=10

Update tblQuestions set Answers='Option3'
where SNo=11

Update tblQuestions set Answers='Option1'
where SNo=12

Update tblQuestions set Answers='Option4'
where SNo=13

Update tblQuestions set Answers='Option2'
where SNo=14

Update tblQuestions set Answers='Option1'
where SNo=15

Update tblQuestions set Answers='Option4'
where SNo=16

Update tblQuestions set Answers='Option3'
where SNo=17

Update tblQuestions set Answers='Option1'
where SNo=18

Update tblQuestions set Answers='Option4'
where SNo=19

Update tblQuestions set Answers='Option2'
where SNo=20
-----------------

--check for user in viewprofile---
alter proc proc_CheckUserinScores(@username varchar(50))
as
begin
select * from tblScores where Username = @username
end



-----------------------------------
truncate table tblTestScorestemp
truncate table tblTempScores
----------------------------------------------