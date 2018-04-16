using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddUserStoryHandler : MonoBehaviour {

    public InputField inputField;
    public ListManager listManager;

    public void OnInputFieldChange()
    {
        listManager.PopulateList(UserStoryManager.Instance.GetSimilarUserStories(inputField.text, 0.5f));
    }
    public void AddUserStory()
    {
        if(inputField.text != "")
        {
            UserStoryManager.Instance.AddUserStory(inputField.text);
            inputField.text = "";
        }
    }
    public void Back()
    {
        GUIManager.Instance().LoadCanvas("Main Menu");
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
