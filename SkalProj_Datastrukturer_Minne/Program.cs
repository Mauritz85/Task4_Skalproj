using System;
using System.Collections;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection.Metadata.Ecma335;

namespace SkalProj_Datastrukturer_Minne
{
    class Program
    {
        static void Main()
        {

            while (true)
            {
                Console.WriteLine("Please navigate through the menu by inputting the number \n(1, 2, 3 ,4, 0) of your choice"
                    + "\n1. Examine a List"
                    + "\n2. Examine a Queue"
                    + "\n3. Examine a Stack"
                    + "\n4. CheckParenthesis"
                    + "\n0. Exit the application");
                char input = ' '; //Creates the character input to be used with the switch-case below.
                try
                {
                    input = Console.ReadLine()![0]; //Tries to set input to the first char in an input line
                }
                catch (IndexOutOfRangeException) //If the input line is empty, we ask the users for some input.
                {
                    Console.Clear();
                    Console.WriteLine("Please enter some input!");
                }
                switch (input)
                {
                    case '1':
                        ExamineList();
                        break;
                    case '2':
                        ExamineQueue();
                        break;
                    case '3':
                        ExamineStack();
                        break;
                    case '4':
                        CheckParanthesis();
                        break;
                    /*
                     * Extend the menu to include the recursive 
                     * and iterative exercises.
                     */
                    case '0':
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Please enter some valid input (0, 1, 2, 3, 4)");
                        break;
                }
            }
        }

        /// <summary>
        /// Examines the datastructure List
        /// </summary>
        static void ExamineList()
        {
            List<string> theList = new List<string>();
            bool running = true; //bool that runs the while-loop if true. '0' input makes it false and exits the loop.
            while (running) 
            {
                Console.WriteLine("Please enter:"
                    + "\n '+NAME' to add a name to the list"
                    + "\n '-NAME' to remove a name from the list"
                    + "\n '0' to exit the application");


                var (nav, name) = getUserInput(); //Method that get the users input and splits it into navigation choice and the input name

                switch (nav)
                {
                    case '+':
                        if (theList.Contains(name)) //checks if the list already includes the names to avoid duplicates.
                        {
                            Console.WriteLine($"The name '{name}' is already in the list");
                        }
                        else
                        {
                            theList.Add(name);
                            Console.WriteLine($"The name '{name}' successfully added! Name count: {theList.Count}. List capacity: {theList.Capacity}");
                        }
                        break;
                    case '-':
                        if (theList.Contains(name)) //checks if the list already includes the names and deletes it from the list if it is.
                        {
                            theList.Remove(name);
                            Console.WriteLine($"The name '{name}' is successfully removed! Name count: {theList.Count}. List capacity: {theList.Capacity}");
                        }
                        else
                            Console.WriteLine($"The name '{name}' doesn't exist in the list");
                        break;
                    case '0':
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Please try again with a valid input");
                        break;
                }
            }
        }

        static void ExamineQueue()
        {

            Queue<string> theQueue = new Queue<string>();
            bool running = true;
            while (running)
            {
                Console.WriteLine("Please enter a choice and press <ENTER>:"
                    + "\n '+NAME' to add a customer to the queue"
                    + "\n '-' to serve next customer in line and make them leave the queue"
                    + "\n '0' to exit the application");

                var (nav, name) = getUserInput();

                switch (nav)
                {
                    case '+':
                        theQueue.Enqueue(name); //adds name/person to the queue
                        Console.WriteLine($"The customer '{name}' is added to the queue. There is now {theQueue.Count} people waiting in the queue.");
                        break;
                    case '-':
                        if (theQueue.Count > 0) //checks that there is still people in the queue.
                        {
                            string firstInLine = theQueue.Peek(); //checks the name of the person first in line.
                            theQueue.Dequeue(); //removes it
                            Console.WriteLine($"The customer '{firstInLine}' was served and left the queue. There is now {theQueue.Count} people left waiting in the queue.");
                        }
                        else Console.WriteLine("The Queue is empty");
                        break;
                    case '0':
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Please try again with a valid input");
                        break;
                }
            }
        }


        static void ExamineStack()
        {
            Stack<string> theStack = new Stack<string>();
            bool running = true;
            while (running)
            {
                Console.WriteLine("Please enter a choice and press <ENTER>:"
                    + "\n '+NAME' to add a name to the stack"
                    + "\n '-' to pick the remove the name on top of the stack"
                    + "\n '0' to exit the application");


                var (nav, name) = getUserInput();

                switch (nav)
                {
                    case '+':
                        theStack.Push(name);
                        Console.WriteLine($"The name '{name}' is added to the stack. There is now {theStack.Count} names in the stack.");
                        break;
                    case '-':
                        if (theStack.Count > 0)
                        {
                            string OnTopOfStack = theStack.Pop(); //Checks name of person on top of stack and removes it
                            Console.WriteLine($"The name '{OnTopOfStack}' was removed.");
                        }
                        else Console.WriteLine("The stack is empty");
                        break;
                    case '0':
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Please try again with a valid input");
                        break;
                }
            }
        }

        static void CheckParanthesis()
        {
            bool running = true;
            while (running)
            {
                Console.WriteLine("Input a combination of paranthesis, brackets and curly brackets to check if they are properly balanced."
                    + "\nor '0' to exit the application");


                string? input = Console.ReadLine();
                input = string.IsNullOrEmpty(input) ? "IsNullOrEmpty" : input;
                char nav = input[0];

                char[] allowedChars = { '(', ')', '[', ']', '{', '}' }; //array with all the allowed characters (brackets)
                char[] openingBrackets = { '(', '[', '{' }; //the order here is crucial
                char[] closingBrackets = { ')', ']', '}' }; //the order here is crucial

                switch (nav)
                {
                    case '0':
                        running = false;
                        break;
                    default:
                        if (input.All(c => allowedChars.Contains(c)))  // First check: input must only contain allowed bracket characters
                        {
                            Stack<char> stack = new Stack<char>();  // Used to track opening brackets

                            bool balanced = true; // Assume balanced until proven otherwise

                            foreach (char c in input)
                            {
                                if (openingBrackets.Contains(c)) // If character is an opening bracket, push it to the stack
                                {
                                    stack.Push(c);
                                }
                                else if (closingBrackets.Contains(c)) // Else if character is an closing bracket..
                                {
                                    if (stack.Count == 0) // ...but no opening bracket to match it with..
                                    {
                                        balanced = false; //...it's unbalanced!
                                        break;
                                    }

                                    char top = stack.Pop();  //...else check the bracket on top of the stack and remove it
                                    int closeIndex = Array.IndexOf(closingBrackets, c); //Check the index of the current closingbracket in the closingbrackets array.
                                    if (top != openingBrackets[closeIndex]) //If the top opening bracket (now removed) isn't matching the corresponding opening bracket in the array..
                                    {
                                        balanced = false; //it's unbalanced
                                        break;
                                    }
                                }
                            }

                            Console.WriteLine(balanced && stack.Count == 0 //When the stack is emptied and the balanced is still "true" 
                                ? "Brackets are balanced!" //all the brackets are balanced.
                                : "Brackets are NOT balanced!");// otherwise show it's not.

                        }
                        else Console.WriteLine("Not a valid input. Try again.");
                        break;
                }
            }


        }

        static (char nav, string name) getUserInput()
        {
            Console.Write("> ");
            string? input = Console.ReadLine();
            input = string.IsNullOrEmpty(input) ? "IsNullOrEmpty" : input;
            char nav = input[0];
            string name = input.Substring(1);

            return (nav, name);
        }

    }
}

