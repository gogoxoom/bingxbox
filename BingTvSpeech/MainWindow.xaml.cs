using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using SpeechRek;

namespace BingTvSpeech
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window {

        private SpeechRek.SpeechRek speechRek;

		private TextBlock selectedTab;
	    private Rectangle selectedBox;
		private Storyboard myTextScrollAnim;
	    private Storyboard myItemScrollAnim;
		private Storyboard scroller;
		
		private Style highlightStyle;
		private Style highlightText;
		
		private bool isGridInFocus;

	    private string[] words =
	        {
	            "home", "weather", "movies", "scroll right", "scroll left", "select tabs", "select grid",
	            "tv", "finance", "travel", "music", "news", "sports","clear",
                "item one","item two","item three","item four","item five","item with a long name","big item"
            };
		
        /**
         * CTOR
         */
        public MainWindow() {
            InitializeComponent();
			
			myTextScrollAnim = (Storyboard)TryFindResource("TextScrollAnim");
			myItemScrollAnim = (Storyboard)TryFindResource("ItemScrollAnim");
			
			highlightStyle = Window.Resources["RectangleStyle1"] as Style;
			highlightText = Window.Resources["highlightText"] as Style;
			
			scroller = null;
			isGridInFocus = false;
            selectedBox = bigBox;
			selectedTab = homeTab;
			selectedBox.Style = highlightStyle;
			selectedTab.Style = highlightText;
        }

        void sp_OnWordHeard(object sender, SpeechHeardArgs e) {
            //listBoxOutput.Items.Insert(0, ("Word heard: " + e.WordHeard));

            switch (e.WordHeard.ToString().ToLower())
            {
                case "scroll left":
                    
					if(isGridInFocus) {
						scroller = (Storyboard)TryFindResource("GridScrollLeft");
					} else {
						scroller = (Storyboard)TryFindResource("TextScrollLeft");
					}
					
					scroller.Begin();
                    break;
                case "scroll right":
					
					if(isGridInFocus) {
						scroller = (Storyboard)TryFindResource("GridScrollRight");
					} else {
						scroller = (Storyboard)TryFindResource("TextScrollRight");
					}
					
					scroller.Begin();
                    break;
                case "item one":
					
					selectedBox.Style = null;
					Box1.Style = highlightStyle;
					selectedBox = Box1;
                    break;
                case "item two":
					
					selectedBox.Style = null;
					Box2.Style = highlightStyle;
					selectedBox = Box2;
                    break;
                case "item three":
					
					selectedBox.Style = null;
					Box3.Style = highlightStyle;
					selectedBox = Box3;
                    break;
                case "item four":

                    selectedBox.Style = null;
                    Box4.Style = highlightStyle;
                    selectedBox = Box4;
                    break;
                case "item five":

                    selectedBox.Style = null;
                    Box5.Style = highlightStyle;
                    selectedBox = Box5;
                    break;
                case "item with a long name":

                    selectedBox.Style = null;
                    Box6.Style = highlightStyle;
                    selectedBox = Box6;
                    break;
                case "big item":
					
					selectedBox.Style = null;
					bigBox.Style = highlightStyle;
					selectedBox = bigBox;
                    break;
                case "select tabs":
					
					isGridInFocus = false;
                    myTextScrollAnim.Begin();
                    break;
                case "select grid":
					
					isGridInFocus = true;
                    myItemScrollAnim.Begin();
                    break;
				case "clear":
					
					selectedBox.Style = null;
                    break;
				case "default":
					
					if(isGridInFocus) {
						scroller = (Storyboard)TryFindResource("GridScrollRight");
						
						selectedBox.Style = null;
            			selectedBox = bigBox;
						selectedBox.Style = highlightStyle;
						
					} else {
						scroller = (Storyboard)TryFindResource("TextScrollRight");
						
						selectedBox.Style = null;
            			selectedBox = bigBox;
						selectedBox.Style = highlightStyle;
						selectedTab.Style = null;
            			selectedTab = homeTab;
						selectedTab.Style = highlightText;
					}
					
					scroller.Begin();
                    break;
				case "home":
                    bigModText.Text = "Home Feature";
					selectedTab.Style = null;
            		selectedTab = homeTab;
					selectedTab.Style = highlightText;
                    break;
                case "weather":
                    bigModText.Text = "Weather Feature";
					selectedTab.Style = null;
            		selectedTab = weatherTab;
					selectedTab.Style = highlightText;
                    break;
				case "movies":
                    bigModText.Text = "Movies Feature";
					selectedTab.Style = null;
            		selectedTab = moviesTab;
					selectedTab.Style = highlightText;
                    break;
                case "tv":
                    bigModText.Text = "TV Feature";
                    selectedTab.Style = null;
                    selectedTab = tvTab;
                    selectedTab.Style = highlightText;
                    break;
                case "finance":
                    bigModText.Text = "Finance Feature";
                    selectedTab.Style = null;
                    selectedTab = financeTab;
                    selectedTab.Style = highlightText;
                    break;
                case "travel":
                    bigModText.Text = "Travel Feature";
                    selectedTab.Style = null;
                    selectedTab = travelTab;
                    selectedTab.Style = highlightText;
                    break;
                case "music":
                    bigModText.Text = "Music Feature";
                    selectedTab.Style = null;
                    selectedTab = musicTab;
                    selectedTab.Style = highlightText;
                    break;
                case "news":
                    bigModText.Text = "News Feature";
                    selectedTab.Style = null;
                    selectedTab = newsTab;
                    selectedTab.Style = highlightText;
                    break;
                case "sports":
                    bigModText.Text = "Sports Feature";
                    selectedTab.Style = null;
                    selectedTab = sportsTab;
                    selectedTab.Style = highlightText;
                    break;
            }


        }

        private void SetUpSpeech() {
            if (speechRek == null) {
                speechRek = new SpeechRek.SpeechRek();
                speechRek.OnWordHeard += new WordHeardEventHandler(sp_OnWordHeard);
                speechRek.OnSpeechHeard += new SpeechHeardEventHandler(speechRek_OnSpeechHeard);
                speechRek.SetWords(words);
                speechRek.ActivateMultiple();
            }
        }

        void speechRek_OnSpeechHeard(object sender, EventArgs e) {
            //Box5.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void Window_Activated(object sender, EventArgs e) {
            SetUpSpeech();
        }

        /*
        private void ButtonAddWord_Click(object sender, RoutedEventArgs e) {
            if (textBoxWord.Text != string.Empty) {
                speechRek.AddWord(textBoxWord.Text);
                listBoxOutput.Items.Add("Word added: " + textBoxWord.Text);
                textBoxWord.Text = string.Empty;
                speechRek.ActivateMultiple();
            }
        }

        private void textBoxWord_KeyUp(object sender, KeyEventArgs e) {
            if (e.Key == Key.Enter) {
                ButtonAddWord_Click(this, new RoutedEventArgs());
            }
        }
        */

	}//cls
}