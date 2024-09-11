using System.Collections.Generic;
using Cards;
using Cards.Hand;
using Dealer;
using DG.Tweening;
using UnityEditor;
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
    [SerializeField] private LastGameData gameData;
    public static GameController Instance { get; private set; }

    private bool _gameIsComplete;
    
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
        dealer.Init(new List<CardData>(generator.CardList), playerHand, dealerHand);
        _gameIsComplete = false;
        if (gameData.HaveData) gameData.Load(dealer);
        else dealer.InitStartingCards();
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
        gameData.Clear();
        _gameIsComplete = true;
    }

    public void PlayerWon()
    {
        endWindow.Draw(EndWindowType.PLAYER_WON, playerHand.CurrentValue, dealerHand.CurrentValue);
        gameData.Clear();
        _gameIsComplete = true;
    }

    public void GameTie()
    {
        endWindow.Draw(EndWindowType.TIE, playerHand.CurrentValue, dealerHand.CurrentValue);
        gameData.Clear();
        _gameIsComplete = true;
    }

    private void OnApplicationQuit()
    {
        if (_gameIsComplete) return;
        gameData.Save(playerHand.CardIds, dealerHand.CardIds);
    }
}