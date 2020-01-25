using AstrologyCalculator.BodyEditor;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AstrologyCalculator.BodyEditor
{
    public class BodyEditorViewModel : INotifyPropertyChanged
    {
        private readonly BodyEditorModel model;

        public BodyEditorViewModel(BodyEditorModel model)
        {
            this.model = model;
        }

        public string BodyName
        {
            get
            {
                return "";
            }
            set
            {
                
            }
        }

        private void OnPropertyChanged(string v)
        {
            
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void AllFeatureMode(bool IsEnabled)
        {
            if (IsEnabled)
            {
                // Turn on the advanced boxes
            }
            else
            {
                // turn advanced boxes off (only use orbit period / radius)
            }
        }
    }
}
