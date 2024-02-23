using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    [SerializeField] private float _distanceFromEntity = 2;
    public void StartDialogue(Vector3 position, string text)
    {
        gameObject.transform.position = new Vector3(position.x, position.y + _distanceFromEntity, position.z);
        GameObject textObj = gameObject.transform.Find("dialogueText").gameObject;
        TMP_Text dialogueText = textObj.GetComponent<TMP_Text>();
        dialogueText.text = text;
        gameObject.SetActive(true);
    }

    private void Awake()
    {
        gameObject.SetActive(false);
        MovementController.onDialogue += StartDialogue;
    }
}
