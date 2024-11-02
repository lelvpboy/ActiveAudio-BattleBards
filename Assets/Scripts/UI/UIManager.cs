using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    public GameObject[] Hearts;
    public int combo;
    public float score;

    public TextMeshProUGUI comboText;
    public TextMeshProUGUI scoreText;

    public TextMeshProUGUI comboEndText;
    public TextMeshProUGUI scoreEndText;

    public GameObject endScreen;

    private void Update()
    {
        comboText.text = "x" + combo.ToString();
        scoreText.text = score.ToString();

        comboEndText.text = "Combo: " + combo.ToString();
        scoreEndText.text = "Score: " + score.ToString();
    }

    public void End()
    {
        endScreen.SetActive(true);
        Time.timeScale = 0f;
    }

    public void TakeDamage(int HP)
    {
        Hearts[HP].SetActive(false);
    }

    public void LoadLevel(string scene)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(scene);
    }
}
