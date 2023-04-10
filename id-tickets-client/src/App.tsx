import React from 'react';
import logo from './logo.svg';
import { CssBaseline } from '@mui/material';
import { ThemeProvider, createTheme } from '@mui/material/styles';
import { orange } from '@mui/material/colors';
import { BrowserRouter } from 'react-router-dom';
import Home from './pages/Home';
import './App.css';


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
       <BrowserRouter >
       <CssBaseline />
     
        <Home />
     
       </BrowserRouter>

 
    </ThemeProvider>
  );
}

export default App;
