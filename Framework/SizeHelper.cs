using System;
using System.Collections.Generic;
using System.Text;

public static class SizeHelper
{
    public static IntPtr GetSize(this List<float> list) => (IntPtr)(list.Count * sizeof(float));
    public static IntPtr GetSize(this float[] list) => (IntPtr)(list.Length * sizeof(float));
    public static IntPtr GetSize(this float[] list,int  divisor) => (IntPtr)(list.Length * sizeof(float)/divisor);


}

