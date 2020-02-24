using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAC_DLL
{
    public class MAC_Series
    {
        //Вычисление суммы членов числового ряда Members,
        //начиная с индекса Initial_Index и заканчивая индексом Last_Index

        public static double Sum_of_Number_Series(
            int Initial_Index,
            int Last_index,
            Func<int,double> Members)
        {
            double global_sum = 0.0;
            for (int k = Initial_Index; k <= Last_index; k++)
                global_sum += Members(k);
            return global_sum;
        }

        public static double Sum_of_Number_Series_A(
            int Initial_Index, 
            double Eps,
            Func<int, double> Members,
            ref int Final_index)
        {
            double ak, sum_1, sum_0 = 0.0; int k = Initial_Index; bool flag;
            do
            {
              ak = Members(k); sum_0 += ak; flag = Math.Abs(ak) >= Eps;
              if(flag) k++;
            } while (flag);

            int N = k - Initial_Index + 1; k++;
            do
            {
              sum_1 = Sum_of_Number_Series(k, N+k, Members);
              sum_0 += sum_1; flag = Math.Abs(sum_1) >= Eps;
              if (flag) k = k + N + 1;
            } while (flag);
            Final_index = k + N; return sum_0;
        }

        public static double Sum_of_Number_Series_D(
            int Initial_Index,
            double Delta,
            Func<int, double> Members,
            ref int Final_index)
        {
            double ak, sum_1, sum_0 = 0.0; int k = Initial_Index; bool flag;
            do
            {
              ak = Members(k); sum_0 += ak; flag = Math.Abs(ak) >= Delta;
              if (flag) k++;
            } while (flag);

            int N = k - Initial_Index + 1; k++;
            do
            {
                sum_1 = Sum_of_Number_Series(k, N + k, Members);
                sum_0 += sum_1; flag = Math.Abs(sum_1 / sum_0) >= Delta;
                if (flag) k = k + N + 1;
            } while (flag);
            Final_index = k + N; return sum_0;
        }
    
    }
}
