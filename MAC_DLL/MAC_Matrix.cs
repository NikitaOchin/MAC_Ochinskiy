using System;
using CI = System.Globalization.CultureInfo;
using System.IO;

namespace MAC_DLL
{
  public enum MatrixType { Simple, Identity, Test, Upper, Lower }

  public class Matrix
  {
    public double[,] M;  // Посилання власно на матрицю М
    public int Size;     // Ранг матрицi М
    public bool flag;    // Флаг останньої операції звертання до M[i,j]
    public double Det { get; internal set; } = double.NaN;

    #region    <--- Конструктори Matrix --->

    public Matrix(int size)  // Конструктор матрицi заданого розмiру
    {
      Size = size; M = new double[Size, Size];
    }

    public Matrix(int size, MatrixType type) // Перевантажений конструктор
    {
      Size = size; M = new double[Size, Size];
      switch (type)
      {
        case MatrixType.Simple:
          for (int i = 1; i <= Size; i++)
            for (int j = 1; j <= Size; j++) this[i, j] = double.NaN;
          break;

        case MatrixType.Identity:
          for (int i = 1; i <= Size; i++) this[i, i] = 1.0;
          break;

        case MatrixType.Test:
          for (int i = 1; i <= Size; i++)
            for (int j = 1; j <= Size; j++) this[i, j] = 10.0 * i + j;
          break;

        case MatrixType.Upper:
          for (int i = 1; i <= Size; i++)
            for (int j = i; j <= Size; j++) this[i, j] = (j - i) + 1.0;
          break;
      }
    }

    public double this[int index_i, int index_j] // Індексатор Matrix
    {
      get                                        // get-аксесор
      {
        if (Ok(index_i, index_j))
        {
          flag = false; return M[index_i - 1, index_j - 1];
        }
        else
        {
          flag = true; return double.NaN;
        }
      }
      set                                        // set-аксесор
      {
        if (Ok(index_i, index_j))
        {
          M[index_i - 1, index_j - 1] = value; flag = false;
        }
        else flag = true;
      }
    }

    // Метод повертає true, якщо обидва індекси в межах розмірності M
    private bool Ok(int i, int j)
    {
      if ((i >= 1 && i <= Size) && (j >= 1 && j <= Size)) return true;
      return false;
    }

    #endregion <--- Конструктори Matrix --->

    #region    <--- Методи вводу-виводу --->

    public static void Read(string path, out Matrix A, out int n)
    {
      FileInfo file = new FileInfo(path);

      if (file.Extension == ".txt")
      {
        bool dot_or_comma =
        (CI.CurrentCulture.NumberFormat.CurrencyDecimalSeparator == ".");

        StreamReader rdr = new StreamReader(file.OpenRead());
        n = Convert.ToInt32(rdr.ReadLine());
        A = new Matrix(n);

        string[] numbers; string line;
        for (int i = 1; i <= n; i++)
        {
          line = rdr.ReadLine().Trim();
          if (dot_or_comma) line = line.Replace(",", ".");
          else line = line.Replace(".", ",");

          numbers = line.Split(new char[] { ' ', ';' },
                    StringSplitOptions.RemoveEmptyEntries);

          for (int j = 1; j <= n; j++) A[i, j] = Convert.ToDouble(numbers[j - 1]);
        }
        rdr.Close(); return;
      }

      if (file.Extension == ".bin")
      {
        BinaryReader rdr = new BinaryReader(file.OpenRead());
        n = rdr.ReadInt32();
        A = new Matrix(n);
        for (int i = 1; i <= n; i++)
          for (int j = 1; j <= n; j++) A[i, j] = rdr.ReadDouble();
        rdr.Close(); return;
      }

      A = null; n = 0; // If Some Mistake with File      
    }

