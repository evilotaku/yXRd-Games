using UnityEngine;
using Normal.Realtime;
using TMPro;

public class GameManager : MonoBehaviour
{
    private Realtime _realtime;
    private RealtimeAvatarManager _avatarManager;
    public string roomName, users;

    public TextMeshProUGUI userList;
    

     void Awake()
    {
        _realtime = FindObjectOfType<Realtime>();  
        _avatarManager = FindObjectOfType<RealtimeAvatarManager>();      
    }

    void OnEnable()
    {
        _avatarManager.avatarCreated += AvatarChanged;
        _avatarManager.avatarDestroyed += AvatarChanged;
        _realtime.didConnectToRoom += Connected;
                
    }

    void AvatarChanged(RealtimeAvatarManager avatarManager, RealtimeAvatar avatar, bool isLocalAvatar )
    {
        SetDisplay();
    }

    public void SetRoomName(string room)
    {
        roomName = room;
    }

    public void Connect()
    {
        _realtime.Disconnect();
        _realtime.Connect(roomName);
    }

    void SetDisplay()
    {        
        users = "";

        foreach (var avatar in _avatarManager.avatars)
        {
            var player = avatar.Value.gameObject.GetComponent<PlayerSync>();
            users += player.GetName() + "\n";
            userList.SetText(users);
        }
    }

    public void SetPlayerName(string name)
    {
        PlayerPrefs.SetString("Name", name);                
    }

    void Connected(Realtime room)
    {        
        SetPlayerName(PlayerPrefs.GetString("Name", "Player"));        
    }
}
