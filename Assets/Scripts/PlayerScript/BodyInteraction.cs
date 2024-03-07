using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class BodyInteraction : MonoBehaviour
{
    #region Pick up items
    private void OnTriggerEnter2D(Collider2D item)
    {
        ICollectable _collectable = item.GetComponent<ICollectable>();
        if(_collectable != null) _collectable.Collect();
    }
    #endregion


    #region Encouter enemies
    private void OnCollisionEnter2D(Collision2D enemy) 
    {
        IBodyCollision _combat = enemy.collider.GetComponent<IBodyCollision>();
        if(_combat != null) _combat.BodyHit();
    }
    #endregion

}

public interface ICollectable
{
    void Collect();
}

public interface IBodyCollision
{
    void BodyHit();
}
