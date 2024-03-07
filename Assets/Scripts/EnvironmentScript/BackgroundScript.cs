using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class BackgroundScript : MonoBehaviour
{
    [SerializeField]private Transform _cam;
    [Range(0,1)]
    [Tooltip("The closer the background, the lower value the parallexfactor")]
    public float ParallaxFactor;
    private float _length;
    private float _startPos;
    
    private void Awake() 
    {
        _length = GetComponent<SpriteRenderer>().bounds.size.x;
    }
    private void Start()
    {
        _startPos = transform.position.x;
    }
    private void FixedUpdate()
    {
        //temp used for checking if the camera out of sprite's bounds 
        var temp = _cam.position.x * (1 - ParallaxFactor);
        var movedDis = _cam.position.x * ParallaxFactor;
        transform.position = new Vector3(_startPos + movedDis, transform.position.y, transform.position.z);
        
        //adjust position to give continuous background 
        if(temp > _startPos + _length) _startPos += _length;
        if(temp < _startPos - _length) _startPos -= _length;
    }

}
