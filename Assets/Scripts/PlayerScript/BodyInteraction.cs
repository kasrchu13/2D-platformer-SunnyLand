using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using System;

public class BodyInteraction : MonoBehaviour
{
    #region Pick up items
    private void OnTriggerEnter2D(Collider2D item)
    {
        ICollectable tarItem = item.GetComponent<ICollectable>();
        if(tarItem != null) tarItem.Collect();
    }
    #endregion


    #region Encouter enemies
    private void OnCollisionEnter2D(Collision2D enemy) 
    {
        IBodyCollision tarEnemy = enemy.collider.GetComponent<IBodyCollision>();
        if(tarEnemy != null) tarEnemy.BodyHit();
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
