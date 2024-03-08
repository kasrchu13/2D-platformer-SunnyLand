using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class BodyInteraction : MonoBehaviour
{
    #region Pick up items
    private void OnTriggerEnter2D(Collider2D item)
    {
        ICollectable _item = item.GetComponent<ICollectable>();
        if(_item != null) _item.Collect();
    }
    #endregion


    #region Encouter enemies
    private void OnCollisionEnter2D(Collision2D enemy) 
    {
        IBodyCollision _enemy = enemy.collider.GetComponent<IBodyCollision>();
        if(_enemy != null) _enemy.BodyHit();
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
