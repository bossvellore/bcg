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
using Android.Print;
using Android.Runtime;
using System.IO;
using Android.Print.Pdf;
using Android.Views;
using Android.PrintServices;
using Android.Graphics.Pdf;

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
        TextView itemId;
        TextView caseCountId;
        TextView skidCountId;
        TextView prodDateId;
        TextView palletizersinitialId;
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

            //table textview
            itemId = FindViewById<TextView>(Resource.Id.itemId);
            caseCountId = FindViewById<TextView>(Resource.Id.caseCountId);
            skidCountId = FindViewById<TextView>(Resource.Id.skidCountId);
            prodDateId = FindViewById<TextView>(Resource.Id.prodDateId);
            palletizersinitialId = FindViewById<TextView>(Resource.Id.palletizersinitialId);

            date = FindViewById<EditText>(Resource.Id.date);
            //var currentTime = new Date();
            //var format = new SimpleDateFormat("MMM dd,yyyy hh:mm a");
            //string dateString = format.Format(currentTime);
            //date.Text = dateString;
            date.Text = DateTime.Now.ToString("MM/dd/yy");
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


               itemId.Text= itemNumber.Text;
                caseCountId.Text=caseCount.Text;
                skidCountId.Text=skidNumber.Text;
                prodDateId.Text=date.Text;
                palletizersinitialId.Text=parallelizer.Text;

                string text = this.GetBarcodeText();
                this.GenerateBarcode(text);
                previewImage.SetImageBitmap(barcode);
            };
        }

        void DatePickerChangeListener(object sender, DatePickerDialog.DateSetEventArgs e)
        {
            //throw new NotImplementedException();
            date.Text = e.Date.ToString("MM/dd/yy");
        }

        private string GetBarcodeText()
        {
            return date.Text + "  " + itemNumber.Text + "  " + caseCount.Text + "  " + skidNumber.Text + "  " + parallelizer.Text;
        }
        private void PrintBarcode()
        {
            //  PrintHelper photoPrinter = new PrintHelper(this);
            //   photoPrinter.ScaleMode = PrintHelper.ScaleModeFit;
            //Bitmap bitmap = BitmapFactory.decodeResource(getResources(),R.drawable.droids);
            //   photoPrinter.PrintBitmap(GetString(Resource.String.ApplicationName), barcode);
            string text = this.GetBarcodeText();
            this.GenerateBarcode(text);
            previewImage.SetImageBitmap(barcode);
            PrintBarcodeCustom();


        }
        private void PrintBarcodeCustom()
        {
           
           var printManager = (PrintManager)GetSystemService(Context.PrintService);

            var content = FindViewById<LinearLayout>(Resource.Id.linearLayout1);
            var printAdapter = new GenericPrintAdapter(this, content);

            printManager.Print("MyPrintJob", printAdapter, null);




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
            canvas.DrawRect(0, y - 10, barcode.Width, barcode.Height, paint);
            paint.Color = new Color(10, 110, 80);
            canvas.DrawText(text, x, y + bounds.Height() - 5, paint);
            return barcode;
        }


        public class GenericPrintAdapter : PrintDocumentAdapter
        {
            View view;
            Context context;
            private int totalPages;
            PrintedPdfDocument document;
            float scale;
            public GenericPrintAdapter(Context context, View view)
            {
                this.view = view;
                this.context = context;
                totalPages = 3;
            }
     
            public override void OnLayout(PrintAttributes oldAttributes, PrintAttributes newAttributes,
                                           CancellationSignal cancellationSignal, LayoutResultCallback callback, Bundle extras)
            {
                document = new PrintedPdfDocument(context, newAttributes);
                CalculateScale(newAttributes);
                if (totalPages > 0)
                {
                    PrintDocumentInfo printInfo = new PrintDocumentInfo
                        .Builder("MyPrint.pdf")
                        .SetContentType(PrintContentType.Document)
                        .SetPageCount(totalPages)
                        .Build();

                    callback.OnLayoutFinished(printInfo, true);
                }
                {
                    callback.OnLayoutFailed("Page count calculation failed.");
                }
            }
            void CalculateScale(PrintAttributes newAttributes)
            {
                 int dpi = Math.Max(newAttributes.GetResolution().HorizontalDpi, newAttributes.GetResolution().VerticalDpi);

                 int leftMargin = (int)(dpi * (float)newAttributes.MinMargins.LeftMils / 2500);
                int rightMargin = (int)(dpi * (float)newAttributes.MinMargins.RightMils / 2500);
                 int topMargin = (int)(dpi * (float)newAttributes.MinMargins.TopMils / 2500);
                 int bottomMargin = (int)(dpi * (float)newAttributes.MinMargins.BottomMils / 2500);

                int w = (int)(dpi * (float)newAttributes.GetMediaSize().WidthMils / 2500) - leftMargin - rightMargin;
                int h = (int)(dpi * (float)newAttributes.GetMediaSize().HeightMils / 2500) - topMargin - bottomMargin;

                scale = Math.Min((float)document.PageContentRect.Width() / w, (float)document.PageContentRect.Height() / h);
            }

            public override void OnWrite(PageRange[] pages, ParcelFileDescriptor destination,
                                          CancellationSignal cancellationSignal, WriteResultCallback callback)
            { 

                for (int i = 0; i < totalPages; i++) {
                    PrintedPdfDocument.Page page = document.StartPage(i);
                    
                    page.Canvas.Scale(scale, scale);
                    view.Top =50;
                    view.Draw(page.Canvas);
                    document.FinishPage(page);
                    WritePrintedPdfDoc(destination);

                }
                document.Close();
                document.Dispose();


                callback.OnWriteFinished(pages);
            }

            void WritePrintedPdfDoc(ParcelFileDescriptor destination)
            {
                var javaStream = new Java.IO.FileOutputStream(destination.FileDescriptor);
                var osi = new OutputStreamInvoker(javaStream);
                using (var mem = new MemoryStream())
                {
                    document.WriteTo(mem);
                    var bytes = mem.ToArray();
                    osi.Write(bytes, 0, bytes.Length);
                }
            }
         
        }

    }

}

