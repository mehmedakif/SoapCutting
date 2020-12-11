using UnityEngine;
using UnityEngine.UI;
using System;

public static class ButtonExtension
{
	public static void AddEventListener<T>(this Button button, T param, Action<T> OnClick)
	{
		button.onClick.AddListener(delegate () {
			OnClick(param);
		});
	}
}

public class ListSoapScript : MonoBehaviour
{
	public GameObject currentSoap;
	public Item[] allSoaps;
	[SerializeField] 

	void Start()
	{
		GameObject buttonTemplate = transform.GetChild(0).gameObject;
		GameObject g;

		int N = allSoaps.Length;

		for (int i = 0; i < N; i++)
		{
			g = Instantiate(buttonTemplate, transform);
			g.transform.GetChild(0).GetComponent<Image>().sprite = allSoaps[i].icon;
			g.transform.GetChild(1).GetComponent<Text>().text = allSoaps[i].name;
			g.transform.GetChild(2).GetComponent<Text>().text = allSoaps[i].description;

			/*g.GetComponent <Button> ().onClick.AddListener (delegate() {
				ItemClicked (i);
			});*/
			g.GetComponent<Button>().AddEventListener(i, ItemClicked);
			
		}

		Destroy(buttonTemplate);
	}

	void ItemClicked(int itemIndex)
	{
		
		currentSoap.GetComponent<MeshRenderer>().material = allSoaps[itemIndex].material;
		Debug.Log("------------item " + itemIndex + " clicked---------------");
		Debug.Log("name " + allSoaps[itemIndex].name);
		Debug.Log("desc " + allSoaps[itemIndex].description);
	}
}
