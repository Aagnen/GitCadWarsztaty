using DatabaseModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using Wpf.ViewModels.Abstract;

namespace Wpf.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        #region State
        private ListItem? _selectedItem;
        #endregion


        #region Bindings
        public ObservableCollection<NoteItem> AvailableItems { get; set; }
        public ListItem SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                OnPropertyChanged(nameof(SelectedItem));
            }
        }
        #endregion


        #region Commands
        public MainWindowViewModel()
        {
            AvailableItems = new ObservableCollection<NoteItem>();
            //populate it
        }
        public ICommand ShowSelectedItemCommand => new RelayCommand(ShowSelectedItem);
        private void ShowSelectedItem(object param)
        {
            if (SelectedItem != null)
            {
                MessageBox.Show("This is a note (To be done ;P )");
            }
        }
        #endregion
    }
}
