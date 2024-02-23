using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public List<string> Hobbies => _hobbies;
    public List<string> Qualities => _qualities;
    public List<string> Antipathies => _antipathies;
    public bool ISInterviewed => _iSInterviewed;
    public string Description => _description;
    [SerializeField] private List<string> _hobbies;
    [SerializeField] private List<string> _qualities;
    [SerializeField] private List<string> _antipathies;
    [SerializeField] private string _description;
    private bool _iSInterviewed = false;

    public void Interview()
    {
        _iSInterviewed = true;
    }

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

        for (int i = 0; i < _antipathies.Count; i++)
        {
            _antipathies[i] = _antipathies[i].ToLower().Trim();
        }
    }
}
