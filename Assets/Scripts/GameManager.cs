using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static APIHandler;
using System.Threading.Tasks;
using System;
using UnityEngine.Events;

[Serializable]
public class Theme
{
    public Color32 backgroundTop;
    public Color32 backgroundBottom;
    public Color32 helix;
    public Color32 platform;
    public Color32 ball;
    public Color32 text;
    public Theme(Color32 backgroundTop, Color32 backgroundBottom, Color32 helix, Color32 platform, Color32 ball, Color32 text)
    {
        this.backgroundTop = backgroundTop;
        this.backgroundBottom = backgroundBottom;
        this.helix = helix;
        this.platform = platform;
        this.ball = ball;
        this.text = text;
    }
}

public class GameManager : MonoBehaviour
{
    public int score = 0;
    public float duration = 0;
    public bool isGameOver;
    public bool isRankingOpen;
    public float timer;
    public UnityEvent nameChange;

    public Player player;
    public Score bestScore;

    public static GameManager singleton;
    private AdMobScript AdMob;

    [Header("Change Colors")]
    public GameObject Ball;
    public GameObject Splash;
    public GameObject Helix;
    public GameObject normalPlatformPart;
    public GameObject splashBounce;
    public GameObject gradient;
    public Text timerText;
    public Text scoreText;
    public List<Sprite> sprites;

    [SerializeField]
    private GameObject gameOverMenu;
    [SerializeField]
    private GameObject pauseButton;
    public List<Theme> themes = new List<Theme>() {
    new Theme(
        new Color32(8, 44, 92, 255),
        new Color32(1, 20, 37, 255),
        new Color32(96, 40, 112, 255),
        new Color32(222, 87, 129, 255),
        new Color32(245, 230, 99, 255),
        new Color32(245, 230, 99, 255)
     ),
       new Theme(
        new Color32(36, 218, 174, 255),
        new Color32(16, 130, 146, 255),
        new Color32(255, 137, 107, 255),
        new Color32(27, 156, 147, 255),
        new Color32(255, 255, 255, 255),
        new Color32(255, 255, 255, 255)
     ),
       new Theme(
        new Color32(205, 238, 253, 255),
        new Color32(150, 198, 220, 255),
        new Color32(157, 158, 202, 255),
        new Color32(240, 213, 198, 255),
        new Color32(59, 73, 82, 255),
        new Color32(59, 73, 82, 255)
     ),
       new Theme(
        new Color32(114, 147, 123, 255),
        new Color32(32, 67, 73, 255),
        new Color32(9, 29, 27, 255),
        new Color32(25, 56, 56, 255),
        new Color32(0, 219, 119, 255),
        new Color32(0, 219, 119, 255)
     ),
       new Theme(
        new Color32(238, 156, 204, 255),
        new Color32(87, 89, 166, 255),
        new Color32(46, 46, 58, 255),
        new Color32(68, 85, 84, 255),
        new Color32(241, 243, 86, 255),
        new Color32(241, 243, 86, 255)
     ),
      new Theme(
        new Color32(210, 222, 198, 255),
        new Color32(164, 167, 138, 255),
        new Color32(64, 68, 50, 255),
        new Color32(30, 123, 91, 255),
        new Color32(135, 182, 22, 255),
        new Color32(135, 182, 22, 255)
     ),
       new Theme(
        new Color32(223, 232, 197, 255),
        new Color32(166, 138, 116, 255),
        new Color32(226, 164, 116, 255),
        new Color32(157, 152, 126, 255),
        new Color32(97, 77, 27, 255),
        new Color32(97, 77, 27, 255)
     ),
       new Theme(
        new Color32(254, 225, 219, 255),
        new Color32(202, 143, 134, 255),
        new Color32(230, 231, 220, 255),
        new Color32(158, 163, 146, 255),
        new Color32(249, 84, 80, 255),
        new Color32(249, 84, 80, 255)
     ),
       new Theme(
        new Color32(1, 220, 120, 255),
        new Color32(0, 147, 156, 255),
        new Color32(178, 217, 93, 255),
        new Color32(79, 164, 86, 255),
        new Color32(255, 255, 255, 255),
        new Color32(255, 255, 255, 255)
     ),
       new Theme(
        new Color32(34, 193, 166, 255),
        new Color32(43, 48, 133, 255),
        new Color32(151, 118, 177, 255),
        new Color32(100, 159, 178, 255),
        new Color32(12, 30, 34, 255),
        new Color32(12, 30, 34, 255)
     )
    };

    async void Awake()
    {
        if (singleton == null)
            singleton = this;
        else if (singleton != this)
            Destroy(gameObject);

        ChangeColors();

        score = 0;
        isGameOver = false;
        isRankingOpen = false;

        try
        {
            await InitializeUser();
        }
        catch (Exception error)
        {
            LoadLocalPlayer();
            Debug.LogWarning("Online player service is unavailable. Using the local profile. " + error.Message);
        }

        await InitializeBestScore();

    }

