using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class SaveSystemSampleTester : MonoBehaviour
{
    private bool gameRunning = false;

    private void Start()
    {
        gameRunning = true;
    }

    [Button]
    private void ChangeMasterVolume()
    {
        if (!gameRunning)
        {
            Debug.LogWarning("Game is not running.");
            return;
        }

        // Retrieving the SaveObject of Options
        SaveObject saveObject = SaveSystem.GetSaveObject("Options");

        // Override the MasterVolume value
        saveObject.SetValue("MasterVolume", Random.Range(0f, 1f));

        // Save isnt needed, because the SetValue method already saves the SaveObject.
        // Auto-Save can be disabled in the SetValue-Parameters.
        //saveObject.Save(); 
    }

    [Button]
    private void PrintLukasNPCInfo()
    {
        if (!gameRunning)
        {
            Debug.LogWarning("Game is not running.");
            return;
        }

        // Retrieving the SaveObject of LukasNPC
        SaveObject saveObject = SaveSystem.GetSaveObject("LukasNPC");

        // Retrieving single Values
        int age = (int)saveObject.GetValue("Age");
        Debug.Log("Lukas is " + age + " years old.");

        // Retrieving values from Lists
        List<float> position = (List<float>)saveObject.GetValue("Position");
        Debug.Log("Lukas is at position: " + position[0] + " / " + position[1] + " / " + position[2]);

        List<string> dialogueListNoFight = (List<string>)saveObject.GetValue("DialoguesNoFight");
        string printText = "Dialogues while not in a fight: \n";
        for (int i = 0; i < dialogueListNoFight.Count; i++)
        {
            printText += dialogueListNoFight[i] + "\n";
        }
        Debug.Log(printText);
        
        List<string> dialogueListFight = (List<string>)saveObject.GetValue("DialoguesFight");
        printText = "Dialogues while in a fight: \n";
        for (int i = 0; i < dialogueListFight.Count; i++)
        {
            printText += dialogueListFight[i] + "\n";
        }            
        Debug.Log(printText);
    }

    [Button]
    private void ChangeLukasNPCPosition()
    {
        if (!gameRunning)
        {
            Debug.LogWarning("Game is not running.");
            return;
        }

        // Retrieving the SaveObject of Player
        SaveObject saveObject = SaveSystem.GetSaveObject("LukasNPC");

        // Either generate a brandnew List or retrieve and override the existing one (we will generate a new one)
        // We also have the freedom to make the list smaller or bigger here, but for position it doesnt make any sense.
        List<float> newPosition = new List<float> { Random.Range(-5f, 5f), Random.Range(-5f, 5f), Random.Range(-5f, 5f) };

        // Override the position value
        saveObject.SetValue("Position", newPosition);

        // Save isnt needed, because the SetValue method already saves the SaveObject.
        // Auto-Save can be disabled in the SetValue-Parameters.
        saveObject.Save();
    }
}


