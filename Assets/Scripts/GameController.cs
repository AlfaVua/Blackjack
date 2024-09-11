using Cards;
using Cards.Hand;
using Dealer;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private CardGenerator generator;
    [SerializeField] private DealerHandler dealer;
    [SerializeField] private Sprite blackBack;
    [SerializeField] private Sprite whiteBack;

    [SerializeField] private Button hitButton;
    [SerializeField] private Button standButton;

    [SerializeField] private PlayerHand playerHand;
    [SerializeField] private DealerHand dealerHand;

    [SerializeField] private EndWindowUI endWindow;
    public static GameController Instance { get; private set; }
    
    private void Awake()
    {
        DOTween.Init();
        Instance = this;
    }

    private void Start()
    {
        generator.InitCardData();
    }

    public void StartNewGame()
    {
        endWindow.Hide();
        dealer.Init(generator.CardList, playerHand, dealerHand);
    }

    public void PlayerValueChanged(int newValue)
    {
        if (newValue > 21)
        {
            dealer.ShowCards();
            PlayerLost();
        }
    }

    public static Sprite GetCardBack(bool isWhite)
    {
        return isWhite ? Instance.whiteBack : Instance.blackBack;
    }

    private void OnEnable()
    {
        hitButton.onClick.AddListener(dealer.AddPlayerCard);
        standButton.onClick.AddListener(dealer.OnPlayerStand);
        endWindow.StartNewGameClicked.AddListener(StartNewGame);
    }

    private void OnDisable()
    {
        hitButton.onClick.RemoveListener(dealer.AddPlayerCard);
        standButton.onClick.RemoveListener(dealer.OnPlayerStand);
        endWindow.StartNewGameClicked.RemoveListener(StartNewGame);
    }

    public void PlayerLost()
    {
        endWindow.Draw(EndWindowType.PLAYER_LOST, playerHand.CurrentValue, dealerHand.CurrentValue);
    }

    public void PlayerWon()
    {
        endWindow.Draw(EndWindowType.PLAYER_WON, playerHand.CurrentValue, dealerHand.CurrentValue);
    }

    public void GameTie()
    {
        endWindow.Draw(EndWindowType.TIE, playerHand.CurrentValue, dealerHand.CurrentValue);
    }
}