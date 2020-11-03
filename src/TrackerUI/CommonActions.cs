using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace TrackerUI
{
    public static class CommonActions
    {
        public static void LabelErrorAnimation(Label label)
        {
            var orinigalCoordinates = label.Location;
            var newCoordinates = label.Location;
            newCoordinates.X += 2;

            label.Location = newCoordinates;
            Thread.Sleep(100);
            label.Location = orinigalCoordinates;
        }
    }
}
