using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AstrologyCalculator.BodyEditor
{
    public class BodyEditorModel
    {
        public BodyEditorModel()
        {
            currentBody = new Body();
        }

        private Body currentBody;

        public string BodyName
        {
            get { return currentBody.Name; }
            set { currentBody.Name = value; }
        }
    }
}
