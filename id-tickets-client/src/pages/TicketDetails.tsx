import React from "react";
import { Box, Typography, Paper } from "@mui/material";


// for t

interface TicketProps{
    ticketId: number
}

const Ticket: React.FC<TicketProps> = ({
    ticketId
  }) => {
    return (
      <Paper elevation={2}>
        <Box p={3}>
          <Typography variant="h4">Id: {ticketId}</Typography>
          <Typography variant="subtitle1">Title: </Typography>
          <Typography variant="subtitle1">Requested By: </Typography>
            <div>
                <Typography variant="subtitle1">Description: </Typography>
                <Typography variant="body1" gutterBottom>Lipsum Stuff</Typography>
            </div>     
          <Typography variant="subtitle1">Date Created: </Typography>
          <Typography variant="subtitle1">Date Updated: </Typography>

          <Typography variant="subtitle1">Attachments: </Typography>

          <Typography variant="subtitle1">Comments: </Typography>
    
      
        </Box>
      </Paper>
    );
  };
  
  export default Ticket;
  