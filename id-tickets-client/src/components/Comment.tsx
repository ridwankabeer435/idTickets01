import React from 'react';
import { Avatar, Box, Typography } from "@mui/material";
import { Comment } from '../datasamples/CommentsSample';

interface CommentsProps {
  commentItem: Comment;
}


// have the box
const CommentUnit: React.FC<CommentsProps> = () => {
  return (
    <Box margin={2}>
      
      <Avatar variant="circular"></Avatar>
      <Typography variant="subtitle2">Username</Typography>
      <Typography variant="body1">Comment Body</Typography>
      
    </Box>
  );
};

export default CommentUnit;
