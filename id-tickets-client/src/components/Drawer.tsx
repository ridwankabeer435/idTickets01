
import React from 'react';
import { Divider, IconButton, Link, List, ListItem, SwipeableDrawer } from '@mui/material';
import ChevronLeftIcon from '@mui/icons-material/ChevronLeft';
import MenuIcon from '@mui/icons-material/Menu';
import { FC, useState } from 'react';


/*
interface DrawerProps {
    isOpen: boolean;
  }
  */
const Drawer = () => {
    const [open, setOpen] = useState(false);

    return (
      <>
      <IconButton  aria-label="drawer-button"  sx={{paddingLeft: 2}} onClick={() => setOpen(true)}>
        <MenuIcon/>
      </IconButton>
    <SwipeableDrawer
        variant="temporary"
        anchor="left"
        open={open}
        onOpen = {() => setOpen(true)}
        onClose = {() => setOpen(false)}
        color="inherit"
      >
        <div>
            <IconButton>
                <ChevronLeftIcon onClick={() => setOpen(false)}/>
            </IconButton>
        </div>
        <Divider />
   
        <List>
        {['Home', 'All tickets'].map((text) => (
            <ListItem>
            <Link 
                color="inherit"
                underline="none"
                variant="button"
                href="#"
            >
            {text}
            </Link >
            </ListItem>
           
          ))}
        </List>
          
      </SwipeableDrawer>
      </>

    )
}
  
export default Drawer;
