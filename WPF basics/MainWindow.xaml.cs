using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Commons.Music.Midi;

namespace Piano
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary
    public partial class MainWindow : Window
    {
        private IMidiAccess access;
        private IMidiOutput output;

        public MainWindow()
        {
            InitializeComponent();
            access = MidiAccessManager.Default;
            output = access.OpenOutputAsync(access.Outputs.Last().Id).Result;
            this.KeyDown += new KeyEventHandler(OnButtonKeyDown);
        }

        private void Key_Click(object sender, RoutedEventArgs e)
        {
            var note = ((Button)sender).Tag.ToString();
            var noteDictionary = new Dictionary<String, Byte>() {
                { "C4", 0x3C },
                { "C4#", 0x3D },
                { "D4", 0x3E },
                { "D4#", 0x3F },
                { "E4", 0x40 },
                { "F4", 0x41 },
                { "F4#", 0x42 },
                { "G4", 0x43 },
                { "G4#", 0x44 },
                { "A5", 0x45 },
                { "A5#", 0x46 },
                { "B5", 0x47 },
                { "C5", 0x48 },
            };
            output.Send(new byte[] { MidiEvent.NoteOn, noteDictionary[note], 0x70 }, 0, 3, 0); // There are constant fields for each MIDI event
        }
        private void OnButtonKeyDown(object sender, KeyEventArgs e)
        {
            byte note = 0x3C;
            // TODO simulate mouse down and mouse up
            //int timestamp = new TimeSpan(DateTime.Now.Ticks).Milliseconds;
            //test.Content = test.Content + e.Key.ToString();
            //c1.RaiseEvent(new MouseButtonEventArgs(Mouse.PrimaryDevice,timestamp, MouseButton.Left));
            var KeyboardDictionary = new Dictionary<String, Byte>() {
                { "Q", 0x3C },
                { "D2", 0x3D },
                { "W", 0x3E },
                { "D3", 0x3F },
                { "E", 0x40 },
                { "R", 0x41 },
                { "D5", 0x42 },
                { "T", 0x43 },
                { "D6", 0x44 },
                { "Y", 0x45 },
                { "D7", 0x46 },
                { "U", 0x47 },
                { "I", 0x48 },
            };
            try
            {
                Console.WriteLine(e.Key.ToString());
                note = KeyboardDictionary[e.Key.ToString()];
                output.Send(new byte[] { MidiEvent.NoteOn, note, 0x70 }, 0, 3, 0);
            }
            catch (Exception e1) { }
        }
    }
}
