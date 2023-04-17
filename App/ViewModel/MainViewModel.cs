using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace App.ViewModel;

public partial class MainViewModel : ObservableObject
{
    readonly IConnectivity _connectivity;
    public MainViewModel(IConnectivity connectivity)
    {
        Items = new ObservableCollection<string>();
        _connectivity = connectivity;
    }



    [ObservableProperty]
    ObservableCollection<string> items;
    
    [ObservableProperty]
    string text;

    [RelayCommand]
    async Task Add()
    {
        if (string.IsNullOrWhiteSpace(Text))
            return;
        
        if (_connectivity.NetworkAccess != NetworkAccess.Internet)
        {
            await Shell.Current.DisplayAlert("Uh Oh!", "No Internet", "OK");
            return;
        }
        
        Items.Add(Text);

        // add our item
        Text = string.Empty;
    }
    
    [RelayCommand]
    void Delete(string s)
    {
        if (Items.Contains(s))
            Items.Remove(s);
    }

    [RelayCommand]
    async Task Tap(string s)
    {
        await Shell.Current.GoToAsync($"{nameof(DetailPage)}?Text={s}");
    }
    
}
