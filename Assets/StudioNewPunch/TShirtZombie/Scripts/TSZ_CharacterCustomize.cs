using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TSZ_CharacterCustomize : MonoBehaviour
{
	private int bodyTyp;
	private int topTyp;
	private int bottomTyp;


	private TSZ_AssetsList materialsList;

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
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	public void charCustomize (int body, int top, int bottom)
	{
		materialsList = gameObject.GetComponent<TSZ_AssetsList> ();
		// Set Body Type
//		
		if (body == 2) {
			materialsList.HairObject.SetActive (false);

		} else {
			materialsList.HairObject.SetActive (true);
		}
		foreach (Transform child in materialsList.BodyObject.transform) {

			Renderer skinRend = child.gameObject.GetComponent<Renderer> ();
			skinRend.material = materialsList.BodyMaterials [body];
		}

		// Set Top Type
		foreach (Transform child in materialsList.TopObject.transform) {
			//print ("Foreach loop: " + child);
			Renderer skinRend = child.gameObject.GetComponent<Renderer> ();
			skinRend.material = materialsList.TopMaterials [top];
		}
		// Set Bottom Type
		foreach (Transform child in materialsList.BottomObject.transform) {
			//print ("Foreach loop: " + child);
			Renderer skinRend = child.gameObject.GetComponent<Renderer> ();
			skinRend.material = materialsList.BottomMaterials [bottom];
		}

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
