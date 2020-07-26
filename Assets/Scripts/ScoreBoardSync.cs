using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Normal.Realtime;

public class ScoreBoardSync : RealtimeComponent
{
    private ScoreboardModel _model;
    private RealtimeAvatarManager _avatarManager;
    private TextMeshProUGUI _textMeshPro;

    private ScoreboardModel model
    {
        set
        {
            if(_model != null)
            {
                _model.scoreTextDidChange -= ScoreChanged;
                
            }

            _model = value;

            if(_model != null)
            {
                UpdateDisplay();

                _model.scoreTextDidChange += ScoreChanged;
                //_model.playernameDidChange += NameChanged;
            }
        }
    }


    void Awake()
    {
        _avatarManager = FindObjectOfType<RealtimeAvatarManager>();
        _textMeshPro = GameObject.Find("List").GetComponent<TextMeshProUGUI>();
    }

    void OnEnable()
    {
        _avatarManager.avatarCreated += AvatarChanged;
        _avatarManager.avatarDestroyed += AvatarChanged;
    }

    void AvatarChanged(RealtimeAvatarManager avatarManager, RealtimeAvatar avatar, bool isLocalAvatar )
    {
        SetDisplay();
    }

    private void ScoreChanged(ScoreboardModel model, string value)
    {
       UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        _textMeshPro.SetText(_model.scoreText);
    }

    private void SetDisplay()
    {        
        _model.scoreText = "";

        foreach (var avatar in _avatarManager.avatars)
        {
            var player = avatar.Value.gameObject.GetComponent<PlayerSync>();
            _model.scoreText += player.GetName() + " : " + player.GetScore() +"\n";
        }
    }

    public void SetScoreForPlayer(int clientId, int score)
    {
        _avatarManager.avatars[clientId].gameObject.GetComponent<PlayerSync>().SetScore(score);
        SetDisplay();
    }


   
}
