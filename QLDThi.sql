create database QLDThi

create table tblUser(
	id int identity(1,1) not null primary key,
	username varchar (20) not null,
	pass_word varchar (max) not null,
	isAdmin bit NOT NULL,
)

create table tblExam(
	 id int identity(1,1) not null primary key,
	 code nvarchar (10) not null,
	 listQuestionId nvarchar (max),
 )

create table tblQuestion(
	 id int identity(1,1) not null primary key,
	 question nvarchar (max) not null,
	 listAnswer nvarchar (max),
	 correctAnswer nvarchar (max),
 )

  create table tblResult(
	 id int identity(1,1) not null primary key,
	 examId int not null,
	 userId int not null,
	 startTime varchar(50) null,
	 endTime varchar(50) null,
	 score int null,
	 totalScore int null
 );

 /*tạo proc đăng ký */
create proc Signup 
@username varchar(20), 
@password varchar(max),
@isAdmin bit
as
insert into tblUser (username,pass_word, isAdmin)
values (@username,@password, @isAdmin)

/*tạo proc kiểm tra đăng nhập */
create proc LoginCheck 
@username varchar(20), 
@password varchar(max)
as
select id, username, pass_word, isAdmin
from tblUser 
where username=@username 

/*tạo proc kiểm tra người dùng tồn tại */
create proc CheckExistUser @username varchar(20)
as
select username 
from tblUser 
where username=@username

/*tạo proc đổi mật khẩu */
create proc ChangePW 
@username varchar(20),
@newPW varchar (max)
as
update tblUser
set pass_word = @newPW 
where username = @username

 /*tạo proc soạn câu hỏi */
create proc addQuestion
@question nvarchar(max), 
@listAnswer nvarchar(max), 
@correctAnswer nvarchar(max)
as
insert into tblQuestion (question, listAnswer, correctAnswer)
values (@question, @listAnswer, @correctAnswer)

/*tạo proc lấy danh sách câu hỏi */
create proc getListQuestion
as 
select * from tblQuestion

/*proc sửa câu hỏi*/ 
CREATE proc editQuestion 
@id int,
@question nvarchar (max),
@listAnswer nvarchar (max),
@correctAnswer nvarchar(max)
as 
update tblQuestion
set question = @question, listAnswer = @listAnswer, correctAnswer = @correctAnswer
WHERE id = @id

/*proc xóa câu hỏi*/
create proc deleteQuestion 
@id int
as 
DELETE
from tblQuestion
WHERE id = @id

/*proc kiểm tra mã đề thi*/
create proc CheckExistExam @code nvarchar(10)
as
select code 
from tblExam
where code=@code

/*proc thêm mới đề thi*/
create proc addExam 
@code nvarchar(10),
@listQuestionId varchar(max)
as
insert into tblExam(code, listQuestionId)
values (@code, @listQuestionId)

/*tạo proc lấy danh sách câu hỏi */
create proc getListExam
as 
select * from tblExam

/*proc sửa câu hỏi*/ 
CREATE proc editExam 
@id int,
@listQuestionId nvarchar (max)
as 
update tblExam
set listQuestionId = @listQuestionId
WHERE id = @id

/*proc xóa câu hỏi*/
create proc deleteExam
@id int
as 
DELETE
from tblExam
WHERE id = @id

/*proc lấy chi tiết câu hỏi*/
create proc getQuestionById
@id int
as 
BEGIN
  SELECT *
  FROM tblQuestion
  where tblQuestion.id = @id
END

EXEC getQuestionById @id = 3

/*proc thêm mới kết quả thi*/
create proc addResult
@userId int,
@examId int,
@score int,
@totalScore int,
@startTime varchar(50),
@endTime varchar(50)
as
insert into tblResult(userId, examId, score, startTime, endTime, totalScore)
values (@userId, @examId, @score, @startTime, @endTime, @totalScore)

/*tạo proc lấy danh sách kết quả */
create proc getListResult
@userId int
as 
select tblExam.code, startTime, endTime, score, totalScore from tblResult inner join tblExam
on tblResult.examId = tblExam.id 
where userId = @userId

exec getListResult 6







INSERT INTO tblQuestion (question, listAnswer, correctAnswer) 
VALUES (N'Hội văn hoá cứu quốc được thành lập vào thời gian nào?', N'[năm 1941, năm 1943, năm 1944, năm 1945]' , N'năm 1941');

INSERT INTO tblQuestion (question, listAnswer, correctAnswer) 
VALUES (N'Chiến khu Hoà - Ninh - Thanh còn có tên là gì?', N'[Trần Hưng Đạo, Hoàng Hoa Thám, Lê Lợi, Quang Trung]' , N'Quang Trung');

INSERT INTO tblQuestion (question, listAnswer, correctAnswer) 
VALUES (N'Trong cao trào kháng Nhật cứu nước, phong trào "Phá kho thóc của Nhật để giải quyết nạn đói" đã diễn ra mạnh mẽ ở đâu?', N'[Đồng bằng Nam Bộ, Đồng bằng Nam Bộ, Đồng bằng Bắc Bộ, Đồng bằng Trung Bộ]' , N'Đồng bằng Bắc Bộ');

INSERT INTO tblQuestion (question, listAnswer, correctAnswer) 
VALUES (N'Hội nghị quân sự cách mạng Bắc kỳ họp vào thời gian nào?', N'[tháng 3-1945, tháng 4-1945, tháng 5-1945, tháng 6-1945]' , N'tháng 5-1945');

INSERT INTO tblQuestion (question, listAnswer, correctAnswer) 
VALUES (N'Uỷ ban dân tộc giải phóng do ai làm chủ tịch?', N'[Hồ Chí Minh, Trường Chinh, Phạm Văn Đồng, Võ Nguyên Giáp]' , N'Trường Chinh');

INSERT INTO tblQuestion (question, listAnswer, correctAnswer) 
VALUES (N'Sau ngày tuyên bố độc lập Chính phủ lâm thời đã xác định các nhiệm vụ cấp bách cần giải quyết:', N'[Chống ngoại xâm, Chống ngoại xâm và nội phản, Diệt giặc đói, giặc dốt và giặc ngoại xâm, Cả ba phương án trên]' , N'Cả ba phương án trên');

INSERT INTO tblQuestion (question, listAnswer, correctAnswer) 
VALUES (N'Hội văn hoá cứu quốc được thành lập vào thời gian nào?', N'[năm 1941, năm 1943, năm 1944, năm 1945]' , N'năm 1941');


