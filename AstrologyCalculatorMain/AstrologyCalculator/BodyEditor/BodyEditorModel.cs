using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AstrologyCalculator.BodyEditor
{
    class BodyEditorModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private Body currentBody;

        public string Name
        {
            get { return currentBody.Name; }
            set
            {
                if (value == currentBody.Name)
                    return;
                currentBody.Name = value;
                RaisePropertyChanged(nameof(Name));
            }
        }

        // https://www.tutorialspoint.com/mvvm/mvvm_first_application.htm

        // Events

        private void RaisePropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
