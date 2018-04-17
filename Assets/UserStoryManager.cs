using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System.Text.RegularExpressions;

public class UserStoryManager {

    public static UserStoryManager Instance { get { return Nested.instance; } }
    private class Nested
    {
        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static Nested()
        {
        }

        internal static readonly UserStoryManager instance = new UserStoryManager();
    }

    public class UserStory
    {
        public UserStory(string _userStory)
        {
            userStory = _userStory;
            tasks = new List<string>();
        }
        public string userStory;
        public List<string> tasks;
    }

    List<UserStory> userStories;
    List<string> tasks;
    List<string> personas;

	// Use this for initialization
	UserStoryManager () {
        userStories = new List<UserStory>();
        tasks = new List<string>();
        personas = new List<string>();
	}

    public void AddUserStory(string userStory)
    {
        userStories.Add(new UserStory(userStory));
    }
    public void AddTask(string task)
    {
        tasks.Add(task);
    }
    public void AddTask(string targetUserStory, string task)
    {
        AddTask(task);
        foreach (UserStory userStory in userStories)
        {
            if (targetUserStory == userStory.userStory)
                userStory.tasks.Add(task);
        }
    }
    public void AddPersona(string persona)
    {
        if(!personas.Contains(persona))
            personas.Add(persona);
    }
    public void AddTaskToUserStory(string targetUserStory, string task)
    {
        foreach (UserStory userStory in userStories)
        {
            if (targetUserStory == userStory.userStory)
                userStory.tasks.Add(task);
        }
    }
    public void RemoveTaskFromUserStory(string targetUserStory, string task)
    {
        foreach (UserStory userStory in userStories)
        {
            if (targetUserStory == userStory.userStory)
                userStory.tasks.Remove(task);
        }
    }
    public void RemoveTask(string task)
    {
        foreach (UserStory userStory in userStories)
        {
            userStory.tasks.Remove(task);
        }
        tasks.Remove(task);
    }

    public List<string> GetSimilarUserStories(string userStoryToCompare, float minScore)
    {
        List<string> similarUserStories = new List<string>();
        foreach (UserStory userStory in userStories)
        {
            WordsMatching.MatchsMaker match = new WordsMatching.MatchsMaker(userStoryToCompare, userStory.userStory);
            if (match.Score >= minScore)
                similarUserStories.Add(userStory.userStory);
        }
        return similarUserStories;
    }
    public List<string> GetSimilarTasks(string taskToCompare, float minScore)
    {
        List<string> similarTasks = new List<string>();
        foreach (UserStory userStory in userStories)
        {
            foreach (string task in userStory.tasks)
            {
                WordsMatching.MatchsMaker match = new WordsMatching.MatchsMaker(taskToCompare, task);
                if (match.Score >= minScore)
                    similarTasks.Add(task);
            }
        }
        return similarTasks;
    }

    public List<string> GetUserStories()
    {
        List<string> userStoryStringList = new List<string>();
        foreach (UserStory userStory in userStories)
        {
            userStoryStringList.Add(userStory.userStory);
        }
        return userStoryStringList;
    }
    public string[] GetTasks()
    {
        return tasks.ToArray();
    }
    public string[] GetPersonas()
    {
        return personas.ToArray();
    }
    public List<string> GetSpecificTasks(string targetUserStory)
    {
        List<string> taskStringList = new List<string>();
        foreach (UserStory userStory in userStories)
            if(userStory.userStory == targetUserStory)
            foreach (string task in userStory.tasks)
                taskStringList.Add(task);

        return taskStringList;
    }

    public void Clear()
    {
        userStories.Clear();
        tasks.Clear();
    }

    public List<string> CreateSaveList()
    {
        List<string> saveList = new List<string>();
        foreach(UserStory userStory in userStories)
        {
            saveList.Add(userStory.userStory);
            foreach(string task in userStory.tasks)
            {
                saveList.Add("-" + task);
            }
        }
        saveList.Add("+Tasks: ");
        foreach(string task in tasks)
        {
            saveList.Add(task);
        }
        return saveList;
    }

}
