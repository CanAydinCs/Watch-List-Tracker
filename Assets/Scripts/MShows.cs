using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MShows 
{
    public int id;

    public string name;

    public List<MSeasons> seasons;

    public bool isFinished;

    public MShows(int _id, string _name, List<MSeasons> _seasons, bool _isF) {
        id = _id;
        name = _name; 
        seasons = _seasons;
        isFinished = _isF;
    }
}
