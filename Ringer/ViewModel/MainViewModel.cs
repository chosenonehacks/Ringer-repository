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
        public MainViewModel()
        {
            
        }

        
        private MediaElement _mediaElementObject = new MediaElement();

        public MediaElement MediaElementObject
        {
            get { return _mediaElementObject; }
            set 
            { 
                _mediaElementObject = value;
                RaisePropertyChanged("MediaElementObject");
            }
        }

        private RelayCommand<string> _playCommand;

        
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
            string source = string.Format("ms-appx:///Assets/Sounds/{0}.mp3", sound.ToString());

            MediaElementObject.Source = new Uri(source);
            
            await MediaElementObject.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                MediaElementObject.Stop();
                MediaElementObject.Play();
            });
        }
    }
}