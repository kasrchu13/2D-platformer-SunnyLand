using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class OneWayPlatform : MonoBehaviour
{
    private Collider2D _tileCollider;
    private Transform _player;
    private void Awake()
    {
        _tileCollider = GetComponent<TilemapCollider2D>();
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void Update()
    {
        //Detecting whether player is above or below
        _tileCollider.enabled = _player.position.y <= transform.position.y? false: true;
    }
}
