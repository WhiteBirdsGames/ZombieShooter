using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TSZ_CharacterCustomizeParts : MonoBehaviour
{
	private int bodyTyp;
	private int topTyp;
	private int bottomTyp;
    public bool IsRandom;

	private TSZ_AssetsListParts materialsList;

	private SkinnedMeshRenderer skinnedMeshRenderer;

	public enum BodyType
	{
		V1,
		V2,
		V3
	}

	public enum TopType
	{
		V1,
		V2,
		V3,
		V4

	}

	public enum BottomType
	{
		V1,
		V2,
		V3
	}

	public BodyType bodyType;
	public TopType topType;
	public BottomType bottomType;

	// Use this for initialization
	void Start ()
	{
		if(IsRandom)
        {
           
            bodyType = (BodyType)Random.Range(0, 3);
            topType = (TopType)Random.Range(0, 4);
            bottomType = (BottomType)Random.Range(0, 3);
            charCustomize((int)bodyType, (int)topType, (int)bottomType);
        }
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	public void charCustomize (int body, int top, int bottom)
	{
		materialsList = gameObject.GetComponent<TSZ_AssetsListParts> ();

		Material[] mat;
		Transform curSub = gameObject.transform.Find ("GeoParts/ForeArmL");
		Renderer skinRend = curSub.GetComponent<Renderer> ();
		skinRend.material = materialsList.BodyMaterials [body];

		curSub = gameObject.transform.Find ("GeoParts/ForeArmR");
		skinRend = curSub.GetComponent<Renderer> ();
		skinRend.material = materialsList.BodyMaterials [body];


		curSub = gameObject.transform.Find ("GeoParts/HandR");
		skinRend = curSub.GetComponent<Renderer> ();
		skinRend.material = materialsList.BodyMaterials [body];

		curSub = gameObject.transform.Find ("GeoParts/HandL");
		skinRend = curSub.GetComponent<Renderer> ();
		skinRend.material = materialsList.BodyMaterials [body];

		curSub = gameObject.transform.Find ("GeoParts/LegL");
		skinRend = curSub.GetComponent<Renderer> ();
		skinRend.material = materialsList.BottomMaterials [bottom];

		curSub = gameObject.transform.Find ("GeoParts/LegR");
		skinRend = curSub.GetComponent<Renderer> ();
		skinRend.material = materialsList.BottomMaterials [bottom];

		curSub = gameObject.transform.Find ("GeoParts/KneeL");
		skinRend = curSub.GetComponent<Renderer> ();
		skinRend.material = materialsList.BottomMaterials [bottom];

		curSub = gameObject.transform.Find ("GeoParts/KneeR");
		skinRend = curSub.GetComponent<Renderer> ();
		skinRend.material = materialsList.BottomMaterials [bottom];

		curSub = gameObject.transform.Find ("GeoParts/FootL");
		skinRend = curSub.GetComponent<Renderer> ();
		skinRend.material = materialsList.BottomMaterials [bottom];

		curSub = gameObject.transform.Find ("GeoParts/FootR");
		skinRend = curSub.GetComponent<Renderer> ();
		skinRend.material = materialsList.BottomMaterials [bottom];

		curSub = gameObject.transform.Find ("GeoParts/ArmL");
		skinRend = curSub.GetComponent<Renderer> ();
		mat = new Material[2];
		mat [1] = materialsList.BodyMaterials [body];
		mat [0] = materialsList.TopMaterials [top];
		skinRend.materials = mat;

		curSub = gameObject.transform.Find ("GeoParts/ArmR");
		skinRend = curSub.GetComponent<Renderer> ();
		mat = new Material[2];
		mat [0] = materialsList.BodyMaterials [body];
		mat [1] = materialsList.TopMaterials [top];
		skinRend.materials = mat;

		curSub = gameObject.transform.Find ("GeoParts/Head");
		skinRend = curSub.GetComponent<Renderer> ();
		mat = new Material[2];
		mat [0] = materialsList.BodyMaterials [body];
		if (body == 2) {
			mat [1] = materialsList.HairMaterials [1];
		} else {
			mat [1] = materialsList.HairMaterials [0];
		}
		skinRend.materials = mat;

		curSub = gameObject.transform.Find ("GeoParts/Torso");
		skinRend = curSub.GetComponent<Renderer> ();
		mat = new Material[3];
		mat [0] = materialsList.TopMaterials [top];
		mat [1] = materialsList.BodyMaterials [body];
		mat [2] = materialsList.BottomMaterials [bottom];
		skinRend.materials = mat;
		skinRend.materials = mat;
	}

	void OnValidate ()
	{
		//code for In Editor customize

		bodyTyp = (int)bodyType;
		topTyp = (int)topType;
		bottomTyp = (int)bottomType;

		charCustomize (bodyTyp, topTyp, bottomTyp);

	}
}
