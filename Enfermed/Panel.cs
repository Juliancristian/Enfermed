using System;
using Android.OS;
using Android.App;
using Android.Views;
using Android.Content;

// importamos libreria AppCompatActivity
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Toolbar = Android.Support.V7.Widget.Toolbar;
using SupportActionBar = Android.Support.V7.App.ActionBar;
using Android.Support.Design.Widget;
using AlertDialog = Android.App.AlertDialog;

namespace Enfermed
{
    [Activity(Label = "Enfermed", Theme = "@style/MyTheme")]
    public class Panel : AppCompatActivity
    {
        private DrawerLayout _mDrawerLayout;
        private Toolbar _toolbar;
        private CardView _btnRecordatorio, _btnCalculadora, _btnGuia, _btnPacientes;
        private IMenuItem previousItem;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Panel);
            
            _mDrawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout); // Traemos drawer layout 
            _toolbar = FindViewById<Toolbar>(Resource.Id.toolbar); // Traemos la barra toobar
            SetSupportActionBar(_toolbar); // características predetermindas de la barra de acciones

            SupportActionBar ab = SupportActionBar;
            ab.SetHomeAsUpIndicator(Resource.Drawable.ic_drawer); // Icon Drawer
            ab.SetDisplayHomeAsUpEnabled(true);
            
            NavigationView navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            if (navigationView != null)
            {
                SetUpDrawerContent(navigationView);    
            }

            // Traemos el boton para la siguiente Activity: RECORDATORIO
            _btnRecordatorio = FindViewById<CardView>(Resource.Id.btnRecordatorio);
            _btnRecordatorio.Click += (sender, args) =>
            {
                /* 
                Iniciando actividades con Intents.
                Se puede usar las intents para varias tareas, pero en este caso el intent inicia otra activity.           
                */
                Intent myIntent = new Intent(this, typeof(Recordatorio));
            };

            // Traemos el boton para la siguiente Activity: CALCULADORA
            _btnCalculadora = FindViewById<CardView>(Resource.Id.btnCalculadora);
            _btnCalculadora.Click += (sender, args) =>
            {
                Intent myIntent = new Intent(this, typeof(Calculadora));
            };

            // Traemos el boton para la siguiente Activity: GUIA
            _btnGuia = FindViewById<CardView>(Resource.Id.btnGuia);
            _btnGuia.Click += (sender, args) =>
            {
                Intent myIntent = new Intent(this, typeof(GuiaList));
            };

            // Traemos el boton para la siguiente Activity: PACIENTES
            _btnPacientes = FindViewById<CardView>(Resource.Id.btnPacientes);
            _btnPacientes.Click += (sender, args) =>
            {
                Intent myIntent = new Intent(this, typeof(PacienteList));
            };
        }

        // Navigation View
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    _mDrawerLayout.OpenDrawer((int)GravityFlags.Left);       
                    return true;

                default:
                    return base.OnOptionsItemSelected(item);
            }
        }

        private void SetUpDrawerContent(NavigationView navigationView)
        {
            navigationView.NavigationItemSelected += (object sender, NavigationView.NavigationItemSelectedEventArgs e) =>
            {
                e.MenuItem.SetChecked(true);        
                previousItem = e.MenuItem;
                switch (e.MenuItem.ItemId)
                {
                    case Resource.Id.nav_recordatorio:
                        ListItemClicked(0);
                        break;

                    case Resource.Id.nav_calculadora:
                        ListItemClicked(1);
                        break;
                    case Resource.Id.nav_guia:
                        ListItemClicked(2);
                        break;

                    case Resource.Id.nav_pacientes:
                        ListItemClicked(3);
                        break;

                    case Resource.Id.nav_salir:
                        ListItemClicked(4);
                        break;
                }

                _mDrawerLayout.CloseDrawers();
            };
        }

        private void ListItemClicked(int position)
        {
            Intent intent = null;

            switch (position)
            {
                case 0:
                    intent = new Intent(this, typeof(Recordatorio));
                    break;
                case 1:
                    intent = new Intent(this, typeof(Calculadora));
                    break;
                case 2:
                    intent = new Intent(this, typeof(GuiaList));
                    break;
                case 3:
                    intent = new Intent(this, typeof(PacienteList));
                    break;
                case 4:
                    AlertDialog.Builder dialog = new AlertDialog.Builder(this);
                    AlertDialog alert = dialog.Create();
                    alert.SetTitle("Salir");
                    alert.SetMessage("¿Estás seguro?");
                    alert.SetIcon(Resource.Drawable.logo);
                    alert.SetButton("Si", (c, ev) =>
                    {
                        this.FinishAffinity();
                        Finish();
                        Android.OS.Process.KillProcess(Android.OS.Process.MyPid());

                        GC.Collect();
                    });

                    alert.SetButton2("no", (c, ev) => { });
                    alert.Show();
                    break;
            }

            if (intent != null)
            {
                StartActivity(intent);
            }
        }
    }
}