﻿using System;
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

namespace AstrologyCalculator.BodyEditor
{
    /// <summary>
    /// Interaction logic for BodyEditorView.xaml
    /// </summary>
    public partial class BodyEditorView : UserControl
    {
        public BodyEditorView()
        {
            var model = new BodyEditorModel();
            DataContext = new BodyEditorViewModel(model);

            InitializeComponent();
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            var viewmodel = (BodyEditorViewModel)DataContext;
            viewmodel.AllFeatureMode(false);
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            var viewmodel = (BodyEditorViewModel)DataContext;
            viewmodel.AllFeatureMode(true);
        }
    }
}
