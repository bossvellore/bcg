using Android.App;
using Android.Widget;
using Android.OS;
using ZXing;
using ZXing.Mobile;
using Android.Content;
using System;
using Android.Graphics;
using Android.Support.V4.Print;
using Android.Icu.Text;
using Java.Util;

namespace Novice
{
    [Activity(Label = "Label Printer", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
		ImageView previewImage;
		Bitmap barcode;
		EditText date;
		EditText itemNumber;
		EditText caseCount;
		EditText skidNumber;
		EditText parallelizer;
		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.Main);
			previewImage = FindViewById<ImageView>(Resource.Id.previewImg);


			itemNumber = FindViewById<EditText>(Resource.Id.itemNumber);
			caseCount = FindViewById<EditText>(Resource.Id.caseCount);
			skidNumber = FindViewById<EditText>(Resource.Id.skidNumber);
			parallelizer = FindViewById<EditText>(Resource.Id.parallelizer);

			date = FindViewById<EditText>(Resource.Id.date);
			//var currentTime = new Date();
			//var format = new SimpleDateFormat("MMM dd,yyyy hh:mm a");
			//string dateString = format.Format(currentTime);
			//date.Text = dateString;
			date.Text = DateTime.Now.ToString("yy-MM-dd hh:mm");
			date.Click += (sender, e) =>
			{
				new DatePickerDialog(this, DatePickerChangeListener, DateTime.Now.Year, 
				                     DateTime.Now.Month, DateTime.Now.Day).Show();
			};

			Button printBtn = FindViewById<Button>(Resource.Id.printBtn);
			printBtn.Click += delegate
			{
				string text = this.GetBarcodeText();
				GenerateBarcode(text);
				PrintBarcode();
			};

			Button previewBtn = FindViewById<Button>(Resource.Id.previewBtn);
			previewBtn.Click += (sender, e) =>
			{
				string text = this.GetBarcodeText();
				this.GenerateBarcode(text);
				previewImage.SetImageBitmap(barcode);
			};
		}

		void DatePickerChangeListener(object sender, DatePickerDialog.DateSetEventArgs e)
		{
			//throw new NotImplementedException();
			date.Text = e.Date.ToString("yy-MM-dd hh:mm");
		}

		private string GetBarcodeText()
		{
			return date.Text + itemNumber.Text + caseCount.Text + skidNumber.Text + parallelizer.Text;
		}
		private void PrintBarcode()
		{
			PrintHelper photoPrinter = new PrintHelper(this);
			photoPrinter.ScaleMode = PrintHelper.ScaleModeFit;
            //Bitmap bitmap = BitmapFactory.decodeResource(getResources(),R.drawable.droids);
			photoPrinter.PrintBitmap(GetString(Resource.String.ApplicationName), barcode);
		}
		private Bitmap GenerateBarcode(string text)
		{
			
			if (text.Trim() == "")
			{
				return null;
			}
			var barcodeWriter = new ZXing.Mobile.BarcodeWriter();
			var Options = new ZXing.Common.EncodingOptions
			{
				Width = 576,
				Height = 384,
				Margin = 5
			};
			barcodeWriter.Renderer = new BitmapRenderer();
			barcodeWriter.Options = Options;
			barcodeWriter.Format = BarcodeFormat.CODE_128;
			//   barcodeWriter.Encode(text);
			barcode = barcodeWriter.Write(text);
			// g = Graphics.FromImage(barcode);
			float scale = Resources.DisplayMetrics.Density;
			Canvas canvas = new Canvas(barcode);
			// new antialised Paint
			Paint paint = new Paint(PaintFlags.AntiAlias);
			paint.SetStyle(Paint.Style.FillAndStroke);
			paint.TextSize = 12 * scale;
			Rect bounds = new Rect();
			paint.GetTextBounds(text, 0, text.Length, bounds);
						int x = (barcode.Width - bounds.Width()) / 2;
			int y = (barcode.Height - bounds.Height());
			paint.Color = new Color(250, 250, 250);
			canvas.DrawRect(0, y-10, barcode.Width, barcode.Height, paint);
						paint.Color = new Color(10, 110, 80);
			canvas.DrawText(text, x, y+bounds.Height()-5, paint);
			return barcode;
		}
    }
}

