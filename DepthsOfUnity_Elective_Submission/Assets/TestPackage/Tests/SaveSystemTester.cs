using System.Collections.Generic;
using UnityEngine;
using SimpleSaveSystem;

public class SaveSystemTester : MonoBehaviour
{
    private bool gameRunning = false;

    private void Start()
    {
        gameRunning = true;
    }

    [Button]
    private void ChangeMasterVolumeSettings()
    {
        if (!gameRunning)
        {
            Debug.LogWarning("Game is not running.");
            return;
        }

        SaveObject saveObject = SaveSystem.GetSaveObject("Options");
        if (saveObject != null)
        {
            saveObject.SetValue("MasterVolume", Random.Range(0f, 1f));
            saveObject.Save();
        }
    }

    [Button]
    private void PrintLukasInfo()
    {
        if (!gameRunning)
        {
            Debug.LogWarning("Game is not running.");
            return;
        }

        SaveObject saveObject = SaveSystem.GetSaveObject("LukasNPC");
        if (saveObject == null)
        {
            return;
        }

        // ! Cant check if int == null!
        int age = (int)saveObject.GetValue("Age");
        Debug.Log("Lukas is " + age + " years old.");

        List<string> dialogueListNoFight = (List<string>)saveObject.GetValue("DialoguesNoFight");
        if (dialogueListNoFight != null)
        {
            string printText = "Dialogues while not in a fight: \n";

            for (int i = 0; i < dialogueListNoFight.Count; i++)
            {
                printText += dialogueListNoFight[i] + "\n";
            }

            Debug.Log(printText);
        }
        List<string> dialogueListFight = (List<string>)saveObject.GetValue("DialoguesFight");
        if (dialogueListFight != null)
        {
            string printText = "Dialogues while in a fight: \n";

            for (int i = 0; i < dialogueListFight.Count; i++)
            {
                printText += dialogueListFight[i] + "\n";
            }
            
            Debug.Log(printText);
        }
    }
}
