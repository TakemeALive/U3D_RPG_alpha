using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TextManager : MonoBehaviour {

	#region Singleton

	public static TextManager instance;

	void Awake(){
		if(instance != null){
			Debug.LogWarning("More than one instance of TextManager found");
			return;
		}

		instance = this;
	}

	#endregion

	// Already set Canvas to TextManager in Game Editor
	public Canvas HUDCanvas;
	public Canvas helpCanvas;
	public Canvas dialogCanvas;

	string messagePanel = "MessagePanel";
	string controlHelpText = "ControlHelpText";
	string interactionText = "InteractionText";

	public void Start(){
		CloseDialog();
		CloseControlHelp();
		CloseInteractionHelp();
	}

	void Update() {
		if(Input.GetKeyDown(KeyCode.H)){
			var text = helpCanvas.transform.Find(controlHelpText);
			if(!text.gameObject.activeSelf){
				OpenControlHelp();
			}else{
				CloseControlHelp();
			}
		}
	}
	
	public void OpenDialog(){
		var dialog = dialogCanvas.transform.Find(messagePanel);
		dialog.gameObject.SetActive(true);
	}

	public void CloseDialog(){
		var dialog = dialogCanvas.transform.Find(messagePanel);
		dialog.gameObject.SetActive(false);
	}

	public void OpenControlHelp(){
		var text = helpCanvas.transform.Find(controlHelpText);
		text.gameObject.SetActive(true);
	}

	public void CloseControlHelp(){
		var text = helpCanvas.transform.Find(controlHelpText);
		text.gameObject.SetActive(false);
	}
	
	public void OpenInteractionHelp(){
		var text = helpCanvas.transform.Find(interactionText);
		text.gameObject.SetActive(true);
	}

	public void CloseInteractionHelp(){
		var text = helpCanvas.transform.Find(interactionText);
		text.gameObject.SetActive(false);
	}
}
