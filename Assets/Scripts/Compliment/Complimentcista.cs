using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Complimentcista : MonoBehaviour
{
    [SerializeField] private List<Entity> _entities;
    [SerializeField] private List<Compliment> _compliments;
    [SerializeField] private string _defaultComplimet; 

    // Событие на комплимент
    public void SayComplimentToGroup()
    {
        int[] grades = new int[_compliments.Count];
        int complimentNumber = 0;

        Compliment resultCompliment = gameObject.AddComponent<Compliment>();
        resultCompliment.ComplimentValue = "";

        for (int i = 0; i < _compliments.Count; i++)
        {
            grades[i] = 0;
        }
        
        
        foreach (Compliment compliment in _compliments)
        {
            foreach (Entity entity in _entities)
            {
                if (entity.Antipathies.Intersect(compliment.Hobbies).Any()
                    || entity.Antipathies.Intersect(compliment.Qualities).Any()
                )
                {
                    grades[complimentNumber] = 0;
                    break;
                }

                if (compliment.Hobbies.Intersect(entity.Hobbies).Any()
                    && compliment.Qualities.Intersect(entity.Qualities).Any()
                )
                {
                    grades[complimentNumber]++;
                }
            }
            complimentNumber++;
        }

        for (int i = 0; i < _compliments.Count; i++)
        {
            if (grades[i] > 0)
            {
                resultCompliment.ComplimentValue += $" {_compliments[i].ComplimentValue}";
            }
        }

        if (resultCompliment.ComplimentValue != "")
        {
            Debug.Log(resultCompliment.ComplimentValue);
        }
        else 
        {
            Debug.Log(_defaultComplimet);
        }
    }

    private void Awake() 
    {
        SayComplimentToGroup();
    }
}