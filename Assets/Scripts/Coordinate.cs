using UnityEngine;

public static class Coordinate
{
    public static Vector3Int GetCoordinates(int index, Vector2Int basis)
    {
        int xzBasis = basis.x * basis.y;
        int y = index / xzBasis;
        int indexInPlane = index % xzBasis;
        int x = indexInPlane / basis.x;
        int z = indexInPlane % basis.x;
        return new Vector3Int(x, y, z);
    }

    public static Vector3 GetLocalCoordinates(Vector3Int arrayPosition, Vector3 size)
    {
        return new Vector3(arrayPosition.x * size.x, arrayPosition.y * size.y, arrayPosition.z * size.z);
    }
}
