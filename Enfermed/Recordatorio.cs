using System.Collections.Generic;
using Android.OS;
using Android.App;
using Enfermed.Fragments;
using Java.Lang;

// importamos libreria AppCompatActivity
using Android.Support.V4.App;
using Android.Support.V7.App;
using Android.Support.V4.View;
using Android.Support.Design.Widget;
using Toolbar = Android.Support.V7.Widget.Toolbar;
using SupportFragment = Android.Support.V4.App.Fragment;
using SupportFragmentManager = Android.Support.V4.App.FragmentManager;

namespace Enfermed
{
    [Activity(Label = "Recordatorio", ParentActivity = typeof(Panel), Theme = "@style/MyTheme")]
    public class Recordatorio : AppCompatActivity
    {
        private Toolbar _toolbar;
        private TabLayout _tabs;
        private ViewPager _viewPager;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Recordatorio);

            _toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            _tabs = FindViewById<TabLayout>(Resource.Id.tabs);
            _viewPager = FindViewById<ViewPager>(Resource.Id.viewpager);
    
            SetSupportActionBar(_toolbar);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true); // Una marca atrás en el icono en ActionBar
            SetUpViewPager(_viewPager);
            _tabs.SetupWithViewPager(_viewPager);
        }

        private void SetUpViewPager(ViewPager viewPager)
        {
            TabAdapter adapter = new TabAdapter(SupportFragmentManager);
            adapter.AddFragment(new RecordMedicamentoFragment(), "Medicamento");
            adapter.AddFragment(new RecordRotacionFragment(), "Rotación");

            viewPager.Adapter = adapter;
        }

        public class TabAdapter : FragmentPagerAdapter
        {
            public List<SupportFragment> Fragments { get; set; }
            public List<string> FragmentNames { get; set; }

            public TabAdapter(SupportFragmentManager sfm) : base(sfm)
            {
                Fragments = new List<SupportFragment>();
                FragmentNames = new List<string>();
            }

            public void AddFragment(SupportFragment fragment, string name)
            {
                Fragments.Add(fragment);
                FragmentNames.Add(name);
            }

            public override int Count
            {
                get
                {
                    return Fragments.Count;
                }
            }

            public override SupportFragment GetItem(int position)
            {
                return Fragments[position];
            }

            public override ICharSequence GetPageTitleFormatted(int position)
            {
                return new Java.Lang.String(FragmentNames[position]);
            }
        }
    }
}