    public static void Read(string path, out Matrix A, out Vector B, out int n)
    {
      FileInfo file = new FileInfo(path);

      if (file.Extension == ".txt")
      {
        bool dot_or_comma =
        (CI.CurrentCulture.NumberFormat.CurrencyDecimalSeparator == ".");

        StreamReader rdr = new StreamReader(file.OpenRead());
        n = Convert.ToInt32(rdr.ReadLine());
        A = new Matrix(n); B = new Vector(n);

        string[] numbers; string line;
        for (int i = 1; i <= n; i++)
        {
          line = rdr.ReadLine().Trim();
          if (dot_or_comma) line = line.Replace(",", ".");
          else line = line.Replace(".", ",");

          numbers = line.Split(new char[] { ' ', ';' },
                    StringSplitOptions.RemoveEmptyEntries);

          for (int j = 1; j <= n; j++) A[i, j] = Convert.ToDouble(numbers[j - 1]);
          B[i] = Convert.ToDouble(numbers[n]);
        }
        rdr.Close(); return;
      }

      if (file.Extension == ".bin")
      {
        BinaryReader rdr = new BinaryReader(file.OpenRead());
        n = rdr.ReadInt32();
        A = new Matrix(n); B = new Vector(n);
        for (int i = 1; i <= n; i++)
        {
          for (int j = 1; j <= n; j++) A[i, j] = rdr.ReadDouble();
          B[i] = rdr.ReadDouble();
        }
        rdr.Close(); return;
      }

      A = null; B = null; n = 0; // If Some Mistake with File      
    }

    public static string
      Print(Matrix A, bool form, int fs, int fd, string title)
    {
      int ka, n = A.Size;
      string txt = "\r\n", frmt;

      if (title != "") txt += "  " + title + $"   Size = {n}\r\n";

      if (form)
      {
        int max_ka = 0;
        double a;
        for (int i = 1; i <= n; i++)
        {
          for (int j = 1; j <= n; j++)
          {
            a = Math.Abs(A[i, j]);
            if (a < 10.0) ka = 1;
            else ka = (int)Math.Ceiling(Math.Log10(a));
            max_ka = Math.Max(max_ka, ka);
          }
        }
        ka = fs + 1 + max_ka + 1 + fd;
        frmt = "{0," + $"{ka}" + ":F" + $"{fd}" + "}";
      }
      else
      {
        ka = fs + 1 + 1 + 1 + fd + 1 + 1 + 3;
        frmt = "{0," + $"{ka}" + ":E" + $"{fd}" + "}";
      }

      for (int i = 1; i <= n; i++)
      {
        for (int j = 1; j <= n; j++)
          txt += string.Format(frmt, A[i, j]);
        txt += "\r\n";
      }

      return txt;
    }

    public static string
      Print(Matrix A, Vector B, bool form, int fs, int fd, string title)
    {
      int ka, kb, n = A.Size; string txt = "\r\n", frmta, frmtb;

      if (title != "") txt += "  " + title + $"   Size = {n}\r\n";

      if (form)
      {
        int max_ka = 0, max_kb = 0; double a, b;
        for (int i = 1; i <= n; i++)
        {
          for (int j = 1; j <= n; j++)
          {
            a = Math.Abs(A[i, j]);
            if (a < 10.0) ka = 1;
            else ka = (int)Math.Ceiling(Math.Log10(a));
            max_ka = Math.Max(max_ka, ka);
          }
          b = Math.Abs(B[i]);
          if (b < 10.0) kb = 1; else kb = (int)Math.Ceiling(Math.Log10(b));
          max_kb = Math.Max(max_kb, kb);
        }
        ka = fs + 1 + max_ka + 1 + fd;
        kb = fs + 1 + max_kb + 1 + fd + 2;
        frmta = "{0," + $"{ka}" + ":F" + $"{fd}" + "}";
        frmtb = "{0," + $"{kb}" + ":F" + $"{fd}" + "}";
      }
      else
      {
        ka = fs + 1 + 1 + 1 + fd + 1 + 1 + 3; kb = ka + 2;
        frmta = "{0," + $"{ka}" + ":E" + $"{fd}" + "}";
        frmtb = "{0," + $"{kb}" + ":E" + $"{fd}" + "}";
      }

      for (int i = 1; i <= n; i++)
      {
        for (int j = 1; j <= n; j++)
          txt += string.Format(frmta, A[i, j]);
        txt += " " + string.Format(frmtb, B[i]) + "\r\n";
      }

      return txt;
    }


    #endregion <--- Методи вводу-виводу --->

