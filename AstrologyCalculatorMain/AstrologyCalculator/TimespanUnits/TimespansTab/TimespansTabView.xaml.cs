using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AstrologyCalculator.TimespanUnits.TimespansTab
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class TimespansTabView : UserControl
    {
        public TimespansTabView()
        {
            var manager = new TimespanUnitManager();
            manager.LoadDefault();
            var model = new TimespansTabModel(manager);
            this.DataContext = new TimespansTabViewModel(model);

            InitializeComponent();
        }

        private void AlternativeUnits_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var viewmodel = (TimespansTabViewModel)DataContext;
            viewmodel.AlternativeUnitChanged(sender, e);
        }
    }
}
