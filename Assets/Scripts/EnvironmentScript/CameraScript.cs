using UnityEngine;


public class CameraScript : MonoBehaviour
{   
    [SerializeField]private Transform _player;
    public float SmoothFollow = 3f;
    private Camera _thisCamera;
    private Bounds _cameraBounds;
    private float _fixedDepth;
    private Vector3 _targetPosition;
    
    private void Awake() 
    {
        _thisCamera = GetComponent<Camera>();
    }

    private void Start() 
    {

        //get the Camera width and height
        //orthograhicSize gives half the height and aspect gives ratio(width divides height)
        var height = _thisCamera.orthographicSize;
        var width = _thisCamera.aspect * height;
        
        var minX = StageMap.MapBounds.min.x + width;
        var maxX = StageMap.MapBounds.max.x - width;

        var minY = StageMap.MapBounds.min.y + height;
        var maxY = StageMap.MapBounds.max.y - height;

        _fixedDepth = transform.position.z;

        _cameraBounds = new Bounds();
        _cameraBounds.SetMinMax(
            min: new Vector3(minX, minY, _fixedDepth), 
            max: new Vector3(maxX, maxY, _fixedDepth)
        );

    }
    private void Update()
    {
        _targetPosition = new Vector3(_player.position.x, _player.position.y, _fixedDepth);
        Vector3 followCam = GetClampedPosition();
        transform.position = Vector3.Lerp(transform.position, followCam, SmoothFollow * Time.deltaTime);

    }

    private Vector3 GetClampedPosition(){
        return new Vector3(
            Mathf.Clamp(_targetPosition.x, _cameraBounds.min.x, _cameraBounds.max.x),
            Mathf.Clamp(_targetPosition.y, _cameraBounds.min.y, _cameraBounds.max.y),
            _fixedDepth
        );
    }
}
