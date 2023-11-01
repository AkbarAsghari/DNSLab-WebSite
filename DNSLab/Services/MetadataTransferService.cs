using DNSLab.Prividers;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace DNSLab.Services
{
    public interface IMetadataProvider
    {
        Task<MetadataValue> GetMetaData(string url);
    }

    public class MetadataTransferService : INotifyPropertyChanged, IDisposable
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly NavigationManager _navigationManager;
        private readonly IMetadataProvider _metadataProvider;

        private string _title = string.Empty;
        public string Title
        {
            get => _title;
            set
            {
                if (_title != value)
                {
                    _title = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _description = string.Empty;
        public string Description
        {
            get => _description;
            set
            {
                if (_description != value)
                {
                    _description = value;
                    OnPropertyChanged();
                }
            }
        }

        private string[] _keywords = Array.Empty<string>();
        public string[] Keywords
        {
            get => _keywords;
            set
            {
                if (_keywords != value)
                {
                    _keywords = value ?? Array.Empty<string>();
                    OnPropertyChanged();
                }
            }
        }

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public MetadataTransferService(NavigationManager navigationManager, IMetadataProvider metadataProvider)
        {
            _navigationManager = navigationManager ?? throw new ArgumentNullException(nameof(navigationManager));
            _metadataProvider = metadataProvider ?? throw new ArgumentNullException(nameof(metadataProvider));
        }

        public void Start()
        {
            _navigationManager.LocationChanged += _navigationManager_LocationChanged;
            UpdateMetadata(_navigationManager.Uri);
        }

        private async Task _navigationManager_LocationChanged(object? sender, LocationChangedEventArgs e)
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

            Keywords = metadataValue.Keywords;
        }

        public void Dispose()
        {
            _navigationManager.LocationChanged -= _navigationManager_LocationChanged;
        }
    }
}
