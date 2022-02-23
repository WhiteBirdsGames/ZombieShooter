using UnityEngine;

public class DestroyTime : MonoBehaviour
{
    public float _DestroyTime; 

    private void Start()
    {
        Destroy(gameObject, _DestroyTime);
    }
}
