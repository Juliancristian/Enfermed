using System;
using Android;
using Android.OS;
using Android.App;
using Android.Util;
using Android.Views;
using Android.Widget;
using Android.Runtime;
using Android.Graphics;
using Android.Content.PM;
using Android.Gms.Vision;
using Android.Gms.Vision.Barcodes;
using static Android.Gms.Vision.Detector;

// importamos libreria AppCompatActivity
using Android.Support.V4.App;
using Android.Support.V7.App;
using Toolbar = Android.Support.V7.Widget.Toolbar;
using AlertDialog = Android.App.AlertDialog;

namespace Enfermed
{
    [Activity(Label = "Escanear Código Barras", ParentActivity = typeof(PacienteList), Theme = "@style/MyTheme")]
    public class ScanCodeBarras : AppCompatActivity, ISurfaceHolderCallback, IProcessor

    {
        const int RequestCameraPermisionID = 1001;
        private SurfaceView _surfaceView;
        private TextView _txtResult;
        private BarcodeDetector _barcodeDetector;
        private CameraSource _cameraSource;

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            switch (requestCode)
            {
                case RequestCameraPermisionID:
                    {
                        if (grantResults[0] == Permission.Granted)
                        {
                            if (ActivityCompat.CheckSelfPermission(ApplicationContext, Manifest.Permission.Camera) != Android.Content.PM.Permission.Granted)
                            {
                                //Solicitar Permiso 
                                ActivityCompat.RequestPermissions(this, new string[]
                                {
                                    Manifest.Permission.Camera
                                }, RequestCameraPermisionID);
                                return;
                            }
                            try
                            {
                                _cameraSource.Start(_surfaceView.Holder);
                            }
                            catch (InvalidOperationException)
                            {
                            }
                        }
                    }
                    break;
            }
        }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ScanCodeBarras);

            _surfaceView = FindViewById<SurfaceView>(Resource.Id.cameraView);
            _txtResult = FindViewById<TextView>(Resource.Id.txtResult);

            // Traemos la barra toobar
            Toolbar toolBar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolBar);

            // Una marca atrás en el icono en ActionBar
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);

            // Creo el detector qr
            _barcodeDetector = new BarcodeDetector.Builder(this)
                .SetBarcodeFormats(BarcodeFormat.Ean13)
                .Build();

            // Creo la camara
            _cameraSource = new CameraSource.Builder(this, _barcodeDetector)
                .SetRequestedPreviewSize(1600, 1024)
                .SetAutoFocusEnabled(true)
                .Build();

            _surfaceView.Holder.AddCallback(this);
            _barcodeDetector.SetProcessor(this);
        }

        public void SurfaceChanged(ISurfaceHolder holder, [GeneratedEnum] Format format, int width, int height)
        {
        }

        public void SurfaceCreated(ISurfaceHolder holder)
        {
            if (ActivityCompat.CheckSelfPermission(ApplicationContext, Manifest.Permission.Camera) != Android.Content.PM.Permission.Granted)
            {
                //Solicitar Permiso
                ActivityCompat.RequestPermissions(this, new string[]
                {
                    Manifest.Permission.Camera
                }, RequestCameraPermisionID);
                return;
            }
            try
            {
                _cameraSource.Start(_surfaceView.Holder);
            }
            catch (InvalidOperationException)
            {
            }
        }
        public void SurfaceDestroyed(ISurfaceHolder holder)
        {
            _cameraSource.Stop();
        }
        public void ReceiveDetections(Detections detections)
        {
            SparseArray qrcodes = detections.DetectedItems;
            if (qrcodes.Size() != 0)
            {

                _txtResult.Post(() => {
                    _txtResult.Text = ((Barcode)qrcodes.ValueAt(0)).RawValue;
                    AlertDialog.Builder dialog = new AlertDialog.Builder(this);
                    AlertDialog alert = dialog.Create();
                    alert.SetTitle("Código de Barras");
                    alert.SetMessage(((Barcode)qrcodes.ValueAt(0)).RawValue);
                    alert.SetIcon(Resource.Drawable.logo);
                    alert.SetButton("Volver", (c, ev) =>
                    {
                        // Para actualizar una actividad desde dentro de sí mismo
                        Finish(); StartActivity(Intent);

                        GC.Collect();
                    });

                    alert.Show();
                });
              

            }
        }
        public void Release()
        {
        }
    }
}