import React from 'react';
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import Paper from '@mui/material/Paper';
import { StringMappingType } from 'typescript';


type Ticket = {
  id: number, 
  requestor: string, 
  title: string, 
  dateAdded: Date,
  dateUpdated: Date,
  status: string,
  priority: string;
};



  // utilize useEffect hooks to populate table with data from backend

 const ticketArray: Ticket[] = [
  {
    id: 1,
    requestor: "John Doe",
    title: "Cannot access email",
    dateAdded: new Date("2022-02-05"),
    dateUpdated: new Date("2022-02-06"),
    status: "In Progress",
    priority: "High",
  },
  {
    id: 2,
    requestor: "Jane Smith",
    title: "Slow internet connection",
    dateAdded: new Date("2022-02-08"),
    dateUpdated: new Date("2022-02-09"),
    status: "Resolved",
    priority: "Medium",
  },
  {
    id: 3,
    requestor: "David Johnson",
    title: "Printer not working",
    dateAdded: new Date("2022-02-11"),
    dateUpdated: new Date("2022-02-12"),
    status: "In Progress",
    priority: "High",
  },
  {
    id: 4,
    requestor: "Sara Lee",
    title: "Can't login to the system",
    dateAdded: new Date("2022-02-14"),
    dateUpdated: new Date("2022-02-15"),
    status: "Resolved",
    priority: "High",
  },
  {
    id: 5,
    requestor: "Michael Brown",
    title: "Software installation issue",
    dateAdded: new Date("2022-02-17"),
    dateUpdated: new Date("2022-02-18"),
    status: "In Progress",
    priority: "Low",
  },
  {
    id: 6,
    requestor: "Lisa Davis",
    title: "System crash",
    dateAdded: new Date("2022-02-20"),
    dateUpdated: new Date("2022-02-21"),
    status: "Resolved",
    priority: "High",
  },
  {
    id: 7,
    requestor: "Kevin Wilson",
    title: "Email configuration issue",
    dateAdded: new Date("2022-02-23"),
    dateUpdated: new Date("2022-02-24"),
    status: "In Progress",
    priority: "Medium",
  },
  {
    id: 8,
    requestor: "Maggie Chen",
    title: "Need access to shared drive",
    dateAdded: new Date("2022-02-26"),
    dateUpdated: new Date("2022-02-27"),
    status: "Resolved",
    priority: "Low",
  },
  {
    id: 9,
    requestor: "Jason Lee",
    title: "VPN connection issue",
    dateAdded: new Date("2022-03-01"),
    dateUpdated: new Date("2022-03-02"),
    status: "In Progress",
    priority: "High",
  },
  {
    id: 10,
    requestor: "Rachel Kim",
    title: "Need new laptop",
    dateAdded: new Date("2022-03-04"),
    dateUpdated: new Date("2022-03-05"),
    status: "Resolved",
    priority: "High",
  },
];




  export default function BasicTable() {
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