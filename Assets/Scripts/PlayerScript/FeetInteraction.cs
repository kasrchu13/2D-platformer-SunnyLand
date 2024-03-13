using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeetInteraction : MonoBehaviour
{
    #region stomp on weakpoint
    private void OnTriggerEnter2D(Collider2D enemy)
    {
        IWeakPointCollision tarEnemy = enemy.GetComponent<IWeakPointCollision>();
        if(tarEnemy != null) tarEnemy.WeakPointHit();
    }
    #endregion
}
public interface IWeakPointCollision
    {
        void WeakPointHit();
    }