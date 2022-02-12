using UnityEngine;

public class Trail : MonoBehaviour
{
    public float DestroyTime = 0.2f;
    public Vector3 posNew;

    public void Start()
    {
        Destroy(gameObject, DestroyTime);
    }

    public void SetNewPosition(Vector3 pos)
    {
        posNew = pos;
        Invoke("InvokeNewPos", 0.02f);
    }

    public void InvokeNewPos ()
    {
        this.transform.position = posNew;
    }
}
