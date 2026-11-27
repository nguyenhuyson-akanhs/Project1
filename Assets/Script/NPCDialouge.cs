using UnityEngine;
using Subtegral.DialogueSystem.DataContainers;

public class NPCDialogue : MonoBehaviour
{
    [SerializeField]
    private DialogueContainer dialogue;

    public DialogueContainer Dialogue => dialogue;
}