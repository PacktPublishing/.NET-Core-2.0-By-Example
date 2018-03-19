#include <ncurses.h>

void WelcomeMessage()
{
	initscr();			/* Start curses mode 		  */
	printw("Welcome to .NET Core 2.0");	/* Print Welcome to .NET Core 2.0 */
	move(3,2);             /*moves cursor to position 3rd row, 2nd column   */
	addch('a'|A_BOLD | A_UNDERLINE);               /* add character */
	refresh();			/* Print it on to the real screen */
	getch();			/* Wait for user input */
	endwin();			/* End curses mode		  */
}

// int main()
// {	
// 	WelcomeMessage();
// 	return 0;
// }