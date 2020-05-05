using System;
using CI = System.Globalization.CultureInfo;
using System.IO;

namespace MAC_DLL
{
  //====================  Vector  ========================

  public enum VectorType { Simple, Identity, Test, Zero }
  public enum PrintType { Horizontal, Vertical }

  public class Vector
  {
    public double[] V;
    public int Size;
    public bool flag;

    #region    <--- Конструктори Vector --->

    public Vector(int size)  // Конструктор вектора заданого розмiру
    {
      Size = size; V = new double[Size];
    }

    public Vector(int size, VectorType type) // Перевантажений конструктор
    {
      Size = size; V = new double[Size];
      switch (type)
      {
        case VectorType.Simple:
          for (int i = 1; i <= Size; i++) this[i] = double.NaN; break;

        case VectorType.Identity:
          for (int i = 1; i <= Size; i++) this[i] = 1.0; break;

        case VectorType.Test:
          for (int i = 1; i <= Size; i++) this[i] = i; break;

        case VectorType.Zero:
          for (int i = 1; i <= Size; i++) this[i] = 0.0; break;
      }
    }

    public double this[int index_i] // Iндексатор Vector
    {
      get
      {                                       // get-аксесор
        if (index_i >= 1 && index_i <= Size)
        { flag = false; return V[index_i - 1]; }
        else
        { flag = true; return double.NaN; }
      }
      set
      {                                       // set-аксесор
        if (index_i >= 1 && index_i <= Size)
        { V[index_i - 1] = value; flag = false; }
        else flag = true;
      }
    }

    #endregion <--- Конструктори Vector --->

    #region    <--- Методи вводу-виводу --->

    public static void Read(string path, out Vector V, out int n)
    {
      FileInfo file = new FileInfo(path);

      if (file.Extension == ".txt")
      {
        bool dot_or_comma;
        if (CI.CurrentCulture.NumberFormat.CurrencyDecimalSeparator == ",")
          dot_or_comma = false;
        else dot_or_comma = true;

        StreamReader rdr = new StreamReader(file.OpenRead());
        n = Convert.ToInt32(rdr.ReadLine()); V = new Vector(n);

        string[] numbers; string line = rdr.ReadLine().Trim();
        if (dot_or_comma) line = line.Replace(",", ".");
        else line = line.Replace(".", ",");

        numbers = line.Split(new char[] { ' ', ';' },
                  StringSplitOptions.RemoveEmptyEntries);
        for (int i = 1; i <= n; i++)
          V[i] = Convert.ToDouble(numbers[i - 1]);
        rdr.Close(); return;
      }

      if (file.Extension == ".bin")
      {
        BinaryReader rdr = new BinaryReader(file.OpenRead());
        n = rdr.ReadInt32(); V = new Vector(n);
        for (int i = 1; i <= n; i++) V[i] = rdr.ReadDouble();
        rdr.Close(); return;
      }

      V = null; n = 0; // Some Mistake with File or data   
    }

    public static string
      Print(Vector V, PrintType type, bool form, int fs, int fd, string title)
    {
      int ka = 0, n = V.Size;
      string txt = "\r\n", frmt;

      if (title != "")
        txt += "  " + title + $"   size = {n}\r\n";
      if (form)
      {
        int max_ka = 0;
        double a;
        for (int i = 1; i <= n; i++)
        {
          a = Math.Abs(V[i]);
          if (a < 10.0) ka = 1;
          else ka = (int)Math.Ceiling(Math.Log10(a));
          max_ka = Math.Max(max_ka, ka);
        }
        ka = fs + 1 + max_ka + 1 + fd;
        frmt = "{0," + $"{ka}:F{fd}" + "}";
      }
      else
      {
        ka = fs + 1 + 1 + 1 + fd + 1 + 1 + 3;
        frmt = "{0," + $"{ka}:E{fd}" + "}";
      }

      for (int i = 1; i <= n; i++)
      {
        switch (type)
        {
          case PrintType.Horizontal:
            txt += string.Format(frmt, V[i]); break;
          case PrintType.Vertical:
            txt += $"{i,3}" + string.Format(frmt, V[i]) + "\r\n"; break;
        }
      }
      switch (type)
      {
        case PrintType.Horizontal: return txt + "\r\n";
        case PrintType.Vertical: return txt;
      }
      return "";
    }

    #endregion <--- Методи вводу-виводу --->

    #region    <--- Генерацiя Векторiв --->

    public static Vector Copy(Vector A)
    {
      Vector C = new Vector(A.Size);
      for (int i = 1; i <= C.Size; i++) C[i] = A[i];
      return C;
    }

    public static void Max_Min(Vector A,
                               ref double max, ref int max_i,
                               ref double min, ref int min_i)
    {
      min = double.MaxValue; max = double.MinValue;
      for (int i = 1; i <= A.Size; i++)
      {
        if (A[i] < min) { min = A[i]; min_i = i; }
        if (max < A[i]) { max = A[i]; max_i = i; }
      }
    }
    #endregion <--- Генерацiя Векторiв --->

    #region    <--- Арифметика  + - *  Векторiв --->

    public static Vector operator +(Vector A, Vector B)
    {
      if (A.Size != B.Size) return null;
      int i, N = A.Size;
      Vector C = new Vector(N);
      for (i = 1; i <= N; i++) C[i] = A[i] + B[i];
      return C;
    }

    public static Vector operator -(Vector A, Vector B)
    {
      if (A.Size != B.Size) return null;
      int i, N = A.Size; Vector C = new Vector(N);
      for (i = 1; i <= N; i++) C[i] = A[i] - B[i];
      return C;
    }

    public static Vector operator *(double a, Vector A)
    {
      int i, N = A.Size; Vector C = new Vector(N);
      for (i = 1; i <= N; i++) C[i] = a * A[i];
      return C;
    }

    public static Vector operator *(Vector A, double a)
    {
      int i, N = A.Size; Vector C = new Vector(N);
      for (i = 1; i <= N; i++) C[i] = a * A[i];
      return C;
    }

    public static double operator *(Vector A, Vector B)
    {
      if (A.Size != B.Size) return double.NaN;
      int i, N = A.Size;
      double c = 0.0;
      for (i = 1; i <= N; i++) c += A[i] * B[i];
      return c;
    }

    public static Vector Multiply(Matrix A, Vector B)
    {
      if (A.Size != B.Size) return null;
      int N = A.Size; Vector X = new Vector(N);
      for (int i = 1; i <= N; i++)
      {
        X[i] = 0.0;
        for (int j = 1; j <= N; j++)
        {
          X[i] += A[i, j] * B[j];
        }
      }
      return X;
    }

    public static double Module(Vector B)
    {
      double mod = 0.0;
      for (int i = 1; i <= B.Size; i++) mod += B[i] * B[i];
      return Math.Sqrt(mod);
    }

    #endregion <--- Арифметика  + - *  Векторiв --->
  }
}
