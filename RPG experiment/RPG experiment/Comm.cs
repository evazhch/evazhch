using System;

public class Comm
{
    public static long Time()
    {
        DateTime dt1 = new DateTime(1970, 1, 1);
        TimeSpan ts=DateTime.Now-dt1;
        return (long)ts.TotalMilliseconds;
    }
   public  enum Direction
   {
       UP=4,
       DOWN=1,
       RIGHT=3,
       LEFT=2,
   }
  public static Direction opposite_direction(Direction direction)
   {
       if (direction == Direction.UP)
           return Direction.DOWN;
       else if (direction == Direction.DOWN)
           return Direction.UP;
       else if (direction == Direction.LEFT)
           return Direction.RIGHT;
       else if (direction == Direction.RIGHT)
           return Direction.LEFT;
       return Direction.UP;
   }

}
