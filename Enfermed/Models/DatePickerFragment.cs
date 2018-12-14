using System;
using Android.OS;
using Android.App;
using Android.Widget;

namespace Enfermed.Models
{
    // Create a class DatePickerFragment  
    public class DatePickerFragment : DialogFragment,
        DatePickerDialog.IOnDateSetListener
    {
        // TAG puede ser cualquier cadena de su elección.  
        public static readonly string TAG = "X:" + typeof(DatePickerFragment).Name.ToUpper();

        // Inicialice este valor para evitar NullReferenceExceptions.  
        Action<DateTime> _dateSelectedHandler = delegate { };

        public static DatePickerFragment NewInstance(Action<DateTime> onDateSelected)
        {
            DatePickerFragment frag = new DatePickerFragment();
            frag._dateSelectedHandler = onDateSelected;
            return frag;
        }
        public override Dialog OnCreateDialog(Bundle savedInstanceState)
        {
            DateTime now = DateTime.Now;
            DatePickerDialog dialog = new DatePickerDialog(Activity, this, now.Year, now.Month - 1, now.Day); // SE RESTA EL MES -1
            return dialog;
        }
        public void OnDateSet(DatePicker view, int year, int monthOfYear, int dayOfMonth)
        {
            // Nota: monthOfYear es un valor entre 0 y 11, no 1 y 12!
            DateTime selectedDate = new DateTime(year, monthOfYear + 1, dayOfMonth);
            _dateSelectedHandler(selectedDate);
        }
    }
}