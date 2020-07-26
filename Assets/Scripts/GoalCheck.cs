using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Normal.Realtime;

public class GoalCheck : MonoBehaviour
{
    private ScoreBoardSync _scoreboard;
    private int ownerId = -1;

    public int score;
    public TextMeshProUGUI goalText;

    // Start is called before the first frame update
    void Start()
    {
        goalText.SetText(score.ToString());
        _scoreboard = FindObjectOfType<ScoreBoardSync>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Ball")
        {
            ownerId = other.gameObject.GetComponent<RealtimeTransform>().ownerID;
            _scoreboard.SetScoreForPlayer(ownerId, score);

            //other.gameObject.GetComponent<RealtimeTransform>().ClearOwnership();
        }
    }
}
