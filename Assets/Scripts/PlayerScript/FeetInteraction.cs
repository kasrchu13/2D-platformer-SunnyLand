using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeetInteraction : MonoBehaviour
{
    #region stomp on weakpoint
    private void OnTriggerEnter2D(Collider2D enemy)
    {
        IWeakPointCollision _enemy = enemy.GetComponent<IWeakPointCollision>();
        if(_enemy != null) _enemy.WeakPointHit();
    }
    #endregion
}
public interface IWeakPointCollision
    {
        void WeakPointHit();
    }