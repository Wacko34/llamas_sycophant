using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public List<string> Hobbies => _hobbies;
    public List<string> Qualities => _qualities;
    public List<string> Antipathies => _antipathies;
    [SerializeField] private List<string> _hobbies; // Хобби, Род деятельности
    [SerializeField] private List<string> _qualities; // Качества
    [SerializeField] private List<string> _antipathies; // Антипатии
}
