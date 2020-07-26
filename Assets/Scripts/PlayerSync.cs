using UnityEngine;
using TMPro;
using Normal.Realtime;

public class PlayerSync : RealtimeComponent
{
    private PlayerModel _model;
    public TextMeshProUGUI _textMeshPro;    

    private PlayerModel model
    {
        set
        {
            if(_model != null)
            {
                _model.scoreDidChange -= ScoreChanged;
                _model.playernameDidChange -= NameChanged;
            }

            _model = value;

            if(_model != null)
            {
                UpdateDisplay();

                _model.scoreDidChange += ScoreChanged;
                _model.playernameDidChange += NameChanged;
            }
        }
    }

    
    private void ScoreChanged(PlayerModel model, int value)
    {
       UpdateDisplay();
    }

    private void NameChanged(PlayerModel model, string value)
    {
        UpdateDisplay();
    }

    private void UpdateDisplay()
    {        
            _textMeshPro.SetText(_model.playername + "\n Score: " + _model.score);
    }

    public int GetScore()
    {
        return _model.score;
    }

    public string GetName()
    {
        if(realtimeView.isOwnedLocally)
        {        
            _model.playername = PlayerPrefs.GetString("Name", "Player");    
        }    
            return _model.playername;
    }

    public void SetScore(int score)
    {
        _model.score += score;
    }

    public void SetName(string username)
    {
        _model.playername = username;
    }
    
}
