﻿using LatinPhrasesApp.Data;
using LatinPhrasesApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LatinPhrasesApp.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LatinPhrasesApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuPage : ContentPage
    {
        public List<LatinPhrase> LatinPhrases { get; set; }
        private LatinPhraseData _latinPhraseData;
        public MenuPage()
        {
            InitializeComponent();
            _latinPhraseData = new LatinPhraseData();

            BindingContext = this;
        }

        private async void OnHomeButtonClicked(object sender, EventArgs e)
        {
            NavigateToPage(new HomePage());
        }

        private async void OnAuthorsListButtonClicked(object sender, EventArgs e)
        {
            NavigateToPage(new AuthorsListPage());
        }

        private async void OnFavoriteLatinPhrasesButtonClicked(object sender, EventArgs e)
        {
            NavigateToPage(new FavoriteLatinPhrasesPage());
        }
        
        
        private async void OnLatinPhrasesListButtonClicked(object sender, EventArgs e)
        {
           
            NavigateToPage(new LatinPhrasesListPage());
        }
        private async void OnMyLatinPhrasesButtonClicked(object sender, EventArgs e)
        {
            NavigateToPage(new MyLatinPhrasesPage());
        }


        private void NavigateToPage(ContentPage page)
        {
            var mainPage = (FlyoutPage)Parent;
            mainPage.Detail = new NavigationPage(page);
            mainPage.IsPresented = false;
        }
    }
}