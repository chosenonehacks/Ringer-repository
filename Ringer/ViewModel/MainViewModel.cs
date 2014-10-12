using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Threading.Tasks;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;

namespace Ringer.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            
        }

        /// <summary>
        /// The <see cref="MediaElementObject" /> property's name.
        /// </summary>
        //public const string MediaElementObjectPropertyName = "MediaElementObject";

        private MediaElement _mediaElementObject = new MediaElement();

        /// <summary>
        /// Sets and gets the MediaElementObject property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public MediaElement MediaElementObject
        {
            get
            {
                return _mediaElementObject;
            }

            set
            {
                if (_mediaElementObject == value)
                {
                    return;
                }                
                _mediaElementObject = value;
                RaisePropertyChanged(() => MediaElementObject);
            }
        }

        private RelayCommand<string> _playCommand;

        /// <summary>
        /// Gets the PlayCommand.
        /// </summary>
        public RelayCommand<string> PlayCommand
        {
            get
            {
                return _playCommand
                    ?? (_playCommand = new RelayCommand<string>(
                                         async (s) =>
                                          {
                                              await Play(s);
                                          }));
            }
        }

        private async Task Play(string sound)
        {
            string source = string.Format("ms-appx:///Assets/{0}.mp3", sound.ToString());
            MediaElementObject.Source = null;
            MediaElementObject.Source = new Uri(source);
            MediaElementObject.AutoPlay = true;
            MediaElementObject.Volume = 15;

            await MediaElementObject.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                MediaElementObject.Stop();
                MediaElementObject.Play();
            });
        }
    }
}