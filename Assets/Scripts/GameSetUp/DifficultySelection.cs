using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultySelection : MonoBehaviour
{
    public static event EventHandler<Difficulty> OnSelectGameDifficulty;
    public class Difficulty : EventArgs
    {
        public int gameDifficulty;
    }

    public void HandleInput(int val)
    {
        if (val == 0)
        {
            OnSelectGameDifficulty?.Invoke(this, new Difficulty{gameDifficulty = 0});
        }
        if (val == 1)
        {
            OnSelectGameDifficulty?.Invoke(this, new Difficulty{gameDifficulty = 1});
        }
        if (val == 2)
        {
            OnSelectGameDifficulty?.Invoke(this, new Difficulty{gameDifficulty = 2});
        }
    }
}
