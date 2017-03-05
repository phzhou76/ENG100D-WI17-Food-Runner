using UnityEngine;
using System.Collections;

public class QuizController : MonoBehaviour {
    public static int quizChosen;

    // Use this for initialization
    void Start () {
        quizChosen = Random.Range(1, 15);
    }
}
