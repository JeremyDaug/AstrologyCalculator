using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AstrologyCalculator.BodyEditor
{
    public class BodyEditorViewModel
    {
        public BodyEditorViewModel()
        {
            
        }

        public ObservableCollection<Body> Bodies { get; set; }

        public void LoadBodies()
        {
            var bodies = new ObservableCollection<Body>();

            bodies.Add(new Body { Name = "Alice" });
            bodies.Add(new Body { Name = "Bob" });
            bodies.Add(new Body { Name = "Charlie" });

            Bodies = bodies;
        }
    }
}
