using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.StoredData
{
    public static class StoredGUIProperties
    {
        private static double storedHighscore;

        public static double StoredHighscore
        {
            get
            {
                return storedHighscore;
            }

            set
            {
                storedHighscore = value;
            }
        }
    }
}
