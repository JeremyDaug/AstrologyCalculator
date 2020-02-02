using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AstrologyCalculator.SystemEditor
{
    public class SystemEditorViewModel : INotifyPropertyChanged
    {
        private readonly SystemEditorModel model;

        public SystemEditorViewModel(SystemEditorModel model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));

            this.model = model;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
