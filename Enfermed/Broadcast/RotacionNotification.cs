using Android.App;
using Android.Net;
using Android.Media;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Enfermed.Models;
using Enfermed.Services;
using Java.Util;

// importamos libreria AppCompatActivity
using Android.Support.V4.App;
using Android.Support.V4.Content;
using TaskStackBuilder = Android.Support.V4.App.TaskStackBuilder;

namespace Enfermed.Broadcast
{
    [BroadcastReceiver]
    public class RotacionNotification : BroadcastReceiver
    {
        private Rotacion _rotacion;
        private RotacionService _rotacionService;

        public override void OnReceive(Context context, Intent intent)
        {
            // Instanciamos
            _rotacion = new Rotacion();
            _rotacionService = new RotacionService();
            _rotacion = _rotacionService.selectRotacion(); // Selecciona la rotacion

            if (_rotacion != null)
            {
                Intent newIntent = new Intent(context, typeof(RotacionAlert));

                // Agrega la siguiente tarea a la pila
                TaskStackBuilder stackBuilder = TaskStackBuilder.Create(context);
                stackBuilder.AddParentStack(Java.Lang.Class.FromType(typeof(RotacionAlert)));
                stackBuilder.AddNextIntent(newIntent);

                // Establecer la intencion que se abrira cuando un clic en la notificación
                PendingIntent notificationIntent = stackBuilder.GetPendingIntent(0, (int)PendingIntentFlags.UpdateCurrent);

                // Define bitmap para LargeIcon
                Bitmap bitmap = ((BitmapDrawable)ContextCompat.GetDrawable(context, Resource.Drawable.logo)).Bitmap;

                // Define sound
                Uri soundUri = RingtoneManager.GetDefaultUri(RingtoneType.Alarm);

                // Creamos la Notificacion
                var builder = new NotificationCompat.Builder(context)
                    .SetContentIntent(notificationIntent)
                    .SetSmallIcon(Resource.Drawable.logo)          // Icono pequeño
                    .SetLargeIcon(bitmap)                          // Icono grande
                    .SetContentTitle("Recordar Rotación")               // Titulo
                    .SetContentText("Ver detalles..")              // Contenido  
                    .SetSound(soundUri)                            // Sonido      
                    .SetVibrate(new long[] { 100, 250, 100, 500 }) // Vibración
                    .SetAutoCancel(true);

                // Mostrar Notificacion
                var manager = (NotificationManager)context.GetSystemService(Context.NotificationService);

                Random random = new Random(); // id random para notificacion
                int randomNumber = random.NextInt(9999 - 1000) + 1000;
                manager.Notify(randomNumber, builder.Build());
            }
        }
    }
}