using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShakesAndFidgetLibrary.Models
{
    public abstract class ItemBase : ModelBase
    {
        public String Name { get; set; }
        public String ImageSource { get; set; }
    }
}
