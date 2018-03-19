# include <stdio.h>
# include <ncurses.h>

int row,column,  // current row and column (Top left is (0,0))

numberOfRows,  // number of rows in current window

numberOfColumns;  // number of columns in current window

void drawCharOnWindow(char drawChar,int row ,int column)

{  move(row,column);  // ncurses call to move cursor to given row, given column

delch();             //ncurses calls to replace character 
insch(drawChar);    //under cursor by drawChar
                        

refresh();  // ncurses call to update screen

row++;  // go to next row

// check for need to shift right or wrap around

if (row == numberOfRows)  {

row = 0;

column++;

if (column == numberOfColumns) column = 0;

}

}


 void hello(int i, int j, char c)
{
   // initscr();			/* Start curses mode 		  */
    // printw("Welcome to .NET Core 2.0");	/* Print Welcome to .NET Core 2.0 */
	// move(3,2);             /*moves cursor to position 3rd row, 2nd column   */
	// addch('a'|A_BOLD | A_UNDERLINE);               /* add character */
	// refresh();			/* Print it on to the real screen */
	// getch();			/* Wait for user input */
   // return 15;
     
        
        WINDOW * windw;
        windw = initscr();  // ncurses call to initialize window
        
        cbreak();  // ncurses call to avoid wait for key press
        noecho();  // ncurses call to set noecho
        
        getmaxyx(windw,numberOfRows,numberOfColumns);  // ncurses call to find size of window
        
        clear();  // ncurses call to clear screen, send cursor to position (0,0)
        
        refresh();  // curses call to implement all changes since last refresh
        
        row = 0; column = 0;
       
        while (1)  {
       
        if (c == 'q') break;  // quit?
      
       {
           drawCharOnWindow(c,i,j);
   
        }
    }
}





