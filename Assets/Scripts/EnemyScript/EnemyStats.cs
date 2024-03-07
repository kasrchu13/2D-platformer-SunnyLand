using UnityEngine;

//this script creates the prototype of stats of mobs
[CreateAssetMenu]
public class EnemyStats : ScriptableObject
{
    [Tooltip("Hitpoint of the Mob")]  
    public int Health;
    [Tooltip("Directly translates to how many tile blocks the mob will roam from starting position")]
    public int RoamingRange;
    [Tooltip("How mush damage player will recieved upon contact")]
    public int MobDmg;
    [Tooltip("How fast the mob is moving")]
    public float MoveSpeed;
    [Tooltip("Downward force apply to the mob, should be smaller than 0")]
    public float DownGravity;
    [Tooltip("Check box for the initial facing of the model")]
    public bool InitialFaceRight;
    [Tooltip("Layers that cause the change in direction")]
    public LayerMask GroundLayer;
    [Tooltip("Interval between 2 direction change calls. Prevent instant direction turn")]
    public float DirChangeThreshold;
    [Tooltip("Length of the raycast detection. Assign for seeing a wall and check whether there is ground in front")]
    public float RayCastDistance;
    [Tooltip("check if mob is deafeated ")]
    public bool IsDefeated;
}
