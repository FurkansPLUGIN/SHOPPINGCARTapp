using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Realms;
using Rg.Plugins.Popup.Extensions;

namespace fingerDraw
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            
        }

        private async void save_Clicked(object sender, EventArgs e)
        {
            var realmDb = Realm.GetInstance();
            var datam = realmDb.All<Kullanici>().ToList();

            var maxID = 0;
            if (datam.Count != 0)
            {
                maxID = datam.Max(m => m.ID) + 1;
            }
            var kullan = new Kullanici
            {
                ID = maxID,
                items = Aitem.Text
            };
            realmDb.Write(() =>
            {
                realmDb.Add(kullan);
            });
            var dataList = realmDb.All<Kullanici>().ToList();
            await DisplayAlert("Warning", "Successful", "OK");
            list.ItemsSource = dataList;
            Aitem.Text = "";
        }

       

        private async void see_Clicked(object sender, EventArgs e)
        {
            var realmDb = Realm.GetInstance();
            var myItemSource = realmDb.All<Kullanici>().ToList();
            list.ItemsSource = myItemSource;
        }

        private async void list_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return;
            }
            var selectKisi = (Kullanici)e.SelectedItem;
            var lv = (ListView)sender;

            await Navigation.PushPopupAsync(new silVeGuncellePopUp(selectKisi.ID.ToString()));
            lv.SelectedItem = null;
        }

        private void list_Refreshing(object sender, EventArgs e)
        {
            var realmDb = Realm.GetInstance();
            var datam = realmDb.All<Kullanici>().ToList();
            list.ItemsSource = datam;
            list.IsRefreshing = false;//#323232
        }
    }
}