    #region    <--- Генерація Матриць та Елементарні Перетворення --->

    public static Matrix Transpose(Matrix A)
    {
      Matrix AT = new Matrix(A.Size);
      for (int i = 1; i <= AT.Size; i++)
        for (int j = 1; j <= AT.Size; j++) AT[i, j] = A[j, i];
      return AT;
    }

    public static Matrix Copy(Matrix A)
    {
      Matrix C = new Matrix(A.Size);
      for (int i = 1; i <= C.Size; i++)
        for (int j = 1; j <= C.Size; j++) C[i, j] = A[i, j];
      return C;
    }

    public static Matrix Rotation(Matrix A, bool key)
    {
      int i, j, N = A.Size;
      Matrix AR = new Matrix(N);

      if (key)
        for (j = 1; j <= N; j++)
          for (i = 1; i <= N; i++) AR[i, j] = A[j, N - i + 1];
      else
        for (i = 1; i <= N; i++)
          for (j = 1; j <= N; j++) AR[i, j] = A[N - j + 1, i];

      return AR;
    }

    public static void
        Max_Min(Matrix A, ref double max, ref int max_i, ref int max_j,
                          ref double min, ref int min_i, ref int min_j)
    {
      min = double.MaxValue; max = double.MinValue;
      for (int i = 1; i <= A.Size; i++)
        for (int j = 1; j <= A.Size; j++)
        {
          if (A[i, j] < min) { min = A[i, j]; min_i = i; min_j = j; }
          if (max < A[i, j]) { max = A[i, j]; max_i = i; max_j = j; }
        }
    }

    #endregion <--- Генерація Матриць та Елементарні Перетворення --->

    #region    <--- Арифметика  + - *  Матриць --->

    public static Matrix operator +(Matrix A, Matrix B)
    {
      int i, j, N = A.Size; Matrix C = new Matrix(N);
      for (i = 1; i <= N; i++)
        for (j = 1; j <= N; j++) C[i, j] = A[i, j] + B[i, j];
      return C;
    }

    public static Matrix operator -(Matrix A, Matrix B)
    {
      int i, j, N = A.Size; Matrix C = new Matrix(N);
      for (i = 1; i <= N; i++)
        for (j = 1; j <= N; j++) C[i, j] = A[i, j] - B[i, j];
      return C;
    }

    public static Matrix operator *(double a, Matrix A)
    {
      int i, j, N = A.Size; Matrix C = new Matrix(N);
      for (i = 1; i <= N; i++)
        for (j = 1; j <= N; j++) C[i, j] = a * A[i, j];
      return C;
    }

    public static Matrix operator *(Matrix A, double a)
    {
      int i, j, N = A.Size; Matrix C = new Matrix(N);
      for (i = 1; i <= N; i++)
        for (j = 1; j <= N; j++) C[i, j] = a * A[i, j];
      return C;
    }

    public static Matrix operator *(Matrix A, Matrix B)
    {
      int i, j, k, N = A.Size; Matrix C = new Matrix(N);
      for (i = 1; i <= N; i++)
        for (j = 1; j <= N; j++)
        {
          C[i, j] = 0.0;
          for (k = 1; k <= N; k++) C[i, j] += A[i, k] * B[k, j];
        }
      return C;
    }

    #endregion <--- Арифметика  + - *  Матриць --->    

    #region    <--- Розкладання Матриць та Детермінанти --->

    //         LU - розкладання заданної матриці А
    public static void
      LU_Factorization(Matrix A, out Matrix L, out Matrix U)
    {
      int n = A.Size; L = new Matrix(n); U = new Matrix(n);

      //       Формуємо перший рядок матриці U                     (3.10)
      for (int j = 1; j <= n; j++) U[1, j] = A[1, j];
      U.Det = U[1, 1];

      //       Формуємо перший стовпчик матриці L                  (3.11)
      for (int i = 1; i <= n; i++) L[i, 1] = A[i, 1] / A[1, 1];

      double s; int m, k;
      for (int i = 2; i <= n; i++)
      {
        for (int j = 2; j <= n; j++)
        {
          m = Math.Min(i, j); s = A[i, j];
          for (k = 1; k <= m; k++) s -= L[i, k] * U[k, j];        // (3.14)
          if (i > j) L[i, j] = s / U[j, j]; else U[i, j] = s;     // (3.15)
        }
        L[i, i] = 1.0; U.Det *= U[i, i];                          // (3.16)
      }
      A.Det = U.Det; L.Det = 1.0;
    }

