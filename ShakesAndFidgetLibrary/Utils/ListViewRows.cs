using ShakesAndFidgetLibrary.Models;
using System.Collections.ObjectModel;

namespace ShakesAndFidgetLibrary.Utils
{
    public class GearsRow
    {
        public ObservableCollection<Gear> Gears { get; set; }
    }

    public class UsableRow
    {
        public ObservableCollection<Usable> Usables { get; set; }
    }
}
