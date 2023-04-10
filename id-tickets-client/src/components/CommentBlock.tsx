import React from "react";
import {  Box, Container, IconButton, TextField } from "@mui/material";
import Paper from "@mui/material/Paper";
import AddCircleIcon from '@mui/icons-material/AddCircle';
import SendIcon from '@mui/icons-material/Send';
import CommentUnit from "./Comment";
import { Comment } from '../datasamples/CommentsSample';



interface CommentsProps{
    comments: Comment[]
}


// take a list of comment
// use the commemnt block to add comment to the list of comments
// make sure to keep track of the user id and the ticket id
export default function CommentBlock(props: CommentsProps){
    return(
        <Box>
            <Paper elevation={2}>
                {
                    props.comments.map((commmentItem) =>(
                        <CommentUnit commentItem={commmentItem}></CommentUnit>
                    ))
                }

            </Paper>
            <Container >
                <IconButton aria-label="add-attachment">
                    <AddCircleIcon />
                </IconButton>
                <TextField
                    hiddenLabel
                    id="outlined-textarea"
                    placeholder="Add a comment..."
                    multiline
                />
                <IconButton aria-label="add-comment">
                    <SendIcon />
                </IconButton>
            </Container>
        </Box>
    )

}