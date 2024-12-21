using UnityEngine;

public class CoinCounter : MonoBehaviour
{
    private int player1CoinCount = 0;
    private int player2CoinCount = 0;
    public int Player1CoinCount => player1CoinCount;
    public int Player2CoinCount => player2CoinCount;

    public void IncrementCoinCount(int player)
    {
        if (player == 1)
        {
            player1CoinCount++;
        }
        else if (player == 2)
        {
            player2CoinCount++;
        }
        
        Debug.Log($"Player 1 Coins: {player1CoinCount}, Player 2 Coins: {player2CoinCount}");
    }
}
