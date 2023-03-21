import React from 'react';
import logo from './logo.svg';
import { CssBaseline } from '@mui/material';
import { ThemeProvider, createTheme } from '@mui/material/styles';
import { orange } from '@mui/material/colors';
import  Navbar from './components/Navbar';
import './App.css';
import Table  from './components/Table';

/*
  import the navbar
*/
function App() {
  const theme = createTheme({
    palette: {
      primary: {
        main: orange[600],
      },
    },
  });
  
  return (
    <ThemeProvider theme={theme}>
      <CssBaseline />
     
      <Navbar title="Id Tickets" />
      
      <Table />
      
 
    </ThemeProvider>
  );
}

export default App;
