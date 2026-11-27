using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    private NPCDialogue currentNPC;

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;

        // Nếu đang hội thoại thì đóng
        if (DialogueManager.Instance.IsTalking)
        {
            DialogueManager.Instance.EndDialogue();
            return;
        }

        // Nếu không đứng gần NPC nào
        if (currentNPC == null)
            return;

        // Mở hội thoại
        DialogueManager.Instance.StartDialogue(currentNPC.Dialogue);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out NPCDialogue npc))
        {
            currentNPC = npc;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out NPCDialogue npc)
            && npc == currentNPC)
        {
            currentNPC = null;
        }
    }
}