using TMPro;
using UnityEngine;

public class HandleBoostAndCoin : MonoBehaviour
{
    [SerializeField] private GameProgression gameProgression;
    [SerializeField] private TextMeshProUGUI coinText;

    public void ConsumeMagnet()
    {
        if(gameProgression.NumberOfMagnet > 0)
        {
            gameProgression.NumberOfMagnet -= 1;
            GameManagers.Instance.OnMagnet();
        }
    }
    public void ConsumeAddtray()
    {
        if(gameProgression.NumberOfAddTray > 0)
        {
            gameProgression.NumberOfAddTray -= 1;
            GameManagers.Instance.OnAddMoreGrill();
        }
    }
    public void ConsumeSwap()
    {
        if(gameProgression.NumberOfSwap > 0)
        {
            gameProgression.NumberOfSwap -= 1;
            GameManagers.Instance.OnShuffle();
        }
    }
    public void UpdateCoinText(int morecoin)
    {
        gameProgression.CurrentGold += morecoin;
        coinText.text = "" + gameProgression.CurrentGold;
    }
}
