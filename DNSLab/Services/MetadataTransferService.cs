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
                    Title = "DNSLab - دی ان اس لب",
                    Description = "DDNS با بالاترین سرعت و آپتایم 100% . DDNS رایگان ما  IP dynamic شما رو به یه هاست‌نیم نشان میدهد.برای مدیریت DNS خود همین الان با چند کلیک رایگان ثبت نام کن.",
                    Keywords = new string[] { "DDNS", "Dynamic DNS", "DNS", "Free" }
                };
            }

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
