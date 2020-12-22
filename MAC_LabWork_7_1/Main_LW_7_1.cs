using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FwD = MAC_Graph_DLL.Form_with_Data;
using MToD = MAC_DLL.MAC_My_Definitions.MyTableOfData;
using CLRG = MAC_DLL.MAC_Regressions;


namespace MAC_LabWork_7_1
{
    class Main_LW_7_1
    {
        static void Main(string[] args)
        {
            string file = "LW_7_1_v09_a.bin";
            MToD TestData = new MToD(file, "LW_7_1_Tests");

            CLRG MyReg = new CLRG(TestData);

            Console.WriteLine(file + "\r\n" + MyReg.ToPrint());

            FwD.LW_7_1_Graph(TestData, MyReg.Regression, file + "\r\n" + MyReg.ToPrint());

            file = "LW_7_1_v09_b.bin";
            TestData = new MToD(file, "LW_7_1_Tests");

            MyReg = new CLRG(TestData);

            Console.WriteLine(file + "\r\n" + MyReg.ToPrint());

            FwD.LW_7_1_Graph(TestData, MyReg.Regression, file + "\r\n" + MyReg.ToPrint());

            file = "LW_7_1_v09_c.bin";
            TestData = new MToD(file, "LW_7_1_Tests");

            MyReg = new CLRG(TestData);

            Console.WriteLine(file + "\r\n" + MyReg.ToPrint());

            FwD.LW_7_1_Graph(TestData, MyReg.Regression, file + "\r\n" + MyReg.ToPrint());

            file = "LW_7_1_v09_d.bin";
            TestData = new MToD(file, "LW_7_1_Tests");

            MyReg = new CLRG(TestData);

            Console.WriteLine(file + "\r\n" + MyReg.ToPrint());
            
            FwD.LW_7_1_Graph(TestData, MyReg.Regression, file + "\r\n" + MyReg.ToPrint());

            file = "LW_7_1_v09_e.bin";
            TestData = new MToD(file, "LW_7_1_Tests");

            MyReg = new CLRG(TestData);

            Console.WriteLine(file + "\r\n" + MyReg.ToPrint());

            FwD.LW_7_1_Graph(TestData, MyReg.Regression, file + "\r\n" + MyReg.ToPrint());


            file = "LW_7_1_v09_f.bin";
            TestData = new MToD(file, "LW_7_1_Tests");

            MyReg = new CLRG(TestData);

            Console.WriteLine(file + "\r\n" + MyReg.ToPrint());

            FwD.LW_7_1_Graph(TestData, MyReg.Regression, file + "\r\n" + MyReg.ToPrint());


            file = "LW_7_1_v09_g.bin";
            TestData = new MToD(file, "LW_7_1_Tests");

            MyReg = new CLRG(TestData);

            Console.WriteLine(file + "\r\n" + MyReg.ToPrint());

            FwD.LW_7_1_Graph(TestData, MyReg.Regression, file + "\r\n" + MyReg.ToPrint());


            file = "LW_7_1_v09_h.bin";
            TestData = new MToD(file, "LW_7_1_Tests");

            MyReg = new CLRG(TestData);

            Console.WriteLine(file + "\r\n" + MyReg.ToPrint());


            FwD.LW_7_1_Graph(TestData, MyReg.Regression, file + "\r\n" + MyReg.ToPrint());

        }
    }
}
