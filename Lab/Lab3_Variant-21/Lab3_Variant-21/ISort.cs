using System.Drawing;

namespace Lab3_Variant_21
{
    interface ISort
    {
        void asc(int[] arr, Graphics g, int MaxVal);
        void desc(int[] arr, Graphics g, int MaxVal);
        void Draw();
    }
}
