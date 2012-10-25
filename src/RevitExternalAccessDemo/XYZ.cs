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