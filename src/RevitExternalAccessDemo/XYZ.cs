/* 
 * Copyright 2012 © Victor Chekalin IVC
 * 
 * THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY 
 * KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
 * IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
 * PARTICULAR PURPOSE.
 * 
 */

namespace RevitExternalAccessDemo
{
    public class XYZ
    {
        public static XYZ Create(double x, double y, double z = 0)
        {
            return new XYZ()
                {
                    X = x,
                    Y = y,
                    Z = z
                };
        }        

        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
    }
}