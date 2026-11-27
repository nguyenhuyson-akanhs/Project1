using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Subtegral.DialogueSystem.DataContainers;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    [Header("UI")]
    [SerializeField] private GameObject panel;
    [SerializeField] private TMP_Text dialogueText;

    [Header("Choices")]
    [SerializeField] private Transform choiceContainer;
    [SerializeField] private Button choiceButtonPrefab;

    private DialogueContainer currentDialogue;
    private string currentNodeGuid;

    public bool IsTalking { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        panel.SetActive(false);
    }

    public void StartDialogue(DialogueContainer dialogue)
    {
        currentDialogue = dialogue;

        var firstLink = currentDialogue.NodeLinks.First();

        currentNodeGuid = firstLink.TargetNodeGUID;

        IsTalking = true;

        panel.SetActive(true);

        ShowCurrentNode();
    }

    private void ShowCurrentNode()
    {
        ClearChoices();

        var node = currentDialogue.DialogueNodeData
            .FirstOrDefault(x => x.NodeGUID == currentNodeGuid);

        if (node == null)
        {
            EndDialogue();
            return;
        }

        dialogueText.text = node.DialogueText;

        var choices = currentDialogue.NodeLinks
            .Where(x => x.BaseNodeGUID == currentNodeGuid)
            .ToList();

        // Node cuối
        if (choices.Count == 0)
        {
            return;
        }

        foreach (var choice in choices)
        {
            Button button =
                Instantiate(choiceButtonPrefab, choiceContainer);

            button.GetComponentInChildren<TMP_Text>().text =
                choice.PortName;

            string nextGuid = choice.TargetNodeGUID;

            button.onClick.AddListener(() =>
            {
                currentNodeGuid = nextGuid;
                ShowCurrentNode();
            });
        }
    }

    private void ClearChoices()
    {
        foreach (Transform child in choiceContainer)
        {
            Destroy(child.gameObject);
        }
    }

    public void EndDialogue()
    {
        IsTalking = false;

        currentDialogue = null;
        currentNodeGuid = string.Empty;

        ClearChoices();

        panel.SetActive(false);
    }
}