    //       Формування мінора M до елемента aIJ матриці А 
    public static Matrix Minor(Matrix A, int I, int J)
    {
      Matrix M = new Matrix(A.Size - 1);
      int i_M = 1;
      for (int i_A = 1; i_A <= A.Size; i_A++)
      {
        if (i_A == I) continue;
        int j_M = 1;
        for (int j_A = 1; j_A <= A.Size; j_A++)
        {
          if (j_A == J) continue;
          M[i_M, j_M] = A[i_A, j_A];
          j_M++;
        }
        i_M++;
      }
      return M;
    }

    // Рекурсивне обчислення детермінанта із розкладанням
    //                           за елементами 1-го рядка 
    public static double Determinant_J(Matrix A)
    {
      double det = 0.0;
      if (A.Size == 2)
      { det = A[1, 1] * A[2, 2] - A[2, 1] * A[1, 2]; } // (3.20) 
      else
      {
        int znak = -1;
        for (int k = 1; k <= A.Size; k++)              // (3.19)
        {
          znak = -znak;
          det += znak * A[k, 1] * Determinant_J(Minor(A, k, 1));
        }
      }
      if (double.IsNaN(A.Det)) A.Det = det;
      return det;
    }

    // Рекурсивне обчислення детермінанта із розкладанням
    //                       за елементами 1-го стовпчика
    public static double Determinant_I(Matrix A)
    {
      double det = 0.0;
      if (A.Size == 2)
      { det = A[1, 1] * A[2, 2] - A[2, 1] * A[1, 2]; } // (3.20) 
      else
      {
        int znak = -1;
        for (int k = 1; k <= A.Size; k++)              // (3.19)
        {
          znak = -znak;
          det += znak * A[1, k] * Determinant_I(Minor(A, 1, k));
        }
      }
      if (double.IsNaN(A.Det)) A.Det = det;
      return det;
    }

    // Обчислення алгебраїчного доповнення до елементу aIJ матриці А
    public static double Cofactor(Matrix A, int I, int J)
    {
      if (A.Size == 2)
      {
        if (I == J) return A[1, 1] * A[2, 2]; else return -A[1, 2] * A[2, 1];
      }
      if ((I + J) % 2 == 0) return Determinant_J(Minor(A, I, J));
      else return -Determinant_I(Minor(A, I, J));
    }

    // Обчислення детермінанта через алгебраїчні доповнення:
    //  flag = true для К-го рядка,  flag = false для К-го стовпчика

    public static double Determinant(Matrix A, int K, bool flag)
    {
      double det = 0.0;
      if (A.Size == 2) { det = A[1, 1] * A[2, 2] - A[2, 1] * A[1, 2]; }
      else
      {
        if (flag)
        {
          for (int j = 1; j <= A.Size; j++)
            det += A[K, j] * Cofactor(A, K, j);  // за К-м рядком
        }
        else
        {
          for (int i = 1; i <= A.Size; i++)
            det += A[i, K] * Cofactor(A, i, K);  // за К-м стовпчиком
        }
      }
      if (double.IsNaN(A.Det)) A.Det = det;
      return det;
     }
    #endregion <--- Розкладання Матриць та Детермінанти --->
        
        #region    <--- Приєднана та зворотня матриці --->
        // Метод, який обчислює Приеднану Матрицю
        public static Matrix Attached(Matrix A)
        {
            int N = A.Size; Matrix Az = new Matrix(N);
            for (int i = 1; i <= N; i++)
                for (int j = 1; j <= N; j++) Az[j, i] = Cofactor(A, i, j);
            return Az;
        }

        // Метод, який обчислює зворотню Матрицю через Приєднану
        public static Matrix Inversion_1(Matrix A, out double error)
        {
            if (double.IsNaN(A.Det)) Determinant_I(A);
            double det = 1.0 / A.Det;
            Matrix Az = Attached(A) * det; error = Error_of_Inversion(Az, A);
            return Az;
        }

