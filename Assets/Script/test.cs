using UnityEngine;

public class test : MonoBehaviour
{
    float D = 3;
    float d = 0;
    private void FixedUpdate()
    {
        d += Time.fixedDeltaTime;
        transform.position = new Vector3(d%D , 0 , 0);
    }
}
