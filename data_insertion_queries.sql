/*
INSERT INTO departments (title) VALUES 
    ('Sales'),
    ('Marketing'),
    ('Finance'),
    ('IT'),
    ('Human Resources');


INSERT INTO users (first_name, last_name, email, created_at, updated_at, is_it, is_supervisor, department_id)
VALUES
    ('John', 'Doe', 'john.doe@example.com', '2022-03-01 12:00:00', '2022-03-01 12:00:00', true, false, 1),
    ('Jane', 'Doe', 'jane.doe@example.com', '2022-03-01 13:00:00', '2022-03-01 13:00:00', false, true, 2),
    ('Sara', 'Smith', 'sara.smith@example.com', '2022-03-02 14:00:00', '2022-03-02 14:00:00', false, false, 1),
    ('Ahmed', 'Ali', 'ahmed.ali@example.com', '2022-03-02 15:00:00', '2022-03-02 15:00:00', false, false, 3),
    ('Juan', 'Garcia', 'juan.garcia@example.com', '2022-03-03 16:00:00', '2022-03-03 16:00:00', false, false, 2),
    ('Marie', 'Dupont', 'marie.dupont@example.com', '2022-03-03 17:00:00', '2022-03-03 17:00:00', false, false, 4),
    ('Jin-Soo', 'Kim', 'jin-soo.kim@example.com', '2022-03-04 18:00:00', '2022-03-04 18:00:00', false, false, 3),
    ('Emil', 'Andersson', 'emil.andersson@example.com', '2022-03-04 19:00:00', '2022-03-04 19:00:00', true, false, 1),
    ('Maria', 'Gonzalez', 'maria.gonzalez@example.com', '2022-03-05 20:00:00', '2022-03-05 20:00:00', false, false, 2),
    ('Svetlana', 'Ivanova', 'svetlana.ivanova@example.com', '2022-03-05 21:00:00', '2022-03-05 21:00:00', false, false, 4),
    ('Pablo', 'Rodriguez', 'pablo.rodriguez@example.com', '2022-03-06 22:00:00', '2022-03-06 22:00:00', false, true, 3),
    ('Hiroshi', 'Yamamoto', 'hiroshi.yamamoto@example.com', '2022-03-06 23:00:00', '2022-03-06 23:00:00', false, false, 1),
    ('Li', 'Wang', 'li.wang@example.com', '2022-03-07 00:00:00', '2022-03-07 00:00:00', true, false, 2);
   */
   
   /*
 INSERT INTO comments (user_id, ticket_id, text_content, created_at) VALUES
  (14, 1, 'This is a sample comment.', '2022-03-01 12:00:00'),
  (15, 2, 'Thanks for reporting this issue. We will investigate further.', '2022-03-01 12:30:00'),
  (16, 3, 'Please provide more details about the problem you are facing.', '2022-03-01 13:00:00'),
  (17, 4, 'I think I have encountered the same issue as well. Any updates?', '2022-03-02 14:00:00'),
  (18, 5, 'I can confirm that the issue is now resolved. Thanks for your help!', '2022-03-02 15:00:00'),
  (19, 6, 'This is a sample comment.', '2022-03-03 16:00:00'),
  (20, 7, 'We have escalated this issue to our engineering team for further investigation.', '2022-03-03 17:00:00'),
  (21, 8, 'Thanks for your feedback. We will take it into consideration.', '2022-03-04 18:00:00'),
  (22, 9, 'Can you please provide more information about the error message you are seeing?', '2022-03-04 19:00:00'),
  (23, 10, 'I have experienced this issue as well. Can you please provide a workaround?', '2022-03-05 20:00:00'),
  (24, 11, 'We are currently investigating this issue and will provide an update soon.', '2022-03-05 21:00:00'),
  (25, 12, 'This is a sample comment.', '2022-03-06 22:00:00'),
  (26, 13, 'Thanks for letting us know. We will look into this and provide an update shortly.', '2022-03-06 23:00:00'),
  (14, 1, 'Glad to hear that the issue is now resolved. Thanks for your help!', '2022-03-07 00:00:00'),
  (15, 2, 'We have identified the root cause of the problem and will deploy a fix shortly.', '2022-03-08 01:00:00'),
  (16, 3, 'This is a sample comment.', '2022-03-08 02:00:00'),
  (17, 4, 'Thanks for reporting this issue. We will investigate further.', '2022-03-09 03:00:00'),
  (18, 5, 'Please provide more details about the problem you are facing.', '2022-03-09 04:00:00'),
  (19, 6, 'I think I have encountered the same issue as well. Any updates?', '2022-03-10 05:00:00'),
  (20, 7, 'I can confirm that the issue is now resolved. Thanks for your help!', '2022-03-10 06:00:00'),
  (21, 8, 'This is a sample comment.', '2022-03-11 07:00:00');
    
   */