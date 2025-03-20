using TMPro;
using UnityEngine;

public class UiLogin : MonoBehaviour
{
    [SerializeField] private WindowsBase windows;
    [SerializeField] private TMP_InputField input_nick;
    
    private string nickName;

    internal void Show()
    {
        windows.ShowWindows();
        input_nick.text = RandomNick;
    }


    public void Applay()
    {
        if(string.IsNullOrWhiteSpace(input_nick.text))
        {
            input_nick.text = RandomNick;
        }
        nickName = input_nick.text;
        windows.HideWindows();
        Lobby.instance.Connect();
    }


    private string RandomNick
    {
        get
        {
            return "Player#" + Random.Range(1111, 9999);
        }
    }
    internal string GetCurrentNick
    {
        get
        {
            return nickName;
        }
    }
}
