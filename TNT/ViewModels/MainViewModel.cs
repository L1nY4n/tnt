using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TNT
{
    class MainViewModel : BaseViewModel
    {
        public int CurrentPage { get; set; } = 0;

        private Window window;
        public MainViewModel( Window window)
        {
            this.window = window;
        }
    }
}
