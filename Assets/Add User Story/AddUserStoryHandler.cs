using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddUserStoryHandler : MonoBehaviour {

    public InputField inputField;
    public ListManager listManager;
    public Dropdown personaDropDown;
    public InputField newPersonaInputField;

    void OnEnable()
    {
        UpdatePersonaDropDown();
    }

    public void OnInputFieldChange()
    {
        listManager.PopulateList(UserStoryManager.Instance.GetSimilarUserStories(GetUserStory(), 0.5f));
    }

    public void OnNewPersonaInputFieldChange()
    {
        personaDropDown.interactable = newPersonaInputField.text == "";
        listManager.PopulateList(UserStoryManager.Instance.GetSimilarUserStories(GetUserStory(), 0.5f));
    }

    public void OnDropDownSelect()
    {
        listManager.PopulateList(UserStoryManager.Instance.GetSimilarUserStories(GetUserStory(), 0.5f));
    }

    public void AddUserStory()
    {
        if(inputField.text != "")
        {
            UserStoryManager.Instance.AddUserStory(GetUserStory());
            inputField.text = "";
            if(newPersonaInputField.text != "")
            {
                UserStoryManager.Instance.AddPersona(newPersonaInputField.text.ToLower());
                newPersonaInputField.text = "";
                UpdatePersonaDropDown();
            }
        }
    }
    public void Back()
    {
        GUIManager.Instance().LoadCanvas("Main Menu");
    }

    string GetUserStory()
    {
        string persona = personaDropDown.captionText.text;
        if (newPersonaInputField.text != "")
            persona = newPersonaInputField.text;


        return "As a " + persona.ToLower() + " i want " + inputField.text.ToLower();
    }

    void UpdatePersonaDropDown()
    {
        personaDropDown.ClearOptions();
        personaDropDown.AddOptions(new List<string>(UserStoryManager.Instance.GetPersonas()));
    }

}
