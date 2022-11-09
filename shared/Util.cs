namespace shared;

public class Util {
    public static DateTime CreateTimeOnly(int hour, int minute, int second) {
        return new DateTime(1, 1, 1, hour, minute, second);
    }

    public static DateTime CreateTimeOnly(TimeOnly time) {
        return new DateTime(1, 1, 1, time.Hour, time.Minute, time.Second);
    }
}