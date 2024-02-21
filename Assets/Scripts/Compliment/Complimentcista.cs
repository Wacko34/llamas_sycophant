using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Complimentcista : MonoBehaviour
{
    [SerializeField] private List<Entity> _entities;
    [SerializeField] private List<Compliment> _compliments;

    // Событие на комплимент
    public void SayComplimentToGroup()
    {
        int[] grades = new int[_compliments.Count];

        for (int i = 0; i < _compliments.Count; i++)
        {
            grades[i] = 0;
        }
        
        int c = 0;
        foreach (Compliment compliment in _compliments)
        {
            foreach (Entity entity in _entities)
            {
                // TODO: Привести комплименты в нижний регистр, удалить лишние пробелы
                bool isHobbyIntersect = compliment.Hobbies.Intersect(entity.Hobbies).Any();
                bool isQualityIntersect = compliment.Qualities.Intersect(entity.Qualities).Any();
                bool isAntipathyIntersect = compliment.Antipathies.Intersect(entity.Antipathies).Any();

                if (isHobbyIntersect && isQualityIntersect && !isAntipathyIntersect)
                {
                    grades[c]++;
                }
            }
            c++;
        }
        // TODO: Убрать дебаги
        int maxIndex = grades.ToList().IndexOf(grades.Max());
        foreach (int grade in grades)
        {
            Debug.Log(grade);
        }

        Debug.Log($"maxIndex is {maxIndex}");
        Debug.Log(_compliments[maxIndex].SayCompliment());
    }

    private void Awake() 
    {
        SayComplimentToGroup();
    }
}