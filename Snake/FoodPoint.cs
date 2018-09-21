namespace Snake
{
    public class FoodPoint
    {
        public int PointRow { get; set; }

        public int PointColumn { get; set; }

        public FoodPoint(int row, int column)
        {
            PointRow = row;
            PointColumn = column;
        }
    }
}