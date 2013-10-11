using System;
using System.Drawing;

namespace TextMap
{
   // A pair of points, such as could describe the selection rectangle dragged-out
   // by a user on a map.  Differs from a Rectangle (pos,size) in that it tracks
   // a starting point and an extension point, which are not contrained to be 
   // upper-left and lower-right respectively.
   public struct Selection
   {
      private Point _pt1;   // the starting point of the selection
      private Point _pt2;   // the extension point of the selection

      public Selection(Point ptAnchor, Point ptExtension) {
         _pt1 = ptAnchor;
         _pt2 = ptExtension;
      }

      public Selection(Point ptAnchor, Size siz) {
         _pt1 = ptAnchor;
         _pt2 = new Point( _pt1.X + siz.Width - 1, _pt1.Y + siz.Height - 1 );
      }      

      public Point pt1 { get { return _pt1; } }
      public Point pt2 { get { return _pt2; } }

      //public Rectangle GetRect()
      //{
      //   Point cel1 = pt1;
      //   Point cel2 = pt2;
      //   Util.NormalizeExtents(ref cel1, ref cel2);
      //   return new Rectangle(cel1, new Size(cel2) - new Size(cel1) + new Size(1, 1));
      //}

      public Rectangle GetTrimRect() { return getRectTrim(0); } 
      public Rectangle GetRect()     { return getRectTrim(1); } 
      
      private Rectangle getRectTrim( int trim ) {
         Point cel1 = pt1;
         Point cel2 = pt2;
         Util.NormalizeExtents(ref cel1, ref cel2);
         return new Rectangle(cel1, new Size(cel2) - new Size(cel1) + new Size(trim, trim));      
      }
      
      
      public Selection Clip( Size siz ) {
         Rectangle r = GetRect();
         r.Intersect( new Rectangle( new Point(0,0), siz ));
         return new Selection( r.Location, r.Size );
      }
   }
   
   static public class Util {

      static public void Swap<T>(ref T a, ref T b) { T c = a; a = b; b = c; }
      
      static public void NormalizeExtents(ref Point pt1, ref Point pt2)
      {
         // Translates two points into the upper-left and lower-right points
         // of the rectangle specified by the original two points.       
         if (pt2.X < pt1.X) { int a = pt1.X; pt1.X = pt2.X; pt2.X = a; }
         if (pt2.Y < pt1.Y) { int a = pt1.Y; pt1.Y = pt2.Y; pt2.Y = a; }
      }

      // Encodes red, green and blue values (0-255) into Windows RGB format.
      static public int RGB(uint R, uint G, uint B)
      {
         return (int)(Math.Min(0xFF, B) * 0x10000 + Math.Min(0xFF, G) * 0x100 + Math.Min(255, R));
      }

      // Translates red, green and blue values (0-255) into a .NET color.
      static public Color ColorFromRGB(uint R, uint G, uint B)
      {
         return ColorTranslator.FromWin32(RGB(R, G, B));
      }      
      
   }
   // ---------------------------------------------------------------------------------------------   
   // UsingBlock
   //                                                                                                      
   // Abstract base class that allows a class to be used with 'using' statement to force some action 
   // to take place automatically at the end of the 'using' block. Derived class may use the 
   // constructor to initialize the object instance and must override the abstract Finally() method, 
   // which will be executed at the end of the block.
   //
   // NOTE: This is for a special usage of the using statement and iDisposable interfaces, not 
   //       not necessarily related to the their intended purpose of freeing unmanaged resources.
   //
   // Example:
   //
   //    class UB1 : UsingBlock {
   //       string s1, s2;
   //       public UB1(string s1, s2) { this.s1 = s1; this.s2 = s2; Debug.WriteLine( s1 ); }
   //       public override Finally() { Debug.WriteLine( s2 ); }
   //    }
   //
   //    using (var a = new UB1("Entering block", "Exiting block")      // prints "Entering block"
   //    {
   //      // do something in between ...
   //    }                                                              // prints "Exiting block" 
   //
   // ---------------------------------------------------------------------------------------------
   abstract class UsingBlock : IDisposable
   {
      public abstract void Finally();
   
      private bool _disposed = true;
      public UsingBlock() { _disposed = false; }      
   
      public void Dispose() {
         Dispose(true);
         GC.SuppressFinalize(this);
      }

      protected virtual void Dispose(bool disposing) {
         if (!_disposed) {
            if (disposing) {
               Finally();
            }
            _disposed = true;
         }
      }         
   }   
}
   