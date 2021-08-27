using UnityEngine;

public class ChooseTable : MonoBehaviour
{

    public GameObject pointer;
    public DynamicTable Table;
    public Referee Ref;

    float TableSize=0;
    string GameType=null;

    public void Choose(float size)
    {
        TableSize = size;
        Debug.Log(size);
    }

    public void Game(string game)
    {
        GameType = game;
        Debug.Log(game);
    }

    public void Continue()
    {
        Debug.Log("Continue");
        Ref.MakeGame(GameType, TableSize);
        //pointer.SetActive(false);
        gameObject.SetActive(false);
    }
}
