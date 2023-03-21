import React from 'react';
import AppBar from '@mui/material/AppBar';
import Box from '@mui/material/Box';
import Toolbar from '@mui/material/Toolbar';
import Typography from '@mui/material/Typography';
import NotificationsIcon from '@mui/icons-material/Notifications';
import AccountCircleIcon from '@mui/icons-material/AccountCircle';
import  Drawer from './Drawer';

import IconButton from '@mui/material/IconButton';

import MenuIcon from '@mui/icons-material/Menu';



interface NavbarProps {
    title: string;
  }
  
/*
  add some icons
  add a grid,
  -- the left part of the grid will contain the title and the menu icon button
  -- the right part of the grid will contain the notification icon and the user profile icon
  -- bring in the hamburger menu icon from the
*/
const Navbar: React.FC<NavbarProps> = ({title}) => {
    return (
      <Box sx={{ flexGrow: 1 }}>
        <AppBar component="nav" position="static">
          <Toolbar disableGutters>
            <Drawer />

            <Typography variant="h5"  sx={{ flexGrow: 1, display: { xs: 'none', sm: 'block' } }}>{title}</Typography>
            <Box sx={{paddingRight: 3}}>
              <IconButton aria-label="notification">
                <NotificationsIcon/>
              </IconButton>
              <IconButton aria-label="account">
                <AccountCircleIcon/>
              </IconButton>
            </Box>
       
          </Toolbar>
   
         
     
        </AppBar>
      </Box>
    );
  
}


export default Navbar;

  
