using SqlLiteXam.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SqlLiteXam
{
    public partial class MainPage : ContentPage
    {
        Person selectedPerson;
        public MainPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            collectionView.ItemsSource = await App.Database.GetAllPeopleAsync();
        }

        //Create and Update
        async void Insert_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(nameEntry.Text) && !string.IsNullOrWhiteSpace(ageEntry.Text))
            {
                if(selectedPerson == null)
                {
                    await App.Database.SavePersonAsync(new Person
                    {
                        Name = nameEntry.Text,
                        Age = int.Parse(ageEntry.Text)
                    });
                }
                else
                {
                    selectedPerson.Name = nameEntry.Text;
                    selectedPerson.Age = int.Parse(ageEntry.Text);
                    await App.Database.UpdatePersonAsync(selectedPerson);
                    selectedPerson = null;
                }
                ClearEntries();
                collectionView.ItemsSource = await App.Database.GetAllPeopleAsync();
            }
        }

        private void collectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(e.CurrentSelection != null)
            {
                selectedPerson = e.CurrentSelection.FirstOrDefault() as Person;
                nameEntry.Text = selectedPerson.Name;
                ageEntry.Text = selectedPerson.Age.ToString();
            }
        }

        //Delete
        private async void Delete_Clicked(object sender, EventArgs e)
        {
            if(selectedPerson != null)
            {
                int responseId = await App.Database.DeletePersonAsync(selectedPerson);
                selectedPerson = null;
                ClearEntries();
                collectionView.ItemsSource = await App.Database.GetAllPeopleAsync();
            }
        }

        ////Update
        //private void Update_Clicked(object sender, EventArgs e)
        //{
        //    if(string.IsNullOrEmpty(nameEntry.Text) && string.IsNullOrEmpty(ageEntry.Text))
        //    {
        //        selectedPerson = null;
        //    }
        //    else
        //    {

        //    }
        //}

        private void ClearEntries()
        {
            nameEntry.Text = ageEntry.Text = string.Empty;
        }

        private async void GetAdultPeoplecLINQ_Clicked(object sender, EventArgs e)
        {
            collectionView.ItemsSource = await App.Database.GetAdultPeopleLINQ();
        }
        private async void GetAdultPeopleQuery_Clicked(object sender, EventArgs e)
        {
            collectionView.ItemsSource = await App.Database.GetAdultPeopleQuery();
        }
    }
}
