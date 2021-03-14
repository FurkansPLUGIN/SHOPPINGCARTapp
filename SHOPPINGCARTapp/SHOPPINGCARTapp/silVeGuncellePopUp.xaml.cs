using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Realms;
namespace fingerDraw
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class silVeGuncellePopUp : Rg.Plugins.Popup.Pages.PopupPage
    {
        string realKisiId;
        public silVeGuncellePopUp(string KisiId)
        {
            InitializeComponent();
            realKisiId = KisiId;
        }

        private async void sil_Clicked(object sender, EventArgs e)
        {
            var realmDb = Realm.GetInstance();
            var seciliKisi = realmDb.All<Kullanici>().First(s => s.ID == Convert.ToInt32(realKisiId));
            try
            {
                using (var db = realmDb.BeginWrite())
                {
                    realmDb.Remove(seciliKisi);
                    db.Commit();
                }
                await DisplayAlert("Warning", "Delete", "OK");
                await Navigation.PopPopupAsync();

            }
            catch
            {
                await DisplayAlert("Warning", "UnDelete", "OK");
            }
        }

        private async void guncelle_Clicked(object sender, EventArgs e)
        {
            var realmDb = Realm.GetInstance();
            var seciliKisi = realmDb.All<Kullanici>().First(s => s.ID == Convert.ToInt32(realKisiId));

            try
            {
                using (var db = realmDb.BeginWrite())
                {
                    seciliKisi.items = "Update" + up.Text;

                    db.Commit();
                }
                await DisplayAlert("Warning", "Updated", "OK");
                await Navigation.PopPopupAsync();
            }
            catch
            {
                await DisplayAlert("Warning", "Unsuccessful", "OK");
            }
        }

        private async void geri_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopPopupAsync();
        }
    }
}