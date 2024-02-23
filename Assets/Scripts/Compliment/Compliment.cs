using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compliment : MonoBehaviour
{
    public List<string> Hobbies => _hobbies;
    public List<string> Qualities => _qualities;
    public string ComplimentValue
    {
        get => _complimentValue;
        set => _complimentValue = value;
    }
    [SerializeField] private List<string> _hobbies;
    [SerializeField] private List<string> _qualities;
    [SerializeField] private string _complimentValue;

    private void Awake() 
    {
        for (int i = 0; i < _hobbies.Count; i++)
        {
            _hobbies[i] = _hobbies[i].ToLower().Trim();
        }

        for (int i = 0; i < _qualities.Count; i++)
        {
            _qualities[i] = _qualities[i].ToLower().Trim();
        }
    }
}
