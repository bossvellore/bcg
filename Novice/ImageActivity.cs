using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Renderscripts;
using Android.Print;
using Android.Support.V4.Print;
using Android.Support.V4.Graphics;
using System.IO;
using ZXing;
using ZXing.Mobile;
using Android.Graphics;

namespace Novice
{
    [Activity(Label = "QR Image")]
    public class ImageActivity : Activity
    {
        ImageView imageBarcode;
      
       protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            string text = Intent.GetStringExtra("qrText") ?? "Data not available";
            SetContentView(Resource.Layout.Imagelayout);
            ActionBar.SetDisplayHomeAsUpEnabled(true);
            ActionBar.SetHomeButtonEnabled(true);


            imageBarcode = FindViewById<ImageView>(Resource.Id.imageBarcode);
            var barcodeWriter = new ZXing.Mobile.BarcodeWriter();
            var Options = new ZXing.Common.EncodingOptions
            {
                Width = 800,
                Height = 400,
                Margin = 0,
                PureBarcode = false,
            };
            var textType = new ZxingOverlayView(this);
            textType.BottomText = "hello";
            barcodeWriter.Renderer = new BitmapRenderer();
            barcodeWriter.Options = Options;
			barcodeWriter.Format = BarcodeFormat.CODE_128;
           //   barcodeWriter.Encode(text);
            var barcode = barcodeWriter.Write(text);
			// g = Graphics.FromImage(barcode);
			float scale = Resources.DisplayMetrics.Density;
			Canvas canvas = new Canvas(barcode);
			// new antialised Paint
			Paint paint = new Paint(PaintFlags.AntiAlias);
			paint.SetStyle(Paint.Style.FillAndStroke);
			paint.TextSize = 20*scale;
			Rect bounds = new Rect();;
			paint.GetTextBounds(text, 0, text.Length, bounds);
			int x = (barcode.Width - bounds.Width()) / 2;
			int y = (barcode.Height - bounds.Height()) ;
			paint.Color = new Color(250, 250, 250);
			canvas.DrawRect(0, y-10, barcode.Width, barcode.Height, paint);
			paint.Color = new Color(10, 110, 80);
			canvas.DrawText(text, x, y+bounds.Height()-5, paint);
            imageBarcode.SetImageBitmap(barcode);
            Button buttonPrint = FindViewById<Button>(Resource.Id.buttonPrint);
            
			buttonPrint.Click += delegate {
                PrintHelper photoPrinter = new PrintHelper(this);
                photoPrinter.ScaleMode = PrintHelper.ScaleModeFit;
                //Bitmap bitmap = BitmapFactory.decodeResource(getResources(),R.drawable.droids);
                photoPrinter.PrintBitmap("droids.jpg - test print", barcode);
            };
        }
        public override bool OnOptionsItemSelected(IMenuItem item)
        {

            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    this.Finish();
                    return true;
            };
            return base.OnOptionsItemSelected(item);
        }
    }
}