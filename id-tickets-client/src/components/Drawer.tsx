
import { Divider, IconButton, Link, List, ListItem, SwipeableDrawer } from '@mui/material';
import ChevronLeftIcon from '@mui/icons-material/ChevronLeft';
import { FC, useState } from 'react';


interface DrawerProps {
    isOpen: boolean;
  }
  
const Drawer: FC<DrawerProps> = ({ isOpen }) => {
    const [open, setOpen] = useState(false);

    return (
    <SwipeableDrawer
        variant="temporary"
        anchor="left"
        open={isOpen}
        onOpen = {() => setOpen(true)}
        onClose = {() => setOpen(false)}
      >
        <div>
            <IconButton>
                <ChevronLeftIcon />
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

    )
}
  
export default Drawer;
