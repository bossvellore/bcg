package md5641421c50131b0c7b266d14a2ae8f082;


public class MainActivity_LabelPrintDocumentAdapter
	extends android.print.PrintDocumentAdapter
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onLayout:(Landroid/print/PrintAttributes;Landroid/print/PrintAttributes;Landroid/os/CancellationSignal;Landroid/print/PrintDocumentAdapter$LayoutResultCallback;Landroid/os/Bundle;)V:GetOnLayout_Landroid_print_PrintAttributes_Landroid_print_PrintAttributes_Landroid_os_CancellationSignal_Landroid_print_PrintDocumentAdapter_LayoutResultCallback_Landroid_os_Bundle_Handler\n" +
			"n_onWrite:([Landroid/print/PageRange;Landroid/os/ParcelFileDescriptor;Landroid/os/CancellationSignal;Landroid/print/PrintDocumentAdapter$WriteResultCallback;)V:GetOnWrite_arrayLandroid_print_PageRange_Landroid_os_ParcelFileDescriptor_Landroid_os_CancellationSignal_Landroid_print_PrintDocumentAdapter_WriteResultCallback_Handler\n" +
			"";
		mono.android.Runtime.register ("Novice.MainActivity+LabelPrintDocumentAdapter, Novice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", MainActivity_LabelPrintDocumentAdapter.class, __md_methods);
	}


	public MainActivity_LabelPrintDocumentAdapter () throws java.lang.Throwable
	{
		super ();
		if (getClass () == MainActivity_LabelPrintDocumentAdapter.class)
			mono.android.TypeManager.Activate ("Novice.MainActivity+LabelPrintDocumentAdapter, Novice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

	public MainActivity_LabelPrintDocumentAdapter (android.content.Context p0) throws java.lang.Throwable
	{
		super ();
		if (getClass () == MainActivity_LabelPrintDocumentAdapter.class)
			mono.android.TypeManager.Activate ("Novice.MainActivity+LabelPrintDocumentAdapter, Novice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.Content.Context, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0 });
	}


	public void onLayout (android.print.PrintAttributes p0, android.print.PrintAttributes p1, android.os.CancellationSignal p2, android.print.PrintDocumentAdapter.LayoutResultCallback p3, android.os.Bundle p4)
	{
		n_onLayout (p0, p1, p2, p3, p4);
	}

	private native void n_onLayout (android.print.PrintAttributes p0, android.print.PrintAttributes p1, android.os.CancellationSignal p2, android.print.PrintDocumentAdapter.LayoutResultCallback p3, android.os.Bundle p4);


	public void onWrite (android.print.PageRange[] p0, android.os.ParcelFileDescriptor p1, android.os.CancellationSignal p2, android.print.PrintDocumentAdapter.WriteResultCallback p3)
	{
		n_onWrite (p0, p1, p2, p3);
	}

	private native void n_onWrite (android.print.PageRange[] p0, android.os.ParcelFileDescriptor p1, android.os.CancellationSignal p2, android.print.PrintDocumentAdapter.WriteResultCallback p3);

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
