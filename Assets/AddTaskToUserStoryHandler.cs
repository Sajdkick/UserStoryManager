using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddTaskToUserStoryHandler : MonoBehaviour {

    public Dropdown userStoryDropDown;
    public ListManager otherTasksList;
    public ListManager tasksOnThisUserStoryList;
    public ListManager similarTasksList;
    public InputField newTaskInputField;
    public Button addNewTaskButton;

    void OnEnable()
    {
        userStoryDropDown.ClearOptions();
        userStoryDropDown.AddOptions(UserStoryManager.Instance.GetUserStories());
        if(userStoryDropDown.options.Count != 0)
        {
            newTaskInputField.interactable = true;
            addNewTaskButton.interactable = true;
            UpdateLists();
        }
    }

    public void OnSelectUserStory()
    {
        UpdateLists();
    }

    public void OnInputFieldChange()
    {
        similarTasksList.PopulateList(UserStoryManager.Instance.GetSimilarTasks(newTaskInputField.text, 0.5f), "task");
    }

    public void AddNewTask()
    {
        if (newTaskInputField.text != "")
        {
            UserStoryManager.Instance.AddTask(userStoryDropDown.captionText.text, newTaskInputField.text);
            tasksOnThisUserStoryList.AddToList(newTaskInputField.text, "task");
            newTaskInputField.text = "";
        }
    }
    public void Back()
    {
        newTaskInputField.interactable = false;
        addNewTaskButton.interactable = false;
    }

    void UpdateLists()
    {
        List<string> tasksOnSelectedUserStory = UserStoryManager.Instance.GetSpecificTasks(userStoryDropDown.captionText.text);
        tasksOnThisUserStoryList.PopulateList(tasksOnSelectedUserStory, "task");

        List<string> otherTasks = new List<string>(UserStoryManager.Instance.GetTasks());
        foreach (string task in tasksOnSelectedUserStory)
            otherTasks.Remove(task);
        otherTasksList.PopulateList(otherTasks, "task");
    }

    public void RemovedFromOtherTasks(string task)
    {
    }
    public void RemovedFromTasksOnSelectedUserStory(string task)
    {
        UserStoryManager.Instance.RemoveTaskFromUserStory(userStoryDropDown.captionText.text, task);
    }
    public void AddedToOtherTasks(string task)
    {
    }
    public void AddedToTasksOnSelectedUserStory(string task)
    {
        UserStoryManager.Instance.AddTaskToUserStory(userStoryDropDown.captionText.text, task);
    }

}
