using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyTD = MAC_DLL.MAC_My_Definitions.MyTableOfData;

namespace MAC_LabWork_1_3
{
    class Main_LW_1_3
    {
        static void Main(string[] args)
        {
            //MyTD T1 = new MyTD("MAC_LabWork_1_3_2020_3k_113_v00.bin", "binary file");
            //string txt = $"\r\n Count of rows: {T1.Length,0}";
            //txt += T1.ToPrint("  *.bin file processing ");
            //Console.WriteLine(txt);

            //MyTD T2 = new MyTD("MAC_LabWork_1_3_2020_3k_113_v00.txt", "txt file");
            //txt = $"\r\n\r\n\r\n Count of rows: {T2.Length,0}";
            //txt += T1.ToPrint("  *.txts file processing ");
            //Console.WriteLine(txt);

            MyTD T1 = new MyTD("MAC_LabWork_1_3_2020_3k_113_v02.bin", "binary file");
            string txt = $"\r\n Count of rows: {T1.Length,0}";
            txt += T1.ToPrint("  *.bin file processing ");
            Console.WriteLine(txt);

            MyTD T2 = new MyTD("MAC_LabWork_1_3_2020_3k_113_v02.txt", "txt file");
            txt = $"\r\n\r\n\r\n Count of rows: {T2.Length,0}";
            txt += T1.ToPrint("  *.txts file processing ");
            Console.WriteLine(txt);

            T2.To_txt_File("Test_LW_1_3.txt", "New result's form");
        }
    }
}