        // Метод, який обчислює зворотню Матрицю через Приєднану
        public static Matrix Inversion_2(Matrix A, out double error)
        {
            Vector x, b; int N = A.Size; Matrix Az = new Matrix(N);
            for(int k = 1; k <= N; k++)
            {
                b = new Vector(N);b[k] = 1.0;
                x = MAC_Algebra.Method_Gaussa(A, b);
                for (int i = 1; i <= N; i++) Az[i, k] = x[i];
            }
            error = Error_of_Inversion(Az, A);
            return Az;
        }

        // Метод, який обчислює помилку зворотньої матриці
        public static double Error_of_Inversion(Matrix Az, Matrix A)
        {
            int N = Az.Size; double error = -10.0; Matrix E = Az * A;
            for (int i = 1; i <= N; i++)
                for (int j = 1; j <= N; j++)
                {
                    if (i == j) error = Math.Max(error, Math.Abs(E[i, i] - 1.0));
                    else error = Math.Max(error, Math.Abs(E[i,j]));
                }
            return error;
        }
        #endregion    <--- Приєднана та зворотня матриці --->

    }
}


//using System;
//using CI = System.Globalization.CultureInfo;
//using System.IO;

//namespace MAC_DLL
//{
//  //====================  Matrix  ========================

//  public enum MatrixType { Simple, Identity, Test, Upper, Lower }

//  public class Matrix
//  {
//    public double[,] M;  // Посилання на матрицю М
//    public int Size;     // Ранг матрицi М
//    public bool flag;    // Флаг останнього звернення до M[i,j]
//    public double Det { get; internal set; } = double.NaN;

//    #region    <--- Конструктори Matrix --->

//    public Matrix(int size)  // Конструктор матрицi заданого розмiру
//    {
//      Size = size; M = new double[Size, Size];
//    }

//    public Matrix(int size, MatrixType type) // Перевантажений конструктор
//    {
//      Size = size; M = new double[Size, Size];
//      switch (type)
//      {
//        case MatrixType.Simple:
//          for (int i = 1; i <= Size; i++)
//            for (int j = 1; j <= Size; j++) this[i, j] = double.NaN;
//          break;

//        case MatrixType.Identity:
//          for (int i = 1; i <= Size; i++) this[i, i] = 1.0;
//          break;

//        case MatrixType.Test:
//          for (int i = 1; i <= Size; i++)
//            for (int j = 1; j <= Size; j++) this[i, j] = 10.0 * i + j;
//          break;

//        case MatrixType.Upper:
//          for (int i = 1; i <= Size; i++)
//            for (int j = i; j <= Size; j++) this[i, j] = (j - i) + 1.0;
//          break;
//      }
//    }

//    public double this[int index_i, int index_j] // Iндексатор Matrix
//    {
//      get                                        // get-аксесор
//      {
//        if (Ok(index_i, index_j))
//        {
//          flag = false; return M[index_i - 1, index_j - 1];
//        }
//        else
//        {
//          flag = true; return double.NaN;
//        }
//      }
//      set                                        // set-аксесор
//      {
//        if (Ok(index_i, index_j))
//        {
//          M[index_i - 1, index_j - 1] = value; flag = false;
//        }
//        else flag = true;
//      }
//    }

//    private bool Ok(int index_i, int index_j)
//    {                                              // Метод повертає true,  
//      bool i = (index_i >= 1 && index_i <= Size);  //  якщо обидва iндекси в
//      bool j = (index_j >= 1 && index_j <= Size);  //  межах розмiрностi M
//      if (i && j) return true; return false;
//    }

//    #endregion <--- Конструктори Matrix --->

//    #region    <--- Методи вводу-виводу --->

//    public static void Read(string path, out Matrix A, out int n)
//    {
//      FileInfo file = new FileInfo(path);

//      if (file.Extension == ".txt")
//      {
//        bool dot_or_comma;
//        if (CI.CurrentCulture.NumberFormat.CurrencyDecimalSeparator == ",")
//          dot_or_comma = false;
//        else dot_or_comma = true;

