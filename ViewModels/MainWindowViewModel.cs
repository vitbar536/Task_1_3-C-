using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Task1_3.Models;

namespace Task1_3.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private Stack<string> _stack;
        private string _inputValue;
        private string _currentItem;
        private string _stackStatus;
        private int _itemCount;

        public MainWindowViewModel()
        {
            _stack = new Stack<string>();
            UpdateStackInfo();

            PushCommand = new RelayCommand(PushItem, () => !string.IsNullOrWhiteSpace(InputValue));
            PopCommand = new RelayCommand(PopItem, () => !_stack.IsEmpty);
            ClearCommand = new RelayCommand(ClearStack);
        }

        public string InputValue
        {
            get => _inputValue;
            set
            {
                SetProperty(ref _inputValue, value);
                ((RelayCommand)PushCommand).RaiseCanExecuteChanged();
            }
        }

        public string CurrentItem
        {
            get => _currentItem;
            set => SetProperty(ref _currentItem, value);
        }

        public string StackStatus
        {
            get => _stackStatus;
            set => SetProperty(ref _stackStatus, value);
        }

        public int ItemCount
        {
            get => _itemCount;
            set => SetProperty(ref _itemCount, value);
        }

        public ObservableCollection<string> StackItems { get; } = new ObservableCollection<string>();

        public ICommand PushCommand { get; }
        public ICommand PopCommand { get; }
        public ICommand ClearCommand { get; }

        private void PushItem()
        {
            _stack.Push(InputValue);
            StackItems.Add(InputValue);
            InputValue = string.Empty;
            UpdateStackInfo();
            ((RelayCommand)PopCommand).RaiseCanExecuteChanged();
        }

        private void PopItem()
        {
            try
            {
                string item = _stack.Pop();
                StackItems.RemoveAt(StackItems.Count - 1);
                UpdateStackInfo();
                ((RelayCommand)PopCommand).RaiseCanExecuteChanged();
            }
            catch (InvalidOperationException ex)
            {
                StackStatus = ex.Message;
            }
        }

        private void ClearStack()
        {
            _stack.Clear();
            StackItems.Clear();
            UpdateStackInfo();
            ((RelayCommand)PopCommand).RaiseCanExecuteChanged();
        }

        private void UpdateStackInfo()
        {
            CurrentItem = _stack.IsEmpty ? "No elements" : _stack.CurrentItem;
            ItemCount = _stack.Count;
            StackStatus = _stack.IsEmpty ? "Stack is empty" : "Stack contains elements";
        }
    }
}
