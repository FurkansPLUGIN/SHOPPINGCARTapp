using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Realms;
using Rg.Plugins.Popup.Extensions;

namespace fingerDraw
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Islemler : ContentPage
    {
        public Islemler()
        {
            InitializeComponent();
            var realmDb = Realm.GetInstance();
            var myItemSource = realmDb.All<Kullanici>().ToList();
            list.ItemsSource = myItemSource;
        }

        private void list_Refreshing(object sender, EventArgs e)
        {
            var realmDb = Realm.GetInstance();
            var datam = realmDb.All<Kullanici>().ToList();
            list.ItemsSource = datam;
            list.IsRefreshing = false;
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
    }
}