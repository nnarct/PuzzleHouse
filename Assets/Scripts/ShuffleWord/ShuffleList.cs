using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShuffleList
{
    // A method to shuffle items in a list
    public static List<E> ShuffleListItems<E>(List<E> inputList)
    {
        // Create a copy of the input list
        List<E> originalList = new List<E>();
        originalList.AddRange(inputList);

        // Create a new list to store shuffled items
        List<E> randomList = new List<E>();

        // Create a random number generator
        System.Random r = new System.Random();
        int randomIndex = 0;

        // Shuffle the list by picking random elements from the original list
        while (originalList.Count > 0)
        {
            //Choose a random object in the list
            randomIndex = r.Next(0, originalList.Count);

            //add it to the new, random list
            randomList.Add(originalList[randomIndex]);

            //remove to avoid duplicates
            originalList.RemoveAt(randomIndex);
        }

        // Return the new randomly shuffled list
        return randomList;
    }
}