    private void Start()
    {
        AdMob = gameObject.GetComponent<AdMobScript>();
    }

    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
    }

    public async void GameOver()
    {
        gameOverMenu.SetActive(true);
        pauseButton.SetActive(false);

        isGameOver = true;

        if (player == null)
        {
            LoadLocalPlayer();
        }

        if (bestScore == null)
        {
            LoadLocalBestScore();
        }

        if (score > bestScore.value)
        {
            int durationMilliseconds = (int)Math.Truncate(duration * 1000);
            bestScore = new Score(player._id, score, durationMilliseconds, bestScore.position);

            if (!string.IsNullOrEmpty(player.token))
            {
                try
                {
                    Score onlineScore = await APIHandler.AddScore(score, durationMilliseconds, player.token);
                    if (onlineScore != null)
                    {
                        bestScore = onlineScore;
                    }
                }
                catch (Exception error)
                {
                    Debug.LogWarning("Score saved locally because the online service is unavailable. " + error.Message);
                }
            }

            PlayerPrefs.SetInt("ScoreValue", score);
            PlayerPrefs.SetInt("ScoreDuration", bestScore.duration);
            PlayerPrefs.SetInt("ScorePosition", bestScore.position);
            PlayerPrefs.Save();
        }

        AdMob.LoadBanner();
    }

    void ChangeColors()
    {
        Image hey = gradient.GetComponent<Image>();

        int themeNumber = UnityEngine.Random.Range(0, themes.Count - 1);
        Theme theme = themes[themeNumber];
        hey.sprite = sprites[themeNumber];

        //Background
        //Camera.main.clearFlags = CameraClearFlags.SolidColor;
        Camera.main.backgroundColor = theme.backgroundTop;
        //RenderSettings.ambientLight = theme.backgroundTop;
        //RenderSettings.subtractiveShadowColor = theme.backgroundBottom;
        RenderSettings.fogColor = theme.backgroundBottom;
        // Helix
        Helix.GetComponent<Renderer>().sharedMaterial.color = theme.helix;
        // Platform
        normalPlatformPart.GetComponent<Renderer>().sharedMaterial.color = theme.platform;
        // Ball
        Ball.GetComponent<Renderer>().material.color = theme.ball;
        Splash.GetComponent<SpriteRenderer>().color = theme.ball;
        /*ParticleSystem ps = splashBounce.GetComponent<ParticleSystem>();
        ParticleSystem.MainModule psmain = ps.main;
        psmain.startColor = theme.ball;*/
        // Trail
        Ball.GetComponent<TrailRenderer>().material.color = theme.ball;

        // text color
        scoreText.color = theme.text;
        timerText.color = theme.text;
    }

    async Task InitializeUser()
    {
        string playerId = PlayerPrefs.GetString("PlayerId");

        if (playerId.Length < 1)
        {
            Player newPlayer = await AddPlayer();

            PlayerPrefs.SetString("PlayerId", newPlayer._id);
            PlayerPrefs.SetString("PlayerName", newPlayer.name);
            PlayerPrefs.SetString("PlayerToken", newPlayer.token);

            player = newPlayer;
        }
        else
        {
            player = new Player(
                PlayerPrefs.GetString("PlayerId"),
                PlayerPrefs.GetString("PlayerName"),
                PlayerPrefs.GetString("PlayerToken")
            );
        }
    }

    async Task InitializeBestScore()
    {
        int scoreValue = PlayerPrefs.GetInt("ScoreValue");
        LoadLocalBestScore();

        if (scoreValue > 0 && !string.IsNullOrEmpty(player.token))
        {
            try
            {
                Score onlineBestScore = await GetBestScorePlayer(player.token);
                if (onlineBestScore != null)
                {
                    bestScore = onlineBestScore;
                }
            }
            catch (Exception error)
            {
                Debug.LogWarning("Using the locally saved best score because the online service is unavailable. " + error.Message);
            }

            PlayerPrefs.SetInt("ScorePosition", bestScore.position);
        }
    }

    private void LoadLocalPlayer()
    {
        string playerId = PlayerPrefs.GetString("PlayerId");
        if (string.IsNullOrEmpty(playerId))
        {
            playerId = "local-" + Guid.NewGuid().ToString("N");
        }

        string playerName = PlayerPrefs.GetString("PlayerName");
        if (string.IsNullOrEmpty(playerName))
        {
            playerName = "Player";
        }

        player = new Player(playerId, playerName, PlayerPrefs.GetString("PlayerToken"));
        PlayerPrefs.SetString("PlayerId", player._id);
        PlayerPrefs.SetString("PlayerName", player.name);
        PlayerPrefs.SetString("PlayerToken", player.token);
        PlayerPrefs.Save();
    }

    private void LoadLocalBestScore()
    {
        bestScore = new Score(
            player != null ? player._id : string.Empty,
            PlayerPrefs.GetInt("ScoreValue"),
            PlayerPrefs.GetInt("ScoreDuration"),
            PlayerPrefs.GetInt("ScorePosition")
        );
    }

    public void UpdateName(string newName)
    {
        PlayerPrefs.SetString("PlayerName", newName);
        player.name = newName;

        if (nameChange != null)
        {
            nameChange.Invoke();
        }
    }
}