//        StreamReader rdr = new StreamReader(file.OpenRead());
//        n = Convert.ToInt32(rdr.ReadLine()); A = new Matrix(n);

//        string[] numbers; string line;
//        for (int i = 1; i <= n; i++)
//        {
//          line = rdr.ReadLine().Trim();
//          if (dot_or_comma) line = line.Replace(",", ".");
//          else line = line.Replace(".", ",");

//          numbers = line.Split(new char[] { ' ', ';' },
//                    StringSplitOptions.RemoveEmptyEntries);

//          for (int j = 1; j <= n; j++) A[i, j] = Convert.ToDouble(numbers[j - 1]);
//        }
//        rdr.Close(); return;
//      }

//      if (file.Extension == ".bin")
//      {
//        BinaryReader rdr = new BinaryReader(file.OpenRead());
//        n = rdr.ReadInt32(); A = new Matrix(n);
//        for (int i = 1; i <= n; i++)
//          for (int j = 1; j <= n; j++)
//            A[i, j] = rdr.ReadDouble();
//        rdr.Close(); return;
//      }

//      A = null; n = 0; // Some Mistake with File      
//    }

//    public static string
//      Print(Matrix A, bool form, int fs, int fd, string title)
//    {
//      int ka, n = A.Size;
//      string txt = "\r\n", frmt;

//      if (title != "")
//        txt += "  " + title + $"   size = {n}\r\n";
//      if (form)
//      {
//        int max_ka = 0;
//        double a;
//        for (int i = 1; i <= n; i++)
//        {
//          for (int j = 1; j <= n; j++)
//          {
//            a = Math.Abs(A[i, j]);
//            if (a < 10.0) ka = 1;
//            else ka = (int)Math.Ceiling(Math.Log10(a));
//            max_ka = Math.Max(max_ka, ka);
//          }
//        }
//        ka = fs + 1 + max_ka + 1 + fd;
//        frmt = "{0," + $"{ka}:F{fd}" + "}";
//      }
//      else
//      {
//        ka = fs + 1 + 1 + 1 + fd + 1 + 1 + 3;
//        frmt = "{0," + $"{ka}:E{fd}" + "}";
//      }

//      for (int i = 1; i <= n; i++)
//      {
//        for (int j = 1; j <= n; j++)
//          txt += string.Format(frmt, A[i, j]);
//        txt += "\r\n";
//      }

//      return txt;
//    }

//    #endregion <--- Методи вводу-виводу --->

//    #region    <--- Генерацiя Матриць i Елементарні Перетворення --->

//    public static void Transpose(Matrix A)
//    {
//      double aij;
//      for (int i = 1; i <= A.Size; i++)
//        for (int j = i + 1; j <= A.Size; j++)
//        {
//          aij = A[i, j]; A[i, j] = A[j, i]; A[j, i] = aij;
//        }
//    }
//    public static void Transpose(Matrix A, out Matrix AT)
//    {
//      AT = new Matrix(A.Size);
//      for (int i = 1; i <= AT.Size; i++)
//        for (int j = 1; j <= AT.Size; j++) AT[i, j] = A[j, i];
//    }

//    public static void Copy(Matrix A, out Matrix C)
//    {
//      C = new Matrix(A.Size);
//      for (int i = 1; i <= C.Size; i++)
//        for (int j = 1; j <= C.Size; j++) C[i, j] = A[i, j];
//    }

//    public static void Rotation(Matrix A, out Matrix AR, bool key)
//    {
//      int i, j, N = A.Size; AR = new Matrix(N);

//      if (key)
//        for (j = 1; j <= N; j++)
//          for (i = 1; i <= N; i++) AR[i, j] = A[j, N - i + 1];
//      else
//        for (i = 1; i <= N; i++)
//          for (j = 1; j <= N; j++) AR[i, j] = A[N - j + 1, i];
//    }
//    public static void Rotation(Matrix A, bool key)
//    {
//      int i, j, N = A.Size; Matrix AR = new Matrix(N);

//      if (key)
//        for (j = 1; j <= N; j++)
//          for (i = 1; i <= N; i++) AR[i, j] = A[j, N - i + 1];
//      else
//        for (i = 1; i <= N; i++)
//          for (j = 1; j <= N; j++) AR[i, j] = A[N - j + 1, i];

