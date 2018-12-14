package md512b8a9f11b5dd8147b3eda9145f9b3fa;


public class MedicamentoNotification
	extends android.content.BroadcastReceiver
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onReceive:(Landroid/content/Context;Landroid/content/Intent;)V:GetOnReceive_Landroid_content_Context_Landroid_content_Intent_Handler\n" +
			"";
		mono.android.Runtime.register ("Enfermed.Broadcast.MedicamentoNotification, Enfermed, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", MedicamentoNotification.class, __md_methods);
	}


	public MedicamentoNotification () throws java.lang.Throwable
	{
		super ();
		if (getClass () == MedicamentoNotification.class)
			mono.android.TypeManager.Activate ("Enfermed.Broadcast.MedicamentoNotification, Enfermed, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onReceive (android.content.Context p0, android.content.Intent p1)
	{
		n_onReceive (p0, p1);
	}

	private native void n_onReceive (android.content.Context p0, android.content.Intent p1);

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
