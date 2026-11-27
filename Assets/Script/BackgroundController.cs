using System;
using UnityEngine;
using UnityEngine.UIElements;

public class BackgroundController : MonoBehaviour
{
    [Header("ScrollOption")]

    private float _startPos, _length;
    public GameObject _cam;
    [Range(0, 1)] public float _parallaxEffect;
    private void Start()
    {
        _startPos = _cam.transform.position.x;
        _length = GetComponent<SpriteRenderer>().bounds.size.x;
    }
    private void FixedUpdate()
    {
        float distance = _cam.transform.position.x * _parallaxEffect; // 0 = đi với cam, 1 = không di chuyển
        float movement = _cam.transform.position.x * (1 - _parallaxEffect);

        transform.position = new Vector3(_startPos + distance, transform.position.y, transform.position.z);

        // start scroll
        if (movement > _startPos + _length) _startPos += _length;
        else if (movement < _startPos - _length) _startPos -= _length;
    }
}

