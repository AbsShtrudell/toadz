using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PersonController : MonoBehaviour
{
    [SerializeField]
    private Person[] persons;

    public Person currentPerson
    { get; private set; }

    [SerializeField]
    private SpriteRenderer spriteRenderer;

    [Zenject.Inject]
    private DialogueController dialogueController;

    public event Action<Person> onPersonChanged;

    private void OnEnable()
    {
        dialogueController.onNodeChanged += OnNodeChanged;
        OnNodeChanged(dialogueController.currentNode);
    }

    private void OnDisable()
    {
        dialogueController.onNodeChanged -= OnNodeChanged;
    }

    private Person FindPerson(int id)
    {
        if (persons.Length == 0) return null;

        for (int i = 0; i < persons.Length; i++)
        {
            if (persons[i].id == id) return persons[i];
        }

        return persons[0];
    }

    private Sprite FindEmotionSprite(Emotions emotion)
    {
        if(currentPerson == null || currentPerson.emotions.Count == 0) return null;

        for(int i =0; i < currentPerson.emotions.Count; i++)
        {
            if (currentPerson.emotions[i].emotion == emotion) return currentPerson.emotions[i].sprite;
        }
        return currentPerson.emotions[0].sprite;
    }

    private void SetPerson(int id)
    {
        currentPerson = FindPerson(id);
        onPersonChanged?.Invoke(currentPerson);
    }

    private void SetEmotion(Emotions emotion)
    {
        spriteRenderer.sprite = FindEmotionSprite(emotion);
    }

    private void OnNodeChanged(DialogueNode node)
    {
        SetPerson(node.person.id);
        SetEmotion(node.person.emotionType);
    }
}
