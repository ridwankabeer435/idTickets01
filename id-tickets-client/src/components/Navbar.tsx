import AppBar from '@mui/material/AppBar';
import Box from '@mui/material/Box';
import Toolbar from '@mui/material/Toolbar';
import Typography from '@mui/material/Typography';
import NotificationsIcon from '@mui/icons-material/Notifications';
import AccountCircleIcon from '@mui/icons-material/AccountCircle';

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
*/
const Navbar: React.FC<NavbarProps> = ({title}) => {
    return (
        <AppBar position="static">
          <Toolbar>
            <IconButton aria-label="drawer">
              <MenuIcon/>
            </IconButton>
            <Typography variant="h5">{title}</Typography>
          </Toolbar>
          
          <Toolbar sx={{marginLeft: "auto", display: "inline-flex"}}>
            <IconButton aria-label="notification">
              <NotificationsIcon/>
            </IconButton>
            <IconButton aria-label="notification">
              <AccountCircleIcon/>
            </IconButton>
          </Toolbar>
        </AppBar>
      );
  
}


export default Navbar;

  
