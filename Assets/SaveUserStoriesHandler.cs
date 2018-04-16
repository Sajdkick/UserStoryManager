using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveUserStoriesHandler : MonoBehaviour {

    public InputField inputField;

    public void SaveUserStories(){

        System.IO.TextWriter tw = new System.IO.StreamWriter(inputField.text + ".txt");

        List<string> saveList = UserStoryManager.Instance.CreateSaveList();
        foreach (string line in saveList)
            tw.WriteLine(line);

        tw.Close();

    }
    
}
