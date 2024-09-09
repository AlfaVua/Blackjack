using System.Collections;
using Cards;
using Dealer;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private CardGenerator generator;
    [SerializeField] private DealerHandler dealer;
    [SerializeField] private Sprite blackBack;
    [SerializeField] private Sprite whiteBack;

    [SerializeField] private Button hitButton;
    [SerializeField] private Button standButton;
    public static GameController Instance { get; private set; }
    
    private void Start()
    {
        DOTween.Init();
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
    }

    private void OnDisable()
    {
        hitButton.onClick.RemoveListener(dealer.AddPlayerCard);
        standButton.onClick.RemoveListener(dealer.OnPlayerStand);
    }

    public void PlayerLost()
    {
        Debug.Log("Player Lost");
        StartCoroutine(nameof(DelayedInit));
    }

    public void PlayerWon()
    {
        Debug.Log("Player Won");
        StartCoroutine(nameof(DelayedInit));
    }

    public void GameTie()
    {
        Debug.Log("Tie");
        StartCoroutine(nameof(DelayedInit));
    }

    private IEnumerator DelayedInit() // Заглушка
    {
        yield return new WaitForSeconds(1.5f);
        dealer.Init(generator.CardList);
    }
}