using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeetInteraction : MonoBehaviour
{
    #region stomp on weakpoint
    private void OnTriggerEnter2D(Collider2D enemy)
    {
        IWeakPointCollision _combat = enemy.GetComponent<IWeakPointCollision>();
        if(_combat != null) _combat.WeakPointHit();
    }
    #endregion
}
public interface IWeakPointCollision
    {
        void WeakPointHit();
    }