﻿/*
 * Copyright (c) 2023 Kodeco Inc.
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * Notwithstanding the foregoing, you may not use, copy, modify, merge, publish, 
 * distribute, sublicense, create a derivative work, and/or sell copies of the 
 * Software in any work that is designed, intended, or marketed for pedagogical or 
 * instructional purposes related to programming, coding, application development, 
 * or information technology.  Permission for such use, copying, modification,
 * merger, publication, distribution, sublicensing, creation of derivative works, 
 * or sale is expressly withheld.
 *    
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    public int score = 0;
    public bool isGameOver = false;

    public int lives = 3;

    [SerializeField] private GameObject shipModel;
    [SerializeField] private GameObject startGameButton;
    [SerializeField] private Text gameOverText;
    [SerializeField] private Text scoreText;
    [SerializeField] private GameObject titleText;
    [SerializeField] private Spawner spawner;
    [SerializeField] public GameObject fullHeart1;
    [SerializeField]  GameObject fullHeart2;
    [SerializeField]  GameObject fullHeart3;

    private static Game instance;

    private void Start()
    {
        instance = this;
        titleText.SetActive(true);
        gameOverText.enabled = false;
        scoreText.enabled = false;
        startGameButton.SetActive(true);
    }

    public static void GameOver()
    {
        instance.titleText.SetActive(true);
        instance.startGameButton.SetActive(true);
        instance.isGameOver = true;
        instance.spawner.StopSpawning();
        instance.shipModel.GetComponent<Ship>().Explode();
        instance.gameOverText.enabled = true;
        instance.lives = 3;
    }

    public void NewGame()
    {
        isGameOver = false;
        titleText.SetActive(false);
        startGameButton.SetActive(false);
        shipModel.transform.localPosition = new Vector3(0, 0, 0);
        score = 0;
        scoreText.text = $"Score: {score}";
        scoreText.enabled = true;
        spawner.BeginSpawning();
        shipModel.GetComponent<Ship>().RepairShip();
        spawner.ClearAsteroids();
        gameOverText.enabled = false;

        instance.lives = 3;
        instance.fullHeart1.SetActive(true);
        instance.fullHeart2.SetActive(true);
        instance.fullHeart3.SetActive(true);


    }

    public static void AsteroidDestroyed()
    {
        instance.score++;
        instance.scoreText.text = $"Score: {instance.score}";
    }

    public Ship GetShip()
    {
        return shipModel.GetComponent<Ship>();
    }

    public Spawner GetSpawner()
    {
        return spawner.GetComponent<Spawner>();
    }

    public static void playerHit()
    {
        instance.lives--;
        if(instance.lives == 2)
        {
            instance.fullHeart1.SetActive(false);
        }
        else if(instance.lives == 1)
        {
            instance.fullHeart2.SetActive(false);
        }

        if (instance.lives <= 0)
        {
            GameOver();
            instance.fullHeart3.SetActive(false);
        }
    }
}
