using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TestSuite
{
    // A Test behaves as an ordinary method
    [Test]
    public void TestSuiteSimplePasses()
    {
        // Use the Assert class to test conditions
    }

    // 1
    private Game game;

    [SetUp]
    public void Setup()
    {
        GameObject gameGameObject = UnityEngine.Object.Instantiate(Resources.Load<GameObject>("Prefabs/Game"));
        game = gameGameObject.GetComponent<Game>();
    }

    [TearDown]
    public void TearDown()
    {
        UnityEngine.Object.Destroy(game.gameObject);
    }

    // 2
    [UnityTest]
    public IEnumerator AsteroidsMoveDown()
    {
        // 4
        GameObject asteroid = game.GetSpawner().SpawnAsteroid();
        // 5
        float initialYPos = asteroid.transform.position.y;
        // 6
        yield return new WaitForSeconds(0.1f);
        // 7
        Assert.Less(asteroid.transform.position.y, initialYPos);
        // 8
    }

    //[UnityTest]
    //public IEnumerator GameOverOccursOnAsteroidCollision()
    //{
    //    GameObject asteroid = game.GetSpawner().SpawnAsteroid();
    //    //1
    //    asteroid.transform.position = game.GetShip().transform.position;

    //    //2
    //    yield return new WaitForSeconds(0.1f);

    //    //3
    //    Assert.True(game.isGameOver);
    //}

    //1
    //[Test]
    //public void NewGameRestartsGame()
    //{
    //    //2
    //    game.isGameOver = true;
    //    game.NewGame();
    //    //3
    //    Assert.False(game.isGameOver);
    //}


    [UnityTest]
    public IEnumerator LaserDestroysAsteroid()
    {
        // 1
        GameObject asteroid = game.GetSpawner().SpawnAsteroid();
        asteroid.transform.position = Vector3.zero;
        GameObject laser = game.GetShip().SpawnLaser();
        laser.transform.position = Vector3.zero;
        yield return new WaitForSeconds(0.1f);
        // 2
        UnityEngine.Assertions.Assert.IsNull(asteroid);
    }

    [UnityTest]
    public IEnumerator LaserMovesUp()
    {
        // 1
        GameObject laser = game.GetShip().SpawnLaser();
        // 2
        float initialYPos = laser.transform.position.y;
        yield return new WaitForSeconds(0.1f);
        // 3
        Assert.Greater(laser.transform.position.y, initialYPos);
    }

    [UnityTest]
    public IEnumerator AsteroidDestroyedOffScreen()
    {
        GameObject asteroid = game.GetSpawner().SpawnAsteroid();

        asteroid.transform.position = new Vector3(asteroid.transform.position.x, -10f, asteroid.transform.position.z);

        yield return new WaitForSeconds(0.1f);

        UnityEngine.Assertions.Assert.IsNull(asteroid);
    }

    [UnityTest]
    public IEnumerator ScoreResetsOnNewGame()
    {
        game.score = 1;
        //2
        game.isGameOver = true;
        game.NewGame();

        yield return new WaitForSeconds(0.1f);

        //3
        Assert.True(game.score == 0);
    }

    [UnityTest]
    public IEnumerator playerStartsWithThreeLives()
    {
        game.NewGame();

        yield return new WaitForSeconds(0.1f);

        //3
        Assert.True(game.lives == 3);
    }

    [UnityTest]
    public IEnumerator playerLoses1LiveOnImpact()
    {
        GameObject asteroid = game.GetSpawner().SpawnAsteroid();
        //1
        asteroid.transform.position = game.GetShip().transform.position;
        //2
        yield return new WaitForSeconds(0.1f);

        //3
        Assert.True(game.lives == 2);
    }

    [UnityTest]
    public IEnumerator UIdisplayesGrayedHearts()
    {
        GameObject asteroid = game.GetSpawner().SpawnAsteroid();
        //1
        asteroid.transform.position = game.GetShip().transform.position;
        //2
        yield return new WaitForSeconds(0.1f);

        //3
        Assert.False(game.fullHeart1.activeSelf);
    }

    [UnityTest]
    public IEnumerator gameOver()
    {
        game.lives = 1;

        GameObject asteroid = game.GetSpawner().SpawnAsteroid();
        //1
        asteroid.transform.position = game.GetShip().transform.position;
        //2
        yield return new WaitForSeconds(0.1f);

        //3
        Assert.True(game.isGameOver);
    }
}
