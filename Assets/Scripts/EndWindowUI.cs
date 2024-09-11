using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum EndWindowType
{
    PLAYER_WON,
    PLAYER_LOST,
    TIE,
}
public class EndWindowUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dealerCountTM;
    [SerializeField] private TextMeshProUGUI playerCountTM;
    [SerializeField] private TextMeshProUGUI resultInfoTM;
    [SerializeField] private Button startNextButton;
    [SerializeField] private Transform mainContainer;

    public Button.ButtonClickedEvent StartNewGameClicked => startNextButton.onClick;

    private void Awake()
    {
        startNextButton.animator.keepAnimatorStateOnDisable = false;
    }

    public void Draw(EndWindowType type, int playerCount, int dealerCount)
    {
        Show();
        resultInfoTM.text = GetResultText(type);
        playerCountTM.text = "Player: " + playerCount;
        dealerCountTM.text = "Dealer: " + dealerCount;
    }

    private void Show()
    {
        mainContainer.DOScale(Vector3.one, .3f);
    }

    public void Hide()
    {
        mainContainer.DOScale(Vector3.zero, .3f);
    }

    private string GetResultText(EndWindowType type)
    {
        return type switch
        {
            EndWindowType.PLAYER_LOST => "Player has lost",
            EndWindowType.PLAYER_WON => "Player has won",
            _ => "TIE"
        };
    }
}