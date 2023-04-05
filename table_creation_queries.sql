/*
CREATE TABLE users (
    id SERIAL PRIMARY KEY,
    first_name VARCHAR(75) NOT NULL,
    last_name VARCHAR(50) NOT NULL,
    
    email VARCHAR(255) NOT NULL,
    password_hash VARCHAR(255) NOT NULL,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    is_it bool NOT NULL DEFAULT FALSE,
    is_supervisor bool NOT NULL DEFAULT FALSE

);
*/


/*
CREATE TABLE departments(
    id SERIAL PRIMARY KEY,
    title VARCHAR(40) NOT NULL
    
);
*/

/*
CREATE TABLE tickets(
    id SERIAL PRIMARY KEY,
    requestor_id bigint not null,
    assignee_id bigint,
    title VARCHAR(80),
    description VARCHAR(255),
    status VARCHAR(10),
    priority VARCHAR(10),
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);


CREATE TABLE comments(
    id SERIAL PRIMARY KEY,
    user_id bigint not null,
    ticket_id bigint not null,
    text_content varchar not null,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP  
);
*/


/*
alter table if exists users add column department_id bigint not null;
*/

/*
alter table if exists users add constraint fk_users_departments FOREIGN KEY (department_id) REFERENCES departments (id);
*/

/*
alter table if exists comments add constraint fk_comment_ticket FOREIGN KEY (ticket_id) REFERENCES tickets (id);
alter table if exists comments add constraint fk_comment_user FOREIGN KEY (user_id) REFERENCES users (id);
*/

/*
alter table if exists users drop column password_hash;
*/

/*
CREATE TABLE files_info(
    id SERIAL PRIMARY KEY,
    name varchar(70) not null,
    type_extension varchar(15),
    uploader_id bigint not null,
    ticket_id bigint,
    comment_id bigint,
    storage_location varchar(255),
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,  
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);
*/