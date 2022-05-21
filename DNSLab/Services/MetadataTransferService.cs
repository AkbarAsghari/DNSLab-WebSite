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
        private readonly NavigationManager _navigationManager;
        private readonly MetadataProvider _metadataProvider;

        public string Description
        {
            get => _description;
            set
            {
                _description = value;
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

            _navigationManager.LocationChanged += _navigationManager_LocationChanged;
            UpdateMetadata(_navigationManager.Uri);
        }

        private void _navigationManager_LocationChanged(object? sender, LocationChangedEventArgs e)
        {
            UpdateMetadata(e.Location);
        }

        private void UpdateMetadata(string url)
        {
            var metadataValue = _metadataProvider.RouteDetailMapping.FirstOrDefault(vp => url.EndsWith(vp.Key)).Value;

            if (metadataValue is null)
            {
                metadataValue = new()
                {
                    Title = "Default",
                    Description = "Default"
                };
            }

            Title = metadataValue.Title;
            Description = metadataValue.Description;
        }

        public void Dispose()
        {
            _navigationManager.LocationChanged -= _navigationManager_LocationChanged;
        }
    }
}
