using LatinPhrasesApp.Models;
using LatinPhrasesApp.Services;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LatinPhrasesApp.ViewModels
{
    public class AuthorsListViewModel : BaseViewModel
    {
        private readonly IDataService _dataService;

        private ObservableCollection<Author> _authors;
        public ObservableCollection<Author> Authors
        {
            get => _authors;
            set => SetProperty(ref _authors, value);
        }

        private Author _selectedAuthor;
        private IEnumerable<Author> _allAuthors;

        public Author SelectedAuthor
        {
            get => _selectedAuthor;
            set => SetProperty(ref _selectedAuthor, value);
        }

        public Command LoadAuthorsCommand { get; }
        public Command SearchCommand { get; }
        public Command AuthorTappedCommand { get; }

        public AuthorsListViewModel(IDataService dataService)
        {
            _dataService = dataService;
            Authors = new ObservableCollection<Author>();

            LoadAuthorsCommand = new Command(async () => await ExecuteLoadAuthorsCommand());
            SearchCommand = new Command<string>(async (searchText) => await ExecuteSearchCommand(searchText));
            AuthorTappedCommand = new Command<Author>(OnAuthorTapped);
        }
        public void FilterAuthors(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                Authors = new ObservableCollection<Author>(_allAuthors);
            }
            else
            {
                Authors = new ObservableCollection<Author>(_allAuthors.Where(author => author.Name.ToLower().Contains(searchTerm.ToLower())));
            }
        }

        private async Task ExecuteLoadAuthorsCommand()
        {
            IsBusy = true;

            try
            {
                var authors = await _dataService.GetAuthorsAsync();
                Authors.Clear();
                foreach (var author in authors)
                {
                    Authors.Add(author);
                }
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task ExecuteSearchCommand(string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
            {
                await ExecuteLoadAuthorsCommand();
                return;
            }

            IsBusy = true;

            try
            {
                var authors = await _dataService.SearchAuthorsAsync(searchText);
                Authors.Clear();
                foreach (var author in authors)
                {
                    Authors.Add(author);
                }
            }
            finally
            {
                IsBusy = false;
            }
        }

        private void OnAuthorTapped(Author author)
        {
            if (author == null)
                return;

            SelectedAuthor = null;
        }

        public async Task OnAppearing()
        {
            await ExecuteLoadAuthorsCommand();
        }
    }
}
