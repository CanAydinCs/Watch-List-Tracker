using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MSeasons
{
    public string name;

    public List<string> episodes;

    public bool isFinished;

    public MSeasons(string _name, List<string> _episodes, bool _isF) {
        name = _name;
        episodes = _episodes;
        isFinished = _isF;
    }   
}
