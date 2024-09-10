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

    public Button.ButtonClickedEvent StartNewGameClicked => startNextButton.onClick;
    
    public void Draw(EndWindowType type, int playerCount, int dealerCount)
    {
        gameObject.SetActive(true);
        resultInfoTM.text = GetResultText(type);
        playerCountTM.text = "Player: " + playerCount;
        dealerCountTM.text = "Dealer: " + dealerCount;
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