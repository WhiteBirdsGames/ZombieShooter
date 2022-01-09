using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandTakes : MonoBehaviour
{
    public Animator AnimatorGUN;
    public GameObject Hand_OB;
    public List<string> Tags;
    [HideInInspector] public string CurrentTag;
    [System.Serializable]
    public class TagObject
    {
        public string Tag;
        public GameObject Object, ObjectTip;
    }
    public GameObject InfoGun;
    public TagObject[] TagObjects;
    public bool IsGunInHand;
    RaycastHit hit;
    public float DistanceRay;

    private bool IsTagRay;

    void Update()
    {
        IsTagRay = false;
        if (Physics.Raycast(transform.position, transform.forward, out hit, DistanceRay))
        {
            if(hit.collider)
            {
                if(Tags.Contains(hit.collider.tag))
                {
                    IsTagRay = true;
                    if(!IsGunInHand)
                    {
                        if(hit.collider.tag == "Door")
                        {
                            IsTagRay = false;
                        }
                    }
                    CurrentTag = hit.collider.tag;
                }
            }
        }
        Hand_OB.SetActive(IsTagRay);

        if(Hand_OB.activeSelf)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                TakeHand();
            }
        }
    }

    public void TakeHand ()
    {
        for (int i = 0; i < TagObjects.Length; i++)
        {
            if (CurrentTag == TagObjects[i].Tag)
            {
                if (CurrentTag == "AKM")
                {
                    TagObjects[i].Object.SetActive(false);
                    TagObjects[i].ObjectTip.GetComponent<Animation>().Play();
                    Invoke("NextTipDoor", 2.3f);
                    AnimatorGUN.SetBool("IsGunInHands", true);
                    IsGunInHand = true;
                    InfoGun.SetActive(true);
                    break;
                }
                if (CurrentTag == "Door")
                {
                    TagObjects[i].Object.GetComponent<Animation>().Play();
                    TagObjects[i].ObjectTip.GetComponent<Animation>().Play();
                    TagObjects[i].Object.GetComponent<Collider>().enabled = false;
                    break;
                }
            }
        }
       
    }

    public void NextTipDoor ()
    {
        for (int i = 0; i < TagObjects.Length; i++)
        {
            if (TagObjects[i].Tag == "Door")
            {
                TagObjects[i].ObjectTip.SetActive(true);
            }
        }
    }
}
