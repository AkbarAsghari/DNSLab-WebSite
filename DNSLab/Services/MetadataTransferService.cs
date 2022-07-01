using DNSLab.Prividers;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DNSLab.Services
{
    public class MetadataTransferService : INotifyPropertyChanged, IDisposable
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private readonly NavigationManager _navigationManager;
        private readonly MetadataProvider _metadataProvider;

        private string _title;
        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }


        private string _description;
        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged();
            }
        }

        private string[] _keywords;
        public string[] Keywords
        {
            get => _keywords;
            set
            {
                _keywords = value;
                OnPropertyChanged();
            }
        }

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new(propertyName));
        }

        public MetadataTransferService(NavigationManager navigationManager, MetadataProvider metadataProvider)
        {
            _navigationManager = navigationManager;
            _metadataProvider = metadataProvider;
        }

        public async Task Start()
        {
            _navigationManager.LocationChanged += _navigationManager_LocationChanged;
            await UpdateMetadata(_navigationManager.Uri);
        }

        private async void _navigationManager_LocationChanged(object? sender, LocationChangedEventArgs e)
        {
            await UpdateMetadata(e.Location);
        }

        private async Task UpdateMetadata(string url)
        {
            var metadataValue = await _metadataProvider.GetMetaData(url);

            Title = metadataValue.Title;
            Description = metadataValue.Description;

            if (metadataValue.Keywords == null)
                metadataValue.Keywords = new string[] { "دی ان اس رایگان", "free dns" };

            string[] staticKeywords = new string[] { "DDNS", "دی دی ان اس" };

            Keywords = staticKeywords.Concat(metadataValue.Keywords).ToArray();
        }

        public void Dispose()
        {
            _navigationManager.LocationChanged -= _navigationManager_LocationChanged;
        }
    }
}
