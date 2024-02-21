using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compliment : MonoBehaviour
{
    public List<string> Hobbies => _hobbies;
    public List<string> Qualities => _qualities;
    public List<string> Antipathies => _antipathies;
    public string ComplimentValue => _complimentValue;
    [SerializeField] private List<string> _hobbies;
    [SerializeField] private List<string> _qualities;
    [SerializeField] private List<string> _antipathies;
    [SerializeField] private string _complimentValue;

    public string SayCompliment()
    {
        return _complimentValue;
    }
}
