using BallisticsSandbox.Objects;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BallisticsSandbox
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class WindowSwitcher : Window
    {
        public WindowSwitcher()
        {
            InitializeComponent();
            Switcher.pageSwitcher = this;
            Switcher.Switch(new MainWindow());
        }

        /// <summary>
        /// <para/> saves the beat and returns the user to the main menu.
        /// <para/>Input:UserControl nextPage - the page being naviagted to
        /// <para/>Output: none
        /// <para/>Author: Andrew Busto
        /// <para/>Date: April 09, 2017
        /// </summary>
        public void Navigate(UserControl nextPage)
        {
            this.Content = nextPage;
        }

        /// <summary>
        /// <para/> saves the beat and returns the user to the main menu.
        /// <para/>Input:UserControl nextPage - the page being naviagted to
        ///              object state - the state the next page uses
        /// <para/>Output: none
        /// <para/>Author: Andrew Busto
        /// <para/>Date: April 09, 2017
        /// </summary>
        public void Navigate(UserControl nextPage, object state)
        {
            this.Content = nextPage;
            ISwitchable s = nextPage as ISwitchable;
            if (s != null)
            {
                s.UtilizeState(state);
            }
            else
            {
                throw new ArgumentException("NextPage is not ISwitchable! "
                  + nextPage.Name.ToString());
            }
        }
    }
}