//      for (i = 1; i <= N; i++)
//        for (j = 1; j <= N; j++) A[i, j] = AR[i, j];
//    }

//    public static void
//        Max_Min(Matrix A, ref double max, ref int max_i, ref int max_j,
//                          ref double min, ref int min_i, ref int min_j)
//    {
//      min = double.MaxValue; max = double.MinValue;
//      for (int i = 1; i <= A.Size; i++)
//        for (int j = 1; j <= A.Size; j++)
//        {
//          if (A[i, j] < min) { min = A[i, j]; min_i = i; min_j = j; }
//          if (max < A[i, j]) { max = A[i, j]; max_i = i; max_j = j; }
//        }
//    }

//    #endregion <--- Генерація Матриць i Елементарнi Перетворення --->

//    #region    <--- Арифметика  + - *  Матриць --->

//    public static void Summ(Matrix A, Matrix B, out Matrix C)
//    {
//      int N = A.Size; C = new Matrix(N);
//      for (int i = 1; i <= N; i++)
//        for (int j = 1; j <= N; j++) C[i, j] = A[i, j] + B[i, j];
//    }

//    public static void Summ(Matrix A, Matrix B)
//    {
//      int N = A.Size;
//      for (int i = 1; i <= N; i++)
//        for (int j = 1; j <= N; j++) A[i, j] += B[i, j];
//    }

//    public static void Subtract(Matrix A, Matrix B, out Matrix C)
//    {
//      int N = A.Size; C = new Matrix(N);
//      for (int i = 1; i <= N; i++)
//        for (int j = 1; j <= N; j++) C[i, j] = A[i, j] - B[i, j];
//    }

//    public static void Subtract(Matrix A, Matrix B)
//    {
//      int N = A.Size;
//      for (int i = 1; i <= N; i++)
//        for (int j = 1; j <= N; j++) A[i, j] -= B[i, j];
//    }

//    public static void Multiply(double a, Matrix A, out Matrix C)
//    {
//      int i, j, N = A.Size; C = new Matrix(N);
//      for (i = 1; i <= N; i++)
//        for (j = 1; j <= N; j++) C[i, j] = a * A[i, j];
//    }

//    public static void Multiply(double a, ref Matrix A)
//    {
//      int i, j, N = A.Size;

//      Matrix D = new Matrix(N);

//      for (i = 1; i <= N; i++)
//        for (j = 1; j <= N; j++)
//          A[i, j] = a * A[i, j];
//      //D[i, j] = a * A[i, j];

//      // Copy(D, out A);
//      //A = D;
//    }

//    public static void Multiply(Matrix A, Matrix B, out Matrix C)
//    {
//      int i, j, k, N = A.Size; C = new Matrix(N);
//      for (i = 1; i <= N; i++)
//        for (j = 1; j <= N; j++)
//        {
//          C[i, j] = 0.0;
//          for (k = 1; k <= N; k++) C[i, j] += A[i, k] * B[k, j];
//        }
//    }

//    public static void Multiply(ref Matrix A, Matrix B)
//    {
//      int i, j, k, N = A.Size;
//      Matrix C = new Matrix(N);
//      for (i = 1; i <= N; i++)
//        for (j = 1; j <= N; j++)
//        {
//          C[i, j] = 0.0;
//          for (k = 1; k <= N; k++) C[i, j] += A[i, k] * B[k, j];
//        }
//      Copy(C, out A);
//    }

//    #endregion <--- Арифметика  + - *  Матриць --->

//    #region    <--- Розкладання Матриць та Визначники --->

//    //                   LU - розкладання заданої матрицi А
//    public static void
//      LU_Factorization(Matrix A, out Matrix L, out Matrix U)
//    {
//      int n = A.Size; L = new Matrix(n); U = new Matrix(n);

//      //          Формуємо перший рядок матрицi U        (3.10)
//      for (int j = 1; j <= n; j++) U[1, j] = A[1, j];
//      U.Det = U[1, 1];

//      //          Формуємо перший стовпчик матрицi L     (3.11)
//      for (int i = 1; i <= n; i++) L[i, 1] = A[i, 1] / A[1, 1];

