using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;

namespace PrintConsoleTry
{
    class Program
    {
        static Font printFont;
        static string c, str;
        static StreamReader sr;
        static byte page_no;
        static void Main(string[] args)
        {
            Console.WriteLine("Print File: ");
            str = Console.ReadLine();

            str = str.Substring(1, str.Length - 2);

            Console.Write(str);
            sr = new StreamReader(str);
            Console.Write("Are You Sure (Y/N): ");
            c = Console.ReadLine();

            if (c == "Y" || c == "y")
            {
                PrintDocument pD = new PrintDocument();
                printFont = new Font("Time New Roman", 12);
                pD.PrintPage += new PrintPageEventHandler(PD_PrintPage);
                pD.PrintPage += new PrintPageEventHandler(PD_10PrintPage);
                pD.EndPrint += PD_EndPrint;

                Console.Write("It's Printing...");

                pD.Print();
            }
            else if (c == "N" || c == "n") Console.WriteLine("Not Printing");
            else Console.WriteLine("Fail");
            Console.ReadLine();
        }

        private static void PD_EndPrint(object sender, PrintEventArgs e)
        {
            printFont.Dispose();
            printFont = null;
            sr.Close();
        }

        private static void PD_10PrintPage(object sender, PrintPageEventArgs e)
        {
            page_no++;
            if (page_no == 3) Console.WriteLine("Thirt Printing Pages");
        }

        private static void PD_PrintPage(object sender, PrintPageEventArgs e)
        {
            Single leftMargin = e.MarginBounds.Left;
            Single topMargin = e.MarginBounds.Top;
            int num = 0;
            string satire = null;
            Single yPosition = 0;
            Single BiaSatire = e.MarginBounds.Height / printFont.GetHeight(e.Graphics);
            while (num < BiaSatire && (satire = sr.ReadLine()) != null)
            {
                yPosition = topMargin + (num * printFont.GetHeight(e.Graphics));
                e.Graphics.DrawString(satire, printFont, Brushes.Black, leftMargin, yPosition, new StringFormat());
                num++;
            }
            if (satire != null) e.HasMorePages = true;
            else e.HasMorePages = false;
        }
    }
}
