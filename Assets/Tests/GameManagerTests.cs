using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;

[TestFixture]
public class GameManagerTests
{
    private GameObject gameObject;
    private GameManager gameManager;

    [SetUp] // Runs before each test
    public void SetUp()
    {
        gameObject = new GameObject();
        gameManager = gameObject.AddComponent<GameManager>();
    }

    [TearDown] // Runs after each test
    public void TearDown()
    {
        Object.Destroy(gameObject);
    }

    [Test]
    public void GameManager_Initializes_Correctly()
    {
        Assert.AreEqual(3, GameManager.lives, "Lives should start at 3.");
        Assert.AreEqual(0, GameManager.totalScore, "Score should start at 0.");
    }

    [Test]
    public void GameManager_Updates_Score_Correctly()
    {
        gameManager.UpdateScore(10);
        Assert.AreEqual(10, GameManager.totalScore, "Score should increase by 10.");
    }
}
