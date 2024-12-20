using System.Collections;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private int[,] board = new int[6, 7]; // 6 rows, 7 columns
    private int currentPlayer = 1; // Player 1 starts

    public GameObject redTokenPrefab;
    public GameObject blueTokenPrefab;

    public GameObject resultUI;

    public float bounceHeight = 0.2f; // Height of the bounce
    public float bounceTime = 0.1f; // Duration of the bounce

    public bool gameOver = false; // Flag to check if the game is over

    public TextMeshProUGUI winText;
    public AudioSource coinDrop;
    public AudioSource Win;
    public AudioSource Bass;
    public AudioSource Instrument;
    public AudioSource Melody;

    public CanvasGroup FadeInCanvasGroup;
    public float fadeDuration = 1f; 
    public int player1CoinCount = 0;
    public int player2CoinCount = 0;
    public int totalCoins = 0;

    private void Awake()
    {
        Instance = this;
    }

    public void OnColumnSelected(int column)
    {
        if(totalCoins == 6)
        {
             StartCoroutine(FadeInCoroutine(Bass));
        }
        if(totalCoins == 15)
        {
             StartCoroutine(FadeInCoroutine(Instrument));
        }
        if(totalCoins == 30)
        {
             StartCoroutine(FadeInCoroutine(Melody));
        }
        //Debug.Log($"Column {column} selected");
        if (gameOver) 
            Debug.Log("Game over!");
        // Drop the token into the column
        else if (DropToken(column, currentPlayer))
        {
            if (currentPlayer == 1)
            {
                player1CoinCount++;
                totalCoins++;
            }
            else if (currentPlayer == 2)
            {
                player2CoinCount++;
                totalCoins++;
            }

            Debug.Log($"\nPlayer 1 Coins: {player1CoinCount}, \nPlayer 2 Coins: {player2CoinCount}\nPlayer {currentPlayer}'s turn is over.");
            // Check for win or draw
            if (CheckWin(currentPlayer))
            {
                Win.Play();
                gameOver = true;
                resultUI.SetActive(true);
                StartCoroutine(FadeCanvasGroup(0f, 1f));
                winText.text = $"Player {currentPlayer} wins!";
                Debug.Log($"Player {currentPlayer} wins!");
            }
            else if (CheckDraw())
            {
                Win.Play();
                gameOver = true;
                resultUI.SetActive(true);
                StartCoroutine(FadeCanvasGroup(0f, 1f));
                winText.text = $"Player {currentPlayer} wins!";
                Debug.Log("It's a draw!");
            }
            else
            {
                // Switch to the next player
                currentPlayer = (currentPlayer == 1) ? 2 : 1;
            }
        }
    }

    private bool DropToken(int column, int player)
    {
        // Start from the bottom row and go upwards
        for (int row = 5; row >= 0; row--)
        {
            if (board[row, column] == 0) // Check if the cell is empty
            {
                if (CheckWin(player))
                {
                    // Trigger
                }
                else if (CheckDraw())
                {
                    // Trigger
                }
                else
                {
                    //Debug.Log($"Player {player}'s turn is over.");
                    // Switch to the next player
                }
            
                board[row, column] = player; // Place the token in the board array
                VisualizeToken(row, column, player); // Visualize the token
                //Debug.Log($"Token placed at Row {row}, Column {column}");
                return true; // Token successfully placed
            }
        }

        Debug.Log("Column is full!"); // All rows in this column are occupied
        return false; // Column is full
    }


    bool CheckWin(int player)
    {
        // Check horizontal
        for (int row = 0; row < 6; row++)
        {
            for (int col = 0; col < 4; col++) // Only need to check till column 3
            {
                if (board[row, col] == player &&
                    board[row, col + 1] == player &&
                    board[row, col + 2] == player &&
                    board[row, col + 3] == player)
                {
                    return true;
                }
            }
        }

        // Check vertical
        for (int row = 0; row < 3; row++) // Only need to check till row 2
        {
            for (int col = 0; col < 7; col++)
            {
                if (board[row, col] == player &&
                    board[row + 1, col] == player &&
                    board[row + 2, col] == player &&
                    board[row + 3, col] == player)
                {
                    return true;
                }
            }
        }

        // Check diagonal (bottom-left to top-right)
        for (int row = 0; row < 3; row++)
        {
            for (int col = 0; col < 4; col++)
            {
                if (board[row, col] == player &&
                    board[row + 1, col + 1] == player &&
                    board[row + 2, col + 2] == player &&
                    board[row + 3, col + 3] == player)
                {
                    return true;
                }
            }
        }

        // Check diagonal (top-left to bottom-right)
        for (int row = 3; row < 6; row++)
        {
            for (int col = 0; col < 4; col++)
            {
                if (board[row, col] == player &&
                    board[row - 1, col + 1] == player &&
                    board[row - 2, col + 2] == player &&
                    board[row - 3, col + 3] == player)
                {
                    return true;
                }
            }
        }

        return false;
    }


    private bool CheckDraw()
    {
        // Check if the board is full
        foreach (int cell in board)
        {
            if (cell == 0) return false;
        }
        return true;
    }

        private void VisualizeToken(int row, int column, int player)
    {
        GameObject tokenPrefab = (player == 1) ? redTokenPrefab : blueTokenPrefab;

        // Grid starting position
        Vector3 gridStart = new Vector3(-4f, 3.5f, 0);

        // Tile size/spacing (adjust for your tiles)
        float tileSpacing = 1.375f;

        // Calculate the final position based on row and column
        Vector3 finalPosition = gridStart + new Vector3(column * tileSpacing, -row * tileSpacing, 0);

        // Instantiate the token above the starting position (for the animation)
        Vector3 startPosition = new Vector3(finalPosition.x, 3.5f, 0); // Start above the final position

        // Instantiate the token at the starting position
        GameObject token = Instantiate(tokenPrefab, startPosition, Quaternion.identity);
        token.transform.SetParent(transform, true); 

        // Start the animation to move the token to its final position
        StartCoroutine(DropTokenAnimation(token, finalPosition));
    }


    private IEnumerator DropTokenAnimation(GameObject token, Vector3 finalPosition)
    {
        float timeToFall = 1.0f; // Time to fall
        float elapsedTime = 0f;

        Vector3 startPosition = new Vector3(finalPosition.x, 3.5f, 0); // Start above the final position

        // Move the token down smoothly
        while (elapsedTime < timeToFall)
        {
            float t = Mathf.SmoothStep(0f, 1f, elapsedTime / timeToFall); // Eased smooth step
            token.transform.position = Vector3.Lerp(startPosition, finalPosition, t);
            elapsedTime += Time.deltaTime; // Increment time
            yield return null;
        }

        // Ensure the token ends exactly at the final position
        token.transform.position = finalPosition;

        // Add a small bounce effect at the end
        Vector3 bouncePosition = finalPosition + new Vector3(0, bounceHeight, 0); // Use bounceHeight from Inspector
        elapsedTime = 0f;

        while (elapsedTime < bounceTime)
        {
            float t = Mathf.SmoothStep(0f, 1f, elapsedTime / bounceTime); // Eased bounce
            token.transform.position = Vector3.Lerp(finalPosition, bouncePosition, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Return the token to its final resting position
        token.transform.position = finalPosition;
        coinDrop.Play();
        }
    private System.Collections.IEnumerator FadeCanvasGroup(float startAlpha, float targetAlpha)
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / fadeDuration);
            FadeInCanvasGroup.alpha = alpha;
            yield return null;
        }

        FadeInCanvasGroup.alpha = targetAlpha;
    }

    private IEnumerator FadeInCoroutine(AudioSource audioSource)
    {
        float startVolume = audioSource.volume;
        float timeElapsed = 0f;
        float targetVolume = 0.5f;

        while (timeElapsed < fadeDuration)
        {
            timeElapsed += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startVolume, targetVolume, timeElapsed / fadeDuration);
            yield return null; // Wait until the next frame
        }

        audioSource.volume = targetVolume; // Ensure it reaches the target volume
    }
}