//      double s; int m, k;
//      for (int i = 2; i <= n; i++)
//      {
//        for (int j = 2; j <= n; j++)
//        {
//          m = Math.Min(i, j); s = A[i, j];
//          for (k = 1; k <= m; k++) s -= L[i, k] * U[k, j];        // (3.14)
//          if (i > j) L[i, j] = s / U[j, j]; else U[i, j] = s;     // (3.15)
//        }
//        L[i, i] = 1.0; U.Det *= U[i, i];                          // (3.16)
//      }
//      A.Det = U.Det; L.Det = 1.0;
//    }

//    //       Формуємо мiнор M до елемента aIJ матрицi А 
//    public static void
//      Minor_1(Matrix A, int I, int J, out Matrix M)
//    {
//      M = new Matrix(A.Size - 1);
//      int i_M = 1;
//      for (int i_A = 1; i_A <= A.Size; i_A++)
//      {
//        if (i_A == I) continue;
//        int j_M = 1;
//        for (int j_A = 1; j_A <= A.Size; j_A++)
//        {
//          if (j_A == J) continue;
//          M[i_M, j_M] = A[i_A, j_A];
//          j_M++;
//        }
//        i_M++;
//      }
//    }

//    // Рекурсивне обчислення визначника 
//    //   з розкладанням за елементами 1-го стовпчика 
//    public static double Determinant_J(Matrix A)
//    {
//      double det = 0.0;
//      if (A.Size == 2)
//      { det = A[1, 1] * A[2, 2] - A[2, 1] * A[1, 2]; } // (3.20) 
//      else
//      {
//        int znak = -1;
//        for (int k = 1; k <= A.Size; k++)              // (3.19)
//        {
//          znak = -znak; Minor_1(A, k, 1, out Matrix M);
//          det += znak * A[k, 1] * Determinant_J(M);
//        }
//      }
//      if (double.IsNaN(A.Det)) A.Det = det;
//      return det;
//    }

//    // Рекурсивне обчислення визначника 
//    //   з розкладанням за елементами 1-го рядка
//    public static double Determinant_I(Matrix A)
//    {
//      double det = 0.0;
//      if (A.Size == 2)
//      { det = A[1, 1] * A[2, 2] - A[2, 1] * A[1, 2]; } // (3.20) 
//      else
//      {
//        int znak = -1;
//        for (int k = 1; k <= A.Size; k++)              // (3.19)
//        {
//          znak = -znak; Minor_1(A, 1, k, out Matrix M);
//          det += znak * A[1, k] * Determinant_I(M);
//        }
//      }
//      if (double.IsNaN(A.Det)) A.Det = det;
//      return det;
//    }

//    // Обчислення алгебраїчного доповнення до елемента aIJ матрицi А
//    public static double Cofactor(Matrix A, int I, int J)
//    {
//      if (A.Size == 2)
//      {
//        if (I == J) return A[1, 1] * A[2, 2];
//        else return -A[1, 2] * A[2, 1];
//      }
//      Minor_1(A, I, J, out Matrix Minor);
//      if ((I + J) % 2 == 0) return Determinant_J(Minor);
//      else return -Determinant_I(Minor);
//    }

//    // Обчислення визначника через алгебраїчні доповнення 
//    //  flag = true для К-го рядка,  flag = false для К-го стовбця
//    public static double Determinant(Matrix A, int K, bool flag)
//    {
//      double det = 0.0;
//      if (A.Size == 2)
//      { det = A[1, 1] * A[2, 2] - A[2, 1] * A[1, 2]; }
//      else
//      {
//        if (flag)
//        {
//          for (int j = 1; j <= A.Size; j++)
//            det += A[K, j] * Cofactor(A, K, j);  // по К-му рядку
//        }
//        else
//        {
//          for (int i = 1; i <= A.Size; i++)
//            det += A[i, K] * Cofactor(A, i, K);  // по К-му стовбцю
//        }
//      }
//      if (double.IsNaN(A.Det)) A.Det = det;
//      return det;
//    }

//    #endregion <--- Розкладання Матриць та Визначники --->
//  }

//}
