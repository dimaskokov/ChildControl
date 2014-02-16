using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Windows.Devices.Geolocation;
using ChildControl.Model;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Info;
using Microsoft.Phone.Shell;

namespace ChildControl
{
    public partial class Child : PhoneApplicationPage
    {

        private StateManager _stateManager;
        private ChildViewModel _viewModel;
        private PosState _current;

        public Child()
        {
            _stateManager = new StateManager((Application.Current as App).StateList);
            _viewModel = new ChildViewModel();
            
            DataContext = _viewModel;
            InitializeComponent();
            StateUpdate();
        }

        public async void StateUpdate()
        {
            var currState = await _stateManager.CalcState();

            _viewModel.PosState = currState == null ? "Неизвестно" : currState.State;
            _viewModel.ChangeButton = currState == null ? "Создать статус" : "Удалить статус";
            DistancePanel.Visibility = currState == null ? Visibility.Visible : Visibility.Collapsed;

            _current = currState;

            if (currState != null)
                SendState(currState);
        }

        public void SendState(PosState state)
        {
            bool isConnected = true;

            _viewModel.ConnectState = isConnected ? "Подключен" : "Не в сети";
        }

        private void ChangeStateButton_Click(object sender, RoutedEventArgs e)
        {
            if (_current != null)
            {
                _stateManager.RemoveState(_current);
            }
            else
            {
                double distance = 0;
                if (Distance50RadioButton.IsChecked ?? false) distance = 50;
                if (Distance100RadioButton.IsChecked ?? false) distance = 100;
                if (Distance200RadioButton.IsChecked ?? false) distance = 200;
                if (Distance500RadioButton.IsChecked ?? false) distance = 500;
                if (distance != 0 && !string.IsNullOrEmpty(NewStateTextBox.Text))
                {
                    _stateManager.AddState(NewStateTextBox.Text, distance);
                }
            }
            StateUpdate();
        }
    }


    public class ChildViewModel : INotifyPropertyChanged
    {
        private string connectState;
        public string ConnectState
        {
            get { return connectState; }
            set
            {
                connectState = value;
                PropChanged("ConnectState");
            }
        }

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

        private string changeButton;
        public string ChangeButton
        {
            get { return changeButton; }
            set
            {
                changeButton = value;
                PropChanged("ChangeButton");
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