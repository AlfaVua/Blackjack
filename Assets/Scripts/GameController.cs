using Cards;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private CardGenerator generator;
    [SerializeField] private DealerHandler dealer;
    [SerializeField] private Sprite blackBack;
    [SerializeField] private Sprite whiteBack;
    public static GameController Instance { get; private set; }
    
    private void Start()
    {
        Instance = this;
        generator.InitCardData();
    }

    public void OnLoadComplete()
    {
        dealer.Init(generator.CardList);
    }

    public void PlayerValueChanged(int newValue)
    {
        if (newValue > 21)
        {
            InitLose();
        }
    }

    public void InitLose()
    {
        
    }

    public static Sprite GetCardBack(bool isWhite)
    {
        return isWhite ? Instance.whiteBack : Instance.blackBack;
    }
}