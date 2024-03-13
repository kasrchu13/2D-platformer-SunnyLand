using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class ItemFeedback: MonoBehaviour, ICollectable
{
    [SerializeField] private PlayerMain _stats;
    private Animator _anim;
    private bool _yetToCollect= true;
    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }
    public void Collect(){
        if(_yetToCollect) StartCoroutine(CollectingFeedback());
    }

    IEnumerator CollectingFeedback()
    {
        _yetToCollect = false;
        GameManager.Instance.PlayerScore += 1;
        _anim.Play("Item_FeedBack");
        yield return new WaitForSeconds(1);
        Destroy(this.gameObject);
    }
}
