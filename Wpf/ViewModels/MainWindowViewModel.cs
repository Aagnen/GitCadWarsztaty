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
using Common;

namespace Wpf.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        #region State
        private NoteItem? _selectedItem;
        private NoteItem? _noteItem;
        private NoteManager _manager = new NoteManager("https://localhost:7184/"); //to do base adress
        #endregion


        #region Bindings
        public ObservableCollection<NoteItem> AvailableItems { get; set; }
        public NoteManager Manager
        {
            get => _manager;
            set
            {
                _manager = value;
                OnPropertyChanged();
            }
        }
        public NoteItem NoteItem 
        {
            get { return _noteItem; }
            set
            {
                _noteItem = value;
                OnPropertyChanged(nameof(NoteItem));
            }
        }
        public NoteItem SelectedItem
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
            NoteItem = new NoteItem();
            AvailableItems = new ObservableCollection<NoteItem>();

        }
        public async Task InitializeAsync()
        {
            var result = await Manager.GetNotes();
            //Now populate AvailableItems
            if (result.Flag == SolveFlag.OK)
                foreach (var item in result.Value)
                    AvailableItems.Add(item);
        }

        public ICommand ShowSelectedItemCommand => new RelayCommand(ShowSelectedItem);
        private void ShowSelectedItem(object param)
        {
            if (SelectedItem != null)
            {
                MessageBox.Show($"Title: {SelectedItem.Title}\nAuthor: {SelectedItem.Author}\nNote: {SelectedItem.Note}\nDate: {SelectedItem.Date}");
            }
            else MessageBox.Show("No selected note");
        }
        public ICommand DeleteSelectedItemCommand => new RelayCommand(DeleteSelectedItem);
        private void DeleteSelectedItem(object param)
        {
            if (SelectedItem != null)
            {
                AvailableItems.Remove(SelectedItem);
            }
            else MessageBox.Show("No selected note");
        }
        public ICommand AddNoteCommand => new RelayCommand(AddNote);
        private void AddNote(object param)
        {
            if (NoteItem.Title == null || NoteItem.Title == "") MessageBox.Show("Title is required");
            else
            {
                NoteItem.Date = DateTime.Now;
                NoteItem.TagList = new List<Tag>();
                NoteItem.Id = 1;
                AvailableItems.Add(NoteItem);
                NoteItem = new NoteItem();
            }
        }
        #endregion
    }
}
