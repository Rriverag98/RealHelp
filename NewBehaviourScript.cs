using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour {
    //These arrays will hold the questions for each level
    public Questions[] level1Questions;
    public Questions[] level2Questions;
    public Questions[] level3Questions;
    //currentQuestion
    private Questions currentQuestion;
    public static List<Questions> unansweredQuestions;

    const string menuHint = "You can type menu at any time.";
    
    int level;
    //enumerated tipe to represent the different states, variable is declared to hold the 
    //current game state
    enum GameState { MainMenu, Trivia, Win, Lose };
    GameState currentScreen = GameState.MainMenu;
    string question;
    // Use this for initialization
    void Start()
    {
        ShowMainMenu();
    }

    void Update()
    {

    }

    void ShowMainMenu()
    {
        //  We clear the screen
        Terminal.ClearScreen();

        //  We show the menu
        Terminal.WriteLine("NBA Trivia");
        Terminal.WriteLine("Choose a difficulty");
        Terminal.WriteLine("");
        Terminal.WriteLine("1. Easy");
        Terminal.WriteLine("2. Medium");
        Terminal.WriteLine("3. Hard");
        Terminal.WriteLine("");
        Terminal.WriteLine("Option?");

        currentScreen = GameState.MainMenu;
    }

    void OnUserInput(string input)
    {
        //if user inputs menu then main menu is shown
        if (input == "menu")
        {
            ShowMainMenu();
        }
        else if (input == "quit" || input == "close" || input == "exit") // if user inputs quit or closed 
        {
            Terminal.WriteLine("Please close the browser's tab");
            Application.Quit();
        }
        //if the user inputs something different to menu,quit or close, the input will be
        //handled depending the gamestate.
        else if (currentScreen == GameState.MainMenu)
        {
            RunMainMenu(input);
        }
        //if GameState is password, then we must call checkPassword().
        else if (currentScreen == GameState.Trivia)
        {
            CheckAnswer(input);
        }
    }

    private void CheckAnswer(string input)
    {
        if (input == "true")
        {
            if (currentQuestion.isTrue) {
                DisplayWinScreen();
            } else {
                DisplayLoseScreen();
            }
            
        }
        else
        {
            if (currentQuestion.isTrue)
            {
                DisplayLoseScreen();
            }
            else
            {
                DisplayWinScreen();
            }
        }
    }

    private void DisplayLoseScreen()
    {
        Terminal.ClearScreen();
        Terminal.WriteLine(@"
 __   __          _           _   _ 
 \ \ / /__ _  _  | |   ___ __| |_| |
  \ V / _ \ || | | |__/ _ (_-<  _|_|
   |_|\___/\_,_| |____\___/__/\__(_)");
        Terminal.WriteLine("You missed the shot and lost the game");
        Terminal.WriteLine(menuHint);
    }

    private void DisplayWinScreen()
    {
        currentScreen = GameState.Win;
        Terminal.ClearScreen();
        ShowLevelReward();
        Terminal.WriteLine(menuHint);
    }

    private void ShowLevelReward()
    {
                Terminal.WriteLine(@"
 __   __         __      __        _ 
 \ \ / /__ _  _  \ \    / /__ _ _ | |
  \ V / _ \ || |  \ \/\/ / _ \ ' \|_|
   |_|\___/\_,_|   \_/\_/\___/_||_(_)");
        Terminal.WriteLine("You got a buzzer beater and won the game!!");
                
    }

    void RunMainMenu(string input)
    {
        //Validate that input is valid
        bool isValidInput = (input == "1") || (input == "2") || (input == "3");
        //If it is then we convert the input into an int and assign it to level
        //and call askForPassword();
        if (isValidInput)
        {
            level = int.Parse(input);
            AskTrivia();
        }
        else if (input == "007")  //if the input is invalid, check if its an easter egg
        {
            Terminal.WriteLine("Please enter a valid level, Mr. Bond");
        }
        else
        {
            Terminal.WriteLine("Enter a valid level");
        }
    }

    private void AskTrivia()
    {
        //set currentScreen as GameState password
        currentScreen = GameState.Trivia;
        //Clear Screen
        Terminal.ClearScreen();
        //Call setRandomPassord to set a password
        SetRandomQuestion();
        Terminal.WriteLine(currentQuestion.fact);
        Terminal.WriteLine("True or False??");
        //show menuHint
        Terminal.WriteLine(menuHint);
    }

    private void SetRandomQuestion()
    {
        switch (level)
        {
            case 1:
                unansweredQuestions = level1Questions.ToList<Questions>();
                int randomQuestionIndex = UnityEngine.Random.Range(0, unansweredQuestions.Count);
                currentQuestion = level1Questions[randomQuestionIndex];
                break;
            case 2:
                unansweredQuestions = level2Questions.ToList<Questions>();
                int randomQuestionIndex2 = UnityEngine.Random.Range(0, unansweredQuestions.Count);
                currentQuestion = level2Questions[randomQuestionIndex2];
                break;
            case 3:
                unansweredQuestions = level2Questions.ToList<Questions>();
                int randomQuestionIndex3 = UnityEngine.Random.Range(0, unansweredQuestions.Count);
                currentQuestion = level2Questions[randomQuestionIndex3];
                break;
            default:
                Debug.LogError("I cant believe it, The ball was stolen from you");
                break;
        }
    }
}
