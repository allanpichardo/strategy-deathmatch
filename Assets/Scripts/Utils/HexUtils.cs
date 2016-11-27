using System;

public class OffsetCoordinate
{
    public int x;
    public int y;

    public OffsetCoordinate(){}

    public OffsetCoordinate(int x, int y){
        this.x = x;
        this.y = y;
    }

    public bool IsEven(){
        return y % 2 == 0;
    }
}

public class CubeCoordinate
{
    public int x;
    public int y;
    public int z;

    public CubeCoordinate(){}

    public CubeCoordinate(int x, int y, int z){
        this.x = x;
        this.y = y;
        this.z = z;
    }
}

public class HexUtils
{

    public static OffsetCoordinate CubeCoordToOffsetCoord(CubeCoordinate cubeCoordinate, bool isEven)
    {
        OffsetCoordinate offset = new OffsetCoordinate();
        offset.y = cubeCoordinate.x;
        offset.x = isEven ? 
                    cubeCoordinate.z + (cubeCoordinate.x - (cubeCoordinate.x & 1)) / 2 :
                    cubeCoordinate.z + (cubeCoordinate.x + (cubeCoordinate.x & 1)) / 2;
        return offset;
    }

    public static CubeCoordinate OffsetCoordToCubeCoord(OffsetCoordinate offsetCoordinate)
    {
        CubeCoordinate cube = new CubeCoordinate();
        cube.x = offsetCoordinate.y;
        cube.z = offsetCoordinate.IsEven() ?
                    offsetCoordinate.x - (offsetCoordinate.y - (offsetCoordinate.y & 1)) / 2 :
                    offsetCoordinate.x - (offsetCoordinate.y + (offsetCoordinate.y & 1)) / 2;
        cube.y = -cube.x - cube.z;
        return cube;
    }

    public static float GetDistance(CubeCoordinate a, CubeCoordinate b)
    {
        return (
            Math.Abs(a.x - b.x) +
            Math.Abs(a.y - b.y) +
            Math.Abs(a.z - b.z)
        ) / 2;
    }

    public static float GetDistance(OffsetCoordinate a, OffsetCoordinate b)
    {
        var ac = OffsetCoordToCubeCoord(a);
        var bc = OffsetCoordToCubeCoord(b);
        return GetDistance(ac, bc);
    }
}
