using TMPro;
using UnityEngine;

public class LetterPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text letterText;
    [SerializeField] private TMP_Text wordsText;

    private void Awake()
    {
        if (letterText == null || wordsText == null)
        {
            TMP_Text[] texts = GetComponentsInChildren<TMP_Text>(true);
            foreach (TMP_Text text in texts)
            {
                if (text.gameObject.name == "LetterText") letterText = text;
                else if (text.gameObject.name == "WordsText") wordsText = text;
            }
        }

        if (letterText == null) Debug.LogError("LetterPanel: LetterText was not found.");
        if (wordsText == null) Debug.LogError("LetterPanel: WordsText was not found.");
    }

    public void ShowLetter(string letter)
    {
        if (letterText == null || wordsText == null)
        {
            Debug.LogError("LetterPanel: Text fields are missing.");
            return;
        }

        letter = letter.ToUpper();

        letterText.text = letter;
        wordsText.text = GetWords(letter);
    }

    private string GetWords(string letter)
    {
        switch (letter)
        {
            case "A":
                return "Apple\nAnt\nAirplane\nAlligator";

            case "B":
                return "Ball\nBanana\nBird\nBook";

            case "C":
                return "Cat\nCar\nCake\nCow";

            case "D":
                return "Dog\nDuck\nDrum\nDolphin";

            case "E":
                return "Elephant\nEagle\nEgg\nEarth";

            case "F":
                return "Fish\nFrog\nFlower\nFox";

            case "G":
                return "Goat\nGrapes\nGuitar\nGorilla";

            case "H":
                return "Hat\nHorse\nHouse\nHoney";

            case "I":
                return "Ice Cream\nIgloo\nInsect\nIsland";

            case "J":
                return "Jam\nJacket\nJuice\nJellyfish";

            case "K":
                return "Kite\nKangaroo\nKey\nKing";

            case "L":
                return "Lion\nLemon\nLamp\nLeaf";

            case "M":
                return "Monkey\nMoon\nMango\nMilk";

            case "N":
                return "Nest\nNose\nNurse\nNut";

            case "O":
                return "Orange\nOwl\nOctopus\nOcean";

            case "P":
                return "Pencil\nPizza\nPenguin\nPanda";

            case "Q":
                return "Queen\nQuilt\nQuail\nQuestion";

            case "R":
                return "Rabbit\nRainbow\nRobot\nRose";

            case "S":
                return "Sun\nSnake\nStar\nSchool";

            case "T":
                return "Tiger\nTree\nTrain\nTable";

            case "U":
                return "Umbrella\nUniform\nUnicorn\nUncle";

            case "V":
                return "Van\nViolin\nVolcano\nVegetable";

            case "W":
                return "Whale\nWatch\nWatermelon\nWindow";

            case "X":
                return "Xylophone\nX-ray\nXerus\nXenon";

            case "Y":
                return "Yacht\nYo-yo\nYak\nYarn";

            case "Z":
                return "Zebra\nZoo\nZipper\nZero";

            default:
                return "No words found.";
        }
    }
}