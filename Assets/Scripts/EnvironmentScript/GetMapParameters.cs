using UnityEngine;
using UnityEngine.Tilemaps;


//this script should assign to the map in the stage
public class GetMapParameters : MonoBehaviour
{
    private void Awake() {
        var bounds = GetComponent<TilemapCollider2D>().bounds;
        StageMap.MapBounds = bounds;
        var collider = GetComponent<TilemapCollider2D>();
        StageMap.mapGroundCollider = collider;
    }
}
public static class StageMap{
    public static Bounds MapBounds;
    public static Collider2D mapGroundCollider;
}
