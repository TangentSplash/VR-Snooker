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
    }

    public void Game(string game)
    {
        GameType = game;
    }

    public void Continue()
    {
        Ref.MakeGame(GameType, TableSize);
        pointer.SetActive(false);
        gameObject.SetActive(false);
    }
}
