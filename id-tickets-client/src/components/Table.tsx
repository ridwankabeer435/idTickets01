import React from 'react';
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import Paper from '@mui/material/Paper';
import { ticketArray } from '../datasamples/TicketSamples';



  export default function TicketTable() {
    return (
      <TableContainer component={Paper}>
        <Table sx={{ minWidth: 650}} aria-label="simple-table">
          <TableHead>
            <TableRow>
              <TableCell>Id</TableCell>
              <TableCell align="left">Requested By</TableCell>
              <TableCell align="left">Title</TableCell>
              <TableCell align="left">Date Added</TableCell>
              <TableCell align="left">Date Updated(g)</TableCell>
              <TableCell align="left">Progress Status</TableCell>
              <TableCell align="left">Priority</TableCell>

            </TableRow>
          </TableHead>
          <TableBody>
            {ticketArray.map((entry) => (
              <TableRow
                key={entry.id}
                sx={{ '&:last-child td, &:last-child th': { border: 0 } }}
              >
                <TableCell component="th" scope="row">
                  {entry.id}
                </TableCell>
                <TableCell align="left">{entry.requestor}</TableCell>
                <TableCell align="left">{entry.title}</TableCell>
                <TableCell align="left">{entry.dateAdded.toDateString()}</TableCell>
                <TableCell align="left">{entry.dateUpdated.toDateString()}</TableCell>
                <TableCell align="left">{entry.status}</TableCell>
                <TableCell align="left">{entry.priority}</TableCell>
              </TableRow>
            ))}
          </TableBody>
        </Table>
      </TableContainer>
    );
  }