using System.Windows.Controls;

namespace BallisticsSandbox.Objects
{
    public static class Switcher
    {
        public static UserControl CurrentPage { get; set; }
        public static WindowSwitcher pageSwitcher;

        /// <summary>
        /// <para/>Purpose: Swtiches to a new page
        /// <para/>Input: UserControl newPage, user control for new page
        /// <para/>Output: none
        /// <para/>Author: Connor Goudie
        /// <para/>Date: March 17, 2017
        /// <para/>Updated by: Connor Goudie
        /// <para/>Date: March 20, 2017
        /// </summary>
        public static void Switch(UserControl newPage)
        {
            pageSwitcher.Navigate(newPage);
        }

        /// <summary>
        /// <para/>Purpose: Swtiches to a new page using a new state
        /// <para/>Input: 
        ///     UserControl newPage, user control for new page
        ///     object state - new state for next page
        /// <para/>Output: none
        /// <para/>Author: Connor Goudie
        /// <para/>Date: March 17, 2017
        /// <para/>Updated by: Connor Goudie
        /// <para/>Date: March 20, 2017
        /// </summary>
        public static void Switch(UserControl newPage, object state)
        {
            pageSwitcher.Navigate(newPage, state);
        }
    }
}
