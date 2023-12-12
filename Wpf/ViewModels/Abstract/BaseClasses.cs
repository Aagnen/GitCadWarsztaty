using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Wpf.ViewModels.Abstract
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public ViewModelBase() { }

        public string DisplayName { get; set; } = "";
        public bool ThrowOnInvalidPropertyName { get; set; } = false;

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            VerifyPropertyName(propertyName);
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                handler(this, e);
            }
        }
        [Conditional("DEBUG")]
        [DebuggerStepThrough]
        public void VerifyPropertyName(string propertyName)
        {
            // Verify that the property name matches a real, 
            // public, instance property on this object. 
            if (TypeDescriptor.GetProperties(this)[propertyName] == null)
            {
                string msg = "Invalid property name: " + propertyName;
                if (ThrowOnInvalidPropertyName)
                    throw new Exception(msg);
                else
                    Debug.Fail(msg);
            }
        }
        public ICommand EnsureCommand(RelayCommand _command, Action<object> _action)
        {
            if (_command == null) _command = new RelayCommand(_action);
            return _command;
        }
        public static string AssemblyDirectory
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }
    }
    public class CommandViewModel : ViewModelBase
    {
        public ICommand Command { get; private set; }
        public CommandViewModel(string displayName, ICommand command)
        {
            base.DisplayName = displayName;
            Command = command ?? throw new ArgumentNullException("command");
        }
    }
}
