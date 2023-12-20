---------------------
-- СОЗДАЁМ ТАБЛИЦЫ --
--------------------- 

CREATE TABLE Job (
    ID uuid PRIMARY KEY,
    Name varchar(50),
    Description varchar(200)
);

CREATE TABLE Candidate (
    ID uuid PRIMARY KEY,
    LastName varchar(50),
    FirstName varchar(50),
    Surname varchar(50),
    FullName varchar(200),
    Age integer,
	CONSTRAINT fk_job
        REFERENCES Job(ID)	
);

CREATE TABLE Skill (
    ID uuid PRIMARY KEY,
    Name varchar(50),
    Description varchar(200)
);

CREATE TABLE Status (
    ID uuid PRIMARY KEY,
    Description varchar(200),
    CandidateID uuid
);

COMMIT;

---------------------------------
-- НАПОЛНЯЕМ ТЕСТОВЫМИ ДАННЫМИ --
---------------------------------

-- таблица JOB
INSERT INTO job(
	id, name, description)
	VALUES ('b75a4409-6b22-487a-b2e7-aa32cab483e0', 
			'Full Stack волшебник', 
			'Писать хорошо спроектированный, поддерживаемый и тестируемый код. Оценивать сложность и стоимость решений.');

INSERT INTO job(
	id, name, description)
	VALUES ('8b0183f8-8745-4572-9d8c-98af828ac5fd', 
			'Ниндзя по работе с клиентами', 
			'Ведение заказов с начала и до конца, решение проблем клиентов на протяжении всего жизненного цикла заказа.');

INSERT INTO job(
	id, name, description)
	VALUES ('04180019-8e66-4ce7-9fde-8565cd906337', 
			'Ковбой оптимизации конверсий', 
			'Аналитик по оптимизации вебсайта, ответственный за проведение тестов и анализа производительности.');

COMMIT;

-- таблица CANDIDATE
INSERT INTO candidate(
	id, lastname, firstname, surname, fullname, age)
	VALUES ('75fd1d1d-3806-4f0b-94e5-196c75041573', 
			'Иванов', 
			'Иван', 
			'Иванович', 
			'Иванов Иван Иванович', 
			'27');

INSERT INTO candidate(
	id, lastname, firstname, surname, fullname, age)
	VALUES ('5ddcdac5-e857-4a8f-8ee7-a23153792a6b', 
			'Петров', 
			'Пётр', 
			'Петрович', 
			'Петров Пётр Петрович', 
			'46');

INSERT INTO candidate(
	id, lastname, firstname, surname, fullname, age)
	VALUES ('963a8d16-be84-4ae1-8cf9-e21b8dcfa832', 
			'Сидоров', 
			'Сидр', 
			'Сидорович', 
			'Сидоров Сидр Сидорович', 
			'34');

COMMIT;

-- таблица SKILL
INSERT INTO skill(
	id, name, description)
	VALUES ('43989e68-414b-4b2d-8034-57e9e1d72d09', 
			'Эффективные коммуникации', 
			'Уметь слушать, давать обратную связь, договариваться, делиться информацией.');

INSERT INTO skill(
	id, name, description)
	VALUES ('f3f15745-cea9-4e4c-9f6d-e5e1038dcc9f', 
			'Понимание бизнес-процессов', 
			'Понимать и умение оптимизировать на должном уровне, для повышение эффективности и производительности бизнеса.');
			
INSERT INTO skill(
	id, name, description)
	VALUES ('eeb21c0b-ad35-41bb-8d25-5e0be9344360', 
			'Визуализация результатов работы', 
			'Качественное представление результатов, умение быстро и эффективно донести информацию для руководства.');	
			
INSERT INTO skill(
	id, name, description)
	VALUES ('2c500359-44bf-4278-8d61-7cb59d0b1f3d', 
			'Программирование на C#', 
			'Умение писать качественный, самодокументируемый код, проводить код-ревью и рефакторинг.');	
			
COMMIT;

-- таблица STATUS
INSERT INTO status(
	id, description, candidateid)
	VALUES ('538fa258-33c4-4f83-b731-c34cfc0811fd', 
			'Первичная подача', 
			'75fd1d1d-3806-4f0b-94e5-196c75041573');

INSERT INTO status(
	id, description, candidateid)
	VALUES ('8cbffb66-c40f-473e-a40d-6215a12690d4', 
			'Техническое интервью', 
			'5ddcdac5-e857-4a8f-8ee7-a23153792a6b');

INSERT INTO status(
	id, description, candidateid)
	VALUES ('df6963fe-1333-4f1c-97d4-986397b98c0f', 
			'Собеседование с руководителем', 
			'963a8d16-be84-4ae1-8cf9-e21b8dcfa832');

COMMIT;