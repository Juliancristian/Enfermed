package md5048a04284979bff265231e98628579ce;


public class AwaitableTaskCompleteListener_1
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.google.android.gms.tasks.OnCompleteListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onComplete:(Lcom/google/android/gms/tasks/Task;)V:GetOnComplete_Lcom_google_android_gms_tasks_Task_Handler:Android.Gms.Tasks.IOnCompleteListenerInvoker, Xamarin.GooglePlayServices.Tasks\n" +
			"";
		mono.android.Runtime.register ("Android.Gms.Extensions.AwaitableTaskCompleteListener`1, Xamarin.GooglePlayServices.Tasks, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", AwaitableTaskCompleteListener_1.class, __md_methods);
	}


	public AwaitableTaskCompleteListener_1 () throws java.lang.Throwable
	{
		super ();
		if (getClass () == AwaitableTaskCompleteListener_1.class)
			mono.android.TypeManager.Activate ("Android.Gms.Extensions.AwaitableTaskCompleteListener`1, Xamarin.GooglePlayServices.Tasks, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onComplete (com.google.android.gms.tasks.Task p0)
	{
		n_onComplete (p0);
	}

	private native void n_onComplete (com.google.android.gms.tasks.Task p0);

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
