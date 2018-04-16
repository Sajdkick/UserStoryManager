using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadUserStoriesHandler : MonoBehaviour {

    public InputField inputField;
    public Toggle clearBeforeLoading;

    public void LoadUserStories(){

        if (clearBeforeLoading.isOn)
        {
            UserStoryManager.Instance.Clear();
        }

        System.IO.TextReader tr = new System.IO.StreamReader(inputField.text + ".txt");

        string userStory = "";
        string line = "";
        do
        {
            line = tr.ReadLine();
            if (line != null && line[0] != '+')
            {
                if(line[0] != '-')
                {
                    userStory = line;
                    UserStoryManager.Instance.AddUserStory(userStory);
                }
                else
                {
                    UserStoryManager.Instance.AddTaskToUserStory(userStory, line.Substring(1, line.Length - 1));
                }

            }

        } while (line[0] != '+');

        while (true)
        {
            line = tr.ReadLine();
            if (line != null)
            {
                UserStoryManager.Instance.AddTask(line);
            }
            else break;
        }

        tr.Close();

    }
    
}
