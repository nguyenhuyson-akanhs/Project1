using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LinerController : MonoBehaviour
{ 
    [Header("SetPoint")]
    private LineRenderer lineRenderer;
    public Transform Point0;
    public Transform Point1;
    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }
    private void Start()
    {
        lineRenderer.SetPosition(0, Point0.position);
        lineRenderer.SetPosition(1, Point1.position);
    }
}
