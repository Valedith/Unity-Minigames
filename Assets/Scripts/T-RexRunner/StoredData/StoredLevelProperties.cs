using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.StoredData
{
    public static class StoredLevelProperties
    {
        private static int difficult;

        public static int Difficult
        {
            get
            {
                return difficult;
            }

            set
            {
                difficult = value;
            }
        }
    }
}
