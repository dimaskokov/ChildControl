using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace ChildControl
{
    public partial class Parent : PhoneApplicationPage
    {
        private ParentViewModel _viewModel;
        public Parent()
        {
            _viewModel = new ParentViewModel();

            DataContext = _viewModel;
            InitializeComponent();
            StateUpdate();
        }

        public void StateUpdate()
        {
            _viewModel.PosState = "Неизвестно";
        }

        public class ParentViewModel : INotifyPropertyChanged
        {
            private string posState;
            public string PosState
            {
                get { return posState; }
                set
                {
                    posState = value;
                    PropChanged("PosState");
                }
            }

            public event PropertyChangedEventHandler PropertyChanged;
            private void PropChanged(string propName)
            {
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(propName));
                }
            }
        }
    }
}