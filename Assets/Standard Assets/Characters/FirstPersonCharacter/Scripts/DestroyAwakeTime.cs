using UnityEngine;

public class DestroyAwakeTime : MonoBehaviour
{
    public float TimeDetroy; 
    private void Awake()
    {
        Destroy(this.gameObject, TimeDetroy);
    }
}
