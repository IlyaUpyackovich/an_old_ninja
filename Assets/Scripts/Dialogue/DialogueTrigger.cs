using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public bool isTrigger;
    public Dialogue dialogue;

    private void Start()
    {
        BoxCollider2D collider;
        isTrigger = TryGetComponent<BoxCollider2D>(out collider) && collider.isTrigger;
    }

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isTrigger && collision.tag == "Player")
        {
            TriggerDialogue();
            Destroy(gameObject);
        }
    }
}
